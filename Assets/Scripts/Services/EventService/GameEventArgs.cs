using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace MyService
{
    public class GameEventArgs : EventArgs
    {
    }
    public class UIEventArgs : GameEventArgs
    {

    }
    //玩家输入事件
    #region
    //这参数为movement状态里面获取键盘输入值
    public class MovementArgs : GameEventArgs
    {
        public static readonly int Id = typeof(MovementArgs).GetHashCode();
        public InputEntity InputEntity;
        public bool Idle = true;
    }
    public class MouseMovementArgs : GameEventArgs
    {
        public static readonly int Id = typeof(MouseMovementArgs).GetHashCode();
        public InputEntity InputEntity;
    }
    public class AttackArgs : GameEventArgs
    {
        public static readonly int Id = typeof(AttackArgs).GetHashCode();
        public InputEntity InputEntity;
        public bool Attack = false;

    }
    public class RandomIdleArgs : GameEventArgs
    {
        public static readonly int Id = typeof(RandomIdleArgs).GetHashCode();
        public bool RandomIdle = false;
    }
    public class DodgeArgs : GameEventArgs
    {
        public static readonly int Id = typeof(DodgeArgs).GetHashCode();
        public InputEntity InputEntity;
        public bool Dodge = false;
    }
    public class CrouchArgs : GameEventArgs
    {
        public static readonly int Id = typeof(CrouchArgs).GetHashCode();
        public InputEntity InputEntity;
        public bool Crouch = false;
    }
    //将状态改为movement
    public class ChangeToMovementArgs : GameEventArgs
    {
        public static readonly int Id = typeof(ChangeToMovementArgs).GetHashCode();
        public bool Movement = false;
    }


    #endregion
    //播放音乐事件
    public class AudioArgs : GameEventArgs
    {
        public static readonly int Id = typeof(AudioArgs).GetHashCode();
        public string Name;
        public AudioEnum AudioEnum;
    }
    //更改视角事件

    public class ChangeViewArgs : GameEventArgs
    {
        public static readonly int Id = typeof(ChangeViewArgs).GetHashCode();
        public InputEntity InputEntity;
        public ViewModeEnum viewModeEnum;
    }

    //更改游戏状态
    public class ChangeGameStateArgs : GameEventArgs
    {
        public static readonly int Id = typeof(ChangeGameStateArgs).GetHashCode();
        public SceneEnum SceneEnum;
    }
    //开始进度条UI
    public class StartLoadingViewArgs : GameEventArgs
    {
        public static readonly int Id = typeof(StartLoadingViewArgs).GetHashCode();
        public int num = -1;
    }


    //UI->更新Item相关
    public class UpdateItemIconArgs : UIEventArgs
    {
        public static readonly int Id = typeof(UpdateItemIconArgs).GetHashCode();

    }
}
