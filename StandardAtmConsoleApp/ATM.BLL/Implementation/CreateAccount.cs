﻿using ATM.BLL.Interfaces;
using ATM.DATA.DataBase;
using ATM.DATA.Domain;
using ATM.DATA.Enums;
using System;

namespace ATM.BLL.Implementation
{
    public class CreateAccount : ICreateAccount
    {
        private readonly IMessage message = new Message();
        private AccountType _accountType;

        public string AccountNumber()
        {
            Random random = new();
            int randomNumber = random.Next(0, 100000000);
            const int SubNumber = 147;
            string IdentityNumber = SubNumber.ToString();
            string numberToString = randomNumber.ToString();
            string accountNumber = $"{IdentityNumber}{ numberToString}";
            return accountNumber;
        }

        public AccountType GetAccountType()
        {
        ChooseAccountType: Console.WriteLine("Choose you account type.\n1.\t Current\n2.\t Savings");
            if (int.TryParse(Console.ReadLine(), out int accType))
            {
              
            switch (accType)
            {
                case 1:
                    _accountType = AccountType.Current;
                    break;
                case 2:
                    _accountType = AccountType.Savings;
                    break;
                default:
                    Console.WriteLine("Entered value was not in the options");
                    goto ChooseAccountType;
            }
            return _accountType;
            }
            else
            {
                message.Error("Please put a valid input");
                goto ChooseAccountType;
            }
        }

        public string GetEmail()
        {
        Start: Console.WriteLine("Enter your email address");
            string email = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(email))
            {
                message.Error("Empty input. Please try again.");
                goto Start;
            }
            if (!email.Contains('@'))
            {
                message.Error("Email must contain '@'");
                goto Start;
            }
            if (email.Contains(' '))
            {
                message.Error("Email should not contain empty space");
                goto Start;
            }
            return email;
        }

        public string GetFullName()
        {
        EnterFullName: Console.WriteLine("Enter your full name");
            string FullName = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(FullName))
            {
                message.Error("Empty input. Please try again.");
                goto EnterFullName;
            }
            return FullName;
        }

        public string UserName()
        {
        EnterUserName: Console.WriteLine("Enter your username");
            string userName = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(userName))
            {
                message.Error("Empty input. Please try again.");
                goto EnterUserName;
            }
            return userName;
        }

        public string GetPassword()
        {
        EnterPassword: Console.WriteLine("Enter password");
            string userPassword = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(userPassword))
            {
                message.Error("Empty input. Please try again.");
                goto EnterPassword;
            }
            return userPassword;
        }

        public string GetPin()
        {
        EnterYourPin: Console.WriteLine("Enter your preffered pin.\n Note: this will serve as the secret number to access your account.");
            string Pin = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(Pin))
            {
                message.Error("Empty input. Please try again.");
                goto EnterYourPin;
            }
            return Pin;
        }
    }
}
