using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class IUserInterface : MonoBehaviour
{
    public static GameObject UIRoot = null;
    public GameObject GetName(string name)
    {
        var t = transform.Find(name);
        if (t == null)
        {
            Debug.LogError("GetName Error :: " + name + " " + gameObject);
            return null;
        }
        return t.gameObject;
    }

    public GridLayoutGroup GetGrid(string name)
    {
        return GetName(name).GetComponent<GridLayoutGroup>();
    }
    public InputField GetInput(string name)
    {
        return GetName(name).GetComponent<InputField>();
    }

    //public UISprite GetSprite(string name)
    //{
    //    return GetName(name).GetComponent<UISprite>();
    //}
    //public UILabel GetLabel(string name)
    //{
    //    var g = GetName(name);
    //    return g.GetComponent<UILabel>();
    //}
    public Slider GetSlider(string name)
    {
        var g = GetName(name);
        return g.GetComponent<Slider>();
    }

    public void SetgameobjectActive(GameObject gameObject,bool flag)
    {
        gameObject.SetActive(flag);
    }
    public void SetButtonCallback(Button button, UnityAction call)
    {
        button.onClick.AddListener(call);
        //UIEventListener.Get(GetName(name)).onClick = callback;
    }

    //public void SetCallback(string name, UIEventListener.EmptyDelegate callback)
    //{
    //    UIEventListener.Get(GetName(name)).onClick = delegate (GameObject go)
    //    {
    //        callback();
    //    };
    //}

    //public void SetCheckBox(string name, BoolDelegate cb)
    //{
    //    var tog = GetName(name).GetComponent<UIToggle>();

    //    EventDelegate.Add(tog.onChange, delegate
    //    {
    //        cb(tog.value);
    //    });
    //}


    //public void SetList(string name, StringDelegate cb)
    //{
    //    var pop = GetName(name).GetComponent<UIPopupList>();
    //    EventDelegate.Add(pop.onChange, delegate
    //    {
    //        cb(pop.value);
    //    });
    //}
    //public UIPopupList GetList(string name)
    //{
    //    return GetName(name).GetComponent<UIPopupList>();
    //}

    public static GameObject GetRoot()
    {
        if (UIRoot == null)
        {
            UIRoot = GameObject.FindGameObjectWithTag("UIRoot");
        }
        return UIRoot;
    }
    //public void SetButtonIcon(string butName, int sheet, string name)
    //{
    //    Log.Important("SetIcon " + butName + " " + sheet + " " + name);
    //    var sp = GetName(butName);
    //    var icon = Util.FindChildRecursive(sp.transform, "icon").GetComponent<UISprite>();
    //    Util.SetIcon(icon, sheet, name);
    //}

    //protected void SetIcon(string iconName, int sheet, string name)
    //{
    //    Log.Important("SetIcon " + iconName + " " + sheet + " " + name);
    //    var sp = GetName(iconName).GetComponent<UISprite>();
    //    Util.SetIcon(sp, sheet, name);
    //}
    //protected void Hide(GameObject g)
    //{
    //    //GameObject.Destroy (gameObject);
    //    gameObject.SetActive(false);
    //    WindowMng.windowMng.PopView();
    //}
    protected void OnlyHide()
    {
        gameObject.SetActive(false);
    }



    //public void resetScrollViewPosition(Transform tra, int _x, int _y)
    //{
    //    tra.transform.localPosition = new Vector3(_x, _y, 0);
    //    UIPanel p = tra.GetComponent<UIPanel>();
    //    p.clipOffset = new Vector2(0, 0);
    //}

    //public void GridSet(Transform trans)
    //{
    //    UIGrid grid = trans.GetComponent<UIGrid>();
    //    grid.repositionNow = true;
    //}

    public static void SetText(GameObject g, string name, string text)
    {
        var lab = (g.transform, name);
        //lab.GetComponent<UILabel>().text = text;
    }
}
