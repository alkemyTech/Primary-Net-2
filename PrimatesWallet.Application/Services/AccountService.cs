using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Enums;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using System.Net;
using System.Transactions;
using System.Security.Principal;


namespace PrimatesWallet.Application.Services
{
    public class AccountService : IAccountService
    {
        public readonly IUnitOfWork unitOfWork;


        public AccountService(IUnitOfWork unitOfWork )
        {
            this.unitOfWork = unitOfWork;
   
        }

        public async Task<bool> DepositToAccount(int id, TopUpDTO topUpDTO)
        {
            var account = await unitOfWork.Accounts.Get_Transaccion(id);
            account.Money += topUpDTO.Money;
            var transactions = new Core.Models.Transaction
            {
                Amount = topUpDTO.Money,
                Concept = topUpDTO.Concept,
                Date = DateTime.Now,
                Type = topUpDTO.Type,
                Account_Id = account.Id,
                To_Account_Id = account.Id,

            };
            account.Transactions.Add(transactions);
            var response = unitOfWork.Save();
            if (response > 0)
                return true;
            else
                return false;

        }

        public async Task<IEnumerable<Account>> GetAccountsList()
        {
            try
            {
                return await unitOfWork.Accounts.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Account> GetAccountById(int id)
        {
            try
            {
                var account = await unitOfWork.Accounts.GetById(id);

                return account;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<bool> ValidateAccount(int userId, int accountId)
        {
            var account = await unitOfWork.Accounts.GetById(accountId);
            if( account == null) throw new Exception("The account does not exist");
            if(account.UserId == userId) return true;
            return false;
        }


        public async Task<TransferDetailDTO > Transfer(decimal amount, int remitentId, string recieverEmail, string concept = "Some")
        {
            try
            {
                var remitent = await unitOfWork.Accounts.GetById(remitentId);
                if (remitent == null) { throw new Exception("Cant find remitent account"); }

                var reciever = await unitOfWork.UserRepository.GetAccountByUserEmail(recieverEmail);
                if (reciever == null) { throw new Exception("The email provided is invalid"); }

                
                if (remitent.Money < amount) { throw new Exception("Insufficient balance to do this transaction"); }

                remitent.Money -= amount;

                reciever.Account.Money += amount;

                unitOfWork.Accounts.Update(remitent);
                unitOfWork.Accounts.Update(reciever.Account);

                var transaction = new Core.Models.Transaction() { Amount = amount, Concept = concept, Date = DateTime.Now, Type = TransactionType.payment, Account_Id = remitentId, To_Account_Id = reciever.Account.Id };

                var transferDetail = new TransferDetailDTO()
                {
                    Amount = amount,
                    Concept = concept,
                    RecieverEmail = recieverEmail,
                    RecieverFullname = $"{reciever.First_Name} {reciever.Last_Name}"
                };

                await unitOfWork.Transactions.Add(transaction);
                var response = unitOfWork.Save();

                if (response > 0)
                {
                    return transferDetail;

                }
                throw new Exception("An error ocurred");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<Account> UpdateAccountAdmin(int accountId, AccountUpdateDTO accountUpdateDTO)
        {
            if(accountId == null) throw new AppException("No id recieved", HttpStatusCode.BadRequest);
            var account = await unitOfWork.Accounts.GetById(accountId);
            
            if(account == null) throw new AppException($"No account with id {accountId}", HttpStatusCode.NotFound);

            var user = await unitOfWork.UserRepository.GetById(account.UserId);

            var isAdmin = await unitOfWork.UserRepository.IsAdmin(user);

            if (!isAdmin) throw new AppException("Invalid credentials", HttpStatusCode.Forbidden);

            account.Money = accountUpdateDTO.Money;
            account.IsBlocked = accountUpdateDTO.IsBlocked;

            unitOfWork.Accounts.Update(account);
            unitOfWork.Save();

            
            return account;


        }

        /// <summary>
        ///     This accountService method creates an account for a user if the user does not have one.
        /// </summary>
        /// <param name="userId">user id extraxted from a token.</param>
        /// <returns>if the account was created successfully, the method returns true</returns>
        /// <exception cref="AppException">If the user have an account, the method throw an error with status 400</exception>
        /// <exception cref="Exception">If there is an internal server error, the method catches it and throws an exception.</exception>
        public async Task<bool> Create(int userId)
        {
            var existingAccount = await unitOfWork.Accounts.CheckAccountByUserId(userId);

            if (existingAccount == true) throw new AppException("There is an account for this user.", HttpStatusCode.BadRequest);

            var newAccount = new Account() { CreationDate = DateTime.Now, Money = 0, IsBlocked = false, UserId = userId };
            await unitOfWork.Accounts.Add(newAccount);
            var response = unitOfWork.Save();

            if (response > 0) return true;
            return false;
        }


    }
}
