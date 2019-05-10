using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyService
{
    public class LockAIPlayer
    {
        private LockAIState currentState;

        public LockAIState CurrentState { get => currentState; set => currentState = value; }
        Dictionary<AIStateEnum, LockAIState> StateDic = new Dictionary<AIStateEnum, LockAIState>();
        public PlayerEntity PlayerEntity;
        public GameEntity MainCamera;

        public LockAIPlayer(PlayerEntity playerEntity, GameEntity gameEntity)
        {
            PlayerEntity = playerEntity;
            MainCamera = gameEntity;
            AddState(new PlayerLockMovement());
            AddState(new PlayeLockInvalid());
            AddState(new PlayerLockCrouch());
            AddState(new PlayerLockRoll());
            AddState(new PlayerLockRandomIdle());
            AddState(new PlayerLockAttack());
            AddState(new PlayerLockHit());
            AddState(new PlayerLockDied());
        }
        //在改变状态时一定要执行取消事件
        public void UnSubEvent()
        {
            MyEventSystem.Instance.UnSubscribe(RandomIdleArgs.Id, OnEventRandomIdle);
            MyEventSystem.Instance.UnSubscribe(AttackArgs.Id, OnEventAttack);
            MyEventSystem.Instance.UnSubscribe(DodgeArgs.Id, OnEventDodge);
            MyEventSystem.Instance.UnSubscribe(ChangeToMovementArgs.Id, OnEventChangeToMovement);
            MyEventSystem.Instance.UnSubscribe(CrouchArgs.Id, OnEventCrouch);
            MyEventSystem.Instance.UnSubscribe(HitArgs.Id, OnHitEvent);
            MyEventSystem.Instance.UnSubscribe(DieArgs.Id, OnEventDie);
            ChangeState(AIStateEnum.INVALID);
        }

        public void SubEvent()
        {
            MyEventSystem.Instance.Subscribe(RandomIdleArgs.Id, OnEventRandomIdle);
            MyEventSystem.Instance.Subscribe(AttackArgs.Id, OnEventAttack);
            MyEventSystem.Instance.Subscribe(DodgeArgs.Id, OnEventDodge);
            MyEventSystem.Instance.Subscribe(ChangeToMovementArgs.Id, OnEventChangeToMovement);
            MyEventSystem.Instance.Subscribe(CrouchArgs.Id, OnEventCrouch);
            MyEventSystem.Instance.Subscribe(HitArgs.Id, OnHitEvent);
            MyEventSystem.Instance.Subscribe(DieArgs.Id, OnEventDie);
        }
        public void AddState(LockAIState state)
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
        public void DeleteState(LockAIState state)
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
                Debug.LogError("当前AIState状态为空，执行失败");
                return;
            }
            CurrentState.OnState();
        }
        public bool ChangeState(AIStateEnum s)
        {
            if (!StateDic.ContainsKey(s))
            {
                Debug.LogError("Who Not Has Such State  state " + s);
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
            Debug.Log("修改AIState为:" + s.ToString());
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
            Debug.Log("修改AIState为:" + s.ToString());
            CurrentState.EnterState();
            CurrentState.OnState();
            return true;
        }


        public void OnEventDodge(object sender, GameEventArgs gameEventArgs)
        {
            if (!CheckCanTransition())
                return;

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
            if (!CheckCanTransition())
                return;

            AttackArgs attackArgs = gameEventArgs as AttackArgs;
            if (attackArgs != null && attackArgs.Attack)
            {
                ChangeState(AIStateEnum.Attack);
            }
            else if (attackArgs.Attack == false)
            {
                ChangeStateForce(AIStateEnum.Movement);
            }
        }
        public void OnEventRandomIdle(object sender, GameEventArgs gameEventArgs)
        {
            if (!CheckCanTransition())
                return;

            RandomIdleArgs args = gameEventArgs as RandomIdleArgs;
            if (args.RandomIdle)
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
            if (!CheckCanTransition())
                return;

            if (gameEventArgs == null)
            {
                Debug.LogError("NULL Reference");
                return;
            }
            ChangeToMovementArgs args = gameEventArgs as ChangeToMovementArgs;
            if (args.Movement)
            {
                ChangeState(AIStateEnum.Movement);
            }
        }
        public void OnEventCrouch(object sender, GameEventArgs gameEventArgs)
        {
            if (!CheckCanTransition())
                return;

            if (gameEventArgs == null)
            {
                Debug.LogError("NULL Reference");
                return;
            }
            CrouchArgs args = gameEventArgs as CrouchArgs;
            if (args.Crouch)
            {
                ChangeState(AIStateEnum.Coruch);
            }
            else
            {
                ChangeState(AIStateEnum.Movement);
            }
        }
        //被打事件
        private void OnHitEvent(object sender, GameEventArgs e)
        {
            if (!CheckCanTransition())
            {
                return;
            }
            if (e == null)
            {
                Debug.LogError("NULL Reference");
                return;
            }
            HitArgs args = e as HitArgs;
            if (args.Hit)
            {
                ChangeState(AIStateEnum.Hit);
            }
            else
            {
                ChangeState(AIStateEnum.Movement);
            }
            
        }
        //Die事件
        private void OnEventDie(object sender, GameEventArgs e)
        {
            if (!CheckCanTransition())
            {
                return;
            }
            if (e == null)
            {
                Debug.LogError("NULL Reference");
                return;
            }
            ChangeState(AIStateEnum.Died);
        }
        public bool CheckCanTransition()
        {
            if (CurrentState == null)
            {
                return true;
            }
            if (AIScene.Instance.CurrentState.Type == SceneEnum.Pause || CurrentState.type == AIStateEnum.Died)
            {
                return false;
            }
            return true;
        }
    }
}
