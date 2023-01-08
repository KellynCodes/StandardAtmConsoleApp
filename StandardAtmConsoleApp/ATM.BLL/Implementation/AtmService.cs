using ATM.BLL.Interfaces;
using ATM.DATA.DataBase;
using ATM.DATA.Domain;
using ATM.DATA.Enums;
using ATM.UI;
using StandardAtmConsoleApp.Helpers;

namespace ATM.BLL.Implementation
{
    public class AtmService : IAtmService
    {
        public AtmData atmData;
        private readonly IAdminService _adminService;
        private static AccountType _accountType;
        private readonly IAuthService authService = new AuthService();
        private readonly IContinueOrEndProcess continueOrEndProcess = new ContinueOrEndProcess();
        private readonly ICreateAccount createAccount = new CreateAccount();
        public static readonly IMessage message = new Message();

        public static int _days = 0;
        private const int Aday = 1;
        private const int OneWeek = 7;
        private static int _cashDenomination;
        private static decimal _amount;

        private static decimal EnteredAmount { get; set; }

        public AtmService(IAdminService adminService) => _adminService = adminService;
        public AtmService() { }
        public void Start()
        {
            GetAtmData.GetData().AvailableCash = 6_000.90m;
            Console.WriteLine($"{GetAtmData.GetData().Name} has booted!");
            Console.WriteLine("Insert Card!");
        }

        public void CheckBalance()
        {
        CheckBalance: Console.WriteLine("1.\t Current\n2.\t Savings");
            string userInput = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrEmpty(userInput))
            {
                if (int.TryParse(userInput, out int answer))
                {
                    switch (answer)
                    {
                        case 1:
                            _accountType = AccountType.Current;
                            break;
                        case 2:
                            _accountType = AccountType.Savings;
                            break;
                        default:
                            message.Error("Entered value was not in the list. Please try again");
                            goto CheckBalance;
                    }
                    var CheckAccountType = AtmDB.Account.Where(user => user.AccountNo == AuthService.SessionUser.AccountNo && user.AccountType == _accountType);
                    if (CheckAccountType.Any())
                    {

                        foreach (var user in CheckAccountType)
                        {
                            message.Success($"{user.UserName} your account balance is {user.Balance}");
                        }
                     
                        continueOrEndProcess.Answer();
                    }
                    else
                    {
                        message.Error("Account not found. Check your account information to be certain you entered the correct account type\nOR contact customer care on 09157060998");
                        authService.Login();
                    }
                }
                else
                {

                    message.Error("Invalid Input. Please try again");
                    goto CheckBalance;
                }
            }
            else
            {
                message.Error("Input was empty or not valid");
                goto CheckBalance;
            }
        }

        public void Withdraw()
        {
            if (_days > OneWeek)
            {
                message.Error("You have exceded your withdrawal limit. [20k: daily and 100k: weekly]");
                authService.Login();
            }
            message.Error("Note: Withdrawal Limit:\nDaily = 20k\nWeekly = 100k");
        CheckBalance: Console.WriteLine("1.\t Current\n2.\t Savings");
            string userInput = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(userInput))
            {
                message.Error("Input was empty or not valid");
                goto CheckBalance;
            }
            if (int.TryParse(userInput, out int answer))
            {
                switch (answer)
                {
                    case 1:
                        _accountType = AccountType.Current;
                        break;
                    case 2:
                        _accountType = AccountType.Savings;
                        break;
                    default:

                        message.Error("Entered value was not in the list. Please try again");
                        goto CheckBalance;
                }
                var UserAccount = AtmDB.Account.Where(user => user.AccountNo == AuthService.SessionUser.AccountNo && user.AccountType == _accountType);
            AmountToWidthDraw: if (UserAccount.Any())
                {
                    Console.WriteLine("How much do you want to withdraw");
                    Console.WriteLine("1. 500\t2. 1000\t3. 2000\n4. 5000\t5. 10000\t6. 20000\n7. Others");
                    if (int.TryParse(Console.ReadLine(), out int choice))
                        switch (choice)
                        {
                            case 1:
                                EnteredAmount = 500;

                                goto EnterDenomination;
                            case 2:
                                EnteredAmount = 1000;

                                goto EnterDenomination;
                            case 3:
                                EnteredAmount = 2000;

                                goto EnterDenomination;
                            case 4:
                                EnteredAmount = 5000;

                                goto EnterDenomination;
                            case 5:
                                EnteredAmount = 10000;
                                goto EnterDenomination;
                            case 6:
                                EnteredAmount = 20000;
                                goto EnterDenomination;
                            case 7:
                                goto Others;
                            default:
                                message.Error("Input was not in the list. Please try again.");
                                goto AmountToWidthDraw;
                        }
                    Others: Console.WriteLine("How much do you want to withdraw");
                    //create a funciton that will be called here in an if statment
                    if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                    {
                        message.Error("Input was not valid. Please enter only digits");
                        goto AmountToWidthDraw;
                    }
                    
                   if(amount < 0)
                    {
                        _amount = EnteredAmount;
                    }else if(amount > 0)
                    {
                        _amount = amount;
                    }
                    var atm = GetAtmData.GetData();
                        if (_amount > atm.AvailableCash)
                        {
                            message.Alert($"Sorry atm is out of cash. Available amount is {atm.AvailableCash}");
                            Program.GetUserChoice();
                        }
                        if (_amount > (int)WithdrawalLimit.Weekly)
                        {
                            message.Error("You cannot withdraw more than 100k in a week");
                            goto AmountToWidthDraw;
                        }
                        if (_amount == (int)WithdrawalLimit.Daily)
                        {
                            _days += Aday;
                        }
                        if (_amount == (int)WithdrawalLimit.Weekly)
                        {
                            _days = OneWeek;
                        }
                EnterDenomination: Console.WriteLine("Enter denomination to despense");
                    Console.WriteLine("1.\t 1000\t2.\t 500\n3.\t 200");
                        if (int.TryParse(Console.ReadLine(), out int CashDenomination))
                        {
                            switch (CashDenomination)
                            {
                                case 1:
                                    _cashDenomination = (int)Denominations.OneThousand;
                                    goto nextBlock;
                                case 2:
                                    _cashDenomination = (int)Denominations.FiveHundred;
                                    goto nextBlock;

                                case 3:
                                    _cashDenomination = (int)Denominations.TwoHundred;
                                    goto nextBlock;
                                default:
                                    message.Error("Input is not available in the options. Please try again.");
                                    goto EnterDenomination;
                            }
                        }
                        else
                        {
                            message.Error("Invalid input. Please try again.");
                            goto EnterDenomination;
                        }
                       nextBlock:
                    foreach (var user in UserAccount)
                        {
                            if (_amount >= user.Balance)
                            {
                                message.Error("Insufficient balance");
                                goto AmountToWidthDraw;
                            }
                        else
                        {
                                user.Balance -= _amount;
                            GetAtmData.GetData().AvailableCash -= _amount;
                            message.Success($"Transaction successfull!. {_amount} have been debited from your account.  Your new account balance is {user.Balance}");
                            message.AlertInfo($"Cash denominations: {_cashDenomination}");
                         }
                        }

                        continueOrEndProcess.Answer();
                   
                }
                else
                {
                    message.Error("Account not found. Check your account information to be certain you entered the correct account type\nOR contact customer care on 09157060998");
                    authService.Login();
                }
            }
            else
            {
                message.Error("Invalid Input. Please try again");
                goto CheckBalance;
            }
        }
        /// <summary>
        /// Method that handles transfer of money
        /// </summary>

        public void Transfer()
        {
        EnterAmount: Console.WriteLine("Enter amount");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (AuthService.SessionUser.Balance <= amount)
                {
                    message.Error("Insufficient balance");
                    goto EnterAmount;
                }
                bool isUserAccountTypeSavings = AuthService.SessionUser.AccountType == AccountType.Savings;
                bool isUserAccountTypeCurrent = AuthService.SessionUser.AccountType == AccountType.Current;
                const decimal maximumAmountInCurrentAccount = 500_000;
                const decimal maximumAmountInSavinsAccount = 100_000;
                if (isUserAccountTypeCurrent && amount > maximumAmountInCurrentAccount)
                {
                    message.Error("Amount should not be greater than 500000 in a current account");
                    goto EnterAmount;
                }
                if (isUserAccountTypeSavings && amount > maximumAmountInSavinsAccount)
                {
                    message.Error("Amount should not be greater than 500000 in a current account");
                    goto EnterAmount;
                }
            }
            else
            {
                message.Error("Invalid input. Please try again.");
                goto EnterAmount;
            }

        EnterAccountNumber: Console.WriteLine("Enter account number");
            string accountNumber = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                message.Error("Input was empty or not valid");
                goto EnterAccountNumber;
            }
            var Recepient = AtmDB.Account.FirstOrDefault(user => user.AccountNo == accountNumber);
            if (Recepient == null)
            {
                message.Error("This account number does not exist. Enter a valid information");
                goto EnterBank;
            }

        EnterBank: Console.WriteLine("Choose Bank");
            Console.WriteLine("1. Gt Bank\n2.\t Access Bank\n3.\t Union Bank\n4.\t Fidelity Bank\n5.\t Ecobank\n6.\t First Bank.");
            string UserBank = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(UserBank))
            {
                message.Error("Input was empty or not valid");
                goto EnterBank;
            }
            if (int.TryParse(UserBank, out int bank))
            {
                string userBank = DefaultSwitchCaseMethod.defaultSwitchCaseMethod(bank);

                var accountInformation = AtmDB.Account.FirstOrDefault(user => user.Id == AuthService.SessionUser.Id);
                if (accountInformation == null)
                {
                    message.Error("Error occured. Please make sure the informations you provided are valid.");
                    goto EnterAmount;
                }
                long userID = accountInformation.UserId;

                if (AuthService.SessionUser.Id == userID)
                {
                question: Console.WriteLine($"Do you want to transfer {amount} to {Recepient?.UserName}");
                    string answer = Console.ReadLine() ?? string.Empty;
                    if (answer.Trim().ToUpper() == "YES")
                    {

                        var newBalance = AuthService.SessionUser.Balance -= amount;
                        Recepient.Balance += amount;
                        message.Success($"Transaction successfull!.");
                    DoYouWantReceipt: Console.WriteLine("Do you need receipt[YES/NO]");
                        string userInput = Console.ReadLine() ?? string.Empty;

                        if (string.IsNullOrWhiteSpace(userInput))
                        {
                            message.Error("Input was empty. Please try again");
                            goto DoYouWantReceipt;
                        }
                        if (userInput.Trim().ToUpper() == "YES")
                        {
                            message.Success($"Transaction successfull!. {AuthService.SessionUser.UserName} {amount} has been debited from your account. You just transfered {amount} to {Recepient.UserName} on {DateTime.Now.ToLongDateString()}\n Your new balance is {newBalance}");
                            continueOrEndProcess.Answer();
                        }
                        else if (userInput.Trim().ToUpper() == "NO")
                        {
                            continueOrEndProcess.Answer();
                        }
                        else
                        {
                            message.Error("Please enter [NO/YES]");
                            goto DoYouWantReceipt;
                        }
                    }
                    else if (answer.Trim().ToUpper() == "NO")
                    {
                        message.Error("Transaction Canceled");
                        authService.LogOut();
                    }
                    else
                    {
                        message.Error("Please enter [NO/YES] for us to be sure you don't want to continue with the transaction.");
                        goto question;
                    }
                }
                else
                {
                    message.Error("Error occured. Please make sure the informations you provided are valid.");
                    goto EnterAmount;
                }


            }
            else
            {
                message.Error("Input was not valid. Please try again");
                goto EnterBank;
            }

        }

        /// <summary>
        /// Method that handles money deposit.
        /// </summary>
        /// <param name="accountNo">User account number</param>
        /// <param name="accountType">User account type. Current | Savings</param>
        /// <param name="amount">Amount to withdraw</param>

        public void Deposit()
        {
        EnterAccountumber: message.Alert("Enter your account number.");
            string accNo = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(accNo))
            {
                message.Error("Empty input. Please try again.");
                goto EnterAccountumber;
            }

        //Get account type
        ChooseAccountType: message.Alert("Choose your account type.");
            Console.WriteLine("1.\t Current\n2.\t Savings");

            if (!int.TryParse(Console.ReadLine(), out int answer))
            {
                message.Error("Invalid input. Please enter only numbers.");
                goto ChooseAccountType;
            }
            switch (answer)
            {
                case 1:
                    _accountType = AccountType.Current;
                    break;
                case 2:
                    _accountType = AccountType.Savings;
                    break;
                default:
                    message.Error("Entered value was not in the list. Please try again");
                    goto ChooseAccountType;
            }

        //Get amount to deposit;
        EnterAmount: message.Alert("Enter amount you want to deposit");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                message.Error("Invalid input. Please enter only numbers.");
                goto EnterAmount;
            }
            if (amount > (decimal)WithdrawalLimit.Daily)
            {
                message.Error("Amount should not be greater than 20k.");
                goto EnterAmount;
            }

            decimal newBalance = AuthService.SessionUser.Balance += amount;
            message.Success($"{AuthService.SessionUser.UserName} your just deposited {amount} to your account {accNo}. Your new balance is {newBalance}");
            continueOrEndProcess.Answer();
        }

        public void PayBill(Bill bill)
        {
            Console.WriteLine(nameof(bill));
        }

        public void CreateAccount()
        {
            long userID = AtmDB.Account.Last().Id + 1;
            string accountNumber = createAccount.AccountNumber();
            AccountType accountType = createAccount.GetAccountType();
            string email = createAccount.GetEmail();
            string fullName = createAccount.GetFullName();
            string userName = createAccount.UserName();
            string userPassword = createAccount.GetPassword();
            string pin = createAccount.GetPin();
            decimal Balance = 0.00m;
            string createdDate = DateTime.Now.ToLongDateString();

            var NewUser = new Customer(id: userID, password: userPassword) { FullName = fullName, Email = email, };
            var NewAccount = new Account {Id = userID, UserId = userID, UserName = userName, AccountNo = accountNumber, AccountType = accountType, Pin = pin, Balance = Balance, CreatedDate = createdDate };
            AtmDB.Users.Add(NewUser);
            AtmDB.Account.Add(NewAccount);
            message.Success($"{userName} your account have been created successfully!.");
            message.Alert($"Your ID {userID} and your account number is {accountNumber}");
            message.AlertInfo($"Make sure you copy your account [{accountNumber}] and also memorize your user ID");
            continueOrEndProcess.Answer();
        }

        public void ReloadCash(decimal amount)
        {
            _adminService.ReloadCash();
        }
    }
}
