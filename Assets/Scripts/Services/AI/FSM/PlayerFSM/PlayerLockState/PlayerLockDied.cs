using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class PlayerLockDied : LockAIState
    {
        public PlayerLockDied()
        {
            type = AIStateEnum.Died;
        }

        public override void EnterState()
        {
            base.EnterState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public override void OnState()
        {
            base.OnState();
        }
    }
}
