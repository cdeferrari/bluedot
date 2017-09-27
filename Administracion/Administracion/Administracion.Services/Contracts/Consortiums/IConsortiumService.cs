﻿using Administracion.DomainModel;
using Administracion.Dto.Consortium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.Consortiums
{
    public interface IConsortiumService
    {
        Consortium GetConsortium(int consortiumId);
        void CreateConsortium(ConsortiumRequest consortium);
        void UpdateConsortium(Consortium consortium);
        void DeleteConsortium(int consortiumId);
    }
}
