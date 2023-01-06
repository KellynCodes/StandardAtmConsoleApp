using ATM.BLL.Implementation;
using ATM.BLL.Interfaces;
using ATM.DATA.Domain;

namespace ATM.UI
{
    public class ContinueOrEndProcess : IContinueOrEndProcess
    {
        readonly IAuthService authService = new AuthService();
        readonly Atm atm = new();
        public void EndProcess()
        {
            Console.WriteLine("Do you want to");
            Console.WriteLine($"Collect your Card. Thank you for using {atm.Name}");
        }

        public void ContinueProcess()
        {
            authService.Login();
        }

        public void Answer()
        {
            IMessage message = new Message();
          question: message.Alert("Would like to End or Continue transactions. Enter [YES/NO]");
            string answer = Console.ReadLine() ?? string.Empty;
            if (answer.Trim().ToUpper() == "YES")
            {
                EndProcess();
            }else if(answer.Trim().ToUpper() == "NO")
            {
                ContinueProcess();
            }
            else
            {
                message.Error("Please enter yes or no for us to be sure you wanted to close the application.");
                goto question;
            }
        }
    }
}
