using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchMain : MonoBehaviour {

    public Text InfoText;
    private List<Mibo.TouchEventType> mTouchBeginList, mTouchEndList, mTapList, mLongPressList;

    private string mInfo = string.Empty;

    private void Awake()
    {
        Mibo.init();
    }

    // Use this for initialization
    void Start()
    {

        mTouchBeginList = new List<Mibo.TouchEventType>();
        mTouchEndList = new List<Mibo.TouchEventType>();
        mTapList = new List<Mibo.TouchEventType>();
        mLongPressList = new List<Mibo.TouchEventType>();

        MiboEventTrigger trigger = this.GetComponent<MiboEventTrigger>();
        if (trigger != null)
        {
            trigger.onTouchBegan.AddListener(OnTouchBegin);
            trigger.onTouchEnd.AddListener(OnTouchEnd);
            trigger.onTap.AddListener(OnTap);
            trigger.onLongPress.AddListener(OnLongPress);
        }
    }


    public void OnTouchBegin(Mibo.TouchEventType type)
    {
        // add
        mTouchBeginList.Add(type);

        //remove
        mTouchEndList.Remove(type);
        mLongPressList.Remove(type);
        mTapList.Remove(type);

        SetInfo();
    }

    public void OnTouchEnd(Mibo.TouchEventType type)
    {
        //add
        mTouchEndList.Add(type);

        //remove
        mTouchBeginList.Remove(type);

        SetInfo();
    }

    public void OnTap(Mibo.TouchEventType type)
    {
        mTapList.Add(type);
        SetInfo();
    }

    public void OnLongPress(Mibo.TouchEventType type)
    {
        mLongPressList.Add(type);
        SetInfo();
    }

    private void SetInfo()
    {
        string touchBegin = "TouchBegin:";
        string touchEnd = "TouchEnd:";
        string tap = "Tap:";
        string longPress = "LongPress:";

        mInfo = SetString(mTouchBeginList, touchBegin)
            + "\n" + SetString(mTouchEndList, touchEnd)
            + "\n" + SetString(mTapList, tap)
            + "\n" + SetString(mLongPressList, longPress)
            ;
    }

    private string SetString(List<Mibo.TouchEventType> list, string input)
    {
        for (int i = 0; i < list.Count; i++)
            input += list[i].ToString() + ",";
        return input;
    }

    public void OnReturnButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(ESceneConfig.Demo_Title.ToString());
    }

    private void Update()
    {
        if(mInfo.Length > 0)
        {
            InfoText.text = mInfo;
            mInfo = string.Empty;
        }
    }

}
