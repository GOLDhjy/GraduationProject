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
    public class OptionView : IUserInterface
    {
        public Button Close;
        public Button Confirm;

        [Header("音乐大小")]
        public Slider slider1;
        [Header("音效大小")]
        public Slider slider2;

        private void Awake()
        {
            SetButtonCallback(Close, CloseEvent);
            SetButtonCallback(Confirm, ConfirmEvent);
            slider1.value = GameConfigService.Instance.MusicVolume;
            slider2.value = GameConfigService.Instance.AudioVolume;
        }

        private void ConfirmEvent()
        {
            GameConfigService.Instance.MusicVolume = slider1.value;
            GameConfigService.Instance.AudioVolume = slider2.value;
            UIService.Instance.PopView(true);
        }

        private void CloseEvent()
        {
            UIService.Instance.PopView(true);
        }
    }
}
