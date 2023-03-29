
using PrimatesWallet.Application.Exceptions;
﻿using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Interfaces;
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

        public async Task<bool> DepositToAccount(int id, TopUpDTO topUpDTO)
        {
            var account = await unitOfWork.Accounts.Get_Transaccion(id);
            account.Money += topUpDTO.Money;
            var transactions = new Transaction
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
    }
}
