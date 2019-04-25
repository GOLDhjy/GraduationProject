using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;
using UnityEngine;
using MyService;
public class LocalPlayerController :  Singleton<LocalPlayerController>
{
    private PlayerEntity player;
    //必须在角色初始化后使用
    public PlayerEntity Player
    {
        get
        {
            if (player == null)
            {
                player = Contexts.sharedInstance.player.localPlayerEntity;
            }
            return player;
        }
        set => player = value;
    }

    public LocalPlayerController()
    {
    }
    public void Initialize()
    {

    }

    //加血
    public void AddHpToPlayer(float num)
    {
        if (!Player.hasHp)
        {
            Player.AddHp(200f);
        }
        if (!Player.hasMaxHp)
        {
            Player.AddMaxHp(200f);
        }

        if ((Player.hp.Value + num) > Player.maxHp.Value)
        {
            Player.hp.Value = Player.maxHp.Value;

        }
        else
        {
            Player.hp.Value += num;
        }
        Debug.Log("给本地玩家加血" + num);
    }
    //加攻击
    public void AddATKToPlayer(float num)
    {
        if (!Player.hasATK)
        {
            Player.AddATK(100f);
        }
        Player.aTK.Value += num;
    }
    public void AddItemToPack(Item item)
    {
        if (!Player.hasBackPack)
        {
            Player.AddBackPack(new BackPack());
        }
        Player.backPack.Value.AddItemToBackPack(item);
    }
    //public void DropItemFromPack(Item item)
    //{
    //    if (!Player.hasBackPack)
    //    {
    //        Player.AddBackPack(new BackPack());
    //    }
    //    Player.backPack.Value.DeleteItemFromBackPack(item);
    //}
    public void DropItemFromPack(int num)
    {
        if (!Player.hasBackPack)
        {
            Player.AddBackPack(new BackPack());
        }
        Player.backPack.Value.DeleteItemFromBackPack(num);

    }
    public void UseItemFromPack(int num)
    {
        if (!Player.hasBackPack)
        {
            Player.AddBackPack(new BackPack());
        }
        Player.backPack.Value.UseItemAtBackPack(num);
        Debug.Log("使用物品");
    }
    //加移速

}
