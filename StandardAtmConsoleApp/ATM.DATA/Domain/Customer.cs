using ATM.DATA.Enums;

namespace ATM.DATA.Domain
{
    public class Customer : User
    {
        public Customer(long id, string password) : base(id, password) { }
        public override Role Role { get; } = Role.Customer;
        public Account Account { get; set; }
    }
}