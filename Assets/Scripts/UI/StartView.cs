using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using MyService;
namespace UI 
{
    public class StartView : IUserInterface
    {
        [Header("开始按钮")]
        public Button Start;

        [Header("设置按钮")]
        public Button Option;

        [Header("退出按钮")]
        public Button Quit;

        private void Awake()
        {
            SetButtonCallback(Start, StartEvent);
            SetButtonCallback(Option, OptionEvent);
            SetButtonCallback(Quit, QuitEvent);
        }

        //退出游戏
        private void QuitEvent()
        {
            Application.Quit();
        }

        //设置
        private void OptionEvent()
        {
            
        }

        //开始游戏
        private void StartEvent()
        {
            MyEventSystem.Instance.Invoke(ChangeGameStateArgs.Id, this, new ChangeGameStateArgs() { SceneEnum = SceneEnum.Battle });
            SecneService.Instance.LoadSceneAsync(1);

        }
    }
}
