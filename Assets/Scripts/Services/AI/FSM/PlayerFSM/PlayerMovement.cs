using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyService
{
    public class PlayerMovement : AIState
    {
        float speed=0;
        public PlayerMovement()
        {
            type = AIStateEnum.Movement;
        }

        public override void EnterState()
        {
            MyEventSystem.Instance.Subscribe(MovementArgs.Id, Movement);
            AIPlayerController.PlayerEntity.animState.Value = AnimStateEnum.Idle;
        }

        public override void ExitState()
        {
            MyEventSystem.Instance.UnSubscribe(MovementArgs.Id, Movement);
        }

        public override void OnState()
        {
            //Debug.Log(AIPlayerController.PlayerEntity.transform.Value.InverseTransformDirection(AIPlayerController.PlayerEntity.rigidbody.Value.velocity));
            if(AIPlayerController.PlayerEntity.animState.Value == AnimStateEnum.Idle)
            {
                speed = Mathf.Lerp(speed, 0, Time.deltaTime*2);
                AIPlayerController.PlayerEntity.animator.Value.SetFloat("Speed", speed);
            }
            else if(AIPlayerController.PlayerEntity.animState.Value == AnimStateEnum.Walk)
            {
                speed = Mathf.Lerp(speed, 0.6f, Time.deltaTime*2);
                AIPlayerController.PlayerEntity.animator.Value.SetFloat("Speed", speed);
            }
            else if(AIPlayerController.PlayerEntity.animState.Value == AnimStateEnum.Run)
            {
                speed = Mathf.Lerp(speed, 1.2f, Time.deltaTime*2);
                AIPlayerController.PlayerEntity.animator.Value.SetFloat("Speed", speed);
            }
                
            //if (AIPlayerController.PlayerEntity.rigidbody.Value.velocity.magnitude>0.05)
            //    Debug.Log("速度大");

        }
        //public void Move(object sender,GameEventArgs args)
        //{
        //    if (InputEntity == null)
        //    {
        //        Debug.LogError("InputEntity");
        //        return;
        //    }
        //    if (PlayerEntity == null)
        //    {
        //        Debug.LogError("PlayerEntity");
        //        return;
        //    }

        //    Vector3 TargetPosition = new Vector3(InputEntity.horizontal.Value, 0, InputEntity.vertical.Value) * PlayerEntity.moveSpeed.Value * Time.deltaTime;
        //    PlayerEntity.rigidbody.Value.MovePosition(PlayerEntity.gameObject.gameobject.transform.position + TargetPosition);
        //}
        //public void Rotate(object sender , GameEventArgs args)
        //{
        //    Vector3 TargetDirection = new Vector3(InputEntity.horizontal.Value, 0, InputEntity.vertical.Value);
        //    Debug.Log(TargetDirection.ToString());
        //    var dir = Vector3.Slerp(PlayerEntity.transform.Value.forward, TargetDirection, GameConfigService.Instance.PlayerRotateSpeed * Time.deltaTime);
        //    PlayerEntity.transform.Value.rotation = Quaternion.LookRotation(dir);
        //}
        public void Movement(object sender, GameEventArgs args)
        {
            MovementArgs movementArgs = args as MovementArgs;

            float TmpSpeed;
            if (movementArgs.Idle)
            {
                AIPlayerController.PlayerEntity.animState.Value = AnimStateEnum.Idle;
                return;
            }
            if (movementArgs.InputEntity.isRun)
            {
                TmpSpeed = AIPlayerController.PlayerEntity.moveSpeed.Value+2f;
                AIPlayerController.PlayerEntity.animState.Value = AnimStateEnum.Run;
            }
            else
            {
                TmpSpeed = AIPlayerController.PlayerEntity.moveSpeed.Value;
                AIPlayerController.PlayerEntity.animState.Value = AnimStateEnum.Walk;
            }
                Vector3 TargetPosition = new Vector3(movementArgs.InputEntity.horizontal.Value, 0, movementArgs.InputEntity.vertical.Value) * TmpSpeed * Time.deltaTime;
                AIPlayerController.PlayerEntity.rigidbody.Value.MovePosition(AIPlayerController.PlayerEntity.gameObject.gameobject.transform.position + TargetPosition);

                Vector3 TargetDirection = new Vector3(movementArgs.InputEntity.horizontal.Value, 0, movementArgs.InputEntity.vertical.Value);
                //Debug.Log(TargetDirection.ToString());
                var dir = Vector3.Slerp(AIPlayerController.PlayerEntity.transform.Value.forward, TargetDirection, GameConfigService.Instance.PlayerRotateSpeed * Time.deltaTime);
                AIPlayerController.PlayerEntity.transform.Value.rotation = Quaternion.LookRotation(dir);
        }
    }
}
