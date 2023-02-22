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
        public static event EventHandler newProcces;
        public static event EventHandler ProgressChange;
        public static void run()
        {
            new Thread(
         () =>
         {
             while (bContinue)
             {

                 int? OrderId = bl?.Order.SelectingOrderForTreatment();
                 if (OrderId == null) bContinue = false;
                 BO.Order? order = bl?.Order.ReadOrderInformation((int)OrderId);
                 previousStatus = order.Status.ToString();
                 nextStatus = order.Status.ToString();
                 int time = random.Next(1000, 4000);
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
                 CurruntOrder cOrder = new(previousStatus, nextStatus, time);
                 if (ProgressChange != null)
                     ProgressChange(null, cOrder);
                 Thread.Sleep(time * 1000);
             }
         }
            ).Start();
        }
    }
    public class CurruntOrder : EventArgs
    {
        public string currentStatus;
        public string nextStatus;
        public int seconds;
        public CurruntOrder(string currentSt,string nextSt, int sec)
        {
            currentStatus= currentSt;
            nextStatus= nextSt;
            seconds = sec;
        }
    }
}