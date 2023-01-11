
using ATM.DATA.DataBase;
using ATM.UI;

namespace ATM.BLL.Interfaces
{
    public interface IAuthService
    {
        void Login();

        Task ResetPin();

        void LogOut();


        public static void ViewListOfUsers()
        {
            foreach (var account  in AtmDB.Account)
            {
                Console.WriteLine($"{account.UserId} {account.UserName}");
            }
            Program.Logout();
        }

    }
}
