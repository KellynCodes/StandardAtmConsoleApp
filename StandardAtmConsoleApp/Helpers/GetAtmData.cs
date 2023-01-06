using ATM.DATA.Domain;

namespace StandardAtmConsoleApp.Helpers
{
    public class GetAtmData
    {
        public static Atm? _info;
        public static Atm GetData()
        {
         AtmData atmData = new();
            foreach (var info in atmData.Data)
            {
                _info = info;
            }
            return _info;
        }
    }
}
