
namespace ATM.BLL.Interfaces
{
    public interface IAuthService
    {
        void Login();

        void ResetPin(string cardNumber, string accNo);

        void LogOut();

    }
}
