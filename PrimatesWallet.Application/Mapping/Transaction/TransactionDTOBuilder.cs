using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Mapping.Transaction
{
    public class TransactionDTOBuilder
    {
        private readonly TransactionDTO _transactionDTO;

        /// <summary>
        /// Builder class para construir instancias de TransactionDTO.
        /// </summary>
        public TransactionDTOBuilder()
        {
            _transactionDTO = new TransactionDTO();
        }

        public TransactionDTOBuilder WithId(int id)
        {
            _transactionDTO.Id = id;
            return this;
        }

        public TransactionDTOBuilder WithAmount(decimal amount)
        {
            _transactionDTO.Amount = amount;
            return this;
        }

        public TransactionDTOBuilder WithConcept(string concept)
        {
            _transactionDTO.Concept = concept;
            return this;
        }

        public TransactionDTOBuilder WithDate(DateTime date)
        {
            _transactionDTO.Date = date;
            return this;
        }

        public TransactionDTOBuilder WithType(TransactionType type)
        {
            _transactionDTO.Type = type == TransactionType.topup ? "topup" : "payment";
            return this;
        }

        public TransactionDTOBuilder WithAccountId(int accountId)
        {
            _transactionDTO.Account_Id = accountId;
            return this;
        }

        public TransactionDTOBuilder WithToAccountId(int toAccountId)
        {
            _transactionDTO.To_Account_Id = toAccountId;
            return this;
        }

        public TransactionDTO Build()
        {
            return _transactionDTO;
        }
    }

}
