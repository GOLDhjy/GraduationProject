using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class CameraInvalidModeState : ICameraViewModeState
    {
        public CameraInvalidModeState()
        {
            type = ViewModeEnum.Invilid;
        }

        public override void EnterState()
        {

        }

        public override void ExitState()
        {

        }

        public override void OnState()
        {

        }
    }
}
