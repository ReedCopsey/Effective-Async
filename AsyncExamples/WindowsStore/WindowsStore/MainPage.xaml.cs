namespace AsyncDemoStoreApp
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AsyncDemoStoreApp.Common;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Async demo

        //private async void ButtonStart_OnClick(object sender, RoutedEventArgs e)
        //{
        //    buttonStart.Click -= this.ButtonStart_OnClick;
        //    try
        //    {
        //        buttonStart.IsEnabled = false;
        //        buttonEnd.IsEnabled = true;
        //        this.TextBlockStatus.Text = "You clicked me.  Asynchronously waiting until you click second button!";
        //        await OnButtonClickAsync(this.buttonEnd);

        //        this.TextBlockStatus.Text = "Done!";
        //    }
        //    finally
        //    {
        //        buttonStart.IsEnabled = true;
        //        buttonEnd.IsEnabled = false;
        //        buttonStart.Click += this.ButtonStart_OnClick;
        //    }
        //}

        public static async Task OnButtonClickAsync(Button button)
        {
            var tcs = new TaskCompletionSource<bool>();
            RoutedEventHandler lambda = null;
            lambda = (o, e) =>
            {
                tcs.TrySetResult(true);
            };

            try
            {
                button.Click += lambda;
                await tcs.Task;
            }
            finally
            {
                button.Click -= lambda;
            }
        }

        public async Task OnBothButtonsClickedAsync()
        {
            var t1 = OnButtonClickAsync(this.buttonStart);
            var t2 = OnButtonClickAsync(this.buttonEnd);

            await Task.WhenAll(t1, t2);
        }
        
        #endregion

        #region Pitfall: Async void

        // private string data;

        //private async void ButtonStart_OnClick(object sender, RoutedEventArgs e)
        //{
        //    data = string.Empty;
        //    this.TextBlockAsyncData.Text = "Making request...";
        //    var data = await this.GetDataAsync();
        //    //await Task.Delay(1000);
        //    this.TextBlockAsyncData.Text = "Received data asynchronously: " + data;
        //}

        private async Task<string> GetDataAsync()
        {
            using (FooService service = new FooService())
            {
                return await service.BarAsync();
            }
        }
        #endregion

        #region Pitfall: Async void lambda
        //private async void ButtonStart_OnClick(object sender, RoutedEventArgs e)
        //{
        //    this.TextBlockAsyncData.Text = "Performing long running operation";
        //    var time = await Utilities.TimeOperationAsync(async () =>
        //        {
        //            double value = 0.0;
        //            for (int i = 1; i < 50; ++i)
        //            {
        //                value += Math.Sqrt(i);
        //                // This could be run with await...
        //                // Should we change it?
        //                await ProcessData(value);
        //            }
        //        });
        //    this.TextBlockAsyncData.Text = "Operation completed in " + time.ToString();
        //}

        private async Task ProcessData(double value)
        {
            await Task.Delay(100).ConfigureAwait(false);
        }
        #endregion

        #region Pitfall: Store a task for OnNavigatedTo
        public MainPage()
        {
            this.InitializeComponent();
            this.TextBlockAsyncData.Text = "Loading data asynchronously...";
        }
        
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            App app = App.Current as App;
            this.TextBlockAsyncData.Text = "Loaded text: " + await app.AsyncData;
        }
        #endregion

        #region Design for Async: Timeouts

        //private async void ButtonStart_OnClick(object sender, RoutedEventArgs e)
        //{
        //    buttonStart.Click -= this.ButtonStart_OnClick;
        //    try
        //    {
        //        buttonStart.IsEnabled = false;
        //        buttonEnd.IsEnabled = true;
        //        this.TextBlockStatus.Text = "You clicked me.  Asynchronously waiting until you click second button!";
        //        await OnButtonClickAsync(this.buttonEnd).TimeoutAfter(TimeSpan.FromSeconds(5.0));

        //        this.TextBlockStatus.Text = "Done!";
        //    }
        //    catch (TimeoutException timeoutException)
        //    {
        //        this.TextBlockStatus.Text = "You took too long!";
        //    }
        //    finally
        //    {
        //        buttonStart.IsEnabled = true;
        //        buttonEnd.IsEnabled = false;
        //        buttonStart.Click += this.ButtonStart_OnClick;
        //    }
        //}
        
        #endregion

        #region Design for Async: Cancellation

        //private async void ButtonStart_OnClick(object sender, RoutedEventArgs ea)
        //{
        //    buttonStart.Click -= this.ButtonStart_OnClick;

        //    var cts = new CancellationTokenSource();
        //    RoutedEventHandler cancelHandler = (o, e) => cts.Cancel();

        //    this.buttonEnd.Click += cancelHandler;
        //    buttonStart.IsEnabled = false;
        //    buttonEnd.IsEnabled = true;

        //    try
        //    {
        //        for (int i = 0; i < 100; ++i)
        //        {
        //            this.TextBlockStatus.Text = string.Format("You clicked me.  Click second button to cancel {0}!", i + 1);
        //            await Task.Delay(100).WithCancellation(cts.Token);
        //        }

        //        this.TextBlockStatus.Text = "Done!";
        //    }
        //    catch (OperationCanceledException)
        //    {
        //        this.TextBlockStatus.Text = "You cancelled!";
        //    }
        //    finally
        //    {
        //        buttonStart.IsEnabled = true;
        //        buttonEnd.IsEnabled = false;
        //        buttonEnd.Click -= cancelHandler;
        //        buttonStart.Click += this.ButtonStart_OnClick;
        //    }
        //}

        #endregion

        #region Design for Async: ConfigureAwait

        private async void ButtonStart_OnClick(object sender, RoutedEventArgs e)
        {
            buttonStart.Click -= this.ButtonStart_OnClick;
            try
            {
                buttonStart.IsEnabled = false;
                this.TextBlockStatus.Text = "You clicked me.  Asynchronously working...";
                var time = await Utilities.PerformTimedWorkAsync();

                this.TextBlockStatus.Text = "Completed in " + time.ToString();
            }
            finally
            {
                buttonStart.IsEnabled = true;
                buttonEnd.IsEnabled = false;
                buttonStart.Click += this.ButtonStart_OnClick;
            }
        }

        #endregion
    }
}
