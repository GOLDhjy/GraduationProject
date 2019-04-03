using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;

namespace MyService
{
    public class PlayerViewModeController
    {
        private IPlayerViewModeState currentState;
        public PlayerEntity PlayerEntity;
        public IPlayerViewModeState CurrentState { get => currentState; set => currentState = value; }
        Dictionary<ViewModeEnum, IPlayerViewModeState> StateDic = new Dictionary<ViewModeEnum, IPlayerViewModeState>();


        public PlayerViewModeController(ref PlayerEntity playerEntity)
        {
            
            PlayerEntity = playerEntity;
            //生成控制器时添加需要的状态进去
            //StateDic.Add(ViewModeEnum.Free, new FreeModeState());
            //StateDic.Add(ViewModeEnum.Lock, new LockModeState());
            AddState(new PlayerFreeModeState());
            ChangeState(ViewModeEnum.Free);
            //AddState(new PlayerLockModeState());

        }

        public void AddState(IPlayerViewModeState state)
        {
            if (StateDic.ContainsKey(state.type))
            {
                LogService.Instance.Log(LogLevel.info, "添加状态成功" + state.type.ToString());
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
                LogService.Instance.Log(LogLevel.err,"当前ViewMode状态为空，执行失败");
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
                LogService.Instance.Log(LogLevel.err, "不包含此状态，修改状态失败");
                return false;
            }


            if (CurrentState != null && CurrentState.type == s)
            {
                LogService.Instance.Log(LogLevel.info,"当前状态与变化状态一致无须修改");
                return false;
            }

            if (CurrentState != null)
            {
                CurrentState.ExitState();
            }
            CurrentState = StateDic[s];
            CurrentState.EnterState();
            CurrentState.OnState();
            LogService.Instance.Log(LogLevel.info, "修改ViewMode为:"+s.ToString());
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
            LogService.Instance.Log(LogLevel.info, "修改ViewMode为:" + s.ToString());
            CurrentState.EnterState();
            CurrentState.OnState();
            return true;
        }

    }
}
