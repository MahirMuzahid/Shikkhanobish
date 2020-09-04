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
        public TutionPage ( TransferInfo trnsInfo )
        {
            InitializeComponent ();
            info = trnsInfo;
            CrossOpenTok.Current.CycleCamera ();
            sec = 0;
            min = 0;
           //StartTimer ();
            
            
        }

        private void OnEndCall ( object sender , EventArgs e )
        {
            CrossOpenTok.Current.EndSession ();
            gotoRatingPage ();
        }

        private void OnSwapCamera ( object sender , EventArgs e )
        {
            CrossOpenTok.Current.CycleCamera ();
            CrossOpenTok.Current.SendMessageAsync ( "HogaMara" );
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

            sec = sec + 1;
            if ( sec == 59 )
            {
                min = min + 1;
                sec = 0;
            }
            Device.BeginInvokeOnMainThread ( ( ) =>
            {
                timerlbl.Text = min + ":" + sec;
            } );
            
            
            
        }
    }
}