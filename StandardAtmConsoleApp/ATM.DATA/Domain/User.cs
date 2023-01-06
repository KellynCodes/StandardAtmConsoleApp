using ATM.DATA.Enums;

namespace ATM.DATA.Domain
{
    public abstract class User
    {

        protected User(long id, string password)
        {
            Password = password;
            Id = id;
        }


        public long Id { get; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; }
        public string UserBank { get; set; }
        public string PhoneNumber { get; set; }
        public virtual Role Role { get; }

    }
}
