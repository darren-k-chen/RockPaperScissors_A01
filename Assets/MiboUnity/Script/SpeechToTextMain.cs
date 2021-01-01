using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechToTextMain : MonoBehaviour {


    public Text ouputTxt;
    private bool mNeedChange = false;
    private string mJsonString = string.Empty;

	void Start () {
        Mibo.init();
        Mibo.onSpeech2TextComplete += SpeechCallback;
    }


   
    private void OnDisable()
    {
        Mibo.onSpeech2TextComplete -= SpeechCallback;
    }


    /// <summary>
    /// 設置聆聽類型的參數
    /// </summary>
    public void SetupLeistenParameter()
    {
        Mibo.setListenParameter(Mibo.ListenType.RECOGNIZE, "language", "en_us");
        Mibo.setListenParameter(Mibo.ListenType.RECOGNIZE, "accent", null);
    }

    /// <summary>
    /// 開始聆聽轉換成文字
    /// </summary>
    public void StartSpeech2Text()
    {
        SetupLeistenParameter();
        Mibo.startSpeech2Text(false);
        mJsonString = "StartSpeechToText\n";
        mNeedChange = true;
    }



    /// <summary>
    /// 聲音回傳的callback
    /// </summary>
    /// <param name="isError"></param>
    /// <param name="json"></param>
    private void SpeechCallback(bool isError, string json)
    {
        if (!isError)
        {
            mJsonString = "FinishSpeechToText, length:"+ json.Length +"\n";
            mJsonString += json;
            mNeedChange = true;
            //ouputTxt.text = json;
        }
    }

    public void RetuenToTitle()
    {
        this.enabled = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(ESceneConfig.Demo_Title.ToString());
    }

    private void Update()
    {
        if(mNeedChange)
        {
            mNeedChange = false;
            ouputTxt.text = mJsonString;
        }
    }

}
