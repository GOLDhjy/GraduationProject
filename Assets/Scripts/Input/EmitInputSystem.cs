﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;
using UnityEngine;
using MyService;
public class EmitInputSystem : IInitializeSystem, IExecuteSystem, ICleanupSystem
{
    private InputEntity UniqueInputEntity;
    readonly InputContext m_contexts;
    private float h, v,Mouse_h,Mouse_v;

    public EmitInputSystem(Contexts contexts)
    {
        m_contexts = contexts.input;
        m_contexts.isUnique = true;
    }

    public void Initialize()
    {
        //添加Input对象的各个组件
        UniqueInputEntity = m_contexts.uniqueEntity;
        UniqueInputEntity.AddHorizontal(0);
        UniqueInputEntity.AddVertical(0);
        UniqueInputEntity.isRun = false;
        UniqueInputEntity.isRoll = false;
        UniqueInputEntity.isFreeScreen = true;
        UniqueInputEntity.isCrouch = false;
        UniqueInputEntity.isLeftMouse = false;
        UniqueInputEntity.isRightMouse = false;
        UniqueInputEntity.AddMouseHorizontal(0);
        UniqueInputEntity.AddMouseVertical(0);
        UniqueInputEntity.isFreeScreen = true;
    }
    public void Execute()
    {
        Mouse_h = Input.GetAxis("Mouse X");
        Mouse_v = Input.GetAxis("Mouse Y");

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        #region 移动控制输入
        //键盘移动旋转输入
        if ((Math.Abs(h) > 0.05 || Math.Abs(v) > 0.05) && Input.GetKey(KeyCode.LeftShift))
        {
            UniqueInputEntity.ReplaceHorizontal(h);
            UniqueInputEntity.ReplaceVertical(v);
            UniqueInputEntity.isRun = true;
            MyEventSystem.Instance.Invoke(MovementArgs.Id, this, new MovementArgs() { InputEntity = UniqueInputEntity, Idle = false });
        }
        else if (Math.Abs(h) > 0.05 || Math.Abs(v) > 0.05)
        {
            UniqueInputEntity.ReplaceHorizontal(h);
            UniqueInputEntity.ReplaceVertical(v);
            UniqueInputEntity.isRun = false;
            MyEventSystem.Instance.Invoke(MovementArgs.Id, this, new MovementArgs() {InputEntity = UniqueInputEntity, Idle = false });
        }
        else
        {
            UniqueInputEntity.ReplaceHorizontal(0);
            UniqueInputEntity.ReplaceVertical(0);
            MyEventSystem.Instance.Invoke(MovementArgs.Id, this, new MovementArgs() { InputEntity = UniqueInputEntity, Idle = true });
        }
        #endregion



        //鼠标移动输入
        if ((Math.Abs(Mouse_h) > 0.05 || Math.Abs(Mouse_v) > 0.05))
        {
            UniqueInputEntity.ReplaceMouseHorizontal(Mouse_h);
            UniqueInputEntity.ReplaceMouseVertical(Mouse_v);
            MyEventSystem.Instance.Invoke(MouseMovementArgs.Id, this, new MouseMovementArgs() { InputEntity = UniqueInputEntity });
        }
        else
        {
            UniqueInputEntity.ReplaceMouseHorizontal(0);
            UniqueInputEntity.ReplaceMouseVertical(0);
            MyEventSystem.Instance.Invoke(MouseMovementArgs.Id, this, new MouseMovementArgs() { InputEntity = UniqueInputEntity });
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            UniqueInputEntity.isRoll = true;
            MyEventSystem.Instance.Invoke(DodgeArgs.Id, this,new DodgeArgs() { Dodge = true , InputEntity = UniqueInputEntity });
        }
        else
        {
            UniqueInputEntity.isRoll = false;
        }

        //攻击
        if (Input.GetMouseButtonDown(0))
        {
            UniqueInputEntity.isLeftMouse = true;
            MyEventSystem.Instance.Invoke(AttackArgs.Id, this, new AttackArgs() { InputEntity = UniqueInputEntity, Attack = true });
        }
        else
        {
            UniqueInputEntity.isLeftMouse = false;
        }
        //转换视角模式
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            UniqueInputEntity.isFreeScreen = !UniqueInputEntity.isFreeScreen;
            if(UniqueInputEntity.isFreeScreen)
            {
                MyEventSystem.Instance.Invoke(ChangeViewArgs.Id, this, new ChangeViewArgs(){ InputEntity = UniqueInputEntity, viewModeEnum = ViewModeEnum.Free});
            }
            else
            {
                MyEventSystem.Instance.Invoke(ChangeViewArgs.Id, this, new ChangeViewArgs(){ InputEntity = UniqueInputEntity, viewModeEnum = ViewModeEnum.Lock });
            }
            
        }

    }

    public void Cleanup()
    {

    }
}

