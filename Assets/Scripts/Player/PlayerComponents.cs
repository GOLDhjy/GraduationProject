using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using MyService;
public enum AnimStateEnum
{
    Invalid =-1,
    Idle,
    Walk,
    Run,
    Roll,
    Crouch,
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
[Player]
public class HpComponent : IComponent
{
    public float Value;
}
[Player]
public class MoveSpeedComponent : IComponent
{
    public float Value;
}
[Player]
public class RotateSpeedComponent : IComponent
{
    public float Value;
}
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
