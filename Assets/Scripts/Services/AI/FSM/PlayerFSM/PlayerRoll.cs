using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace MyService
{
    public class PlayerRoll : AIState
    {
        public PlayerRoll()
        {
            type = AIStateEnum.Roll;
        }

        public override void EnterState()
        {
            AIPlayerController.PlayerEntity.animState.Value = AnimStateEnum.Roll;
            AIPlayerController.PlayerEntity.animator.Value.SetTrigger("Dodge");
        }

        public override void ExitState()
        {
             
        }

        public override void OnState()
        {

            //if(!AIPlayerController.PlayerEntity.animator.Value.GetCurrentAnimatorStateInfo(0).IsTag("Dodge"))
            //{
            //    Debug.Log(AIPlayerController.PlayerEntity.animator.Value.GetCurrentAnimatorStateInfo(0).ToString());
            //    AIPlayerController.ChangeState(AIStateEnum.Movement);
            //}
        }
    }
}
