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
        float x = 0, y = 0;
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
            x = args.InputEntity.mouseHorizontal.Value;
            y = args.InputEntity.mouseVertical.Value;
            //方法一：
            //Vector3 DistanceVec = CameraViewModeController.MainCamera.gameTransform.Value.position - CameraViewModeController.PlayerEntity.transform.Value.position;
            //Vector3 NewDis = DistanceVec.normalized * 6;
            ////x = x * CameraViewModeController.MainCamera.gameTransform.Value.rotation.eulerAngles.x;
            ////Quaternion.Euler(0, CameraViewModeController.MainCamera.camera.Camera.transform.rotation.eulerAngles.y, 0);
            //Vector3 tmp =  new Vector3(x, y, 0);
            //tmp = Quaternion.Euler(0, CameraViewModeController.MainCamera.camera.Camera.transform.rotation.eulerAngles.y, 0) * tmp;


            //Quaternion quaternion = Quaternion.Euler(y, x, 0);
            //NewDis = Vector3.Slerp(DistanceVec, NewDis, 1.8f* Time.deltaTime);

            //CameraViewModeController.MainCamera.gameTransform.Value.position = quaternion * DistanceVec + CameraViewModeController.PlayerEntity.transform.Value.position;


           //CameraViewModeController.MainCamera.camera.Camera.transform.LookAt(CameraViewModeController.PlayerEntity.transform.Value.position);

            //方法二：
            CameraViewModeController.MainCamera.gameTransform.Value.transform.RotateAround(CameraViewModeController.PlayerEntity.transform.Value.position, Vector3.up, x);
            float Tmpy = Mathf.Clamp(y, 0, 85);
            CameraViewModeController.MainCamera.gameTransform.Value.transform.RotateAround(CameraViewModeController.PlayerEntity.transform.Value.position, CameraViewModeController.MainCamera.camera.Camera.transform.right, -y);

            Vector3 DistanceVec = CameraViewModeController.MainCamera.gameTransform.Value.position - CameraViewModeController.PlayerEntity.transform.Value.position;
            Vector3 NewDis = DistanceVec.normalized * 6;
            NewDis = Vector3.Slerp(DistanceVec, NewDis, 1.8f * Time.deltaTime);
            NewDis.y = Mathf.Clamp(NewDis.y, 1, 6);

            CameraViewModeController.MainCamera.gameTransform.Value.position = NewDis + CameraViewModeController.PlayerEntity.transform.Value.position;
            CameraViewModeController.MainCamera.camera.Camera.transform.LookAt(CameraViewModeController.PlayerEntity.transform.Value.position);
        }
    }
}
