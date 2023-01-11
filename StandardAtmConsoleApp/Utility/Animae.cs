namespace StandardAtmConsoleApp.Helpers
{
    public class Animae
    {
        public static void PrintDotAnimation(int Timer = 10, int seconds = 200)
        {
            for (int i = 0; i < Timer; i++)
            {
                Console.Write(".");
                Thread.Sleep(seconds);
            }
            Console.Clear();
        }
    }
}
