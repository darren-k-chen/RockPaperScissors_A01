using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecognizeMain : MonoBehaviour {
    public bool RecognizeAnswerClick; //給辦識按鈕用的

    private Mibo.OutputData[] mData = null;
    private bool mIsNeedCheck = false;

    public Text InfoText;

    // Use this for initialization
    void Awake () {
        Mibo.init ();
	}


    public void isConnectRecognizeSystem (bool isConnected) {
        if ( isConnected ) {
            Mibo.onOutput+=ReconizeCheck;
        }
    }

    public void StartRecognizeClick () {
        Mibo.onConnected += isConnectRecognizeSystem;
        Mibo.startRecognition(Mibo.MiboRecognition.OBJ);
        RecognizeAnswerClick = true;
        InfoText.text = "Start Recognize";
    }

	// ReconizeCheck is called once per frame
    public void ReconizeCheck (Mibo.OutputData[] data)
    {
        Debug.Log("ReconizeCheck, RecognizeAnswerClick:" + RecognizeAnswerClick);
        if ( RecognizeAnswerClick ) {
            for(int i = 0; i < data.Length; i++)
            {
                for(int j = 0; j < data[i].dataSets.Length; j++)
                {
                    Debug.Log("ReconizeCheck," + i + ", " + j + "," + data[i].dataSets[j].ToString());
                }
            }

            mData = data;
            mIsNeedCheck = true;
        }
	}

    private void UnRegisterProcess () {
        Mibo.stopRecognition ();
        Mibo.onOutput-=ReconizeCheck;
        Mibo.onConnected-=isConnectRecognizeSystem;

        InfoText.text += "\nRecognizeSystem Disconnected";
    }

    public void RetuenToTitle()
    {
        UnRegisterProcess();
        UnityEngine.SceneManagement.SceneManager.LoadScene(ESceneConfig.Demo_Title.ToString());
    }

    private void Update()
    {
        if(mIsNeedCheck)
        {
            InfoText.text += "\nRecognizeSystem Connected ,setInfo";
            mIsNeedCheck = false;
            SetInfoData(mData);
        }
    }


    private void SetInfoData(Mibo.OutputData[] data)
    {
        for(int i = 0; i< data.Length; i++)
        {
            for(int j = 0; j < data[i].dataSets.Length; j++)
            {
                InfoText.text += "\n i:" + i + ", j:" + j + ", " + data[i].dataSets[j].title;
            }
        }
        UnRegisterProcess();
    }
}
