using ATM.BLL.Implementation;
using ATM.BLL.Interfaces;
using StandardAtmConsoleApp.ATM.DATA.Enums;

namespace ATM.UI
{
    public class Program
    {
        private static readonly IAtmService atmService = new AtmService(new AdminService());
        private static readonly IAuthService authService = new AuthService();
        private static readonly IAdminService _adminService = new AdminService();
        private static readonly IMessage message = new Message();


        public static void Main()
        {
            Console.Title = "Gt Bank Atm Machine";
            atmService.Start();
            GetUserChoice();
        }

        public static void GetUserChoice()
        {
        Option:
            Console.WriteLine("Choose from the Option");
            Console.WriteLine("1.\t Login As Admin. \n2.\t Login as User\n3.\t Create new Account\n4\t Quit App");
            string Answer = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(Answer, out int Choice))
            {
                switch (Choice)
                {
                    case (int)SwitchCase.One:
                        _adminService.LoginAdmin();
                        break;
                    case (int)SwitchCase.Two:
                        Console.WriteLine("Please provide your details");
                        authService.Login();
                        break;
                    case (int)SwitchCase.Three:
                        atmService.CreateAccount();
                        break;
                    case (int)SwitchCase.Four:
                        Logout();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Input is not in the option");
                        Main();
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Please Enter a valid input");
                goto Option;
            }
        }
        public static void Logout()
        {
        Mbido: Console.WriteLine("Are you sure you want to Logout [NO/YES]");
            string Answer = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(Answer))
            {
                message.Error("Input was empty or not valid. Please try again");
                goto Mbido;
            }
            if (Answer.Trim().ToUpper() == "YES")
            {
                authService.LogOut();
            }
            else if (Answer.Trim().ToUpper() == "NO")
            {
                Console.Clear();
                Main();
            }
            else
            {
                message.Error("Please enter [NO/YES] for us to be certain you want to logout");
                goto Mbido;
            }
        }
    }
}

