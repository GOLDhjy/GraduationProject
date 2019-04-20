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
    public enum ButtonCanvasEnum
    {
        Equip,Skill,Item,Setting
    }
    public class SelectionView : IUserInterface
    {
        private Button SelectItem = null;
        private Button SelectSkill = null;
        ButtonCanvasEnum CurrentCanvas = ButtonCanvasEnum.Equip;

        [Header("Root")]
        public GameObject Root; 
        //根画布下
        [Header("Root Canvas")]
        public Button QuitButton;
        public Button EquipButton;
        public Button ItemButtom;
        public Button SkillButton;
        public Button SettingButton;

        //装备按钮下
        [Header("Equip Button")]
        public Canvas EquipCanvas;
        public Canvas SelectItemCanvas;
        public Canvas SelectSkillCanvas;

        //装备按钮下的装备画布
        [Header("Equip Button/Equip Canvas")]
        public Button[] ItemMenu = new Button[5];
        public Button SkillMenu;
        public Text Describe;
        //装备按钮下的选择物品画布
        [Header("Equip Button/SelectItem Canvas")]
        public Text SelectItemDescribe;
        public Button SelectItemQuit;
        public GridLayoutGroup GridLayoutGroup;
        public GameObject SelectItemContent;
        //装备按钮下的选择技能画布
        [Header("Equip Button/SelectSkill Canvas")]
        public Text SelectSkillDescribe;
        public Button SelectSkillQuit;
        public GameObject SelectSkillContent;

        //设置按钮下
        [Header("Setting Button")]
        public Canvas SettingCanvas;
        //设置按钮下的SettingCanvas
        [Header("Setting Button/Setting Canvas")]
        public Button GameOption;
        public Button QuitGame;
        public Button BackToStart;

        private void Awake()
        {
            
            Button[] buttons = new Button[5];
            for (int i = 0; i < 5;i++)
            {
                buttons[i] = ResourceService.Instance.InstantiateAsset<Button>(GameConfigService.Instance.UIPrefabPath + "Button");
                buttons[i].transform.SetParent(SelectItemContent.transform);

                Debug.Log(buttons[i].gameObject.GetComponent<RectTransform>().localScale);
                //buttons[i].gameObject.GetComponent<RectTransform>().localScale.Scale(new Vector3(1, 1, 1));
                //buttons[i].gameObject.GetComponent<RectTransform>().localScale.Set(1, 1, 1);
                buttons[i].gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

                Vector3 vector3 = buttons[i].gameObject.GetComponent<RectTransform>().localPosition;
                vector3.z = 0;
                buttons[i].gameObject.GetComponent<RectTransform>().localPosition = vector3;
                Debug.Log(buttons[i].gameObject.GetComponent<RectTransform>().localScale);
            }
            //初始化设置
            SetgameobjectActive(EquipCanvas.gameObject, true);
            SetgameobjectActive(SelectItemCanvas.gameObject, false);
            SetgameobjectActive(SelectItemCanvas.gameObject, false);
            SetgameobjectActive(SelectSkillCanvas.gameObject, false);
            SetgameobjectActive(SettingCanvas.gameObject, false);
            //初始化设置回调函数
            SetButtonCallback(QuitButton, QuitEvent);
            SetButtonCallback(EquipButton, EquipEvent);
            SetButtonCallback(SettingButton, SettingEvent);
            foreach (var item in ItemMenu)
            {
                SetButtonCallback(item, ItemSelectEvent);
            }
            SetButtonCallback(SkillMenu, SkillSelectEvent);
            SetButtonCallback(SelectItemQuit, SelectItemQuitEvent);
            SetButtonCallback(SelectSkillQuit, SelectSkillQuitEvent);


        }

        //选这物品按钮回调
        void SelectItemEvent()
        {
        }
        //选这个技能按钮的回调
        void SelectSkillEvent()
        {

        }
        //选择技能界面的退出按钮
        private void SelectSkillQuitEvent()
        {
            UIService.Instance.CloseNotifyWithinCanvas(SelectSkillCanvas.gameObject);
        }
        //选择物品界面的退出按键
        private void SelectItemQuitEvent()
        {
            UIService.Instance.CloseNotifyWithinCanvas(SelectItemCanvas.gameObject);
            
        }
        //装备界面点击选择技能
        private void SkillSelectEvent()
        {
            UIService.Instance.OpneNotifyWithinCanvas(SelectSkillCanvas.gameObject);
        }
        //装备界面点击选择物品
        void ItemSelectEvent()
        {
            UIService.Instance.OpneNotifyWithinCanvas(SelectItemCanvas.gameObject);
        }

        void SettingEvent()
        {
            UIService.Instance.ChangeNotifyWithinCanvas(GetCurrentCanvas(), SettingCanvas.gameObject);
            CurrentCanvas = ButtonCanvasEnum.Setting;

        }

        void EquipEvent()
        {
            UIService.Instance.ChangeNotifyWithinCanvas(GetCurrentCanvas(), EquipCanvas.gameObject);
            CurrentCanvas = ButtonCanvasEnum.Equip;
        }

        void QuitEvent()
        {
            Root.SetActive(false);
        }


        public GameObject GetCurrentCanvas()
        {
            switch (CurrentCanvas)
            {
                case ButtonCanvasEnum.Equip:
                    return EquipCanvas.gameObject;
                case ButtonCanvasEnum.Skill:
                    
                case ButtonCanvasEnum.Item:
                    return null;
                case ButtonCanvasEnum.Setting:
                    return SettingCanvas.gameObject;
                default:
                    return null;
            }
        }
    }
}
