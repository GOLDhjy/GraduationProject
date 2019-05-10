using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using MyService;

public class GameStateSystem : IInitializeSystem, IExecuteSystem
{
    //public static AIScene AIScene;

    InputEntity InputEntity;
    PlayerEntity PlayerEntity;
    InputContext InputContext;
    GameEntity m_MainCamera;
    Contexts m_Contexts;

    public GameStateSystem(Contexts m_Contexts)
    {
        this.m_Contexts = m_Contexts;
    }
    public void Initialize()
    {
        //这个事件没有取消注册，标记一下
        MyEventSystem.Instance.Subscribe(ChangeGameStateArgs.Id, OnChangeGameState);
        

        InputEntity = m_Contexts.input.uniqueEntity;
        PlayerEntity = m_Contexts.player.localPlayerEntity;
        m_MainCamera = m_Contexts.game.mainCameraEntity;
        //LogService.Instance.Log(LogLevel.info, PlayerEntity.rotateSpeed.Value.ToString());
        if (PlayerEntity.gameObject.gameobject == null)
            Debug.LogError("为空");
    }

    public void Execute()
    { 
        AIScene.Instance.CurrentState.OnState();
        if (AIScene.Instance.CurrentState.Type == SceneEnum.Battle)
        {
            PlayerEntity.viewMode.Vlaue.DoState();
            m_MainCamera.viewModeController.Value.DoState();
        }
        
        else if (AIScene.Instance.CurrentState.Type == SceneEnum.Start)
        {
            
        }
        //Debug.Log(AIScene.Instance.CurrentState.Type);
    }
    
    public void OnChangeGameState(object sender,GameEventArgs args)
    {
        ChangeGameStateArgs change = args as ChangeGameStateArgs;
        if(change.SceneEnum == SceneEnum.Battle)
        {
            AIScene.Instance.ChangeState(SceneEnum.Battle);
        }
        else if(change.SceneEnum == SceneEnum.Pause)
        {
            if (AIScene.Instance.CurrentState.Type == SceneEnum.Battle)
                AIScene.Instance.ChangeState(SceneEnum.Pause);
        }
        else
        {
            AIScene.Instance.ChangeState(SceneEnum.Start);
            //退出战斗场景是删除实体
            //m_MainCamera.isMainCamera = false;
            //InputEntity.isUnique = false;
            //PlayerEntity.isLocalPlayer = false;

            //m_Contexts.input.isUnique = false;
            //m_Contexts.player.isLocalPlayer = false;
            //m_Contexts.game.isMainCamera = false;

            //m_MainCamera.Destroy();
            //InputEntity.Destroy();
            //PlayerEntity.Destroy();
            //Contexts.sharedInstance.player.DestroyAllEntities();
            //Contexts.sharedInstance.input.DestroyAllEntities();
            //Contexts.sharedInstance.game.DestroyAllEntities();
        }
    }
}
