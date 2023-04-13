using PrimatesWallet.Application.DTOS;
using UserModel =  PrimatesWallet.Core.Models.User;
using TransactionModel = PrimatesWallet.Core.Models.Transaction;
using FixedTermModel = PrimatesWallet.Core.Models.FixedTermDeposit;

namespace PrimatesWallet.Application.Mapping.Account
{
    public class AccountDTOBuilder
    {
        private readonly AccountResponseDTO _accountDTO;

        public AccountDTOBuilder()
        {
            _accountDTO = new AccountResponseDTO();
        }
        public AccountDTOBuilder WithId(int id)
        {
            _accountDTO.Id = id;
            return this;
        }

        public AccountDTOBuilder WithCreationDate(DateTime creationDate)
        {
            _accountDTO.CreationDate = creationDate;
            return this;
        }

        public AccountDTOBuilder WithMoney(decimal money)
        {
            _accountDTO.Money = money;
            return this;
        }

        public AccountDTOBuilder WithIsBlocked(bool isBlocked)
        {
            _accountDTO.IsBlocked = isBlocked;
            return this;
        }

        public AccountDTOBuilder WithUserId(int userId)
        {
            _accountDTO.UserId = userId;
            return this;
        }


        public AccountDTOBuilder WithName(string name)
        {
            _accountDTO.Name = name;
            return this;
        }
        public AccountDTOBuilder WithLastname(string lastname)
        {
            _accountDTO.LastName = lastname;
            return this;
        }




        public AccountResponseDTO Build()
        {
            return _accountDTO;
        }



    }
}
