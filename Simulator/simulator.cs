namespace Simulator
{
    public static class simulator
    {
        static bool bContinue = true;

        public static void run()
        {

            new Thread(
        () =>
        {
            while (bContinue)
            {
                Console.WriteLine("I'm running on another thread!");
                Thread.Sleep(1000);
            }
            Console.WriteLine("ByeBye Anonymouse Thread");
        }
            ).Start();
        }

    }
}