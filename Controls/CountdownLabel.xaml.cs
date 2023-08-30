using System.Timers;

namespace CountDownControlMaui.Controls;

public partial class CountdownLabel : Label
{
    public int Minutes { get; set; }
    public int Seconds { get; set; }

    private IDispatcherTimer _timer = Application.Current.Dispatcher.CreateTimer();
    private TimeSpan _timeRemaining;
    public CountdownLabel()
    {
        InitializeComponent();
        InitializeTimer();
    }
    private void InitializeTimer()
    {
        _timeRemaining = new TimeSpan(0, Minutes, Seconds);
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += (s,e) => OnTick();
        _timer.Start();
    }

    private void OnTick()
    {
        _timeRemaining = _timeRemaining.Add(TimeSpan.FromSeconds(-1));

        if (_timeRemaining.TotalSeconds <= 0)
        {
            // Execute the event when the timer reaches 0:00
            OnCounterEnds?.Invoke(this, new EventArgs());

            // Reset the timer back to first value
            _timeRemaining = new TimeSpan(0, Minutes, Seconds);
        }

        UpdateLabel();
    }
    private void UpdateLabel()
    {
        // Ensure the UI updates are done on the main thread
        Dispatcher.DispatchAsync(() =>
        {
            Text = _timeRemaining.ToString(@"mm\:ss");
        });
    }
    public delegate void CounterEndsHandler(object sender, EventArgs e);
    public event CounterEndsHandler OnCounterEnds;
}