using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;
using MyService;

public class AddStateMSystem : IInitializeSystem
{
    Contexts Contexts;
    PlayerContext PlayerContext;
    GameContext GameContext;
    PlayerEntity PlayerEntity;
    GameEntity GameEntity;

    public AddStateMSystem(Contexts contexts)
    {
        Contexts = contexts;
        PlayerContext = contexts.player;
        GameContext = contexts.game;
    }

    public void Initialize()
    {
        PlayerEntity = PlayerContext.localPlayerEntity;
        GameEntity = GameContext.mainCameraEntity;
        //添加场景状态机
        AIScene.Instance.m_contexts = Contexts;
        AIScene.Instance.AddState(new StartScene());
        AIScene.Instance.AddState(new BattleScene());
        AIScene.Instance.AddState(new PauseState());
        AIScene.Instance.ChangeState(SceneEnum.Battle);

        if (!PlayerEntity.hasViewMode)
        {
            PlayerEntity.AddViewMode(new PlayerViewModeController(ref PlayerEntity, GameEntity));//给角色添加状态机
        }
        if (!GameEntity.hasViewModeController)
        {
            GameEntity.AddViewModeController(new MyService.CameraViewModeController(GameEntity, PlayerEntity));//给摄像机添加状态机
        }

        //给背包加东西
        PlayerEntity.backPack.Value.AddItemToBackPack(new BloodVial());
        PlayerEntity.backPack.Value.AddItemToBackPack(new BloodVial());
        PlayerEntity.backPack.Value.AddItemToBackPack(new StrengthenItem());

        //控制器初始化
        //PlayerController.Instance.Player = PlayerEntity;
    }
}

