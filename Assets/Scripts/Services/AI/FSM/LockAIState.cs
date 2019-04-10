using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class LockAIState
    {
        public AIStateEnum type = AIStateEnum.INVALID;
        protected LockAIPlayer AIPlayerController;
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
        public void SetController(LockAIPlayer aIPlayer)
        {
            AIPlayerController = aIPlayer;
        }
    }
}
