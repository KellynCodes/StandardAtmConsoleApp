﻿using ATM.BLL.Implementation;
using ATM.BLL.Interfaces;
using StandardAtmConsoleApp.Helpers;

namespace ATM.UI
{
    public class ContinueOrEndProcess : IContinueOrEndProcess
    {
        readonly IAuthService authService = new AuthService();

        public void EndProcess()
        {
            Console.WriteLine($"Collect your Card. Thank you for using {GetAtmData.GetData().Name}");
        }

        public void ContinueProcess()
        {
            authService.Login();
        }

        public void Answer()
        {
            IMessage message = new Message();
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        question: message.Alert("Would like to End or Continue transactions. Enter [YES/NO]");
            string answer = Console.ReadLine() ?? string.Empty;
            if (answer.Trim().ToUpper() == "YES")
            {
                EndProcess();
            }else if(answer.Trim().ToUpper() == "NO")
            {
                Program.GetUserChoice();
            }
            else
            {
                message.Error("Please enter yes or no for us to be sure you wanted to close the application.");
                goto question;
            }
        }
    }
}
