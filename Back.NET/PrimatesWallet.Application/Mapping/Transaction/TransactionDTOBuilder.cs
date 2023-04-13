using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Mapping.Transaction
{
    public class TransactionDtoBuilder
    {
        private readonly TransactionDto _transactionDTO;

        /// <summary>
        /// Builder class para construir instancias de TransactionDTO.
        /// </summary>
        public TransactionDtoBuilder()
        {
            _transactionDTO = new TransactionDto();
        }

        public TransactionDtoBuilder WithId(int id)
        {
            _transactionDTO.Id = id;
            return this;
        }

        public TransactionDtoBuilder WithAmount(decimal amount)
        {
            _transactionDTO.Amount = amount;
            return this;
        }

        public TransactionDtoBuilder WithConcept(string concept)
        {
            _transactionDTO.Concept = concept;
            return this;
        }

        public TransactionDtoBuilder WithDate(DateTime date)
        {
            _transactionDTO.Date = date.ToString("yyyy-MM-dd"); ;
            return this;
        }

        public TransactionDtoBuilder WithType(TransactionType type)
        {
            _transactionDTO.Type = type.ToString();
            return this;
        }

        public TransactionDtoBuilder WithAccountId(int accountId)
        {
            _transactionDTO.Account_Id = accountId;
            return this;
        }

        public TransactionDtoBuilder WithToAccountId(int toAccountId)
        {
            _transactionDTO.To_Account_Id = toAccountId;
            return this;
        }

        public TransactionDto Build()
        {
            return _transactionDTO;
        }
    }

}
