﻿using ArchiProject.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchiProject.DAL.Interfaces
{
   public interface IUserRepository
    {

        //List<NoteDTO> GetNotes();

        UserDTO FindUser(UserDTO dto);

        UserDTO FindUserByEmail(UserDTO dto);

        void AddUser(UserDTO dto);


    }
}
