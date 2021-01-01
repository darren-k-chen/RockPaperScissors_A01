using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MovementMain : MonoBehaviour {

    //[Range(-0.2f, 0.2f)]
    public Slider MoveSlider;
    public Text MoveSliderText;

    //[Range(-20, 20)]
    public Slider TurnSlider;
    public Text TurnSliderText;

    public Text LockWheelText;
    private string mLockWheelText = "LockWheel:";

	// Use this for initialization
	void Start () {
        Mibo.init();

        MoveSlider.onValueChanged.AddListener(
            delegate (float value) { MoveSliderText.text = value.ToString(); }
            );

        TurnSlider.onValueChanged.AddListener(
            delegate (float value) { TurnSliderText.text = value.ToString(); }
            );

        LockWheel();
    }




    #region Move
    public void OnMoveButtonClick()
    {
        Debug.Log("OnMoveButtonClick, value:"+MoveSlider.value);
        Mibo.SetMove(MoveSlider.value);
    }

    public void OnStopMoveButtonClick()
    {
        Mibo.SetMove(0);
    }

    public void MoveForwardInAccelerationEx()
    {
        Mibo.MoveForwardInAccelerationEx();
    }

    public void MoveBackInAccelerationEx()
    {
        Mibo.MoveBackInAccelerationEx();
    }

    public void StopInAcclerationEx()
    {
        Mibo.StopInAcclerationEx();
    }

    #endregion Move

    #region Turn


    public void OnTurnButtonClick()
    {
        Debug.Log("OnTurnButtonClick, value:" + TurnSlider.value);
        Mibo.SetTurn(TurnSlider.value);
    }


    public void OnStopTurnButtonClick()
    {
        Mibo.SetTurn(0);
    }

    public void TurnLeftEx()
    {
        Mibo.TurnLeftEx();
    }

    public void TurnRightEx()
    {
        Mibo.TurnRightEx();
    }

    public void StopTurnEx()
    {
        Mibo.StopTurnEx();
    }

    #endregion turn


    #region Wheel

    public void LockWheel()
    {
        Mibo.LockWheel();
        LockWheelText.text = mLockWheelText + "true";
    }

    public void UnlockWheel()
    {
        Mibo.UnlockWheel();
        LockWheelText.text = mLockWheelText + "false";
    }


    #endregion Wheel

    public void OnReturnButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(ESceneConfig.Demo_Title.ToString());

        Mibo.SetMove(0);
        Mibo.StopInAcclerationEx();
        Mibo.SetTurn(0);
        Mibo.StopTurnEx();
    }
}
