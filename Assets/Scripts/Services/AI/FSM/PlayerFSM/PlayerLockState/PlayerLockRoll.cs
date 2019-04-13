using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace MyService
{
    public class PlayerLockRoll : LockAIState
    {
        float CountTime;
        public PlayerLockRoll()
        {
            type = AIStateEnum.Roll;
        }

        public override void EnterState()
        {
            CountTime = 0;
            MyEventSystem.Instance.Subscribe(MovementArgs.Id, OnEventMovement);
            AIPlayerController.PlayerEntity.animState.Value = AnimStateEnum.Roll;
            AIPlayerController.PlayerEntity.animator.Value.SetTrigger("Dodge");
        }

        public override void ExitState()
        {
            MyEventSystem.Instance.UnSubscribe(MovementArgs.Id, OnEventMovement);
        }

        public override void OnState()
        {
            CountTime += Time.deltaTime;
        }
        public void OnEventMovement(object sender, GameEventArgs gameEventArgs)
        {
            MovementArgs args = gameEventArgs as MovementArgs;
            Vector3 TargetDirection = new Vector3(args.InputEntity.horizontal.Value, 0, args.InputEntity.vertical.Value);
            TargetDirection = Quaternion.Euler(0, AIPlayerController.MainCamera.camera.Camera.transform.rotation.eulerAngles.y, 0) * TargetDirection;
            //Debug.Log(TargetDirection.ToString());
            var dir = Vector3.Slerp(AIPlayerController.PlayerEntity.transform.Value.forward, TargetDirection, 0.5f);
            if (CountTime<0.2)
            {
                AIPlayerController.PlayerEntity.transform.Value.rotation = Quaternion.LookRotation(dir);
            }
            
        }
    }
}
