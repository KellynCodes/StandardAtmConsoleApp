using ATM.BLL.Implementation;
using ATM.BLL.Interfaces;

namespace ATM.UI
{
    public class Program
    {
        private static readonly IAtmService atmService = new AtmService(new AdminService());
        private static readonly IAuthService authService = new AuthService();
        private static readonly IAdminService _adminService = new AdminService();


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
            Console.WriteLine("1.\t Login As Admin. \n2.\t Login as User\n3\t Logout");
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
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input was empty or not valid. Please try again");
                Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter [NO/YES] for us to be certain you want to logout");
                Console.ResetColor();
                goto Mbido;
            }
        }
    }
}

