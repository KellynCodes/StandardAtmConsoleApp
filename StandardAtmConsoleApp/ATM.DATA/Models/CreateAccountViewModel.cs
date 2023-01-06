using ATM.DATA.Enums;

namespace ATM.DATA.Models
{
    internal class CreateAccountViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountNo { get; set; }
        public AccountType AccountType { get; set; }
        public string Pin { get; set; }
        public string ConfirmPin { get; set; }
       
    }
}