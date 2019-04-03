using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class ICameraViewModeState
    {
        public ViewModeEnum type;
        public CameraViewModeController CameraViewModeController;
        protected bool quit = false;
        public virtual void EnterState()
        {
            quit = false;
        }
        public virtual void OnState()
        {

        }
        public virtual void ExitState()
        {
            quit = true;
        }
        public void SetController(CameraViewModeController viewModeController)
        {
            CameraViewModeController = viewModeController;
        }
    }
}
