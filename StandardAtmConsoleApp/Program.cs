using ATM.BLL.Implementation;
using ATM.BLL.Interfaces;

namespace ATM.UI
{
    public class Program
    {
        private static readonly IAtmService atmService = new AtmService(new AdminService());
        private static readonly IAuthService authService = new AuthService();
        private static readonly IAdminService _adminService = new AdminService();
        private static readonly IContinueOrEndProcess continueOrEndProcess = new ContinueOrEndProcess();


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
                    /* case (int)Choice.ChoiceOne:*/
                    case 1:
                        _adminService.LoginAdmin();
                        break;
                    /*  case (int)Choice.ChoiceTwo:*/
                    case 2:
                        Console.WriteLine("Please provide your details");
                        authService.Login();
                        break;
                    case 3:
                        atmService.CreateAccount();
                        break;
                    case 4:
                        continueOrEndProcess.Answer();
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
    }
}

