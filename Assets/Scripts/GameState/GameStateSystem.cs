using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using MyService;

public class GameStateSystem : IInitializeSystem, IExecuteSystem
{
    AIScene AIScene;

    InputEntity InputEntity;
    PlayerEntity PlayerEntity;
    InputContext InputContext;
    Contexts m_Contexts;

    public GameStateSystem(Contexts m_Contexts)
    {
        this.m_Contexts = m_Contexts;
    }
    public void Initialize()
    {
        MyEventSystem.Instance.Subscribe(ChangeGameStateArgs.Id, OnChangeGameState);

        AIScene = new AIScene();
        AIScene.ChangeState(SceneEnum.Battle);

        InputEntity = m_Contexts.input.uniqueEntity;
        PlayerEntity = m_Contexts.player.localPlayerEntity;
        //LogService.Instance.Log(LogLevel.info, PlayerEntity.rotateSpeed.Value.ToString());
        if (PlayerEntity.gameObject.gameobject == null)
            Debug.LogError("为空");
    }

    public void Execute()
    { 
        AIScene.CurrentState.OnState();
        if (AIScene.CurrentState.Type == SceneEnum.Battle)
        {
            PlayerEntity.viewMode.Vlaue.DoState();
        }
        Debug.Log(AIScene.CurrentState.Type);
    }
    
    public void OnChangeGameState(object sender,GameEventArgs args)
    {
        ChangeGameStateArgs change = args as ChangeGameStateArgs;
        if(change.SceneEnum == SceneEnum.Battle)
        {
            AIScene.ChangeState(SceneEnum.Battle);
        }
        else if(change.SceneEnum == SceneEnum.Pause)
        {
            AIScene.ChangeState(SceneEnum.Pause);
        }
        else
        {
            AIScene.ChangeState(SceneEnum.Start);
        }
    }
}
