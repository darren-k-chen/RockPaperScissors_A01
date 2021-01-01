using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class TTsPlayMain : MonoBehaviour {

    public string[] TTsArr = new string[] {
       
    };

    public Dropdown TTSDropdown;

    public Text logText;

    public InputField InputFieldText;

    private void Start()
    {
        TTSDropdown.ClearOptions();
        TTSDropdown.AddOptions(TTsArr.ToList());

        MiboEventTrigger trigger = this.GetComponent<MiboEventTrigger>();
        if(trigger != null)
        {
            trigger.onTTSComplete.AddListener(OnTTsComplete);
        }
    }

    private void OnTTsComplete(bool isError)
    {
        Debug.Log("TTsPlay , OnTTsComplete===");
        logText.text += "\n OnTTsComplete, isError:"+ isError;
    }

    public void TTsPlay()
    {
        int idx = TTSDropdown.value;
        Mibo.startTTS(TTsArr[idx]);
        logText.text = "Play tts";
    }

    public void TTsStop()
    {
        Mibo.stopTTS();
        logText.text += "\nStopTTs";
    }

    public void TTsResume()
    {
        Mibo.resumeTTS();
        logText.text += "\nResumeTTs";
    }

    public void TTsPause()
    {
        Mibo.pauseTTS();
        logText.text += "\nPauseTTs";
    }

    public void OnReturnButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(ESceneConfig.Demo_Title.ToString());
    }

    public void PlayInputTTs()
    {
        String text = InputFieldText.text;
        Mibo.startTTS(text);
    }

    public void PlayMotion()
    {
        Mibo.motionPlay("888_ML_AiYA_10");
    }

    public void PlayMotor()
    {
        int id = UnityEngine.Random.Range(3, 8);
        int range = UnityEngine.Random.Range(-30, 30);
        Mibo.setMotorPositionInDegree(id, range, 10);
    }
}
