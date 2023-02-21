using BlApi;

namespace Simulator
{
    public static class simulator
    {
        static bool bContinue = true;
        private static readonly BlApi.IBl? bl = BlApi.Factory.Get();
      

        public static void run()
        {

            new Thread(
         () =>
         {
            while (bContinue)
            {
                int? OrderId= bl?.Order.SelectingOrderForTreatment();
                int time = 20;
                if (OrderId == null) bContinue = false;
                BO.Order? order = bl?.Order.ReadOrderInformation((int)OrderId);
                Thread.Sleep(time * 1000);
                if (order?.ShipDate == DateTime.MinValue)
                {
                     bl?.Order.UpdateShipping((int)OrderId);
                }
                else if (order?.DeliveryDate == DateTime.MinValue)
                {
                    bl?.Order.UpdateDelivery((int)OrderId);
                }
            }
         }
            ).Start();
        }
    }
}