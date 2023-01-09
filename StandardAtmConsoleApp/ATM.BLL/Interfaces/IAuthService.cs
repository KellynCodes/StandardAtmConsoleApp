
using ATM.DATA.DataBase;
using ATM.UI;

namespace ATM.BLL.Interfaces
{
    public interface IAuthService
    {
        void Login();

        void ResetPin();

        void LogOut();


        public static void ViewListOfUsers()
        {
            foreach (var users in AtmDB.Account)
            {
                Console.WriteLine($"{users.UserId} {users.UserName}");
            }
            foreach(var account in AtmDB.Users)
            {
                Console.WriteLine($"{account.FullName} {account.Password}");
            }
            Program.Logout();
        }

    }
}
