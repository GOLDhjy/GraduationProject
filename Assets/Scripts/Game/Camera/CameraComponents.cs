using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using Entitas.VisualDebugging.Unity;
using MyService;
[Game, Unique]
public class MainCameraComponent :IComponent
{
}
[Game]
public class CameraComponent : IComponent
{
    public Camera Camera;
}
[Game]
public class GameTransformComponent : IComponent
{
    public Transform Value;
}
[Game]
public class ViewModeControllerComponent : IComponent
{
    public CameraViewModeController Value;
}
[Game]
public class AudioSourceComponent : IComponent
{
    public AudioSource Value;
}