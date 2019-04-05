using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class PlayerRandomIdle : AIState
    {
        public PlayerRandomIdle()
        {
            type = AIStateEnum.RandomIdle;
        }

        public override void EnterState()
        {
            AIPlayerController.PlayerEntity.animator.Value.SetInteger("RandomId", UnityEngine.Random.Range(1, 7));
            AIPlayerController.PlayerEntity.animator.Value.SetTrigger("RandomIdle");
        }

        public override void ExitState()
        {

        }

        public override void OnState()
        {

        }
    }
}
