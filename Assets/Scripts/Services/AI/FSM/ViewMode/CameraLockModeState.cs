using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using MyService;

namespace MyService
{
    public class CameraLockModeState : ICameraViewModeState
    {
        Canvas Flag = null;
        Transform FlagTransform;

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

                //var tmp = ResourceService.Instance.InstantiateAsset<Sprite>(GameConfigService.Instance.UIIcon + "SelectField");
                if (Flag == null)
                {
                    Flag = ResourceService.Instance.InstantiateAsset<Canvas>(GameConfigService.Instance.UIPrefabPath + "SelectFieldCanvas");
                    FlagTransform = Flag.transform.Find("Flag").transform;
                }
                else
                {
                    Flag.enabled = true;
                }
                
                //Flag = UIService.Instance.PushView(GameConfigService.Instance.UIPrefabPath + "SelectFieldCanvas");
                


                //Image image = Flag.AddComponent<Image>();
                //image.sprite = tmp;
                //Flag.name = "Flag";
                //Flag.layer = LayerMask.NameToLayer("UI");
                //Flag.transform.SetParent(GameObject.Find("UIRoot").transform);
                //Flag.transform.localScale = new Vector2(50, 50);
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

            //UIService.Instance.PopView();
            // GameObject.Destroy(Flag);
            if (Flag!=null)
            {
                Flag.enabled = false;
            }
        }

        public override void OnState()
        {
            if (Enemy == null)
            {
                MyEventSystem.Instance.Invoke(ChangeViewArgs.Id, this, new ChangeViewArgs() { viewModeEnum = ViewModeEnum.Free });
                return;
            }

            //锁定敌人后，要在敌人身上渲染一个图标
            if (Flag != null)
            {
                FlagTransform.position = Camera.main.WorldToScreenPoint(Enemy.position+Vector3.up*2);
            }
            
        }
        public void OnMouseMovementEvent(object sender, GameEventArgs GameEventArgs)
        {
            if (Enemy == null)
                return;
            Vector3 PlayerToCamera = CameraViewModeController.MainCamera.gameTransform.Value.position - CameraViewModeController.PlayerEntity.transform.Value.position;
            Vector3 EnemyToPlayer = CameraViewModeController.PlayerEntity.transform.Value.position - Enemy.position;
            Vector3 NewDir = EnemyToPlayer.normalized;
            //玩家到相机向上抬45°
            Vector3 NewDis = new Vector3(NewDir.x, 0.5f, NewDir.z) * 6;

            NewDis = Vector3.Slerp(PlayerToCamera, NewDis, 20f * Time.deltaTime);
            var TmpPosition = CameraViewModeController.PlayerEntity.transform.Value.position;//用来保存玩家之前的位置
            //设置相机位置
            CameraViewModeController.MainCamera.gameTransform.Value.position = NewDis + CameraViewModeController.PlayerEntity.transform.Value.position;

            TmpPosition = Vector3.Slerp(TmpPosition, CameraViewModeController.PlayerEntity.transform.Value.position, 20f*Time.deltaTime);
            CameraViewModeController.MainCamera.camera.Camera.transform.LookAt(TmpPosition);
        }
    }
}
