using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;
using UnityEngine;

namespace MyService
{
    public class PlayerController : Singleton<PlayerController>
    {
        private PlayerEntity player ;
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
        //得到整个背包
        public Dictionary<int, Dictionary<Item, int>> GetBackPackDate()
        {
            return Player.backPack.Value.ItemsPack;
        }
        //得到当前背包的5个装备物品
        public Item[] GetCurrentPackDate()
        {
            return Player.backPack.Value.CurrentPack;
        }
        //得到HUD界面当前物品
        public Item GetCurrentItemDate()
        {
            return Player.backPack.Value.CurrentItem;
        }
        //得到背包里面物品数量
        public int GetCountFromBack()
        {
            return Player.backPack.Value.GetItemCount();
        }
        public float GetPlayerHp()
        {
            return Player.hp.Value;
        }
        //public void ChangePlayerHp(float num)
        //{
        //    Player.hp.Value += num;
        //}
        public float GetPlayerATK()
        {
            return Player.aTK.Value;
        }
        //public void ChangePlayerATK(float num)
        //{
        //    Player.aTK.Value += num;
        //}

        //加血
        public void ChangeHpToPlayer(float num)
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
        public void ChangeATKToPlayer(float num)
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
}
