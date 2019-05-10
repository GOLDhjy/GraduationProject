using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyService;

public class Boss1WeaponCollider : MonoBehaviour
{
    [Header("本身的游戏物体")]
    public GameObject Self;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(Self == null|| Self.GetComponent<BossController>() == null )
            {
                Debug.LogError("Boss Has not BossController");
                return;
            }
            MyEventSystem.Instance.Invoke(HitArgs.Id, this, new HitArgs() { Hit = true});
            PlayerController.Instance.ChangeHpToPlayer(-Self.GetComponent<BossController>().GetBossATK());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
}
