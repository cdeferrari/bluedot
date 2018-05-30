using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.FireExtinguisherControls
{
    public interface IFireExtinguisherControlService
    {
        FireExtinguisherControl CreateFireExtinguisherControl(ControlRequest FireExtinguisherControl);
        FireExtinguisherControl GetById(int FireExtinguisherControlId);
        IList<FireExtinguisherControl> GetAll();        
        FireExtinguisherControl UpdateFireExtinguisherControl(FireExtinguisherControl originalFireExtinguisherControl, ControlRequest FireExtinguisherControl);
        void DeleteFireExtinguisherControl(int FireExtinguisherControlId);        
    }
}
