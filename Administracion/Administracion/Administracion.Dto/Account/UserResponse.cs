﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Dto.Account
{
    public class UserResponse
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string DNI { get; set; }
        public virtual string CUIT { get; set; }
        public virtual string ProfilePic { get; set; }
    }
}
