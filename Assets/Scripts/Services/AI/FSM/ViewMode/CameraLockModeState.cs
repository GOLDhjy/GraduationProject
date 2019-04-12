using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace MyService
{
    public class CameraLockModeState : ICameraViewModeState
    {
        public Transform Enemy = null;
        Collider[] hitColliders;
        public CameraLockModeState()
        {
            type = ViewModeEnum.Lock;
        }

        public override void EnterState()
        {
            MyEventSystem.Instance.Subscribe(MouseMovementArgs.Id, OnMouseMovementEvent);
            hitColliders = Physics.OverlapSphere(CameraViewModeController.MainCamera.gameTransform.Value.position, 20f, LayerMask.GetMask("Enemy"));
            
            foreach(var i in hitColliders)
            {
                var RelativePoint = Camera.main.transform.InverseTransformDirection(i.transform.position);
                if (RelativePoint.z < 0)
                    continue;
                Enemy = i.transform;
                CameraViewModeController.PlayerEntity.ReplaceLockEnemy(Enemy);
                break;
            }
            if (Enemy == null)
            {
                //CameraViewModeController.ChangeState(ViewModeEnum.Free);
                MyEventSystem.Instance.Invoke(ChangeViewArgs.Id, this, new ChangeViewArgs() { viewModeEnum = ViewModeEnum.Free });
                return;
            }
        }

        public override void ExitState()
        {
            Enemy = null;
            CameraViewModeController.PlayerEntity.ReplaceLockEnemy(Enemy);
            MyEventSystem.Instance.UnSubscribe(MouseMovementArgs.Id, OnMouseMovementEvent);
        }

        public override void OnState()
        {

        }
        public void OnMouseMovementEvent(object sender, GameEventArgs GameEventArgs)
        {
            if (Enemy == null)
                return;
            Vector3 PlayerToCamera = CameraViewModeController.MainCamera.gameTransform.Value.position - CameraViewModeController.PlayerEntity.transform.Value.position;
            Vector3 EnemyToPlayer = CameraViewModeController.PlayerEntity.transform.Value.position - Enemy.position;
            Vector3 NewDir = EnemyToPlayer.normalized;
            Vector3 NewDis = new Vector3(NewDir.x, 0.5f, NewDir.z) * 6;
            NewDis = Vector3.Slerp(PlayerToCamera, NewDis, 10f * Time.deltaTime);
            var TmpPosition = CameraViewModeController.PlayerEntity.transform.Value.position;//用来保存玩家之前的位置
            CameraViewModeController.MainCamera.gameTransform.Value.position = NewDis + CameraViewModeController.PlayerEntity.transform.Value.position;

            TmpPosition = Vector3.Slerp(TmpPosition, CameraViewModeController.MainCamera.gameTransform.Value.position, 10f * Time.deltaTime);
            CameraViewModeController.MainCamera.camera.Camera.transform.LookAt(TmpPosition);
        }
    }
}
