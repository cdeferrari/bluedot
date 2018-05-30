using Administracion.DomainModel;
using Administracion.Dto;
using Administracion.Dto.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.FireExtinguisherControlService
{
    public interface IFireExtinguisherControlService
    {
        IList<FireExtinguisherControl> GetAll();
        FireExtinguisherControl GetFireExtinguisherControl(int FireExtinguisherControlId);
        bool CreateFireExtinguisherControl(ControlRequest FireExtinguisherControl);
        bool UpdateFireExtinguisherControl(ControlRequest FireExtinguisherControl);
        bool DeleteFireExtinguisherControl(int FireExtinguisherControlId);
    }
}
