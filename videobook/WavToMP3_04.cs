

// ---------------------------------------------------------------
// Date    	    170212
// Author  	    BBIM
// Version    	Visual C# Express 2010
// Framework  	4, WPF.
// Module name	WavToMP3_04
// Purpose	    WAV to MP3 converter with the LAME encoder.
// Comments	    Source:             <http://lame.sourceforge.net/links.php>
//              Binary (lame.exe):  <http://www.rarewares.org/index.php>
//              D:\PC3000_Audio\Programs_2012\Program_002_16022012_000_Lame_Compiled_3994
// ---------------------------------------------------------------

// History
// ---------------------------------------------------------------
// 25-08-10 - WavToMP3_03.
// 16-02-12 - WavToMP3_04 - Path for LAME encoder added as parameter for WavToMP3().
//                          Test lame.exe v 3.99.4.
// ---------------------------------------------------------------


// ---------------------------------------------------------------
/*
% lame [options] inputfile [outputfile]

-m m    mono
-m s    stereo
-m j    joint stereo
-k      will disable all lowpass filtering
112-128 kbps   Joint Stereo -mj

For more options, just type:
% lame --longhelp
*/
// ---------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;


namespace videobook
{
    public class WavToMP3_04
    {


        // ---------------------------------------------------------------
        // Date   	    170212
        // Purpose	    Method - WAV to MP3 converter with LAME encoder.
        // Entry	    sPathName_enc - Path for the LAME encoder (lame.exe).
        //              sFileName_wav - The WAV file to convert (+ path).
        //              sFileName_mp3 - The converted MP3 file (+ path).
        //              bWait - true: wait until ready.
        // Return	    An empty string "" if no errors, else an error description.
        // Comments	    1) lame.exe must exist in the sPathName_enc directory.
        //              Use the root of the application.
        //              2) Bitrate = 128 kbps (Joint Stereo).
        //              --resample 22.05 = output sampling frequency in kHz.
        //              -m j = Joint Stereo.
        // ---------------------------------------------------------------
        public string WavToMP3(string sPathName_enc, 
                               string sFileName_wav, 
                               string sFileName_mp3, bool bWait)
        {
            FileInfo fi;

            // Path for WAV file.
            fi = new FileInfo(sFileName_wav);
            string sPath_wav = fi.DirectoryName;

            // Path for MP3 file.
            fi = new FileInfo(sFileName_mp3);
            string sPath_mp3 = fi.DirectoryName;

            if (!Directory.Exists(sPathName_enc))
            {
                return "Directory for encoder not found:\r\n" + sPathName_enc;
            }

            if (!File.Exists(sFileName_wav))
            {
                return "WAV file not found:\r\n" + sFileName_wav;
            }

            if (!Directory.Exists(sPath_mp3))
            {
                return "Directory for MP3 file not found:\r\n" + sPath_mp3;
            }

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;      // No Command Prompt window.
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.FileName = "\"" + sPathName_enc + @"\lame.exe" + "\"";
                psi.Arguments = "-b 128 --resample 22.05 -m j " + 
                                "\"" + sFileName_wav + "\"" + " " +
                                "\"" + sFileName_mp3 + "\"";
                Process p = Process.Start(psi);
                if (bWait)
                {
                    p.WaitForExit();
                }
                p.Close();
                p.Dispose();
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


    }   // end class
}       // end namespace
