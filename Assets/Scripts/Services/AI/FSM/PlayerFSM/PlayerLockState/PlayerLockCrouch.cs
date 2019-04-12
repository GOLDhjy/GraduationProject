﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace MyService
{
    public class PlayerLockCrouch : LockAIState
    {
        float speed;
        public PlayerLockCrouch()
        {
            type = AIStateEnum.Coruch;
        }

        public override void EnterState()
        {
            MyEventSystem.Instance.Subscribe(MovementArgs.Id, Movement);
            AIPlayerController.PlayerEntity.animState.Value = AnimStateEnum.CrouchIdle;
            AIPlayerController.PlayerEntity.animator.Value.SetBool("Crouch", true);
        }

        public override void ExitState()
        {
            AIPlayerController.PlayerEntity.animator.Value.SetBool("Crouch", false);
            MyEventSystem.Instance.UnSubscribe(MovementArgs.Id, Movement);
        }

        public override void OnState()
        {
            if (AIPlayerController.PlayerEntity.animState.Value == AnimStateEnum.CrouchIdle)
            {
                speed = Mathf.Lerp(speed, 0, Time.deltaTime * 2);
                AIPlayerController.PlayerEntity.animator.Value.SetFloat("Speed", speed);
            }
            else if (AIPlayerController.PlayerEntity.animState.Value == AnimStateEnum.CrouchWalk)
            {
                speed = Mathf.Lerp(speed, 0.6f, Time.deltaTime * 2);
                AIPlayerController.PlayerEntity.animator.Value.SetFloat("Speed", speed);

            }
        }
        public void Movement(object sender, GameEventArgs args)
        {
            MovementArgs movementArgs = args as MovementArgs;
            float TmpSpeed;
            if (movementArgs.Idle)
            {
                AIPlayerController.PlayerEntity.animState.Value = AnimStateEnum.CrouchIdle;
                AIPlayerController.PlayerEntity.animator.Value.SetFloat("HInput", 0);
                AIPlayerController.PlayerEntity.animator.Value.SetFloat("VInput", 0);
                return;
            }
            else
            {
                TmpSpeed = AIPlayerController.PlayerEntity.moveSpeed.Value-1;
                AIPlayerController.PlayerEntity.animState.Value = AnimStateEnum.CrouchWalk;
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
                AIPlayerController.PlayerEntity.transform.Value.LookAt(AIPlayerController.PlayerEntity.lockEnemy.Value.position);
            }

            Vector3 TargetPosition = new Vector3(movementArgs.InputEntity.horizontal.Value, 0, movementArgs.InputEntity.vertical.Value) * TmpSpeed * Time.deltaTime;
            Vector3 NetTarget = TargetPosition.magnitude * TargetDirection.normalized;
            AIPlayerController.PlayerEntity.rigidbody.Value.MovePosition(AIPlayerController.PlayerEntity.gameObject.gameobject.transform.position + NetTarget);
        }
    }
}
