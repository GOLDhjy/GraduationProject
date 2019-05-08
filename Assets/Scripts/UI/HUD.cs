using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MyService;
using UnityEngine.UI;

namespace UI
{
    public class HUD : IUserInterface
    {
        [Header("血条")]
        public Slider HpBar;
        [Header("物品图像")]
        public Image Main;
        [Header("技能图像")]
        public Image Left;
        //UI当前物品
        Item CurrentItem =null;
        //装备的当前物品
        Item Item = null;
        //当前5个物品
        Item[] Items = null;


        private void Start()
        {
            Item = PlayerController.Instance.GetCurrentItemDate();
            Items = PlayerController.Instance.GetCurrentPackDate();
            if (CurrentItem == null)
            {
                InitializeAndUpdateItem();
            }
            MyEventSystem.Instance.Subscribe(UpdateItemIconArgs.Id, UpdateIconEvent);
            MyEventSystem.Instance.Subscribe(UpdateHpBarArgs.Id, UpdateHpBarEvent);
            MyEventSystem.Instance.Subscribe(ChangeCurrentItemArgs.Id, ChangeCurrentItemEvent);
            MyEventSystem.Instance.Subscribe(UseCurrentItemArgs.Id, UseCurrentItemEvent);
        }
        private void OnDisable()
        {
            MyEventSystem.Instance.UnSubscribe(UpdateItemIconArgs.Id, UpdateIconEvent);
            MyEventSystem.Instance.UnSubscribe(UpdateHpBarArgs.Id, UpdateHpBarEvent);
            MyEventSystem.Instance.UnSubscribe(ChangeCurrentItemArgs.Id, ChangeCurrentItemEvent);
            MyEventSystem.Instance.UnSubscribe(UseCurrentItemArgs.Id, UseCurrentItemEvent);
        }
        private void UseCurrentItemEvent(object sender, GameEventArgs e)
        {
            UseCurrentItem();
        }

        private void ChangeCurrentItemEvent(object sender, GameEventArgs e)
        {
            ChangeCurrentItemArgs args = e as ChangeCurrentItemArgs;
            ChangeCurrentItem(args.Der);
        }

        private void UpdateHpBarEvent(object sender, GameEventArgs e)
        {
            UpdateHpBar();
        }
        private void UpdateIconEvent(object sender, GameEventArgs e)
        {
            InitializeAndUpdateItem();
            UpdateCurrentItem();
        }

       
        //更新玩家血条UI
        private void UpdateHpBar()
        {
            if (PlayerController.Instance.GetPlayerHp()<=0)
            {
                MyEventSystem.Instance.Invoke(DieArgs.Id, this, new DieArgs() { });
            }
            HpBar.value = Mathf.Clamp01(PlayerController.Instance.GetPlayerHp() / PlayerController.Instance.GetPlayerMaxHp());
        }
        /// <summary>
        /// 使用当前物品
        /// </summary>
        void UseCurrentItem()
        {
            if (Item  == null)
            {
                Debug.LogError("Current Item Is Null，If this oparate is legal,plaese ignore");
                //使用物品为空时
                //应该播放音效

                return;
            }
            //ItemManagerService.Instance.ProductItem(Item);
            PlayerController.Instance.UseItemFromPack(Item);
        }

        /// <summary>
        /// Der代表方向，向左切换还是向右切换:-1代表左边，1代表右边
        /// </summary>
        /// <param name="Der"></param>
        void ChangeCurrentItem(int Der)
        {
            int index = FindIndexAtItems(Item, Items);
            if (index == -1)
            {
                Debug.LogError("Cant find index");
                return;
            }
            switch (Der)
            {
                case -1:
                    
                    int tmpindex = index;
                    for (int i = 0; i < Items.Count(); i++)
                    {
                        tmpindex = ((tmpindex - 1)+ Items.Count()) % Items.Count();
                        if (Items[tmpindex] != null)
                        {
                            if (tmpindex != index)
                            {
                                Item = Items[tmpindex];
                                UpdateCurrentItem();
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    break;
                case 1:
                    //index = FindIndexAtItems(Item, Items);
                    int tmpindex2 = index;
                    for (int i = 0; i < Items.Count(); i++)
                    {
                        tmpindex2 = (tmpindex2 + 1) % Items.Count();
                        if (Items[tmpindex2] != null)
                        {
                            if (tmpindex2 != index)
                            {
                                Item = Items[tmpindex2];
                                UpdateCurrentItem();
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }


        int FindIndexAtItems(Item TargetItem,Item[] items)
        {
            if (TargetItem == null || items.Count()<=0)
            {
                Debug.LogError("需要查找的物品为空");
                return -1;
            }
            else
            {
                for (int i = 0; i < items.Count(); i++)
                {
                    if (items[i] == null)
                    {
                        continue;
                    }
                    if (TargetItem.GetType() == items[i].GetType())
                    {
                        return i;
                    }
                    else
                    {
                        continue;
                    }
                }
                return -1;
            }
        }
        //初始化背包当前装备物品
        void InitializeAndUpdateItem()
        {
            if (Item !=null)
            {
                if(Items.Contains(Item))
                {
                    return;
                }
                else
                {
                    for (int i = 0; i < Items.Count(); i++)
                    {
                        if (Items[i] == null)
                        {
                            continue;
                        }
                        else
                        {
                            Item = Items[i];
                            UpdateCurrentItem();
                            return;
                        }

                    }
                    Item = null;
                    UpdateCurrentItem();
                }
            }
            if(Items.Count()<=0)
            {
                Item = null;
                UpdateCurrentItem();
                return;
            }
            for (int i = 0; i < Items.Count(); i++)
            {
                if (Items[i] == null)
                {
                    continue;
                }
                else
                {
                    Item = Items[i];
                    UpdateCurrentItem();
                    return;
                }
            }
        }
        //更新当前物品UI图标
        void UpdateCurrentItem()
        {
            if (Item ==null)
            {
                CurrentItem = null;
                Main.GetComponent<Image>().sprite = ResourceService.Instance.LoadAsset<Sprite>(GameConfigService.Instance.UIIcon + Item.DefaultIconName);
                return;
            }
            if (CurrentItem == null)
            {
                CurrentItem = Item;
                Main.GetComponent<Image>().sprite = Item.GetItemIcon(Item.IconName);
            }
            else
            {
                if (IsSame(CurrentItem, Item))
                {
                    return;
                }
                else
                {
                    CurrentItem = Item;
                    Main.GetComponent<Image>().sprite = Item.GetItemIcon(Item.IconName);
                }
            }
        }
        bool IsSame(Item item1,Item item2)
        {
            if (item1 == null || item2 == null)
            {
                return false;
            }
            if (item1.GetType() == item2.GetType())
            {
                return true;
            }
            else
                return false;
        }

    }
}
