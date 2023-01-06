using ATM.DATA.Enums;

namespace ATM.DATA.Domain
{
    public class Atm
    {
        public string Name { get; set; }
        public decimal AvailableCash { get; set; }
        public Language CurrentLanguage { get; set; }
    }

    public class AtmData
    {
        readonly Atm atm = new();

        public AtmData()
        {

        }
        public AtmData(string name, decimal availableCash, Language currentLanguage)
        {
            atm.Name = name;
            atm.AvailableCash = availableCash;
            atm.CurrentLanguage = currentLanguage;
        }

        public IList<Atm> Data { get; set; } = new List<Atm>()
        {
            new Atm{Name = "KellynCodes Atm Machine", AvailableCash = 0.00m, CurrentLanguage = Language.English}
        };
    }

}