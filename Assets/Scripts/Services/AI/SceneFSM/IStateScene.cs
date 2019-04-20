using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace MyService
{
    public enum SceneEnum
    {

        Start,
        Battle,
        Pause,
        Ivalid
    }
    public class IStateScene
    {
        
        private SceneEnum type = SceneEnum.Ivalid;
        protected AIScene AIScene;

        public SceneEnum Type
        {
            get
            {
                if(type == SceneEnum.Ivalid)
                {
                    Debug.LogError("当前状态为空");
                }
                return type;
            }
            set
            {
                type = value;
            }
        }

        public virtual void EnState() { }
        public virtual void OnState() { }
        public virtual void ExitState() { }

        public void SetController(AIScene aIPlayer)
        {
            AIScene = aIPlayer;
        }
    }
}
