
using ATM.DATA.DataBase;
using ATM.UI;

namespace ATM.BLL.Interfaces
{
    public interface IAuthService
    {
        void Login();

        void ResetPin(string cardNumber, string accNo);

        void LogOut();


        public static void ViewListOfUsers()
        {
            foreach (var users in AtmDB.Account)
            {
                Console.WriteLine($"{users.UserId} {users.UserName}");
            }
            Program.Logout();
        }

    }
}
