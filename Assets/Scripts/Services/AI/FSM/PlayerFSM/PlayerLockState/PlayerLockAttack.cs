﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace MyService
{
    public class PlayerLockAttack : LockAIState
    {
        public PlayerLockAttack()
        {
            type = AIStateEnum.Attack;
        }

        public override void EnterState()
        {
            //MyEventSystem.Instance.Subscribe(MovementArgs.Id, OnEventMovement);
            AIPlayerController.PlayerEntity.animState.Value = AnimStateEnum.Attack;
            AIPlayerController.PlayerEntity.animator.Value.SetTrigger("Attack");
        }

        public override void ExitState()
        {
            //MyEventSystem.Instance.UnSubscribe(MovementArgs.Id, OnEventMovement);
        }

        public override void OnState()
        {
            var TmpTarget = AIPlayerController.PlayerEntity.transform.Value.rotation;
            var forword = AIPlayerController.PlayerEntity.lockEnemy.Value.position - AIPlayerController.PlayerEntity.transform.Value.position;

            TmpTarget = Quaternion.LookRotation(forword, Vector3.up);
            //AIPlayerController.PlayerEntity.transform.Value.LookAt(AIPlayerController.PlayerEntity.lockEnemy.Value.position);
            //差值
            TmpTarget = Quaternion.Slerp(AIPlayerController.PlayerEntity.transform.Value.rotation, TmpTarget, 20 * Time.deltaTime);
            AIPlayerController.PlayerEntity.transform.Value.rotation = TmpTarget;
        }
        public void OnEventMovement(object sender, GameEventArgs gameEventArgs)
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