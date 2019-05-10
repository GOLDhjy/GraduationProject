using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class PlayerLockHit : LockAIState
    {
        public PlayerLockHit()
        {
            type = AIStateEnum.Hit;
        }

        public override void EnterState()
        {
            AIPlayerController.PlayerEntity.animState.Value = AnimStateEnum.Hit;
            AIPlayerController.PlayerEntity.animator.Value.SetTrigger("Hit");
        }

        public override void ExitState()
        {

        }

        public override void OnState()
        {

        }
    }
}
