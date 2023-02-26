using BlApi;
using BO;
namespace Simulator
{
    public static class simulator
    {
        static bool bContinue = true;
        private static readonly BlApi.IBl? bl = BlApi.Factory.Get();
        private static string previousStatus { get; set; }
        private static string nextStatus { get; set; }
        static Random random = new Random();
        public static event EventHandler StopSimulator;
        public static event EventHandler ProgressChange;
        public static bool x = false;
        public static void DoStop()
        {
            bContinue = false;
         
            StopSimulator("", EventArgs.Empty);

        }
        public static void run()
        {
            bContinue = true;
            Thread thread = new Thread(new ThreadStart(ChooseOrder));
            bContinue = true;
    
            thread.Start();
            return;

        }
        public static void ChooseOrder()
        {

            while (bContinue)
            {
                int? OrderId = bl?.Order.SelectingOrderForTreatment();
                x = true;
                if (OrderId == null) DoStop(); //bContinue = false;
                else
                {
                    BO.Order? order = bl?.Order.ReadOrderInformation((int)OrderId);
                    previousStatus = order.Status.ToString();
                    nextStatus = order.Status.ToString();
                    int time = random.Next(1000, 6000);
                    if (previousStatus == "Dispatched")
                    {
                        order = bl?.Order.UpdateShipping((int)OrderId);
                        nextStatus = order.Status.ToString();
                    }
                    else if (order?.DeliveryDate == DateTime.MinValue)
                    {
                        order = bl?.Order.UpdateDelivery((int)OrderId);
                        nextStatus = order.Status.ToString();
                    }
                    CurruntOrder cOrder = new(order, previousStatus, nextStatus, time, OrderId);
                    if (ProgressChange != null)
                        ProgressChange(null, cOrder);
                    Thread.Sleep(time);
                }
            }
            return;       
        }
    }
    public class CurruntOrder : EventArgs
    {
        public BO.Order order;
        public string currentStatus;
        public string nextStatus;
        public int seconds;
        public int? Id;
        public CurruntOrder(BO.Order ord, string currentSt, string nextSt, int sec, int? id)
        {
            currentStatus = currentSt;
            nextStatus = nextSt;
            seconds = sec;
            Id = id;
            order = ord;
        }
    }
}