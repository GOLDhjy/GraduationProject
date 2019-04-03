using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;
using UnityEngine;
using MyService;
public class EmitInputSystem : IInitializeSystem, IExecuteSystem, ICleanupSystem
{
    private InputEntity KeyBordInputEntity;
    private InputEntity MouseInputEntity;
    readonly InputContext m_contexts;
    private float h, v;

    public EmitInputSystem(Contexts contexts)
    {
        m_contexts = contexts.input;
        m_contexts.isKeyboard = true;
        m_contexts.isMouse = true;
    }

    public void Initialize()
    {
        KeyBordInputEntity = m_contexts.keyboardEntity;
        KeyBordInputEntity.AddHorizontal(0);
        KeyBordInputEntity.AddVertical(0);
        KeyBordInputEntity.isRun = false;
        KeyBordInputEntity.isRoll = false;
        KeyBordInputEntity.isFreeScreen = true;
        KeyBordInputEntity.isCrouch = false;

        MouseInputEntity = m_contexts.mouseEntity;

    }
    public void Execute()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        if ((Math.Abs(h) > 0.05 || Math.Abs(v) > 0.05) && Input.GetKey(KeyCode.LeftShift))
        {
            KeyBordInputEntity.ReplaceHorizontal(h);
            KeyBordInputEntity.ReplaceVertical(v);
            KeyBordInputEntity.isRun = true;
            MyEventSystem.Instance.Invoke(MovementArgs.Id, this, new MovementArgs() { InputEntity = KeyBordInputEntity, Idle = false });
        }
        else if (Math.Abs(h) > 0.05 || Math.Abs(v) > 0.05)
        {
            KeyBordInputEntity.ReplaceHorizontal(h);
            KeyBordInputEntity.ReplaceVertical(v);
            KeyBordInputEntity.isRun = false;
            MyEventSystem.Instance.Invoke(MovementArgs.Id, this, new MovementArgs() {InputEntity = KeyBordInputEntity, Idle = false });
        }
        else
        {
            KeyBordInputEntity.ReplaceHorizontal(0);
            KeyBordInputEntity.ReplaceVertical(0);
            MyEventSystem.Instance.Invoke(MovementArgs.Id, this, new MovementArgs() { InputEntity = KeyBordInputEntity, Idle = true });
        }


        if(Input.GetKeyDown(KeyCode.Space))
        {
            MyEventSystem.Instance.Invoke(DodgeArgs.Id, this,new DodgeArgs() { Dodge = true , InputEntity = KeyBordInputEntity });
        }

    }

    public void Cleanup()
    {

    }
}

