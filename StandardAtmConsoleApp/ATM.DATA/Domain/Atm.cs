using ATM.DATA.Enums;

namespace ATM.DATA.Domain
{
    public class Atm
    {
        public string Name { get; set; }
        public decimal AvailableCash { get; set; }
        public Language CurrentLanguage { get; set; }

        public static IList<Atm> Data { get; set; } = new List<Atm>()
        {
            new Atm{Name = "KellynCodes Atm Machine", AvailableCash = 0.00m, CurrentLanguage = Language.English}
        };
    }

    public class AtmData
    {
/*
        public AtmData()
        {

        }*/
    /*    public AtmData(string name, decimal availableCash, Language currentLanguage)
        {
            atm.Name = name;
            atm.AvailableCash = availableCash;
            atm.CurrentLanguage = currentLanguage;
        }*/

      
    }

}