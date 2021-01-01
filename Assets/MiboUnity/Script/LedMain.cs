using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LedMain : MonoBehaviour {


    private List<Color> mColorList = new List<Color>();
    private List<Mibo.LEDPosition> mTypeList = new List<Mibo.LEDPosition>();

    public Dropdown ColorDropDown;

    public Dropdown TypeDropDown;

    private int mColorIdx = 0;
    private int mTypeIdx = 0;

    private int mBreathValue = 3;

    public Text BreathSpeedText;
    public Slider BreathSlider;

    // Use this for initialization
    void Start () {
        Mibo.init();

        TypeDropDown.ClearOptions();
        mTypeList.Add(Mibo.LEDPosition.Head);
        mTypeList.Add(Mibo.LEDPosition.Chest);
        mTypeList.Add(Mibo.LEDPosition.LeftHand);
        mTypeList.Add(Mibo.LEDPosition.RightHand);
        List<string> dataList = new List<string>();
        for (int i = 0; i < mTypeList.Count; i++)
            dataList.Add(mTypeList[i].ToString());
        TypeDropDown.AddOptions(dataList);

        ColorDropDown.ClearOptions();
        mColorList.Add(Color.red);
        mColorList.Add(Color.blue);
        mColorList.Add(Color.green);
        mColorList.Add(Color.yellow);
        mColorList.Add(Color.black);
        mColorList.Add(Color.gray);
        dataList.Clear();

        dataList.Add("Red");
        dataList.Add("Blue");
        dataList.Add("Green");
        dataList.Add("Yellow");
        dataList.Add("black");
        dataList.Add("gray");
        ColorDropDown.AddOptions(dataList);

        TypeDropDown.onValueChanged.AddListener(OnTypeChange);
        ColorDropDown.onValueChanged.AddListener(OnColorChange);


        BreathSlider.onValueChanged.AddListener(OnSliderValueChange);

        SetSppedText();
    }

    private void OnTypeChange(int value)
    {
        mTypeIdx = value;
    }

    private void OnColorChange(int value)
    {
        mColorIdx = value;
    }


    public void EnableLED()
    {
        Mibo.disableSystemLED();//app自由控制燈
        Mibo.setLedColor(mTypeList[mTypeIdx] , mColorList[mColorIdx]);
        Mibo.enableLed(mTypeList[mTypeIdx], true);
    }

    public void DisableLed()
    {

        for (int idx = 0; idx < mTypeList.Count; idx++)
            Mibo.enableLed(mTypeList[idx], false);
        Mibo.enableSystemLED(); //離開app時讓系統來管理燈
    }

    public void RetuenToTitle()
    {
        DisableLed();
        UnityEngine.SceneManagement.SceneManager.LoadScene(ESceneConfig.Demo_Title.ToString());
    }

    private void OnSliderValueChange(float value)
    {
        mBreathValue = (int)value;
        SetSppedText();
    }

    public void SetLedBreath()
    {
        Mibo.enableLedBreath(mTypeList[mTypeIdx], mBreathValue, mBreathValue);
    }

    private void SetSppedText()
    {
        BreathSpeedText.text = "BreathSpeed:" + mBreathValue.ToString();
    }
}
