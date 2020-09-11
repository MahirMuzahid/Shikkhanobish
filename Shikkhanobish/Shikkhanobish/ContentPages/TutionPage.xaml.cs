using Shikkhanobish.ViewModel;
using System;
using Xamarin.Forms;
using System.Timers;
using Xamarin.Forms.OpenTok.Service;
using Xamarin.Forms.Xaml;
using Java.Lang;

namespace Shikkhanobish.ContentPages
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class TutionPage : ContentPage
    {
        private TransferInfo info = new TransferInfo ();
        private Timer timer = new Timer ();
        int sec, min;
        int ownthing = 0, i=0;
        bool firstTime;
        public TutionPage ( TransferInfo trnsInfo )
        {
            InitializeComponent ();
            info = trnsInfo;
            sec = 0;
            min = 0;
            firstTime = true;
            tnamelbl.Text = trnsInfo.Teacher.TeacherName;
            Device.StartTimer ( TimeSpan.FromSeconds ( 1.0 ) , CheckPositionAndUpdateSlider );
        }

        private void OnEndCall ( object sender , EventArgs e )
        {
            CrossOpenTok.Current.EndSession ();
            gotoRatingPage ();
        }

        private void OnSwapCamera ( object sender , EventArgs e )
        {
            CrossOpenTok.Current.CycleCamera ();
        }
        private void StopOwnVideo ( object sender , EventArgs e )
        {
            ownthing++;

            if(ownthing%2 == 1)
            {
                ownvideo.IsVisible = false;

            }
            else
            {
                ownvideo.IsVisible = true;

            }
            Device.StartTimer ( new TimeSpan ( 0 , 0 , 60 ) , ( ) =>
            {
                // do something every 60 seconds
                Device.BeginInvokeOnMainThread ( ( ) =>
                {
                   
                    // interact with UI elements
                } );
                return true; // runs again, or false to stop
            } );
        }


        public async void gotoRatingPage ( )
        {
            //info.StudyTimeInAPp = Int32.Parse(TimeEntry.Text);
            info.StudyTimeInAPp = 30;
            await Application.Current.MainPage.Navigation.PushModalAsync ( new RatingPage ( info ) ).ConfigureAwait ( false );
        }

        public void StartTimer ( )
        {
            timer = new Timer ();
            timer.Interval = 1000;
            timer.Elapsed += counter;
            timer.Start ();
            
            
        }
        private void counter ( object sender , ElapsedEventArgs e )
        {            
        }


        private bool CheckPositionAndUpdateSlider ( )
        {
            sec = sec + 1;
            if ( sec == 59 )
            {
                min = min + 1;
                sec = 0;
            }
            if(firstTime ==  true)
            {
                safelbl.IsVisible = true;
                timerlbl.TextColor = Color.Green;
                if(sec == 20)
                {
                    sec = 0;
                    firstTime = false;
                    safelbl.IsVisible = false;
                    timerlbl.TextColor = Color.Black;
                }
            }
            timerlbl.Text = min + ":" + sec;
            return true;
        }
    }
}