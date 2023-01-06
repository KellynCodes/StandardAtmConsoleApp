using ATM.DATA.Enums;

namespace ATM.DATA.Domain
{
    internal class Admin : User
    {

        public Admin(long id, string password) : base(id, password)
        {
        }

        public override Role Role { get; } = Role.Admin;

    }
}