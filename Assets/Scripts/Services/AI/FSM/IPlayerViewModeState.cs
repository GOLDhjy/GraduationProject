using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyService
{
    public enum ViewModeEnum
    {
        Free,
        Lock
    }
    public class IPlayerViewModeState
    {
        public ViewModeEnum type;
        public PlayerViewModeController PlayerViewModeController;
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
        public void SetController(PlayerViewModeController viewModeController)
        {
            PlayerViewModeController = viewModeController;
        }
    }
}
