using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace MyService
{
    public class CameraViewModeController
    {

        private ICameraViewModeState currentState;
        public GameEntity MainCamera;
        public PlayerEntity PlayerEntity;
        public ICameraViewModeState CurrentState { get => currentState; set => currentState = value; }
        Dictionary<ViewModeEnum, ICameraViewModeState> StateDic = new Dictionary<ViewModeEnum, ICameraViewModeState>();


        public CameraViewModeController(GameEntity gameEntity,PlayerEntity playerEntity)
        {
            MainCamera = gameEntity;
            PlayerEntity = playerEntity;
            //生成控制器时添加需要的状态进去

            AddState(new CameraFreeModeState());
            AddState(new CameraLockModeState());
            ChangeState(ViewModeEnum.Free);

            MyEventSystem.Instance.Subscribe(ChangeViewArgs.Id, ChangeViewEvent);
            //AddState(new CameraLockModeState());
        }

        public void AddState(ICameraViewModeState state)
        {
            if (StateDic.ContainsKey(state.type))
            {
                LogService.Instance.Log(LogLevel.info, "相机添加状态成功" + state.type.ToString());
                //Log.AI("Error Has SameState In Map " + state.type + " " + stateMap[state.type] + " " + state);
                return;
            }
            StateDic[state.type] = state;
            state.SetController(this);
        }
        public void DeleteState(ICameraViewModeState state)
        {
            if (StateDic.ContainsKey(state.type))
            {
                //Log.AI("Error Has SameState In Map " + state.type + " " + stateMap[state.type] + " " + state);
                StateDic[state.type] = null;
            }
        }
        public void DoState()
        {
            if (CurrentState == null)
            {
                LogService.Instance.Log(LogLevel.err, "当前相机ViewMode状态为空，执行失败");
                return;
            }
            CurrentState.OnState();
        }
        public bool ChangeState(ViewModeEnum s)
        {

            if (!StateDic.ContainsKey(s))
            {
                Debug.LogError("相机不包含此状态，修改状态失败");
                return false;
            }


            if (CurrentState != null && CurrentState.type == s)
            {
                Debug.Log("相机当前状态与变化状态一致无须修改");
                return false;
            }

            if (CurrentState != null)
            {
                CurrentState.ExitState();
            }
            CurrentState = StateDic[s];
            Debug.Log("修改相机ViewMode为:" + s.ToString());
            CurrentState.EnterState();
            CurrentState.OnState();
            return true;
        }
        public bool ChangeStateForce(ViewModeEnum s)
        {

            if (CurrentState != null)
            {
                CurrentState.ExitState();
            }
            CurrentState = StateDic[s];
            Debug.Log("修改相机ViewMode为:" + s.ToString());
            CurrentState.EnterState();
            CurrentState.OnState();
            return true;
        }


        public void ChangeViewEvent(object sender,GameEventArgs gameEventArgs)
        {


            if (!CheckCanTransition())
                return;

            if (gameEventArgs == null)
            {
                Debug.LogError("Null Reference");
                return;
            }
            ChangeViewArgs args = gameEventArgs as ChangeViewArgs;
            if(args == null)
            {
                Debug.LogError("Null Reference");
                return;
            }

            if (args.viewModeEnum == ViewModeEnum.Free)
            {
                ChangeState(ViewModeEnum.Free);
            }
            else
            {
                ChangeState(ViewModeEnum.Lock);
            }
        }
        public bool CheckCanTransition()
        {
            if (AIScene.Instance.CurrentState.Type == SceneEnum.Pause)
            {
                return false;
            }
            return true;
        }
    }
}
