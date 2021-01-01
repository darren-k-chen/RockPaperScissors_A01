using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceDetectMain : MonoBehaviour {

    private Mibo.FaceRecognizeData[] mData = null;
    private bool mIsNeedCheck = false;

    public Text InfoText;
    private bool RecognizeAnswerClick = false;

    // Use this for initialization
    void Awake()
    {
        Mibo.init();
    }


    public void isConnectRecognizeSystem(bool isConnected)
    {
        if (isConnected)
        {
            Mibo.onFaceRecognize += ReconizeCheck;
        }
    }


    public void StartRecognizeClick()
    {
        Mibo.onConnected += isConnectRecognizeSystem;
        Mibo.startRecognition(Mibo.MiboRecognition.FACE_RECOGNITION);
        RecognizeAnswerClick = true;
        InfoText.text = "Start Recognize";
    }



    public void UnRegisterProcess()
    {
        Mibo.stopRecognition();
        Mibo.onFaceRecognize -= ReconizeCheck;
        Mibo.onConnected -= isConnectRecognizeSystem;
        mIsNeedCheck = false;

        InfoText.text += "\nRecognizeSystem Disconnected";
    }

    public void RetuenToTitle()
    {
        UnRegisterProcess();
        UnityEngine.SceneManagement.SceneManager.LoadScene(ESceneConfig.Demo_Title.ToString());
    }



    // ReconizeCheck is called once per frame
    public void ReconizeCheck(Mibo.FaceRecognizeData[] data)
    {
        if(RecognizeAnswerClick)
        {
            mData = data;
            mIsNeedCheck = true;
        }
    }


    private void Update()
    {
        if (mIsNeedCheck)
        {
            InfoText.text += "\nRecognizeSystem Connected ,setInfo";
            mIsNeedCheck = false;
            SetInfoData(mData);
        }
    }

    private void SetInfoData(Mibo.FaceRecognizeData[] data)
    {
        if(data != null)
        {
            for(int i = 0; i < data.Length; i++)
            {
                InfoText.text += "\nid:" + data[i].idx + ", name:" + data[i].name + ", conf:" + data[i].conf + ", rect:" + JsonUtility.ToJson(data[i].rect);
            }
        }
        Debug.Log("FaceDetectMain, SetInfoData finish, UnRegisterProgress");

        UnRegisterProcess();
    }

}
