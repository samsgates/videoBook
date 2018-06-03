using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Logging;
using DZ.MediaPlayer;
using DZ.MediaPlayer.Vlc.Io;
using DZ.MediaPlayer.Vlc.WindowsForms;
using SimplePlayer.Playlist;
using SimplePlayer.MediaInfo;
using SimplePlayer;
using WavExtract;
using Sonic;
using System.IO;
using System.Diagnostics;



namespace videobook
{
    public partial class MForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public VideoWindow videoWindow;
        string curErrLog = "";
        BackgroundWorker _bw;
        delegate void update_probar(string text, int max, int cur);
        int audio_gen = 0;

        WavExtractGraph m_Playback = null;
        public MForm()
        {
            InitializeComponent();
         

        }


        public void convert_epub(Updf cPDF) {

            try
            {
                string videoFile = cPDF.b_videofile;
               
                int time_interval = cPDF.b_timeinvertal;
                int page_row = cPDF.b_pagerow;
                int page_col = cPDF.b_pagecol;
                string rootPath = cPDF.b_rootpath;
                string outPath = cPDF.b_outpath;
                string fnameWOE = cPDF.b_filenamewoe;
                string b_title = cPDF.b_title;
                string b_author = cPDF.b_author;

                m_Playback = new WavExtractGraph();
                m_Playback.Visible = false;
                m_Playback.OnPlaybackStart += new EventHandler(Playback_OnPlaybackStart);
                m_Playback.OnPlaybackStop += new EventHandler(Playback_OnPlaybackStop);

                probar_update("Create Directory...", 10, 0);

                #region create dir
                string wrkDir = rootPath;
                if (!Directory.Exists(Path.Combine(wrkDir , "META-INF")))
                {
                    Directory.CreateDirectory(Path.Combine(wrkDir , "META-INF"));
                }
                if (!Directory.Exists(Path.Combine(wrkDir , "OEBPS")))
                {
                    Directory.CreateDirectory(Path.Combine(wrkDir , "OEBPS"));
                }
                string oebDir = Path.Combine( wrkDir , "OEBPS");
                if (!Directory.Exists(Path.Combine(oebDir , "html")))
                {
                    Directory.CreateDirectory(Path.Combine(oebDir , "html"));
                }
                if (!Directory.Exists(Path.Combine(oebDir , "images")))
                {
                    Directory.CreateDirectory(Path.Combine(oebDir , "images"));
                }
                if (!Directory.Exists(Path.Combine(oebDir , "audio")))
                {
                    Directory.CreateDirectory(Path.Combine(oebDir , "audio"));
                }
                if (!Directory.Exists(Path.Combine(oebDir , "styles")))
                {
                    Directory.CreateDirectory(Path.Combine(oebDir , "styles"));
                }
                #endregion

                string imgDir = Path.Combine( oebDir , "images");
                string styDir = Path.Combine(oebDir , "styles");
                string htmlDir = Path.Combine( oebDir, "html");
                string audioDir = Path.Combine(oebDir , "audio");
                            

                probar_update("Capture audio...", 10, 3);

                #region capture image and audio

                PlaylistItem playlistItem = new PlaylistItem(
                                 new MediaInput(MediaInputType.File, videoFile),
                                 videoFile, TimeSpan.FromMilliseconds(0));


                PlaylistItem currentItem = playlistItem;
                initializeVlcPlayerControl(true);
                currentItem.IsError = false;
              
                BasicVideoInformation information = MediaInfoHelper.GetBasicVideoInfo(currentItem.MediaInput.Source);

                m_Playback.FileName = videoFile;
                m_Playback.OutputFileName = Path.Combine(audioDir , "001.wav");
                audio_gen = 0;
                m_Playback.Start();
                while (audio_gen == 0) {
                    Application.DoEvents();
                }

                if (!File.Exists(Path.Combine(audioDir , "001.wav"))) {
                    curErrLog += "Unable to generate audio file";
                    videoWindow.VlcPlayerControl.Dispose();
                    videoWindow.Close();
                    return;
                }

                convert_mp3(audioDir);

                while (!File.Exists(Path.Combine(audioDir, "001.mp3"))) {
                    Application.DoEvents();
                }
             
                videoWindow.VlcPlayerControl.Play(currentItem.MediaInput);
                videoWindow.VlcPlayerControl.PauseOrResume();
                long tTime = information.DurationMilliseconds;
                int vWidth = 0;
                int vHeight = 0;
                if (cPDF.b_customwidth == 0)
                {
                    vWidth = information.Width;
                }
                else { vWidth = cPDF.b_customwidth; }
                if (cPDF.b_customheight == 0)
                {
                    vHeight = information.Height;
                }
                else { vHeight = cPDF.b_customheight; }

                long cTime = time_interval;
                int imgNo = 1;
                
                while (cTime < tTime)
                {
                    probar_update("Capture image...", Convert.ToInt32(tTime), Convert.ToInt32(cTime));
                    videoWindow.VlcPlayerControl.Time = TimeSpan.FromMilliseconds(cTime);
                    videoWindow.VlcPlayerControl.PauseOrResume();
                    System.Threading.Thread.Sleep(500);
                    videoWindow.VlcPlayerControl.Player.TakeSnapshot(Path.Combine( imgDir , "" + imgNo + ".jpg"), vWidth, vHeight);

                    cTime += time_interval;
                    imgNo++;
                }

              //  videoWindow.VlcPlayerControl.Dispose();
                videoWindow.Close();

                #endregion
                int borderWidth = Convert.ToInt32(Convert.ToDouble(information.Width) * Convert.ToDouble(0.030241935));

                probar_update("Generate html...", 10, 7);

                #region html generation
                int totalImg = imgNo - 1;
                int pCount = totalImg / (page_row * page_col);

                if (pCount * (page_row * page_col) < totalImg) {
                    pCount = pCount + 1;
                }

                int imgSeq = 1;
                //each page
                for (int p = 1; p <= pCount; p++)
                {
                    string pgTxt = "<!DOCTYPE HTML>\n<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:ibooks=\"http://apple.com/ibooks/html-extensions\" xmlns:epub=\"http://www.idpf.org/2007/ops\">\n";
                    pgTxt += "<head>\n";
                    pgTxt += "<title>" + b_title + " : Page " + p.ToString() + "</title>\n";
                    pgTxt += "<link rel=\"stylesheet\" type=\"text/css\" href=\"../styles/page.css\" />\n";
                    int viewWidth = vWidth * page_col;
                    int viewHeight = vHeight * page_row;
                    viewWidth = viewWidth + (14 * page_col) + borderWidth;
                    viewHeight = viewHeight + (14 * page_row) + borderWidth;

                    pgTxt += "<meta name=\"viewport\" content=\"width=" + viewWidth.ToString() + ", height=" + viewHeight.ToString() + "\"/>\n";
                    pgTxt += "</head>\n";
                    pgTxt += "<body>\n<div style=\"width:" + viewWidth + "px; height:" + viewHeight + "px;\">\n<table>";

                    for (int r = 1; r <= page_row; r++)
                    {
                        pgTxt += "<tr>";
                        for (int c = 1; c <= page_col; c++) {
                            if (imgSeq > totalImg) { break; }
                            pgTxt += "<td>";
                            pgTxt += "<img id=\"m" + imgSeq.ToString() + "\" src=\"../images/" + imgSeq.ToString() + ".jpg\" alt=\"\" />";
                            pgTxt += "</td>";
                            imgSeq++;
                        }//end col

                        pgTxt += "</tr>";
                    }//end row

                    pgTxt += "</table></div></body>\n</html>";
                    File.WriteAllText(Path.Combine(htmlDir, "page" + p.ToString() + ".html"), pgTxt);
                }//end page

                #endregion


                probar_update("Metadata generation...", 10, 7);

                #region content.opf
                string contX = "<manifest>\n";
                //contX += "<item id=\"cover\" href=\"html/cover.html\" media-type=\"application/xhtml+xml\" />\n";
                contX += "<item id=\"ncx\" href=\"toc.ncx\" media-type=\"application/x-dtbncx+xml\" />\n";
                contX += "<item id=\"pagecommon\" href=\"styles/page.css\" media-type=\"text/css\" />\n";

                for (int p = 1; p <= pCount; p++) {
                    contX += "<item id=\"page" + p.ToString() + "\" href=\"html/page" + p.ToString() + ".html\" media-type=\"application/xhtml+xml\" media-overlay=\"page" + p.ToString() + "smil\" />\n";
                    
                    contX += "<item id=\"page" + p.ToString() + "smil\" href=\"html/page" + p.ToString() + ".smil\" media-type=\"application/smil+xml\" />\n";   
                }
                contX += "<item id=\"001audio\" href=\"audio/001.mp3\" media-type=\"audio/mpeg\" />\n";
                for (int m = 1; m <= totalImg; m++) {
                    contX += "<item id=\"image" + m.ToString() + "\" href=\"images/" + m.ToString() + ".jpg\" media-type=\"image/jpeg\" />\n";
                }
                contX += "<item id=\"cover-image\" href=\"images/cover.jpg\" media-type=\"image/jpeg\" />\n";


                contX += "</manifest>\n";
                contX += "<spine toc=\"ncx\">\n";
                contX += "<itemref idref=\"cover\" />\n";
                for (int px = 1; px < pCount + 1; px++)
                {
                    contX += "<itemref idref=\"page" + px.ToString() + "\" />\n";
                }
                contX += "</spine>\n";
                string rcont = File.ReadAllText(Path.Combine( Application.StartupPath , "template\\OEBPS\\content.opf"));
                rcont = rcont.Replace("<dc:title></dc:title>", "<dc:title>" + b_title + "</dc:title>");
                rcont = rcont.Replace("<dc:creator></dc:creator>", "<dc:creator>" + b_author + "</dc:creator>");
                rcont = rcont.Replace("<dc:publisher></dc:publisher>", "<dc:publisher>" + b_author + "</dc:publisher>");
                rcont = rcont.Replace("<dc:rights></dc:rights>", "<dc:rights>" + b_author + "</dc:rights>");
                string mdyDate = DateTime.Today.ToString("yyyy-MM-dd");
                rcont = rcont.Replace("<dc:date></dc:date>", "<dc:date>" + mdyDate + "</dc:date>");

                rcont = rcont.Replace("</metadata>", "</metadata>" + contX);
                File.WriteAllText(Path.Combine( oebDir , "content.opf"), rcont);
       
                
                #endregion

               

                #region create page.css
                string pStyle = ".-epub-media-overlay-active{\n border:"  + borderWidth.ToString() + "px solid black; \n border-color:#F00; \n border-style:dashed; \n }\n";
                File.WriteAllText(Path.Combine(styDir, "page.css"), pStyle);


                #endregion

                #region create smil file
                imgSeq = 1;
                double c_begin = 0;
                double c_end = time_interval;

                for (int s = 1; s <= pCount; s++) { 
                  string smTxt = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";
                      smTxt += "<smil xmlns=\"http://www.w3.org/ns/SMIL\" version=\"3.0\" profile=\"http://www.idpf.org/epub/30/profile/content/\">\n";
                      smTxt += "<body>\n";
                      int parID = 1;
                      for (int r = 1; r <= page_row; r++) {
                          for (int c = 1; c <= page_col; c++) {
                              if (imgSeq > totalImg) { break; }
                              smTxt += "<par id=\"par" + parID.ToString() + "\">\n";
                              smTxt += "<text src=\"page" + s.ToString() + ".html#m" + imgSeq.ToString() + "\" />\n";
                              string sTime = String.Format("{0:0.########}", TimeSpan.FromMilliseconds(c_begin).TotalSeconds) + "s";
                              string eTime = String.Format("{0:0.########}", TimeSpan.FromMilliseconds(c_end).TotalSeconds) + "s";
                              smTxt += "<audio src=\"../audio/001.mp3\" clipBegin=\"" + sTime + "\" clipEnd=\"" + eTime + "\"/>\n";

                              smTxt += "</par>\n";
                              parID++;
                              imgSeq++;
                              c_begin = c_end;
                              c_end = c_end + time_interval;
                          }                      
                      }                     
                      smTxt += "</body>\n";
                      smTxt += "</smil>";
                      File.WriteAllText(Path.Combine(htmlDir, "page" + s.ToString() + ".smil"), smTxt);
                }
                #endregion

                #region toc.ncx
                string tocX = File.ReadAllText(Path.Combine( Application.StartupPath , "template\\OEBPS\\toc.ncx"));
                tocX = tocX.Replace("<docTitle><text></text></docTitle>", "<docTitle><text>" + b_title + "</text></docTitle>");
                tocX = tocX.Replace("<docAuthor><text></text></docAuthor>", "<docAuthor><text>" + b_author + "</text></docAuthor>");
                tocX = tocX.Replace("<navLabel><text></text></navLabel>", "<navLabel><text>" + b_title + "</text></navLabel>");
                File.WriteAllText(oebDir + "\\toc.ncx", tocX);
                #endregion

                #region cover image

                if (cPDF.b_coverpath != "")
                {
                    File.Copy(cPDF.b_coverpath, Path.Combine(imgDir, "cover.jpg"), true);
                }
                else
                {
                    File.Copy(Path.Combine(Application.StartupPath, "template\\cover.jpg"), Path.Combine(imgDir, "cover.jpg"), true);
                }
                #endregion


                File.Copy(Path.Combine( Application.StartupPath , "template\\META-INF\\container.xml"), Path.Combine( wrkDir , "META-INF\\container.xml"), true);
                File.Copy(Path.Combine(Application.StartupPath, "template\\META-INF\\com.apple.ibooks.display-options.xml"), Path.Combine(wrkDir, "META-INF\\com.apple.ibooks.display-options.xml"), true);
                File.Copy(Path.Combine( Application.StartupPath , "template\\mimetype"), Path.Combine( wrkDir , "mimetype"), true);



                string[] content = { wrkDir + "\\mimetype", wrkDir + "\\OEBPS", wrkDir + "\\META-INF" };

                probar_update("ePub Package creation...", 10, 9);

                //epub package
                #region epub package
                string outepubPath = outPath + "\\" + fnameWOE + ".epub";
                ProcessStartInfo psInfo = new ProcessStartInfo();
                Directory.SetCurrentDirectory(wrkDir);
                psInfo.CreateNoWindow = true;
                psInfo.UseShellExecute = false;
                psInfo.RedirectStandardOutput = true;
                psInfo.WindowStyle = ProcessWindowStyle.Hidden;
                psInfo.FileName = "ezip.exe";
                psInfo.Arguments = " -Xr9D \"" + outepubPath + "\" mimetype *";
                //psInfo.Arguments = "-Xr9D " + cPDF.b_filename + ".epub mimetype *";
                try
                {

                    using (Process exeProcess = Process.Start(psInfo))
                    {
                        string outE = exeProcess.StandardOutput.ReadToEnd();
                        exeProcess.WaitForExit();
                    }
                }
                catch (Exception erd)
                {
                    curErrLog += erd.Message.ToString();

                }

                Directory.SetCurrentDirectory(Application.StartupPath);


                #endregion



                Directory.Delete(rootPath, true);

            }
            catch (Exception erd) {
                curErrLog += erd.Message.ToString();
                return;
            
            }
        
        }

        public void convert_mp3(string wavPath)
        {
            try
            {
                string[] wavFiles = Directory.GetFiles(wavPath, "*.wav");
                foreach (string w in wavFiles)
                {
                    string sPath = Path.GetDirectoryName(w);
                    string fName = Path.GetFileNameWithoutExtension(w);

                    WavToMP3_04 oConv = new WavToMP3_04();
                    oConv.WavToMP3(Application.StartupPath + "\\videoex", w, sPath + "\\" + fName + ".mp3", true);

                 

                    File.Delete(w);
                }
            }
            catch (Exception erd) {
                gCls.show_error(erd.Message.ToString());
                return;

            }
        }

        public void convert_frame() {
            try
            {

                #region file validate
                if (txt_videopath.Text == "") {
                    gCls.show_error("Select video file");
                    return;
                }

                if (txt_coverpath.Text == "") {
                    gCls.show_error("Select cover image file");
                    return;
                }

                if (txt_booktitle.Text == "") {
                    gCls.show_error("Enter book title");
                    return;
                }

                if (txt_bookauthor.Text == "") {
                    gCls.show_error("Enter book author");
                    return;
                }

                if (!File.Exists(txt_videopath.Text)) {
                    gCls.show_error("Video file not found");
                    return;
                }

                if (!File.Exists(txt_coverpath.Text)) {
                    gCls.show_error("Cover image file not found");
                    return;
                }
                #endregion

                string videoFile = txt_videopath.Text;
                string coverImageFile = txt_coverpath.Text;
                string book_title = txt_booktitle.Text;
                string book_author = txt_bookauthor.Text;
                int time_invertal = Convert.ToInt32( txt_interval.Value);
                int page_row = Convert.ToInt32(txt_row.Value);
                int page_col = Convert.ToInt32(txt_column.Value);
                int custom_width = 0;
                int custom_height = 0;
                if (txt_customwidth.Enabled) {
                    custom_width = Convert.ToInt32(txt_customwidth.Value);
                }
                if (txt_customheight.Enabled) {
                    custom_height = Convert.ToInt32(txt_customheight.Value);
                }

                string temp_path = Path.Combine(Application.StartupPath, "in_video");

                if (Directory.Exists(temp_path)) {
                    Directory.Delete(temp_path, true);                    
                }

              

                Directory.CreateDirectory(temp_path);

                string outPath = Path.GetDirectoryName(txt_videopath.Text);
                string fNameWOE = Path.GetFileNameWithoutExtension(txt_videopath.Text);

                Updf pdf_info = new Updf(fNameWOE, txt_booktitle.Text, txt_bookauthor.Text, txt_videopath.Text , temp_path, outPath, txt_coverpath.Text, time_invertal, page_row, page_col,custom_width,custom_height);
                curErrLog = "";




                _bw = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                _bw.DoWork += bw_DoWork;
                probar.Visible = true;
                _bw.ProgressChanged += bw_ProgressChanged;
                _bw.RunWorkerCompleted += bw_RunWorkerCompleted;
               

                _bw.RunWorkerAsync(pdf_info);

               
             


            }
            catch (Exception erd) {
                gCls.show_error(erd.Message.ToString());
                return;
            }


        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Updf live_pdf_info = (Updf)e.Argument;
            convert_epub(live_pdf_info);

        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            probar.Visible = false;
            lbstatus.Text = "";
            if (curErrLog != "")
            {
                gCls.show_error(curErrLog);
               
                return;
            }
            else
            {

                gCls.show_message("ePub converted successfully.");
            }
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
      
        public void probar_update(string Etext, int max, int cur)
        {
            try
            {
                if (probar.InvokeRequired)
                {
                    update_probar up = new update_probar(probar_update);
                    this.Invoke(up, new object[] { Etext, max, cur });
                }
                else
                {
                    if (Etext != "")
                    {
                        lbstatus.Text = Etext;
                    }
                    probar.Maximum = max;
                    probar.Value = cur;
                }

            }
            catch { }

        }

        private void initializeVlcPlayerControl(bool showVideoWindow)
        {
            try
            {
                initializeVideoWindow();
                //
                if (showVideoWindow)
                {
                    videoWindow.Show();
                }
                //
                if (!videoWindow.VlcPlayerControl.IsInitialized)
                {
                    videoWindow.VlcPlayerControl.Initialize(this);
                }
            }
            catch (Exception erd) {
                gCls.show_error(erd.Message.ToString());
                return;
            }
        }

        private void initializeVideoWindow()
        {
            try
            {
                if ((videoWindow == null) || (videoWindow.IsDisposed))
                {
                    if (videoWindow != null)
                    {
                        // videoWindow.Closing -= VideoWindowOnClosing;
                    }
                    videoWindow = new VideoWindow();
                    // videoWindow.Closing += VideoWindowOnClosing;
                    if (!videoWindow.VlcPlayerControl.IsInitialized)
                    {
                        videoWindow.VlcPlayerControl.Initialize(this);
                    }
                    //
                    // videoWindow.VlcPlayerControl.StateChanged += vlc_onStateChanged;
                    // videoWindow.VlcPlayerControl.PositionChanged += vlc_onPositionChanged;
                    //videoWindow.VlcPlayerControl.EndReached += vlc_onEndReached;
                    //
                }
            }
            catch (Exception erd) {
                gCls.show_error(erd.Message.ToString());
                return;
            }
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text += " " + Application.ProductVersion;
           
            gCls.update_path_var();
            oscreen wscr = new oscreen();
            wscr.Show();
           
            top_menu_ctrl.Visible = false; 
            wscr.Close();
           // Playback_OnPlaybackStop(sender, e);
        }

        private void Playback_OnPlaybackStop(object sender, EventArgs e)
        {
            audio_gen = 1;
            
        }

        private void Playback_OnPlaybackStart(object sender, EventArgs e)
        {
            audio_gen = 0;
        }

        private void kryptonLabel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception erd) {
                gCls.show_error(erd.Message.ToString());
                return;
            }
        }

        private void btn_convert_Click(object sender, EventArgs e)
        {
            convert_frame();   
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    txt_customheight.Enabled = true;
                    txt_customwidth.Enabled = true;
                }
                else {
                    txt_customheight.Enabled = false;
                    txt_customwidth.Enabled = false;
                }
            }
            catch (Exception erd) {
                gCls.show_error(erd.Message.ToString());
                return;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fld = new OpenFileDialog();
                fld.Title = "Select Video File";
                fld.Filter = "AVI File|*.avi|MPG File|*.mpg|MPEG File|*.mpeg";
                fld.ShowDialog();
                if (fld.FileName != "")
                {
                    txt_videopath.Text = fld.FileName;
                }

            }
            catch (Exception erd) {
                gCls.show_error(erd.Message.ToString());
                return;
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fld = new OpenFileDialog();
                fld.Title = "Select cover image file";
                fld.Filter = "JPG File|*.jpg";
                fld.ShowDialog();
                if (fld.FileName != "")
                {
                   txt_coverpath.Text  = fld.FileName;
                }

            }
            catch (Exception erd) {
                gCls.show_error(erd.Message.ToString());
                return;
            }
        }

        private void kryptonGroupBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void releaseLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

               

            }
            catch (Exception erd) {
                gCls.show_error(erd.Message.ToString());
                return;
            }
        }

        private void kryptonLinkLabel1_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                gCls.show_message(Application.ProductName + " " + Application.ProductVersion + "\nSend your Feedbacks to : vickypatel2020@gmail.com\n");
            }
            catch { }
        }

       

    }
}
