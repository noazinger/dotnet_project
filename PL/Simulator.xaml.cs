using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Simulator;
namespace PL
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class Simulator : Window
    {
        int second;
        DateTime dt = new DateTime();
        Tuple<string, string,DateTime, DateTime,int?,int> dc;

        public Simulator()
        {
                InitializeComponent();
                BackgroundWorker worker; //field
                worker = new BackgroundWorker();
                worker.DoWork += Worker_DoWork;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.WorkerReportsProgress = true;
                worker.RunWorkerAsync();
                worker.WorkerReportsProgress = true;
                worker.WorkerSupportsCancellation = true;
                
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            simulator.newProsses += newOrderProsses;
            simulator.run();


        }
        private void newOrderProsses(object sender, EventArgs e)
        {
            if (!(e is CurruntOrder))
                return;
            dt = DateTime.Now;
            CurruntOrder cOrder = e as CurruntOrder;
            dc = new Tuple<string, string, DateTime, DateTime, int?, int>(cOrder.currentStatus, cOrder.nextStatus, dt, dt, cOrder.Id, cOrder.seconds);
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(newOrderProsses, sender, e);
            }
            else
            {
                DataContext = dc;
            }
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object result = e.Result;
        }
    }
}
