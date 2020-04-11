using ArchiProject.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchiProject.BL.Interfaces
{
    public  interface IAuthorizationService
    {

        UserDTO FindUser(UserDTO dto);

        UserDTO FindUserByEmail(UserDTO dto);

        void AddUser(UserDTO dto);



    }
}
