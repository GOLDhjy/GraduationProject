using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;
using UnityEngine;
namespace MyService
{
    public class PlayerViewModeController
    {
        private IPlayerViewModeState currentState;
        public PlayerEntity PlayerEntity;
        public GameEntity MainCamera;
        public IPlayerViewModeState CurrentState { get => currentState; set => currentState = value; }
        Dictionary<ViewModeEnum, IPlayerViewModeState> StateDic = new Dictionary<ViewModeEnum, IPlayerViewModeState>();


        public PlayerViewModeController(ref PlayerEntity playerEntity , GameEntity gameEntity)
        {
            
            PlayerEntity = playerEntity;
            MainCamera = gameEntity;
            //生成控制器时添加需要的状态进去
            //StateDic.Add(ViewModeEnum.Free, new FreeModeState());
            //StateDic.Add(ViewModeEnum.Lock, new LockModeState());
            AddState(new PlayerFreeModeState());
            AddState(new PlayerLockModeState());
            ChangeState(ViewModeEnum.Free);
            //AddState(new PlayerLockModeState());
            MyEventSystem.Instance.Subscribe(ChangeViewArgs.Id, ChangeViewEvent);

        }

        public void AddState(IPlayerViewModeState state)
        {
            if (StateDic.ContainsKey(state.type))
            {
                LogService.Instance.Log(LogLevel.info, "角色添加状态成功" + state.type.ToString());
                //Log.AI("Error Has SameState In Map " + state.type + " " + stateMap[state.type] + " " + state);
                return;
            }
            StateDic[state.type] = state;
            state.SetController(this);
        }
        public void DeleteState(IPlayerViewModeState state)
        {
            if (StateDic.ContainsKey(state.type))
            {
                //Log.AI("Error Has SameState In Map " + state.type + " " + stateMap[state.type] + " " + state);
                StateDic[state.type] = null;
            }
        }
        public void DoState()
        {
            if(CurrentState ==null)
            {
               Debug.LogError("当前角色ViewMode状态为空，执行失败");
                return;
            }

            CurrentState.OnState();
        }
        public bool ChangeState(ViewModeEnum s)
        {

            if (!StateDic.ContainsKey(s))
            {
                //Debug.LogError("Who Not Has Such State "+GetAttr().gameObject+" state "+s);
                //Log.Sys("gameObject No State " + GetAttr().gameObject + " state " + s);
                Debug.Log("角色不包含此状态，修改状态失败");
                return false;
            }


            if (CurrentState != null && CurrentState.type == s)
            {
                Debug.Log("角色当前状态与变化状态一致无须修改");
                return false;
            }

            if (CurrentState != null)
            {
                CurrentState.ExitState();
            }
            CurrentState = StateDic[s];
            CurrentState.EnterState();
            CurrentState.OnState();
            Debug.Log("修改角色ViewMode为:" + s.ToString());
            return true;
        }
        public bool ChangeStateForce(ViewModeEnum s)
        {
            //Log.AI("Change State Force " + GetAttr().gameObject + " state " + s);

            if (CurrentState != null && CurrentState.type == s)
            {

                return false;
            }
            if (CurrentState != null)
            {
                CurrentState.ExitState();
            }
            CurrentState = StateDic[s];
            Debug.Log("修改角色ViewMode为:" + s.ToString());
            CurrentState.EnterState();
            CurrentState.OnState();
            return true;
        }

        public void ChangeViewEvent(object sender, GameEventArgs gameEventArgs)
        {
            if (gameEventArgs == null)
            {
                Debug.LogError("Null Reference");
                return;
            }
            ChangeViewArgs args = gameEventArgs as ChangeViewArgs;
            if (args == null)
            {
                Debug.LogError("Null Reference");
                return;
            }

            if (args.viewModeEnum == ViewModeEnum.Free)
            {
                PlayerEntity.animator.Value.SetBool("LockView", false);
                ChangeState(ViewModeEnum.Free);
                
            }
            else
            {
                PlayerEntity.animator.Value.SetBool("LockView", true);
                ChangeState(ViewModeEnum.Lock);
            }
        }
        //检查能不能转换状态，需要把不能转换的调节加入里面
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
