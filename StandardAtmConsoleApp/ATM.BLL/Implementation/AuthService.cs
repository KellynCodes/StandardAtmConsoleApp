using ATM.BLL.Interfaces;
using ATM.DATA.DataBase;
using ATM.DATA.Domain;
using ATM.UI;

namespace ATM.BLL.Implementation
{
    public class AuthService : IAuthService
    {
        static readonly IAtmService atmService = new AtmService();
        public static Account SessionUser { get; set; } = new Account(); 
        private readonly static Message message = new();

        /// <summary>
        /// Login Validation.
        /// </summary>
        public void Login()
        {
        Start: Console.WriteLine("Enter your User ID");
            string userId = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(userId))
            {
                message.Error("Input was empty of not valid. Please try agian");
                goto Start;
            }
        EnterUserID: Console.WriteLine("Enter your account number");
            string AccountNo = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(AccountNo))
            {
                message.Error("Input was empty of not valid. Please try agian");
                goto EnterUserID;
            }

            if (int.TryParse(userId, out int UserID))
            {
                var UserDetails = AtmDB.Account.FirstOrDefault(user => user.UserId == UserID && user.AccountNo == AccountNo);
                SessionUser = UserDetails;
                if (UserDetails != null)    
                {
                   message.Alert($"Welcome back {UserDetails.UserName}");
                AtmServices: Console.WriteLine("What would like to Do");
                    Console.WriteLine("1.\t Check Balance\n2.\t Withdrawal\n3.\t Transfer\n4.\t Deposit");
                    string userInput = Console.ReadLine() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(userInput))
                    {
                        Console.Clear();
                        Console.WriteLine("Input was empty or not valid");
                        goto AtmServices;
                    }
                    if (int.TryParse(userInput, out int answer))
                    {
                        switch (answer)
                        {
                            case 1:
                                atmService.CheckBalance();
                                break;
                            case 2: atmService.Withdraw();
                                break;
                            case 3: atmService.Transfer();
                                break;
                            case 4: atmService.Deposit();
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
            else
            {
                message.Error("Input was not valid. Please Try again");
                goto Start;
            }

        }

        /// <summary>
        /// When user wants to recet Pin
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="accNo"></param>

        public void ResetPin(string cardNumber, string accNo)
        {
            Console.WriteLine($"{cardNumber} {accNo}");
        }

        /// <summary>
        /// Logout Users
        /// </summary>
        public void LogOut()
        {
            const int ThreeSeconds = 3000;
            message.Error("Logging out....\nPLease Wait.");
            Thread.Sleep(ThreeSeconds);
        }
    }
}