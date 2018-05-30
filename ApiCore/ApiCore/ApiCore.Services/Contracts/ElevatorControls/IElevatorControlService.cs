using ApiCore.DomainModel;
using ApiCore.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Services.Contracts.ElevatorControls
{
    public interface IElevatorControlService
    {
        ElevatorControl CreateElevatorControl(ControlRequest ElevatorControl);
        ElevatorControl GetById(int ElevatorControlId);
        IList<ElevatorControl> GetAll();        
        ElevatorControl UpdateElevatorControl(ElevatorControl originalElevatorControl, ControlRequest ElevatorControl);
        void DeleteElevatorControl(int ElevatorControlId);        
    }
}
