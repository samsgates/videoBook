using System;
using DirectShow;
using DirectShow.BaseClasses;
using System.Runtime.InteropServices;
using Sonic;
using System.IO;

namespace WavExtract
{
    #region Wav Dest Filter

    public class WavDestInputPin : RenderedInputPin
    {
        #region Constructor

        public WavDestInputPin(string _name, BaseFilter _filter)
            : base(_name, _filter)
        {
        }

        #endregion

        #region Overridden Methods

        public override int CheckMediaType(AMMediaType pmt)
        {
            return (m_Filter as WavDestFilter).CheckMediaType(pmt);
        }

        public override int OnReceive(ref IMediaSampleImpl _sample)
        {
            HRESULT hr = (HRESULT)CheckStreaming();
            if (hr != S_OK) return hr;
            return (m_Filter as WavDestFilter).OnReceive(ref _sample);
        }

        public override int EndOfStream()
        {
            (m_Filter as WavDestFilter).EndOfStream();
            return base.EndOfStream();
        }

        #endregion
    }

    public class WavDestFilter : BaseFilter, IFileSinkFilter, IAMFilterMiscFlags
    {
        #region Structures

        [StructLayout(LayoutKind.Sequential)]
        private class OUTPUT_DATA_HEADER
        {
            public uint dwData = 0;
            public uint dwDataLength = 0;
        }

        [StructLayout(LayoutKind.Sequential)]
        private class OUTPUT_FILE_HEADER
        {
            public uint dwRiff = 0;
            public uint dwFileSize = 0;
            public uint dwWave = 0;
            public uint dwFormat = 0;
            public uint dwFormatLength = 0;
        }

        #endregion

        #region Constants

        private const uint RIFF_TAG = 0x46464952;
        private const uint WAVE_TAG = 0x45564157;
        private const uint FMT__TAG = 0x20746D66;
        private const uint DATA_TAG = 0x61746164;
        private const uint WAVE_FORMAT_PCM = 0x01;

        #endregion

        #region Variables

        private FileStream m_Stream = null;
        private string m_sFileName = "";

        #endregion

        #region Constructor

        public WavDestFilter()
            : base("CSharp Wav Dest Filter")
        {

        }

        #endregion

        #region Overridden Methods

        protected override int OnInitializePins()
        {
            AddPin(new WavDestInputPin("In", this));
            return NOERROR;
        }

        public override int Pause()
        {
            if (m_State == FilterState.Stopped && m_Stream == null)
            {
                OpenFile();
            }
            return base.Pause();
        }

        public override int Stop()
        {
            int hr = base.Stop();
            CloseFile();
            return hr;
        }

        #endregion

        #region Methods

        public int CheckMediaType(AMMediaType pmt)
        {
            if (pmt.majorType != MediaType.Audio)
            {
                return VFW_E_TYPE_NOT_ACCEPTED;
            }
            if (pmt.subType != MediaSubType.PCM)
            {
                return VFW_E_TYPE_NOT_ACCEPTED;
            }
            WaveFormatEx _wfx = pmt;
            if (_wfx == null || _wfx.wFormatTag != WAVE_FORMAT_PCM)
            {
                return VFW_E_TYPE_NOT_ACCEPTED;
            }
            return S_OK;
        }

        public int EndOfStream()
        {
            CloseFile();
            NotifyEvent(EventCode.Complete, (IntPtr)((int)S_OK), Marshal.GetIUnknownForObject(this));
            return S_OK;
        }

        public int OnReceive(ref IMediaSampleImpl _sample)
        {
            lock (m_Lock)
            {
                if (m_Stream == null)
                {
                    OpenFile();
                }

                int _length = _sample.GetActualDataLength();
                if (m_Stream != null && _length > 0)
                {
                    byte[] _data = new byte[_length];
                    IntPtr _ptr;
                    _sample.GetPointer(out _ptr);
                    Marshal.Copy(_ptr, _data, 0, _length);
                    m_Stream.Write(_data, 0, _length);
                }
            }
            return S_OK;
        }

        #endregion

        #region Helper Methods

        protected int OpenFile()
        {
            if (m_Stream == null && m_sFileName != "" && Pins[0].IsConnected)
            {
                m_Stream = new FileStream(m_sFileName, FileMode.Create, FileAccess.Write, FileShare.Read);

                WaveFormatEx _wfx = Pins[0].CurrentMediaType;

                int _size;
                byte[] _buffer;
                IntPtr _ptr;

                OUTPUT_FILE_HEADER _header = new OUTPUT_FILE_HEADER();

                _header.dwRiff = RIFF_TAG;
                _header.dwFileSize = 0;
                _header.dwWave = WAVE_TAG;
                _header.dwFormat = FMT__TAG;
                _header.dwFormatLength = (uint)Marshal.SizeOf(_wfx);

                _size = Marshal.SizeOf(_header);
                _buffer = new byte[_size];
                _ptr = Marshal.AllocCoTaskMem(_size);
                Marshal.StructureToPtr(_header, _ptr, true);
                Marshal.Copy(_ptr, _buffer, 0, _size);
                m_Stream.Write(_buffer, 0, _size);
                Marshal.FreeCoTaskMem(_ptr);

                _size = Marshal.SizeOf(_wfx);
                _buffer = new byte[_size];
                _ptr = Marshal.AllocCoTaskMem(_size);
                Marshal.StructureToPtr(_wfx, _ptr, true);
                Marshal.Copy(_ptr, _buffer, 0, _size);
                m_Stream.Write(_buffer, 0, _size);
                Marshal.FreeCoTaskMem(_ptr);

                OUTPUT_DATA_HEADER _data = new OUTPUT_DATA_HEADER();
                _data.dwData = DATA_TAG;
                _data.dwDataLength = 0;

                _size = Marshal.SizeOf(_data);
                _buffer = new byte[_size];
                _ptr = Marshal.AllocCoTaskMem(_size);
                Marshal.StructureToPtr(_data, _ptr, true);
                Marshal.Copy(_ptr, _buffer, 0, _size);
                m_Stream.Write(_buffer, 0, _size);
                Marshal.FreeCoTaskMem(_ptr);

                return NOERROR;
            }
            return S_FALSE;
        }

        protected int CloseFile()
        {
            lock (m_Lock)
            {
                if (m_Stream != null)
                {
                    WaveFormatEx _wfx = Pins[0].CurrentMediaType;

                    int _size;
                    byte[] _buffer;
                    IntPtr _ptr;

                    OUTPUT_FILE_HEADER _header = new OUTPUT_FILE_HEADER();

                    _header.dwRiff = RIFF_TAG;
                    _header.dwFileSize = (uint)m_Stream.Length - 2 * 4;
                    _header.dwWave = WAVE_TAG;
                    _header.dwFormat = FMT__TAG;
                    _header.dwFormatLength = (uint)Marshal.SizeOf(_wfx);

                    _size = Marshal.SizeOf(_header);
                    _buffer = new byte[_size];
                    _ptr = Marshal.AllocCoTaskMem(_size);
                    Marshal.StructureToPtr(_header, _ptr, true);
                    Marshal.Copy(_ptr, _buffer, 0, _size);
                    m_Stream.Write(_buffer, 0, _size);
                    Marshal.FreeCoTaskMem(_ptr);

                    _size = Marshal.SizeOf(_wfx);
                    _buffer = new byte[_size];
                    _ptr = Marshal.AllocCoTaskMem(_size);
                    Marshal.StructureToPtr(_wfx, _ptr, true);
                    Marshal.Copy(_ptr, _buffer, 0, _size);
                    m_Stream.Write(_buffer, 0, _size);
                    Marshal.FreeCoTaskMem(_ptr);

                    OUTPUT_DATA_HEADER _data = new OUTPUT_DATA_HEADER();
                    _data.dwData = DATA_TAG;
                    _data.dwDataLength = (uint)(m_Stream.Length - Marshal.SizeOf(_header) - _header.dwFormatLength - Marshal.SizeOf(_data));

                    _size = Marshal.SizeOf(_data);
                    _buffer = new byte[_size];
                    _ptr = Marshal.AllocCoTaskMem(_size);
                    Marshal.StructureToPtr(_data, _ptr, true);
                    Marshal.Copy(_ptr, _buffer, 0, _size);
                    m_Stream.Write(_buffer, 0, _size);
                    Marshal.FreeCoTaskMem(_ptr);

                    m_Stream.Dispose();
                    m_Stream = null;
                }
            }
            return NOERROR;
        }

        #endregion

        #region IFileSinkFilter Members

        public int SetFileName(string pszFileName, AMMediaType pmt)
        {
            if (string.IsNullOrEmpty(pszFileName)) return E_POINTER;
            if (IsActive) return VFW_E_WRONG_STATE;
            m_sFileName = pszFileName;
            return NOERROR;
        }

        public int GetCurFile(out string pszFileName, AMMediaType pmt)
        {
            pszFileName = m_sFileName;
            if (pmt != null)
            {
                pmt.Set(Pins[0].CurrentMediaType);
            }
            return NOERROR;
        }

        #endregion

        #region IAMFilterMiscFlags Members

        public int GetMiscFlags()
        {
            return 1;
        }

        #endregion
    }

    #endregion

    #region Filter Graph

    public class WavExtractGraph : DSFilePlayback, IFileDestSupport
    {
        #region Variables

        private string m_sOutputFileName = "";

        #endregion

        #region IFileDestSupport Members

        public string OutputFileName
        {
            get
            {
                return m_sOutputFileName;
            }
            set
            {
                Save(value, false);
            }
        }

        public HRESULT Save()
        {
            return Save(true);
        }

        public HRESULT Save(bool bStart)
        {
            return Save(m_sOutputFileName, bStart);
        }

        public HRESULT Save(string sFileName)
        {
            return Save(sFileName, false);
        }

        public HRESULT Save(string sFileName, bool bStart)
        {
            m_sOutputFileName = sFileName;
            return NOERROR;
        }

        #endregion

        #region Overridden methods

        public override HRESULT Open(string sFileName, bool bStart)
        {
            m_sFileName = sFileName;
            return NOERROR;
        }

        protected override HRESULT OnInitInterfaces()
        {
            // Create writer
            DSBaseWriterFilter _writer = new DSBaseWriterFilter(new WavDestFilter());
            // Add it to filter graph
            _writer.FilterGraph = m_GraphBuilder;
            // setup the file name
            _writer.FileName = m_sOutputFileName;
            // Call base class method which do Render File call
            HRESULT hr = base.OnInitInterfaces();
            // If it's ok we should check if our filter connected
            if (SUCCEEDED(hr))
            {
                // Not connected - possible no audio - we should ignore that file then
                if (!_writer.InputPin.IsConnected)
                {
                    hr = VFW_E_NOT_CONNECTED;
                }
            }
            return hr;
        }

        #endregion
    }

    #endregion
}
