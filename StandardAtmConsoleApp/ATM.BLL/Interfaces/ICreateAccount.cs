﻿using ATM.DATA.Enums;

namespace ATM.BLL.Interfaces
{
    public interface ICreateAccount
    {
        string GetFullName();
        string UserName();
        string GetEmail();
        string AccountNumber();
        AccountType GetAccountType();
        string GetPassword();
        string ConfirmPassword();
        string GetPin();
    }
}
