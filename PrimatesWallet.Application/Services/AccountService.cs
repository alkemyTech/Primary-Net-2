using Microsoft.AspNetCore.Http;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Enums;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using System.Net;

namespace PrimatesWallet.Application.Services
{
    public class AccountService : IAccountService
    {
        public readonly IUnitOfWork unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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

                var transaction = new Transaction() { Amount = amount, Concept = concept, Date = DateTime.Now, Type = TransactionType.payment, Account_Id = remitentId, To_Account_Id = reciever.Account.Id };

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


    }
}
