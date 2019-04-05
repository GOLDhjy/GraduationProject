using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyService;
using UnityEngine;
namespace MyService
{
    public class CameraFreeModeState : ICameraViewModeState
    {
        public override void EnterState()
        {
            MyEventSystem.Instance.Subscribe(MouseMovementArgs.Id, OnMouseMovementEvent);
        }

        public override void ExitState()
        {
            MyEventSystem.Instance.UnSubscribe(MouseMovementArgs.Id, OnMouseMovementEvent);
        }

        public override void OnState()
        {

        }
        public void OnMouseMovementEvent(object sender,GameEventArgs GameEventArgs)
        {

            MouseMovementArgs args = GameEventArgs as MouseMovementArgs;
            float x = args.InputEntity.mouseHorizontal.Value;
            float y = args.InputEntity.mouseVertical.Value;
            Vector3 DistanceVec = CameraViewModeController.MainCamera.gameTransform.Value.position - CameraViewModeController.PlayerEntity.transform.Value.position;
            Vector3 NewDis = DistanceVec.normalized * 6;
            //x = x * CameraViewModeController.MainCamera.gameTransform.Value.rotation.eulerAngles.x;
            //Quaternion.Euler(0, CameraViewModeController.MainCamera.camera.Camera.transform.rotation.eulerAngles.y, 0);
            Vector3 tmp =  new Vector3(x, y, 0);
            tmp = Quaternion.Euler(0, CameraViewModeController.MainCamera.camera.Camera.transform.rotation.eulerAngles.y, 0) * tmp;
           

            Quaternion quaternion = Quaternion.Euler(0, x, y);
            NewDis = Vector3.Slerp(DistanceVec, NewDis, 1.8f* Time.deltaTime);

            CameraViewModeController.MainCamera.gameTransform.Value.position = quaternion * NewDis + CameraViewModeController.PlayerEntity.transform.Value.position;


            CameraViewModeController.MainCamera.camera.Camera.transform.LookAt(CameraViewModeController.PlayerEntity.transform.Value.position);
        }
    }
}
