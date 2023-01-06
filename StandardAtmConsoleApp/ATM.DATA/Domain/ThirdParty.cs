using ATM.DATA.Enums;

namespace ATM.DATA.Domain
{
    internal class ThirdParty : User
    {
        public ThirdParty(long id, string password) : base(id, password) { }
        public override Role Role { get; } = Role.ThirdParty;
        public Account Account { get; set; }
    }
}