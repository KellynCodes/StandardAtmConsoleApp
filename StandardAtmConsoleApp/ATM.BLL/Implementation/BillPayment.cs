﻿using StandardAtmConsoleApp.ATM.BLL.Interfaces;

namespace StandardAtmConsoleApp.ATM.BLL.Implementation
{
    public class BillPayment : IBillPayment
    {
        public void Airtime()
        {
            Console.WriteLine("How much would like to buy.");
        }

        public void CableTransmission()
        {
            Console.WriteLine("How much would like to Pay.");
        }

        public void Nepa()
        {
            Console.WriteLine("How much would like to subscribe.");
        }
    }
}
