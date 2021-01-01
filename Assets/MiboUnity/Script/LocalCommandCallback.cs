using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LocalCommandCallback : MonoBehaviour {

    public LocalCommandRecognition miboVoice;
    
    public Text txt;
    bool isFirst = true;

    private void Start()
    {
        if (miboVoice == null)
            return;

        miboVoice.trueEvent += MiboTrueFunction;
        miboVoice.falseEvent += MiboFalseFunction;
      
    }
   
    public void StartVoice()
    {
        if (isFirst)
        {
            Debug.Log(" VoiceRecognition__Register");
            miboVoice.VoiceRecognition__Register();
            isFirst = false;
        }
        else
        {
            Debug.Log("startLocalCommand");
            Mibo.startLocalCommand();
        }

        mIsNeedUpdate = true;
        mResultText = "開始辨識";
    }

    private bool mIsNeedUpdate = false;
    private string mResultText = string.Empty;

    void MiboTrueFunction(Mibo.VoiceRecognition recognitionInfo)
    {
        mIsNeedUpdate = true;
        mResultText += "\n辨識正確 :  " + recognitionInfo.ToString();
    }


    void MiboFalseFunction(Mibo.ResultType resultType, string json)
    {
        mIsNeedUpdate = true;
        mResultText += "\n辨識錯誤 :  " + '\n' + "resultType :["+ resultType.ToString() + "]  json : " +json;
    }

    public void RetuenToTitle()
    {
        Mibo.stopListen();
        UnityEngine.SceneManagement.SceneManager.LoadScene(ESceneConfig.Demo_Title.ToString());
    }

    public void Update()
    {
        if(mIsNeedUpdate)
        {
            mIsNeedUpdate = false;
            txt.text = mResultText;
        }
    }

}
