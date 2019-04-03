using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Input]
public class HorizontalComponent : IComponent
{
    public float Value;
}
[Input]
public class VerticalComponent : IComponent
{
    public float Value;
}
[Input]
public class CrouchComponent : IComponent
{
}

[Input]
public class FreeScreenComponent : IComponent
{
}
[Input]
public class RollComponent : IComponent{}

[Input]
public class RunComponent : IComponent
{

}
[Input]
public class CameraRotateComponent :IComponent
{

}
[Input]
public class LeftMouseComponent : IComponent
{

}
[Input]
public class RightMouseComponent : IComponent
{

}
[Input]
public class MouseHorizontalComponent : IComponent
{
    public float Value;
}
[Input]
public class MouseVerticalComponent :IComponent
{
    public float Value;
}

[Input,Unique]
public class UniqueComponent : IComponent
{

}


