using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Mapping.User
{
    /// <summary>
    /// Builder class to construct instances of UserResponseDTO.
    /// </summary>
    public class UserResponseDtoBuilder
    {
        private readonly UserResponseDto _userDTO;

        public UserResponseDtoBuilder()
        {
            _userDTO = new UserResponseDto();
        }

        public UserResponseDtoBuilder WithUserId(int userId)
        {
            this._userDTO.UserId = userId;
            return this;
        }

        public UserResponseDtoBuilder WithFirstName(string firstName)
        {
            this._userDTO.First_Name = firstName;
            return this;
        }

        public UserResponseDtoBuilder WithLastName(string lastName)
        {
            this._userDTO.Last_Name = lastName;
            return this;
        }

        public UserResponseDtoBuilder WithEmail(string email)
        {
            this._userDTO.Email = email;
            return this;
        }

        public UserResponseDtoBuilder WithPoints(int points)
        {
            this._userDTO.Points = points;
            return this;
        }

        public UserResponseDtoBuilder WithRolId(int rolId)
        {
            this._userDTO.Rol = rolId == 1 ?  "Admin" : "Regular";
            return this;
        }

        public UserResponseDto Build()
        {
            return _userDTO;
        }
    }
}
