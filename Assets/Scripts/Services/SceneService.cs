using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
namespace MyService
{
    public class SceneService : Singleton<SceneService>
    {
        int scenemin = 0;
        int scenemax = 1;
        public void LoadSecneSync(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                Debug.LogError("name IsNullOrEmpty");
                return;
            }
            SceneManager.LoadScene(name);
        }
        public void LoadSecneSync(int num)
        {
            if(num< scenemin || num> scenemax)
            {
                Debug.LogError("加载场景序号不对");
                return;
            }
            SceneManager.LoadScene(num);

        }
        public void LoadSceneAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.LogError("name IsNullOrEmpty");
                return;
            }
            //var LoadingCanvas = ResourceService.Instance.LoadAsset<Canvas>();
            GameObject LoadingCanvas = UIService.Instance.PushView(GameConfigService.Instance.UIPrefabPath + "LoadingCanvas");
            //Slider LoadingSlider = LoadingCanvas.transform.Find("Slider").GetComponent<Slider>();
            //Text LoadingText = LoadingCanvas.transform.Find("Text").GetComponent<Text>();

            AsyncOperation async = SceneManager.LoadSceneAsync(name);


        }
        public void LoadSceneAsync(int num)
        {
            if (num < scenemin || num > scenemax)
            {
                Debug.LogError("加载场景序号不对");
                return;
            }
            //打开加载界面
            GameObject LoadingCanvas = UIService.Instance.PushView(GameConfigService.Instance.UIPrefabPath + "LoadingCanvas");

            MyEventSystem.Instance.Invoke(StartLoadingViewArgs.Id, this, new StartLoadingViewArgs() { num = num});
        }
    }
}
