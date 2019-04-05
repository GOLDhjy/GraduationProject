using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace MyService
{
    public class PlayerAttack : AIState
    {
        float PressTime;
        public PlayerAttack()
        {
            type = AIStateEnum.Attack;
        }

        public override void EnterState()
        {
            MyEventSystem.Instance.Subscribe(MovementArgs.Id, OnEventMovement);
            AIPlayerController.PlayerEntity.animState.Value = AnimStateEnum.Attack;
            //AIPlayerController.PlayerEntity.animator.Value.SetTrigger("Actions");
            AIPlayerController.PlayerEntity.animator.Value.SetTrigger("Attack");
            //AIPlayerController.PlayerEntity.animator.Value.SetInteger("AttackId", Convert.ToInt16(UnityEngine.Random.Range(1, 6)));

        }

        public override void ExitState()
        {
            MyEventSystem.Instance.UnSubscribe(MovementArgs.Id, OnEventMovement);
        }

        public override void OnState()
        {
            //AnimatorClipInfo[] animationClip = AIPlayerController.PlayerEntity.animator.Value.GetCurrentAnimatorClipInfo(0);
            //foreach(var i in animationClip)
            //{
            //    Debug.Log(i.clip.name);
            //}
                //AIPlayerController.ChangeState(AIStateEnum.Movement);
            //else if(AIPlayerController.PlayerEntity.animator.Value.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            //{
            //    Debug.Log("IsAttack...");
            //}
        }
        public void OnEventMovement(object sender,GameEventArgs gameEventArgs)
        {
            MovementArgs args = gameEventArgs as MovementArgs;
            Vector3 TargetDirection = new Vector3(args.InputEntity.horizontal.Value, 0, args.InputEntity.vertical.Value);
            TargetDirection = Quaternion.Euler(0, AIPlayerController.MainCamera.camera.Camera.transform.rotation.eulerAngles.y, 0) * TargetDirection;
            //Debug.Log(TargetDirection.ToString());
            var dir = Vector3.Slerp(AIPlayerController.PlayerEntity.transform.Value.forward, TargetDirection, GameConfigService.Instance.PlayerRotateSpeed * Time.deltaTime);
            AIPlayerController.PlayerEntity.transform.Value.rotation = Quaternion.LookRotation(dir);
        }

    }
}
