using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace MyService
{
    public class UIService : Singleton<UIService>
    {
        GameObject background = null;
        GameObject alphaBlock;
        GameObject justBlock;
        GameObject UIRoot = null;

        public GameObject GetUIRoot()
        {
            if (UIRoot == null)
            {
                UIRoot = GameObject.Find("UIRoot");
            }
            return UIRoot;
        }

        //        GameObject back = null;
        List<GameObject> stack = new List<GameObject>();
        //        List<GameObject> alphaStack;
        Dictionary<string, GameObject> UIDic = new Dictionary<string, GameObject>();

        //        public GameObject GetMainUI()
        //        {
        //            return stack[0];
        //        }

        //        public WindowMng()
        //        {
        //            stack = new List<GameObject>();
        //            alphaStack = new List<GameObject>();

        //            background = Resources.Load<GameObject>("UI/Background");
        //            alphaBlock = Resources.Load<GameObject>("UI/alphaBlock");
        //            justBlock = Resources.Load<GameObject>("UI/justBlock");

        //            MyEventSystem.myEventSystem.RegisterEvent(MyEvent.EventType.MeshShown, OnEvent);
        //            MyEventSystem.myEventSystem.RegisterEvent(MyEvent.EventType.MeshHide, OnEvent);

        //            MyEventSystem.myEventSystem.RegisterEvent(MyEvent.EventType.ChangeScene, OnEvent);
        //            MyEventSystem.myEventSystem.RegisterEvent(MyEvent.EventType.EnterScene, OnEvent);
        //        }

        //        void OnEvent(MyEvent evt)
        //        {
        //            //界面需要显示一个Mesh对象
        //            //TODO: FakeUI在MeshShow的时候需要初始化一下装备显示
        //            Log.GUI("In Come Event window manager " + evt.type + " " + evt.intArg);
        //            if (evt.type == MyEvent.EventType.MeshShown)
        //            {
        //                FakeObjSystem.fakeObjSystem.OnUIShown(evt.intArg, evt.rolesInfo);
        //            }
        //            else if (evt.type == MyEvent.EventType.MeshHide)
        //            {
        //                FakeObjSystem.fakeObjSystem.OnUIHide(evt.intArg);
        //            }
        //            else if (evt.type == MyEvent.EventType.ChangeScene)
        //            {
        //                //切换场景需要弹出所有的UI
        //                //ClearView ();
        //            }
        //            else if (evt.type == MyEvent.EventType.EnterScene)
        //            {
        //                //进入场景，清理loading页面
        //                ClearView();
        //            }
        //        }

        //        void ClearView()
        //        {
        //            Log.GUI("Window Manager Clear View");
        //            uiRoot = null;
        //            stack.Clear();
        //            alphaStack.Clear();
        //            uiMap.Clear();
        //            back = null;
        //        }

        //        public int GetCurUILayer()
        //        {
        //            return (int)UIDepth.Window + stack.Count * 2;
        //        }

        //        //替换掉当前显示的UI类似于切换场景
        //        public GameObject ReplaceView(string viewName, bool needAlpha = true, bool destroy = true)
        //        {
        //            Log.GUI("ReplaceView");
        //            while (stack.Count > 0)
        //            {
        //                PopView(destroy);
        //            }

        //            return PushView(viewName, needAlpha);
        //        }

        public GameObject PushView(string viewName)
        {
            //Log.Important("Push UI View " + viewName + " " + needAlpha);
            if (UIRoot == null)
            {
                UIRoot = GameObject.Find("UIRoot");
            }
            //Log.GUI("UIRoot " + uiRoot);
            //if (needAlpha)
            //{
            //    if (back == null)
            //    {
            //        back = NGUITools.AddChild(uiRoot, background);
            //    }
            //    else
            //    {
            //        if (stack.Count == 0)
            //        {
            //            back.SetActive(true);
            //        }
            //    }
            //}

            GameObject bag = null;
            UIDic.TryGetValue(viewName, out bag);
            if (bag != null)
            {
                //bag.GetComponent<IUserInterface>().RegEvent();
                bag.SetActive(true);
            }
            else
            {

                //bag = Resources.Load<GameObject>(viewName);
                bag = ResourceService.Instance.InstantiateAsset<GameObject>(viewName);
                if(bag == null)
                {
                    Debug.LogError("加载失败");
                }
                //Debug.Log(UIRoot.transform.position);
                bag.transform.SetParent(UIRoot.transform);
                UIDic[viewName] = bag;
                bag.SetActive(true);
            }

            if (bag == null)
            {
                Debug.LogError("can't Find UI " + viewName);
            }
            bag.GetComponent<Canvas>().sortingOrder = UIDic.Count * 10;
            //if (needAlpha)
            //{
            //    var alpha = NGUITools.AddChild(UIRoot, alphaBlock);
            //    alpha.GetComponent<UIPanel>().depth = (int)UIDepth.Window + stack.Count * 10;
            //    alphaStack.Add(alpha);
            //}
            //else
            //{
            //    var just = NGUITools.AddChild(UIRoot, justBlock);
            //    just.GetComponent<UIPanel>().depth = (int)UIDepth.Window + stack.Count * 10;
            //    alphaStack.Add(just);
            //}

            //var allPanel = Util.GetAllPanel(bag);
            //int oldDepth = allPanel[0].depth;
            //foreach (UIPanel p in allPanel)
            //{
            //    p.depth = (int)UIDepth.Window + stack.Count * 10 + 1 + (p.depth - oldDepth);
            //}

            //bag.GetComponent<UIPanel> ().depth = (int)UIDepth.Window+stack.Count*10+1;
            stack.Add(bag);
            //Log.GUI("Push UI " + bag.name);
#if UNITY_EDITOR
            foreach (GameObject g in stack)
            {
                if (g != null)
                {
                    Debug.Log("Stack UI is " + g.name);
                }
            }
#endif
            //BackgroundSound.Instance.PlayEffect("sheet_opencenter");
            return bag;
        }
        /// <summary>
        /// 关闭a，打开b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void ChangeNotifyWithinCanvas(GameObject a,GameObject b)
        {
            if(a==null || b== null)
            {
                Debug.Log("传入目标为空，UI Erro");
                return;
            }
            if(a.name == b.name)
            {
                Debug.Log("UI Canvas 相同，无须变换");
            }
            a.SetActive(false);
            b.SetActive(true);
        }
        public void OpneNotifyWithinCanvas(GameObject a)
        {
            if (a == null)
            {
                Debug.Log("传入目标为空，UI Erro");
                return;
            }
            a.SetActive(true);
        }
        public void CloseNotifyWithinCanvas(GameObject a)
        {
            if (a == null)
            {
                Debug.Log("传入目标为空，UI Erro");
                return;
            }
            a.SetActive(false);
        }


        /// <summary>
        /// 最顶层显示一个UI界面 
        /// </summary>
        /// <returns>The top notify.</returns>
        /// <param name="viewName">View name.</param>
        //        public GameObject PushTopNotify(GameObject viewName)
        //        {
        //            if (UIRoot == null)
        //            {
        //                UIRoot = GameObject.Find("UIRoot");
        //            }
        //            if (UIRoot == null)
        //            {
        //                return null;
        //            }
        //            GameObject bag;
        //            if (UIDic.TryGetValue(viewName.name, out bag))
        //            {
        //                if (bag == null)
        //                {
        //                    Debug.LogError("can't Find UI " + viewName);
        //                    return null;
        //                }
        //                //bag.SetActive(true);
        //            }
        //            else
        //            {
        //                UIDic[viewName.ToString()] = viewName;
        //                viewName.SetActive(true);
        //            }

        //            var allPanel = Util.GetAllPanel(bag);
        //            int oldDepth = allPanel[0].depth;
        //            foreach (UIPanel p in allPanel)
        //            {
        //                p.depth = (int)UIDepth.Window + 100 + 1 + (p.depth - oldDepth);
        //            }

        //            Log.GUI("Push Notify UI " + bag.name);
        //#if UNITY_EDITOR
        //            foreach (GameObject g in stack)
        //            {
        //                if (g != null)
        //                {
        //                    Log.GUI("Stack UI is " + g.name);
        //                }
        //            }
        //#endif
        //            return bag;
        //        }

        /// <summary>
        /// 对于有大量内部状态的UI，下次打开重新构建初始化
        // 简单UI可以复用 
        /// </summary>
        /// <param name="destroy">If set to <c>true</c> destroy.</param>
        public void PopView(bool destroy = false)
        {
           // Log.Important("UI Layer " + stack.Count + " alphaCount " + alphaStack.Count + " backactive " + back);
            var top = stack[stack.Count - 1];
            stack.RemoveAt(stack.Count - 1);
            //var alpha = alphaStack[alphaStack.Count - 1];
            //alphaStack.RemoveAt(alphaStack.Count - 1);
            //if (alpha != null)
            //{
            //    GameObject.Destroy(alpha);
            //}

            if (destroy)
            {
                GameObject.Destroy(top);
            }
            else
            {
                top.SetActive(false);
            }
            //top.GetComponent<IUserInterface>().DropEvent();//Close Remove Event
            //GameObject.Destroy(top);

            //除了主UI其它UI才有Back遮挡
            //if (stack.Count == 1)
            //{
            //    if (back != null)
            //    {
            //        back.SetActive(false);
            //    }
            //}

            //BackgroundSound.Instance.PlayEffect("sheet_close");
        }

        //public void ShowNotifyLog(string text, float time = 3, System.Action<GameObject> cb = null, bool forceTime = false)
        //{
        //    Log.GUI("ShowNotifyLog " + text);
        //    NotifyUIManager.Instance.AddNotify(text, time, cb, forceTime);
        //}
        


        //弹出提示框
        //public void ShowDialog(BoolDelegate action)
        //{
        //    var g = PushView("UI/Dialog");
        //    var dia = g.GetComponent<DialogUI>();
        //    dia.SetCb(action);

        //}
    }
}
