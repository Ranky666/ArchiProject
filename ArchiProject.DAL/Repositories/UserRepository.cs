using ArchiProject.Common;
using ArchiProject.DAL.EF;
using ArchiProject.DAL.Entities;
using ArchiProject.DAL.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArchiProject.DAL.Repositories
{
    public  class UserRepository : IUserRepository
    {

        private readonly UserContext db;

        private readonly IMapper _mapper;


        public UserRepository(UserContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        public UserDTO FindUser(UserDTO dto)
        {
            return _mapper.Map<UserDTO>(db.Users.FirstOrDefault(u => u.Email == dto.Email && u.Password == dto.Password));
        }

        public UserDTO FindUserByEmail(UserDTO dto)
        {
            return _mapper.Map<UserDTO>(db.Users.FirstOrDefault(u => u.Email == dto.Email));
        }

        public void AddUser(UserDTO dto)
        {
            db.Users.Add(_mapper.Map<User>(dto));
            db.SaveChanges();
        }

    }
}
