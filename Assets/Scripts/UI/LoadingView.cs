using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyService;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LoadingView : IUserInterface
    {
        public Text Text;
        public Slider Slider;
        public int num = -1;

        private void Awake()
        {
            MyEventSystem.Instance.Subscribe(StartLoadingViewArgs.Id, StartLoadingVieweEvent);
        }
        private void Update()
        {
        }
        private IEnumerator StartLoadingView()
        {
            float displayProgress = 0;
            float toProgress = 0;
            AsyncOperation async = SceneManager.LoadSceneAsync(num);
            async.allowSceneActivation = false;

            while(async.progress <= 0.85)
            {
                toProgress = async.progress * 100;
                Debug.Log(async.progress);
                while (displayProgress <= async.progress * 100)
                {
                    ++displayProgress;
                    SetLoadingPercentage(displayProgress);
                    Text.text = "加载中..." + Slider.value*100 + "%";
                    Debug.Log(Slider.value.ToString());
                    yield return new WaitForEndOfFrame();
                }
            }
            toProgress = 100;
            yield return new WaitForSeconds(1);
            while (displayProgress <= toProgress)
            {
                ++displayProgress;
                SetLoadingPercentage(displayProgress);
                Text.text = "加载中..." + Slider.value * 100 + "%";
                yield return new WaitForEndOfFrame();
            }
            Slider.value = 1;
            UIService.Instance.PopView();
            async.allowSceneActivation = true;
            async = null;

        }
        void SetLoadingPercentage(float value)
        {
            Slider.value = value / 100;
        }
        public void StartLoadingVieweEvent(object sender,GameEventArgs args)
        {
            StartLoadingViewArgs viewArgs = args as StartLoadingViewArgs;
            if (viewArgs.num != -1)
            {
                num = viewArgs.num;
                StartCoroutine("StartLoadingView");
            }
        }
    }
}
