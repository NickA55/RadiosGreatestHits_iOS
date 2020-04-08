using AVFoundation;
using Foundation;
using System;
using System.Xml;
using UIKit;
using Xamarin.Essentials;

namespace RadiosGreatestHits_iOS
{
    public partial class ViewController : UIViewController
    {
        AVPlayer player;

        string radioURL = "http://radiosgreatesthits.out.airtime.pro:8000/radiosgreatesthits_a";
        string trackInfoURL = "http://radiosgreatesthits.out.airtime.pro:8000/radiosgreatesthits_a.xspf";
        string currentSongTitle = "";

        bool isPlaying = false;
        bool isPaused = false;

        AVPlayerTimeControlStatus avPlayerStatus;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            lblNowPlaying1.Text = currentSongTitle;

            if (player == null)
            {
                isPaused = true;
                player = new AVPlayer();
            }


            avPlayerStatus = player.TimeControlStatus;
        }

        partial void btnPlay_Click(UIButton sender)
        {
            if (player == null)
            {
                player = new AVPlayer();

                isPaused = true;
            }

            avPlayerStatus = player.TimeControlStatus;

            if (avPlayerStatus == AVPlayerTimeControlStatus.Paused ||
                avPlayerStatus == AVPlayerTimeControlStatus.WaitingToPlayAtSpecifiedRate)
            {
                
                btnPlay.SetTitle(@"", UIControlState.Normal); // Pause button
                player = AVPlayer.FromUrl(NSUrl.FromString(radioURL));
                player.Play();

                isPaused = false;
                isPlaying = true;

                UpdateSongTitle();

            }
            else if(avPlayerStatus == AVPlayerTimeControlStatus.Playing)
            {
                btnPlay.SetTitle(@"", UIControlState.Normal); // Play Button
                isPaused = true;
                isPlaying = false;

                
                player.Pause();
            }
        }

        partial void btnMute_Click(UIButton sender)
        {
            if (player != null)
            {
                avPlayerStatus = player.TimeControlStatus;

                if (IsMuted())
                {
                    player.Muted = false;
                    btnMute.SetTitle(@"", UIControlState.Normal);
                    btnMute.SetTitleColor(UIColor.White, UIControlState.Normal);
                }
                else
                {
                    player.Muted = true;
                    btnMute.SetTitle(@"", UIControlState.Normal);
                    btnMute.SetTitleColor(UIColor.Red, UIControlState.Normal);
                }
            }
        }

        partial void btnStop_Click(UIButton sender)
        {
            if (player != null)
            {
                player.Dispose();

                btnPlay.SetTitle(@"", UIControlState.Normal); // Play Button
            }
        }

        private bool IsMuted()
        {
            return player.Muted;
        }


        // private void UpdateSongTitle(Object source, System.Timers.ElapsedEventArgs e)
        private void UpdateSongTitle()
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(trackInfoURL);

                bool getTitle = false;
                int gotIt = 0;

                while (reader.Read())
                {

                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element: // The node is an element.



                            //Console.Write("<" + reader.Name);

                            while (reader.MoveToNextAttribute())
                            {
                                // Read the attributes.
                                //Console.Write(" " + reader.Name + "='" + reader.Value + "'");
                            }
                            //Console.Write(">");
                            //Console.WriteLine(">");

                            if (reader.Name.ToLower() == "title")
                            {
                                getTitle = true;

                            }
                            break;
                        case XmlNodeType.Text: //Display the text in each element.
                            if (getTitle)
                            {

                                if (gotIt == 0)
                                {
                                    gotIt++;
                                }
                                else
                                {
                                    //Console.WriteLine("********** TITLE ******************** " + reader.Value);
                                    currentSongTitle = reader.Value;

                                    MainThread.BeginInvokeOnMainThread(() =>
                                    {
                                        //Console.WriteLine("********** TITLE THREAD ******************** " + currentSongTitle);
                                        lblNowPlaying1.Text = currentSongTitle;
                                    });



                                    getTitle = false;

                                }
                            }
                            //Console.WriteLine(reader.Value);
                            break;
                        case XmlNodeType.EndElement: //Display the end of the element.
                                                     //Console.Write("</" + reader.Name);
                                                     //Console.WriteLine(">");
                            break;
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update Title Error: " + ex.Message);
            }




        }



    }
}