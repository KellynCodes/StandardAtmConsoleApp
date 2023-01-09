using ATM.BLL.Implementation;
using ATM.BLL.Interfaces;
using System.Text;

namespace StandardAtmConsoleApp.Helpers
{
    public class SecretInput
    {
        private static readonly IMessage message = new Message();
        public static string Hashed(string? userInput = null)
        {
            const int exactInputLength = 10;
            if (userInput?.Length <= exactInputLength)
            {
                message.Error($"{userInput} is small. Input must be ten in numbers. Please do try again");
                Hashed();
                return string.Empty;
            }
            else if (userInput?.Length > exactInputLength)
            {
                message.Error($"{userInput} is more than ten. Input must be ten in numbers. Please do try again");
                Hashed();
                return string.Empty;
            }
            else
            {
                static string DisplayString(string originalString, int lastDigit)
                {
                    string strResult = new string('#', originalString.Length - lastDigit) +
                            originalString.Substring(originalString.Length - lastDigit);
                    return strResult;
                }
                const int LastFiveDigit = 5;
                return (DisplayString(userInput, LastFiveDigit));
            }
        }

        public static string Hash()
        {
            bool IsPrompt = true;
            string asterics = "";
            StringBuilder input = new();

            while (true)
            {
                if (IsPrompt)
                IsPrompt = false;   
                ConsoleKeyInfo inputKey = Console.ReadKey(true);

                if (inputKey.Key == ConsoleKey.Enter)
                {
                    if (input.Length >= 4)
                    {
                        break;
                    }
                    else
                    {
                        message.Error("\nPlease input must be greater Four.");
                        input.Clear();
                        IsPrompt = true;
                        continue;
                    }
                }
                if (inputKey.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Remove(input.Length - 1, 1);
                }
                else if (inputKey.Key != ConsoleKey.Backspace)
                {
                    input.Append(inputKey.KeyChar);
                    Console.Write(asterics + "*");
                }
            }

                    Console.WriteLine();
            return input.ToString();
        }
    }
}

