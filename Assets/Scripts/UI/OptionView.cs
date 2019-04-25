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

        private void Awake()
        {
            SetButtonCallback(Close, CloseEvent);
            SetButtonCallback(Confirm, ConfirmEvent); 
        }

        private void ConfirmEvent()
        {

        }

        private void CloseEvent()
        {
            UIService.Instance.PopView();
        }
    }
}
