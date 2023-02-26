using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using Simulator;
using System.Windows.Media.Animation;


namespace PL
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class Simulator : Window
    {
        BlApi.IBl bl;
        string nextStatus;
        string previousStatus;
        BackgroundWorker worker; //field
        //===============================================
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        //================================================
        private Stopwatch stopwatch { get; set; }
        private bool IsTaimerRun;
        DateTime timeOfStarting= DateTime.Now;
        DateTime timeOfEnding= DateTime.Now;
        int second;
        DateTime dt = new DateTime();
        Tuple<string, string,DateTime, DateTime,int?,int> dc;
        Duration duration;
        DoubleAnimation doubleAnimation;
        ProgressBar ProgressBar;
        public Simulator(BlApi.IBl BL)
        {
            InitializeComponent();
            bl = BL;
            Loaded += ToolWindow_Loaded;
            TimerStart();
        }
        private void Timer_DoWork(object sender, DoWorkEventArgs e)
        {
            simulator.ProgressChange += newOrderProsses;
            simulator.StopSimulator += StopPr;
            simulator.run();
            while (IsTaimerRun)
            {
                worker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }
        void TimerStart()
        {
            stopwatch=new Stopwatch();
            worker=new BackgroundWorker();
            worker.DoWork += Timer_DoWork;
            worker.ProgressChanged += Timer_ProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            //worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            stopwatch.Restart();
            IsTaimerRun=true;
            worker.RunWorkerAsync();
        }
        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        void ProgressBarStart(int sec)
        {
            if (ProgressBar != null)
            {
                statusBar.Items.Remove(ProgressBar);
            }
            ProgressBar = new ProgressBar();
            ProgressBar.IsIndeterminate = false;
            ProgressBar.Orientation = Orientation.Horizontal;
            ProgressBar.Width = 500;
            ProgressBar.Height = 30;
            duration = new Duration(TimeSpan.FromSeconds(sec * 2));
            doubleAnimation = new DoubleAnimation(200.0, duration);
            ProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);
            statusBar.Items.Add(ProgressBar);
        }
        private void newOrderProsses(object sender, EventArgs e)
        {
            if (!(e is CurruntOrder))
                return;
            CurruntOrder cOrder = e as CurruntOrder;
            this.previousStatus = (cOrder.order.ShipDate == null) ? BO.OrderStatus.Dispatched.ToString() : BO.OrderStatus.Shipped.ToString();
            this.nextStatus = (cOrder.order.ShipDate == null) ? BO.OrderStatus.Shipped.ToString() : BO.OrderStatus.Delivered.ToString();
            dc = new Tuple< string, string, DateTime, DateTime, int?, int>(cOrder.currentStatus, cOrder.nextStatus, dt, dt, cOrder.Id, cOrder.seconds/1000);
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(newOrderProsses, sender, e);
            }
            else
            {
                DataContext = dc;
                ProgressBarStart(cOrder.seconds / 1000);
            }
        }
        private void Timer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string timerText = stopwatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            simulatorClock.Text = timerText;
        }
        //private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    object result = e.Result;
        //}
        
        public void StopPr(object sender, EventArgs e)
        {
            MessageBox.Show("successfully finish updating all orders");

            simulator.ProgressChange -= newOrderProsses;
            simulator.StopSimulator -= StopPr;
            /*if (worker.WorkerSupportsCancellation == true)
                worker.CancelAsync();*/
            while (!CheckAccess())
            {
                Dispatcher.BeginInvoke(StopPr, sender, e);
            }
            this.Close();
        }

        private void StopSimulatorBTN_Click(object sender, RoutedEventArgs e)
        {
            if (IsTaimerRun)
            {
                stopwatch.Stop();
                IsTaimerRun = false;
            }
            simulator.DoStop();
            this.Close();
        }
    }
}
