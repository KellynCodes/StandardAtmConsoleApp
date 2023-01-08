using ATM.BLL.Interfaces;
using ATM.DATA.DataBase;
using ATM.DATA.Domain;
using ATM.UI;
using StandardAtmConsoleApp.Helpers;

namespace ATM.BLL.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly IMessage message = new Message();
        private readonly IContinueOrEndProcess continueOrEndProcess = new ContinueOrEndProcess();

        private User SessionAdmin { get; set; }
        public decimal CashLimit { get; set; }

        public void LoginAdmin()
        {
        Start: Console.WriteLine("Enter your Email");
            string UserEmail = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(UserEmail))
            {
                message.Error("Input was empty of not valid. Please try agian");
                goto Start;
            }
        EnterUserID: Console.WriteLine("Enter your Password");
            string UserPassword = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(UserPassword))
            {
                message.Error("Input was empty of not valid. Please try agian");
                goto EnterUserID;
            }

            var UserDetails = AtmDB.Users.FirstOrDefault(user => user.Email == UserEmail && user.Password == UserPassword);
            SessionAdmin = UserDetails;
            if (UserDetails != null)
            {
                message.Alert($"Welcome back {UserDetails.Email}");
            AtmServices: Console.WriteLine("What would like to Do");
                Console.WriteLine("1.\t Reload Cash\n2.\t Set Cash Limit\n3.\t View list of Users");
                string userInput = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    message.Error("Input was empty or not valid");
                    goto AtmServices;
                }
                if (int.TryParse(userInput, out int answer))
                {
                    switch (answer)
                    {
                        case 1:
                            ReloadCash();
                            break;
                        case 2:
                            SetCashLimit();
                            break;
                        case 3:
                            IAuthService.ViewListOfUsers();
                            break;
                        default:
                            message.Error("Entered value was not in the list");
                            goto AtmServices;
                    }
                }
            }
            else
            {
                message.Error("Opps!. Sorry this users does not exist. Please try again with a valid user information");
                goto Start;
            }
        }


        public void SetCashLimit()
        {
            EnterCashLimit: message.AlertInfo($"Hi {SessionAdmin.FullName} How much do you want to set as cash limit?.");
            if (decimal.TryParse(Console.ReadLine(), out decimal cashLimit))
            {
                CashLimit = cashLimit;
            }
            else
            {
                message.Error("Wrong input. Please enter only numbers.");
                goto EnterCashLimit;
            }
            continueOrEndProcess.Answer();
        }

        public void ReloadCash()
        {
        EnterAmount: Console.WriteLine("Enter amount to reload");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                const int ThreeSeconds = 3000;
                var atm = GetAtmData.GetData();
                message.Success($"Reloading {amount}...");
                Thread.Sleep(ThreeSeconds);
                atm.AvailableCash += amount;
                message.Alert($"New Balance :: {atm.AvailableCash}");
                Program.GetUserChoice();
            }
            else
            {
                message.Error("Input was not valid. Please Try again.");
                goto EnterAmount;
            }
        }

    }
}