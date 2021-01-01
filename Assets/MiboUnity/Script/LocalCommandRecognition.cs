using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LocalCommandRecognition : MonoBehaviour {


	public string mi_Name;
    //	public string mi_Tag;
    public BnfDataClip[] clips;
	public Action<Mibo.VoiceRecognition> trueEvent;
    public Action<Mibo.ResultType, string> falseEvent;

    private BnfDataClip[] clips2, clips3;
    Mibo.BnfData mBnfData;

    private bool isInit = false;

	void Awake () {
		Mibo.init();
        clips2 = clips;
        clips3 = clips;

    }
    // Use this for initialization
    int idx = 0;

	void Init(){
		if(clips == null ) {
			Debug.Log( "參數設置錯誤不執行Init&StartEvent" );
			return;
		}
		isInit = true;
        //Mibo.onGrammarState += OnGrammarState;
        //Mibo.BnfData mBnfData = new Mibo.BnfData( mi_Name );
        //      foreach (BnfDataClip clip in clips)
        //      {
        //          Debug.Log("add value : " + clip.values);
        //          mBnfData.addSlot(clip.mi_Tag, clip.isRequired, clip.cmdKey, clip.values);
        //      }
        //      mBnfData.updateBody();
        //Mibo.createCrammer( mi_Name , mBnfData.body);
        SetClip();

    }

    private void SetClip()
    {
        idx++;
        BnfDataClip[] clipsData = this.clips;
        if(idx == 2)
        {
            clipsData = this.clips2;
        }
        else if(idx > 2)
        {
            clipsData = this.clips3;
        }

        Mibo.onGrammarState += OnGrammarState;
        if(mBnfData == null)
        {
            mBnfData = new Mibo.BnfData(mi_Name);
        }
        
        foreach (BnfDataClip clip in clips)
        {
            Debug.Log(">>>> BnfDataClip add value : " + String.Join(";", clip.values) );
            mBnfData.addSlot(clip.mi_Tag, clip.isRequired, clip.cmdKey, clip.values);
        }
        mBnfData.updateBody();
        Mibo.createCrammer(mi_Name, mBnfData.body);

        Debug.Log("SetClip==");
    }

	void OnGrammarState(bool isError, string info) {
        //SetClip();

        Mibo.startLocalCommand();
	}


    public void VoiceRecognition__Register(){
		if( !isInit ) Init();
		Mibo.onLocalCommandComplete += TrueFunction;
		Mibo.onLocalCommandException += FalseFunction;
	}

	public void VoiceRecognition__Remove() {
		Mibo.onLocalCommandComplete -= TrueFunction;
		Mibo.onLocalCommandException -= FalseFunction;
        Mibo.onGrammarState -= OnGrammarState;

    }

	void TrueFunction(Mibo.VoiceRecognition recognitionInfo) {
		trueEvent.Invoke(recognitionInfo);
        Mibo.stopListen();
    }

	void FalseFunction(Mibo.ResultType resultType, string json) {
		falseEvent.Invoke(resultType,json);
        Mibo.stopListen();
    }
}

/// <summary>
/// 使用BNF來存語音命令資料
/// </summary>
[System.Serializable]
public class BnfDataClip
{
    /// <summary>
    /// tag: slot name in BNF file
    /// </summary>
	public string mi_Tag;
    /// <summary>
    /// isRequired: the slot is optional in BNF file。假如為True的話，在LocalCommand中一定需要念這邊的文字才能正確使用，為False的話可以不念
    /// </summary>
	public bool isRequired;
    /// <summary>
    /// cmdKey: always set as "1"
    /// </summary>
	public int cmdKey;
    /// <summary>
    /// cmds: commands in slot , 裡面放語音命令用文字
    /// </summary>
	public string[] values;
}
