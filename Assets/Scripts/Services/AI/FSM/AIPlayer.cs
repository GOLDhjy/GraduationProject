using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace MyService
{
    public class AIPlayer
    {
        public bool ControlMode;//是否锁定视角
        private AIState currentState;

        public AIState CurrentState { get => currentState; set => currentState = value; }
        Dictionary<AIStateEnum, AIState> StateDic = new Dictionary<AIStateEnum, AIState>();
        public PlayerEntity PlayerEntity;
        public AIPlayer(PlayerEntity playerEntity)
        {
            MyEventSystem.Instance.Subscribe(DodgeArgs.Id, OnEventDodge);
            PlayerEntity = playerEntity;
            AddState(new PlayerMovement());
            AddState(new PlayerRoll());
            ChangeState(AIStateEnum.Movement);
        }

        public void AddState(AIState state)
        {
            if (StateDic.ContainsKey(state.type))
            {
                //Log.AI("Error Has SameState In Map " + state.type + " " + stateMap[state.type] + " " + state);
                return;
            }
            StateDic[state.type] = state;
            Debug.Log("添加状态成功:" + state.type.ToString());
            state.SetController(this);
        }
        public void DeleteState(AIState state)
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
                Debug.LogError("当前ViewMode状态为空，执行失败");
                return;
            }
            CurrentState.OnState();
        }
        public bool ChangeState(AIStateEnum s)
        {
            if (!StateDic.ContainsKey(s))
            {
                //Debug.LogError("Who Not Has Such State "+GetAttr().gameObject+" state "+s);
                //Log.Sys("gameObject No State " + GetAttr().gameObject + " state " + s);
                return false;
            }

            if (CurrentState != null && CurrentState.type == s)
            {
                return false;
            }

            if (CurrentState != null)
            {
                CurrentState.ExitState();
            }
            CurrentState = StateDic[s];
            Debug.Log("修改ViewMode为:" + s.ToString());
            CurrentState.EnterState();
            CurrentState.OnState();
            return true;
        }
        public bool ChangeStateForce(AIStateEnum s)
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
            Debug.Log("修改ViewMode为:" + s.ToString());
            CurrentState.EnterState();
            CurrentState.OnState();
            return true;
        }
        public void OnEventDodge(object sender, GameEventArgs gameEventArgs)
        {
            DodgeArgs dodgeArgs = gameEventArgs as DodgeArgs;
            if (dodgeArgs.Dodge == true)
            {
                if(PlayerEntity.animator.Value.GetCurrentAnimatorStateInfo(0).IsTag("Dodge"))
                {
                    Debug.Log("IsTag : Dodge");
                }
                if(!PlayerEntity.animator.Value.GetCurrentAnimatorStateInfo(0).IsTag("Dodge"))
                    ChangeStateForce(AIStateEnum.Roll);
                
            }
        }

    }
}
