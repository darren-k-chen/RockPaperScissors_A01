using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTrackSystem : MonoBehaviour {

    public bool isRecognization = false;
    public bool initRecog = false;
    

    public Vector2 FaceOriginPos;
    public Vector2 FaceCenterPos;
    public Vector2 FaceOriginSize;
    public Vector2 FaceFixedPos;


    private void Awake()
    {
        Mibo.init();
        isRecognization = false;
    }

    public void StartRecognize()
    {
        initRecog = true;
        Mibo.startRecognition(Mibo.MiboRecognition.FACE_TRACK);
    }
    public void StopRecognize()
    {
        if(isRecognization)
        Mibo.stopRecognition();
    }

    public void ReturnToMenu()
    {
        if (initRecog) return;

        UnRegisterProcess();
        UnityEngine.SceneManagement.SceneManager.LoadScene(ESceneConfig.Demo_Title.ToString());
    }

    private void UnRegisterProcess()
    {
        if (initRecog) return;

        StopRecognize();
    }

    void GetTrackData(Mibo.TrackData[] data)
    {
        Debug.Log("GetTrackData");
        if (initRecog) initRecog = false;

        if (data == null)
        {
            isRecognization = false;
            FaceOriginPos = Vector2.zero;
            FaceOriginSize = Vector2.zero;
        }
        else
        {
            isRecognization = true;
            float _x  = float.Parse(data[0].x); //只攔截第一人
            float _y = float.Parse(data[0].y);
            float _w = float.Parse(data[0].width);
            float _h = float.Parse(data[0].height);
            FaceOriginPos = new Vector2(_x, _y);
            FaceOriginSize = new Vector2(_w, _h);

            FaceCenterPos = FaceOriginPos + (FaceOriginSize / 2f);
        }

    }

    private void OnEnable()
    {
        Mibo.onTrack += GetTrackData;
    }

    private void OnDisable()
    {
        StopRecognize();
        Mibo.onTrack -= GetTrackData;
    }
    
}
