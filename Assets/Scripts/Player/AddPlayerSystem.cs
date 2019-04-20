using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;
using MyService;
using UnityEngine;
using Entitas.Unity;

public class AddPlayerSystem : IInitializeSystem
{
    readonly Transform Parent = new GameObject("MyPlayer").transform;

    readonly PlayerContext m_context;
    readonly GameContext gameContext;
    PlayerEntity player;
    GameEntity MainCamera;
    public AddPlayerSystem(Contexts contexts)
    {
        m_context = contexts.player;
        gameContext = contexts.game;
        m_context.isLocalPlayer = true;
    }

    public void Initialize()
    {  
        player = m_context.localPlayerEntity;
        player.isLocalPlayer = true;
        GameObject gameObject = ResourceService.Instance.InstantiateAsset<GameObject>(GameConfigService.Instance.PlayerPrefabPath,Vector3.forward,Quaternion.identity);
        gameObject.transform.SetParent(Parent);
        player.AddGameObject(gameObject);
        gameObject.Link(player);
        GameObject Sword = GameObject.FindGameObjectWithTag("Sword");
        LogService.Instance.Log(LogLevel.info, Sword.name);
        player.AddRigidbody(gameObject.GetComponent<Rigidbody>());
        player.AddCollider(gameObject.GetComponent<Collider>());
        player.AddAnimator(gameObject.GetComponent<Animator>());
        player.AddPlayerAudio(gameObject.GetComponent<AudioSource>());
        player.AddLockEnemy(null);
        player.AddTransform(gameObject.transform);
        player.AddAnimState(AnimStateEnum.Idle);
        player.AddSword(Sword);
        player.AddMoveSpeed(GameConfigService.Instance.PlayerMoveSpeed);
        player.AddRotateSpeed(GameConfigService.Instance.PlayerRotateSpeed);
        player.AddHp(100f);
        player.AddATK(100f);
        player.isFreeView = true;
    }
}