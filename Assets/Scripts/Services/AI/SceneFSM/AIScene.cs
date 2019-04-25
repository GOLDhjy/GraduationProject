using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Entitas;
namespace MyService
{
    public class AIScene : Singleton<AIScene>
    {
        public Contexts m_contexts;
        private IStateScene currentState;

        public IStateScene CurrentState { get => currentState; set => currentState = value; }
        private Dictionary<SceneEnum, IStateScene> StateDic = new Dictionary<SceneEnum, IStateScene>();


        public AIScene()
        {
        }

        public void AddState(IStateScene state)
        {
            if (StateDic.ContainsKey(state.Type))
            {
                Debug.Log("已经添加此状态");
                return;
            }
            StateDic[state.Type] = state;
            Debug.Log("场景添加状态成功:" + state.Type.ToString());
            state.SetController(this);
        }
        public void DeleteState(IStateScene state)
        {
            if (StateDic.ContainsKey(state.Type))
            {
                StateDic[state.Type] = null;
            }
            else
            {
                Debug.Log("不包含此状态");
            }
        }
        public void DoState()
        {
            if (CurrentState == null)
            {
                Debug.LogError("当前场景AIState状态为空，执行失败");
                return;
            }
            CurrentState.OnState();
        }
        public bool ChangeState(SceneEnum s)
        {
            if (!StateDic.ContainsKey(s))
            {
                Debug.LogError("Who Not Has Such State  state " + s);
                return false;
            }

            if (CurrentState != null && CurrentState.Type == s)
            {
                Debug.Log("与当前状态相同，无须改变");
                return false;
            }

            if (CurrentState != null)
            {
                CurrentState.ExitState();
            }
            CurrentState = StateDic[s];
            Debug.Log("修改场景AIState为:" + s.ToString());
            CurrentState.EnState();
            CurrentState.OnState();
            return true;
        }
        public bool ChangeStateForce(SceneEnum s)
        {
            if (CurrentState != null)
            {
                CurrentState.ExitState();
            }
            CurrentState = StateDic[s];
            Debug.Log("修改场景AIState为:" + s.ToString());
            CurrentState.EnState();
            CurrentState.OnState();
            return true;
        }
        public void SubEvent()
        {

        }
        public void UnSubEvent()
        {

        }
    }
}
