﻿using ATM.DATA.Domain;

namespace StandardAtmConsoleApp.Helpers
{
    public class GetAtmData
    {
        public static Atm _info;

        public static Atm GetData()
        {
            foreach (var info in Atm.Data)
            {
                _info = info;
            }
            return _info;
        }
    }
}
