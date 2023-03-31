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
    public class UserResponseDTOBuilder
    {
        private readonly UserResponseDTO _userDTO;

        public UserResponseDTOBuilder()
        {
            _userDTO = new UserResponseDTO();
        }

        public UserResponseDTOBuilder WithUserId(int userId)
        {
            this._userDTO.UserId = userId;
            return this;
        }

        public UserResponseDTOBuilder WithFirstName(string firstName)
        {
            this._userDTO.First_Name = firstName;
            return this;
        }

        public UserResponseDTOBuilder WithLastName(string lastName)
        {
            this._userDTO.Last_Name = lastName;
            return this;
        }

        public UserResponseDTOBuilder WithEmail(string email)
        {
            this._userDTO.Email = email;
            return this;
        }

        public UserResponseDTOBuilder WithPoints(int points)
        {
            this._userDTO.Points = points;
            return this;
        }

        public UserResponseDTOBuilder WithRolId(int rolId)
        {
            this._userDTO.Rol_Id = rolId == 1 ? "Regular" : "Admin";
            return this;
        }

        public UserResponseDTO Build()
        {
            return _userDTO;
        }
    }
}
