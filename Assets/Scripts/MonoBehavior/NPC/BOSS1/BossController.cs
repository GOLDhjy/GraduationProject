using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField]
    private BossProperty MyProperty = new BossProperty();
    [SerializeField]
    public BossStateEnum m_State = BossStateEnum.Normal;
    private void Start()
    {
        MyProperty.Name = "BOSS:红衣剑客";
        MyProperty.ATK = 20;
        MyProperty.Hp = 1000;
        MyProperty.MaxHp = 1000;
    }
    public BossStateEnum GetState()
    {
        return m_State;
    }
    public string GetBossName()
    {
        return MyProperty.Name;
    }
    public float GetBossHp()
    {
        return MyProperty.Hp;
    }
    public float GetBossMaxHp()
    {
        return MyProperty.MaxHp;
    }
    public float GetBossATK()
    {
        return MyProperty.ATK;
    }
    public void ChangeHp(float value)
    {
        MyProperty.Hp += value;
    }
    public void ChangeATK(float value)
    {
        MyProperty.ATK += value;
    }

}

[System.Serializable]
public enum BossStateEnum
{
    Normal,
    Attack,
    Death
}

[System.Serializable]
public class BossProperty
{

    public string Name = "boss";
    public float Hp = 0;
    public float MaxHp = 0;
    public float ATK = 0;
}
