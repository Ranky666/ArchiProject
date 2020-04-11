
using ArchiProject.BL.Interfaces;
using ArchiProject.Common;
using ArchiProject.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchiProject.BL.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserRepository _userRepository;


        public AuthorizationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDTO FindUser(UserDTO dto)
        {
            return _userRepository.FindUser(dto);

        }

        public UserDTO FindUserByEmail(UserDTO dto)
        {
            return _userRepository.FindUserByEmail(dto);

        }

        public void AddUser(UserDTO dto)
        {
            _userRepository.AddUser(dto);

        }
    }
}
