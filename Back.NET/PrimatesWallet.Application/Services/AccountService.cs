using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using System.Net;
using PrimatesWallet.Application.Mapping.Account;
using PrimatesWallet.Core.Enums;

namespace PrimatesWallet.Application.Services
{
    public class AccountService : IAccountService
    {
        public readonly IUnitOfWork unitOfWork;


        public AccountService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> DepositToAccount(int id, TopUpDto topUpDTO)
        {
            var account = await unitOfWork.Accounts.Get_Transaccion(id);
            account.Money += topUpDTO.Money;
            var transactions = new Transaction
            {
                Amount = topUpDTO.Money,
                Concept = topUpDTO.Concept,
                Date = DateTime.Now,
                Type = TransactionType.topup,
                Account_Id = account.Id,
                To_Account_Id = account.Id,

            };
            account.Transactions!.Add(transactions);

            var response = unitOfWork.Save();
            if (response > 0) return true;
            return false;
        }

        public async Task<IEnumerable<Account>> GetAccountsList()
        {
            return await unitOfWork.Accounts.GetAll();
        }

        public async Task<Account> GetAccountById(int id)
        {
            var account = await unitOfWork.Accounts.GetById(id);
            return account;
        }

        public async Task<bool> ValidateAccount(int userId, int accountId)
        {
            var account = await unitOfWork.Accounts.GetById(accountId);
            if (account == null) throw new AppException("The account does not exist", HttpStatusCode.BadRequest);
            if (account.UserId == userId) return true;
            return false;
        }

        public async Task<TransferDetailDto> Transfer(int userId, TransferDto transferDTO)
        {

            var remitent = await unitOfWork.Accounts.Get_Transaccion(userId);
            if (remitent == null) throw new AppException("Cant find remitent account", HttpStatusCode.NotFound);

            var reciever = await unitOfWork.Users.GetAccountByUserEmail(transferDTO.Email);
            if (reciever == null) throw new AppException("The email provided is invalid", HttpStatusCode.BadRequest);


            if (remitent.Money < transferDTO.Amount) throw new AppException("Insufficient balance to do this transaction", HttpStatusCode.BadRequest);

            remitent.Money -= transferDTO.Amount;

            reciever.Account.Money += transferDTO.Amount;

            unitOfWork.Accounts.Update(remitent);
            unitOfWork.Accounts.Update(reciever.Account);

            var transaction = new Transaction() { Amount = transferDTO.Amount, Concept = transferDTO.Concept, Date = DateTime.Now, Type = TransactionType.payment, Account_Id = remitent.Id, To_Account_Id = reciever.Account.Id };

            var transferDetail = new TransferDetailDto()
            {
                Amount = transferDTO.Amount,
                Concept = transferDTO.Concept,
                RecieverEmail = transferDTO.Email,
                RecieverFullname = $"{reciever.First_Name} {reciever.Last_Name}"
            };

            await unitOfWork.Transactions.Add(transaction);
            var response = unitOfWork.Save();

            if (response > 0) return transferDetail;

            throw new AppException("An error ocurred", HttpStatusCode.InternalServerError);
        }


        public async Task<Account> UpdateAccountAdmin(int accountId, AccountUpdateDto accountUpdateDTO)
        {
            var account = await unitOfWork.Accounts.GetById(accountId) ?? throw new AppException($"No account with id {accountId}", HttpStatusCode.NotFound);
            var user = await unitOfWork.Users.GetById(account.UserId);
            var isAdmin = unitOfWork.Users.IsAdmin(user);

            if (!isAdmin) throw new AppException("Invalid credentials", HttpStatusCode.Forbidden);

            account.Money = accountUpdateDTO.Money;
            account.IsBlocked = accountUpdateDTO.IsBlocked;

            unitOfWork.Accounts.Update(account);
            unitOfWork.Save();

            return account;
        }

        public async Task<bool> Create(int userId)
        {
            var existingAccount = await unitOfWork.Accounts.CheckAccountByUserId(userId);

            if (existingAccount) throw new AppException("There is an account for this user.", HttpStatusCode.BadRequest);

            var newAccount = new Account() { CreationDate = DateTime.Now, Money = 0, IsBlocked = false, UserId = userId };
            await unitOfWork.Accounts.Add(newAccount);
            var response = unitOfWork.Save();

            if (response > 0) return true;
            return false;
        }

        public async Task<IEnumerable<AccountResponseDTO>> GetAccounts(int page, int pageSize)
        {
            var accounts = await unitOfWork.Accounts.GetAll(page, pageSize)
                 ?? throw new AppException(ReplyMessage.MESSAGE_QUERY_EMPTY, HttpStatusCode.NotFound);


            var accountsDTO = accounts.Select(x =>
              new AccountDTOBuilder()
              .WithId(x.Id)
              .WithCreationDate(x.CreationDate)
              .WithMoney((decimal)x.Money!)
              .WithIsBlocked(x.IsBlocked)
              .WithUserId(x.UserId)
              .WithName(x.User.First_Name)
              .WithLastname(x.User.Last_Name)
              .Build()).ToList();

            return accountsDTO;
        }

        public async Task<int> TotalPageAccounts(int PageSize)
        {
            var totalAccounts = await unitOfWork.Accounts.GetCount();
            return (int)Math.Ceiling((double)totalAccounts / PageSize);
        }


        public async Task<string> ActivateAccount(int accountId)
        {
            var account = await unitOfWork.Accounts.GetById(accountId);
            account.IsBlocked = !account.IsBlocked;
            unitOfWork.Accounts.Update(account);
            unitOfWork.Save();
            var message = account.IsBlocked ? "Locked" : "Unlocked";
            return $"Account {message}" ;
        }

        public async Task<string> DeleteAccount(int accountId, int currentUser)
        {
            var user = await unitOfWork.Users.GetById(currentUser);
            var isAdmin = unitOfWork.Users.IsAdmin(user);
            if (!isAdmin) throw new AppException("Invalid Credentials", HttpStatusCode.Forbidden);

            var account = await unitOfWork.Accounts.GetById(accountId);
            if (account == null) throw new AppException($"Account {accountId} not found", HttpStatusCode.NotFound);

            unitOfWork.Accounts.Delete(account);
            unitOfWork.Save();

            return $"Account {accountId} deleted.";

        }

    }
}
