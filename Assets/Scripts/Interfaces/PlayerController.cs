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
        //获取角色主武器
        public GameObject GetPlayerMainSword()
        {
            return Player.sword.Sword;
        }
        //得到角色的物品
        public GameObject GetPlayerGameObject()
        {
            return Player.gameObject.gameobject;
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
        public float GetPlayerMaxHp()
        {
            return Player.maxHp.Value;
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
            Debug.Log("改变本地血量" + num);

            //血量改变后通知UI更新
            MyEventSystem.Instance.Invoke(UpdateHpBarArgs.Id, this, new UIEventArgs() { });
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
        //丢掉物品
        public void DropItemFromPack(Item item)
        {
            if (!Player.hasBackPack)
            {
                Player.AddBackPack(new BackPack());
            }
            Player.backPack.Value.DeleteItemFromBackPack(item);
            UpdateCurrentPackDate();

        }

        //获取背包里面的当前物品
        public void UseItemFromPack(Item item)
        {
            if (!Player.hasBackPack)
            {
                Player.AddBackPack(new BackPack());
            }
            Player.backPack.Value.UseItemAtBackPack(item);
            DropItemFromPack(item);
            //通知物品UI更新
            MyEventSystem.Instance.Invoke(UpdateItemIconArgs.Id, this, new UpdateItemIconArgs() { });

            Debug.Log("使用物品");
        }

        /// <summary>
        /// 在使用/丢弃物品后，应该更新5物品栏里面的物品，是否用完
        /// </summary>
        private void UpdateCurrentPackDate()
        {
            Dictionary<int, Dictionary<Item, int>> m_backpack = GetBackPackDate();
            Item[] items = GetCurrentPackDate();
            for (int i = 0; i < items.Count(); i++)
            {
                if (items[i] != null)
                {
                    Dictionary<Item, int> tmp;
                    //m_backpack.TryGetValue(items[i].Id,out tmp);
                    if (!m_backpack.TryGetValue(items[i].Id, out tmp))
                    {
                        items[i] = null;
                    }
                }
            }
        }
        //加移速


    }
}
