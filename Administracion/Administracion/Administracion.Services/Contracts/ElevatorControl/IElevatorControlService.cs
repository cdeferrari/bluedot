using Administracion.DomainModel;
using Administracion.Dto;
using Administracion.Dto.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracion.Services.Contracts.ElevatorControlService
{
    public interface IElevatorControlService
    {
        IList<ElevatorControl> GetAll();
        ElevatorControl GetElevatorControl(int ElevatorControlId);
        bool CreateElevatorControl(ControlRequest ElevatorControl);
        bool UpdateElevatorControl(ControlRequest ElevatorControl);
        bool DeleteElevatorControl(int ElevatorControlId);
    }
}
