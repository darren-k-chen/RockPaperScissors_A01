using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class MotionPlayMain : MonoBehaviour {

    private string[] mMotionNameArr = new string[] {
        "888_ML_AiYA_10",
        "888_ML_Cough_10",
        "888_ML_ThrowPotato_10",
        "888_ML_HandsUp_14",
        "888_ML_PlugIceCr_18",
        "888_ML_Putdown_04"
    };

    public Dropdown MotionDropDown;

    public Text LogText;


	// Use this for initialization
	void Start ()
    {
        MotionDropDown.ClearOptions();
        MotionDropDown.AddOptions(mMotionNameArr.ToList());

        Mibo.onCompleteOfMotionPlay += OnCompleteMotionPlay;
        Mibo.onStartOfMotionPlay += OnStartMotionPlay;
        Mibo.onStopOfMotionPlay += OnStopMotionPlay;
        Mibo.onPauseOfMotionPlay += OnPauseMotionPlay;
    }



    public void PlayMotion()
    {
        string motionName = mMotionNameArr[MotionDropDown.value];
        if(motionName.Length > 0)
        {
            Mibo.motionPlay(motionName);

            LogText.text = "PlayMotion:" + motionName;
        }
    }

    public void StopMotion()
    {
        Mibo.motionStop();
        LogText.text += "\nStopMotion";
    }

    public void PauseMotion()
    {
        Mibo.motionPause();
        LogText.text += "\nPauseMotion";
    }

    public void ResumeMotion()
    {
        Mibo.motionResume();
        LogText.text += "\nResumeMotion";
    }

    public void OnCompleteMotionPlay(string info)
    {
        LogText.text += "\n OnCompleteMotionPlay ";
    }

    private void OnPauseMotionPlay(string obj)
    {
        LogText.text += "\n OnPauseMotionPlay ";
    }

    private void OnStopMotionPlay(string obj)
    {
        LogText.text += "\n OnStopMotionPlay ";
    }

    private void OnStartMotionPlay(string obj)
    {
        LogText.text += "\n OnStartMotionPlay ";
    }

    public void OnReturnButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(ESceneConfig.Demo_Title.ToString());
    }
}
