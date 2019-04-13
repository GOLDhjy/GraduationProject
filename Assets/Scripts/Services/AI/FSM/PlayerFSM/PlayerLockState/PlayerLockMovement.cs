using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyService
{
    public class PlayerLockMovement : LockAIState
    {
        float speed = 0;
        float IdleTime = 0;

        public PlayerLockMovement()
        {
            type = AIStateEnum.Movement;
        }

        public override void EnterState()
        {
            IdleTime = 0;
            MyEventSystem.Instance.Subscribe(MovementArgs.Id, Movement);
            AIPlayerController.PlayerEntity.animState.Value = AnimStateEnum.Idle;
        }

        public override void ExitState()
        {
            MyEventSystem.Instance.UnSubscribe(MovementArgs.Id, Movement);
        }

        public override void OnState()
        {
            IdleTime += Time.deltaTime;
            if (IdleTime > 15f)
            {

                MyEventSystem.Instance.Invoke(RandomIdleArgs.Id, this, new RandomIdleArgs() { RandomIdle = true });
            }

            if (AIPlayerController.PlayerEntity.animState.Value == AnimStateEnum.Idle)
            {
                speed = Mathf.Lerp(speed, 0, Time.deltaTime * 2);
                AIPlayerController.PlayerEntity.animator.Value.SetFloat("Speed", speed);
            }
            else if (AIPlayerController.PlayerEntity.animState.Value == AnimStateEnum.Walk)
            {
                speed = Mathf.Lerp(speed, 0.6f, Time.deltaTime * 2);
                AIPlayerController.PlayerEntity.animator.Value.SetFloat("Speed", speed);
                
            }
            else if (AIPlayerController.PlayerEntity.animState.Value == AnimStateEnum.Run)
            {
                speed = Mathf.Lerp(speed, 1.2f, Time.deltaTime * 2);
                AIPlayerController.PlayerEntity.animator.Value.SetFloat("Speed", speed);
            }
        }

        public void Movement(object sender, GameEventArgs args)
        {
            MovementArgs movementArgs = args as MovementArgs;

            float TmpSpeed;
            if (movementArgs.Idle)
            {
                AIPlayerController.PlayerEntity.animState.Value = AnimStateEnum.Idle;
                AIPlayerController.PlayerEntity.animator.Value.SetFloat("HInput", 0);
                AIPlayerController.PlayerEntity.animator.Value.SetFloat("VInput", 0);
                return;
            }
            if (movementArgs.InputEntity.isRun)
            {
                IdleTime = 0;
                TmpSpeed = AIPlayerController.PlayerEntity.moveSpeed.Value + 2f;
                AIPlayerController.PlayerEntity.animState.Value = AnimStateEnum.Run;
                AIPlayerController.PlayerEntity.animator.Value.SetFloat("HInput", movementArgs.InputEntity.horizontal.Value);
                AIPlayerController.PlayerEntity.animator.Value.SetFloat("VInput", movementArgs.InputEntity.vertical.Value);
            }
            else
            {
                IdleTime = 0;
                TmpSpeed = AIPlayerController.PlayerEntity.moveSpeed.Value;
                AIPlayerController.PlayerEntity.animState.Value = AnimStateEnum.Walk;
                AIPlayerController.PlayerEntity.animator.Value.SetFloat("HInput", movementArgs.InputEntity.horizontal.Value);
                AIPlayerController.PlayerEntity.animator.Value.SetFloat("VInput", movementArgs.InputEntity.vertical.Value);
            }

            Vector3 TargetDirection = new Vector3(movementArgs.InputEntity.horizontal.Value, 0, movementArgs.InputEntity.vertical.Value);

            TargetDirection = Quaternion.Euler(0, AIPlayerController.MainCamera.gameTransform.Value.rotation.eulerAngles.y, 0) * TargetDirection;

            //var dir = Vector3.Slerp(AIPlayerController.PlayerEntity.transform.Value.forward, TargetDirection, GameConfigService.Instance.PlayerRotateSpeed * Time.deltaTime);
            //AIPlayerController.PlayerEntity.transform.Value.rotation = Quaternion.LookRotation(dir);
            if (AIPlayerController.PlayerEntity.lockEnemy.Value == null)
                Debug.LogError("锁定敌人为空");
            else
            {
                //AIPlayerController.PlayerEntity.transform.Value.rotation = Quaternion.LookRotation(AIPlayerController.PlayerEntity.lockEnemy.Value.position);
                //玩家一直朝向敌人
                var TmpTarget = AIPlayerController.PlayerEntity.transform.Value.rotation;
                var forword = AIPlayerController.PlayerEntity.lockEnemy.Value.position - AIPlayerController.PlayerEntity.transform.Value.position;

                TmpTarget = Quaternion.LookRotation(forword, Vector3.up);
                //AIPlayerController.PlayerEntity.transform.Value.LookAt(AIPlayerController.PlayerEntity.lockEnemy.Value.position);
                //差值
                TmpTarget = Quaternion.Slerp(AIPlayerController.PlayerEntity.transform.Value.rotation, TmpTarget, 20 * Time.deltaTime);
                AIPlayerController.PlayerEntity.transform.Value.rotation = TmpTarget;
            }
                    
            Vector3 TargetPosition = new Vector3(movementArgs.InputEntity.horizontal.Value, 0, movementArgs.InputEntity.vertical.Value) * TmpSpeed * Time.deltaTime;
            Vector3 NetTarget = TargetPosition.magnitude * TargetDirection.normalized;
            AIPlayerController.PlayerEntity.rigidbody.Value.MovePosition(AIPlayerController.PlayerEntity.gameObject.gameobject.transform.position + NetTarget);


        }
    }
}
