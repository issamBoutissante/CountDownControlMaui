using System.Timers;

namespace CountDownControlMaui
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }
        private void CountdownLabel_OnCounterEnds(object sender, EventArgs e)
        {
            Shell.Current.DisplayAlert("Message", "The counter has ended!", "OK");
        }
    }
}