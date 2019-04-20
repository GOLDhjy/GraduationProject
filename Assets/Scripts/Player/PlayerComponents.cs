using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using MyService;
//玩家角色的动画状态
public enum AnimStateEnum
{
    Invalid =-1,
    Idle,
    Walk,
    Run,
    Roll,
    CrouchIdle,
    CrouchWalk,
    Jump,
    Attack,
}
public enum PostEnum
{
    Stand,
    Crouch
}

[Player,Unique]
public class LocalPlayerComponent : IComponent
{

}
[Player]
public class GameObjectComponent : IComponent
{
    public GameObject gameobject;
}
[Player]
public class TransformComponent : IComponent
{
    public Transform Value;
}
//玩家的属性
[Player]
public class ATKComponent : IComponent
{
    public float Value;
}
[Player]
public class HpComponent : IComponent
{
    public float Value;
}
//移动速度
[Player]
public class MoveSpeedComponent : IComponent
{
    public float Value;
}
//旋转速度
[Player]
public class RotateSpeedComponent : IComponent
{
    public float Value;
}
//视角模式
[Player]
public class ViewModeComponent : IComponent
{
    public PlayerViewModeController Vlaue;
}
[Player]
public class AnimStateComponent : IComponent
{
    public AnimStateEnum Value;
}
[Player]
public class PostComponent : IComponent
{
    public PostEnum Value;
}
[Player]
public class FreeViewComponent : IComponent
{

}
[Player]
public class SwordComponent : IComponent
{
    public GameObject Sword;
}
[Player]
public class RigidbodyComponent : IComponent
{
    public Rigidbody Value;
}
[Player]
public class ColliderComponent : IComponent
{
    public Collider Value;
}
[Player]
public class AnimatorComponent : IComponent
{
    public Animator Value;
}
[Player]
public class PlayerAudioComponent : IComponent
{
    public AudioSource Value;
}

//玩家锁定的敌人
[Player]
public class LockEnemyComponent : IComponent
{
    public Transform Value;
}



