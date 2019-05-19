using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyService;

public class Boss1WeaponCollider : MonoBehaviour
{
    [Header("本身的游戏物体")]
    public GameObject Self;
    public int AttackNum = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Self == null || Self.GetComponent<BossController>() == null)
            {
                Debug.LogError("Boss Has not BossController");
                return;
            }
            if (Self.GetComponent<BossController>().GetState() == BossStateEnum.Attack && AttackNum<2)
            {
                MyEventSystem.Instance.Invoke(HitArgs.Id, this, new HitArgs() { Hit = true });
                PlayerController.Instance.ChangeHpToPlayer(-Self.GetComponent<BossController>().GetBossATK());
                AttackNum++;
            }

        }
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {

    }
}
