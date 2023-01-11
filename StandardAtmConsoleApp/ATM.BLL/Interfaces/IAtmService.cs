using ATM.DATA.Domain;

namespace ATM.BLL.Interfaces
{
    public interface IAtmService
    {
        void Start();
        void CheckBalance();
        void Withdraw();
        void Transfer();
        void Deposit();
        void PayBill();
        void CreateAccount();
        void ReloadCash(decimal amount);

    }



}