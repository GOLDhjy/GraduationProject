﻿using System;
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
        public GameEntity MainCamera;
        public AIPlayer(PlayerEntity playerEntity,GameEntity gameEntity)
        {
            PlayerEntity = playerEntity;
            MainCamera = gameEntity;
            AddState(new PlayerMovement());
            AddState(new PlayerRoll());
            AddState(new PlayerAttack());
            AddState(new PlayerRandomIdle());
            AddState(new PlayerInvalid());
        }
        //在改变状态时一定要执行取消事件
        public void UnSubEvent()
        {
            
            MyEventSystem.Instance.UnSubscribe(RandomIdleArgs.Id, OnEventRandomIdle);
            MyEventSystem.Instance.UnSubscribe(AttackArgs.Id, OnEventAttack);
            MyEventSystem.Instance.UnSubscribe(DodgeArgs.Id, OnEventDodge);
            MyEventSystem.Instance.UnSubscribe(ChangeToMovementArgs.Id, OnEventChangeToMovement);
            ChangeState(AIStateEnum.INVALID);
        }
        public void SubEvent()
        {
            MyEventSystem.Instance.Subscribe(RandomIdleArgs.Id, OnEventRandomIdle);
            MyEventSystem.Instance.Subscribe(AttackArgs.Id, OnEventAttack);
            MyEventSystem.Instance.Subscribe(DodgeArgs.Id, OnEventDodge);
            MyEventSystem.Instance.Subscribe(ChangeToMovementArgs.Id, OnEventChangeToMovement);
        }
        public void AddState(AIState state)
        {
            if (StateDic.ContainsKey(state.type))
            {
                //Log.AI("Error Has SameState In Map " + state.type + " " + stateMap[state.type] + " " + state);
                return;
            }
            StateDic[state.type] = state;
            Debug.Log("角色添加状态成功:" + state.type.ToString());
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
                Debug.LogError("当前角色AIState状态为空，执行失败");
                return;
            }
            CurrentState.OnState();
        }
        public bool ChangeState(AIStateEnum s)
        {
            if (!StateDic.ContainsKey(s))
            {
                Debug.LogError("Who Not Has Such State  state "+s);
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
            Debug.Log("修改角色AIState为:" + s.ToString());
            CurrentState.EnterState();
            CurrentState.OnState();
            return true;
        }
        public bool ChangeStateForce(AIStateEnum s)
        {
            //Log.AI("Change State Force " + GetAttr().gameObject + " state " + s);

            //if (CurrentState != null && CurrentState.type == s)
            //{
            //    Debug.Log("当前状态与改变状态一致，无须改变");
            //    return false;
            //}
            if (CurrentState != null)
            {
                CurrentState.ExitState();
            }
            CurrentState = StateDic[s];
            Debug.Log("修改角色AIState为:" + s.ToString());
            CurrentState.EnterState();
            CurrentState.OnState();
            return true;
        }
        public void OnEventDodge(object sender, GameEventArgs gameEventArgs)
        {
            DodgeArgs dodgeArgs = gameEventArgs as DodgeArgs;
            if (dodgeArgs.Dodge == true)
            {
                Vector3 TargetDirection = new Vector3(dodgeArgs.InputEntity.horizontal.Value, 0, dodgeArgs.InputEntity.vertical.Value);
                var dir = Vector3.Slerp(PlayerEntity.transform.Value.forward, TargetDirection, 0.9f);
                PlayerEntity.transform.Value.rotation = Quaternion.LookRotation(dir);

                ChangeState(AIStateEnum.Roll);
            }
            else
            {
                ChangeStateForce(AIStateEnum.Movement);
            }
        }
        public void OnEventAttack(object sender, GameEventArgs gameEventArgs)
        {
            AttackArgs attackArgs = gameEventArgs as AttackArgs;
            if(attackArgs!= null && attackArgs.Attack )
            {
                ChangeState(AIStateEnum.Attack);
            }
            else if(attackArgs.Attack == false)
            {
                ChangeStateForce(AIStateEnum.Movement);
            }
        }
        public void OnEventRandomIdle(object sender,GameEventArgs gameEventArgs)
        {
            RandomIdleArgs args = gameEventArgs as RandomIdleArgs;
            if(args.RandomIdle)
            {
                ChangeState(AIStateEnum.RandomIdle);
            }
            else if (args.RandomIdle == false)
            {
                ChangeStateForce(AIStateEnum.Movement);
            }
        }
        public void OnEventChangeToMovement(object sender, GameEventArgs gameEventArgs)
        {
            if(gameEventArgs == null)
            {
                Debug.LogError("NULL Reference");
                return;
            }
            ChangeToMovementArgs args = gameEventArgs as ChangeToMovementArgs;
            if(args.Movement)
            {
                ChangeState(AIStateEnum.Movement);
            }
        }
    }
}
