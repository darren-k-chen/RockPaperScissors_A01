using System;
using UnityEngine;
using UnityEngine.Events;

public class MiboEventTrigger : MonoBehaviour {

	public UnityEvent onWikiServiceStart = null;
	public UnityEvent onWikiServiceStop = null;
	public UnityEvent onWikiServiceCrash = null;
	public UnityEvent onWikiServiceRecovery = null;
	public UnityEventWithString onStartOfMotionPlay = null;
	public UnityEventWithString onPauseOfMotionPlay = null;
	public UnityEventWithString onStopOfMotionPlay = null;
	public UnityEventWithString onCompleteOfMotionPlay = null;
	public UnityEventWithString onPlayBackOfMotionPlay = null;
	public UnityEventWithInteger onErrorOfMotionPlay = null;
	public UnityEventOnPrepareMotion onPrepareMotion = null;
	public UnityEventWithString onCameraOfMotionPlay = null;
	public UnityEventWithCameraPosition onGetCameraPose = null;
	public UnityEventWithTouch onTouchBegan = null;
	public UnityEventWithTouch onTouchEnd = null;
	public UnityEventWithTouch onTap = null;
	public UnityEventWithTouch onLongPress = null;
	public UnityEventWithInteger onPIREvent = null;
	public UnityEvent onWindowSurfaceReady = null;
	public UnityEvent onWindowSurfaceDestroy = null;
	public UnityEventWithTwoIntegers onTouchEyes = null;
	public UnityEventWithFloat onFaceSpeaker = null;
	public UnityEventOnWakeUp onWakeup = null;
	public UnityEventOnMixUnderstandComplete onMixUnderstandComplete;
	public UnityEventOnLocalCommandComplete onLocalCommandComplete;
	public UnityEventOnLocalCommandException onLocalCommandException;
	public UnityEventOnGrammarState onGrammarState = null;
	public UnityEventWithBoolean onTTSComplete = null;
	public UnityEventWithTwoIntegers onActionEvent = null;
	public UnityEventWithInteger onDropSensorEvent = null;
	public UnityEventWithBoolean onConnected = null;
	public UnityEventOnOutput onOutput = null;

	[Serializable] public class UnityEventWithBoolean : UnityEvent<bool> {}
	[Serializable] public class UnityEventWithInteger : UnityEvent<int> {}
	[Serializable] public class UnityEventWithFloat : UnityEvent<float> {}
	[Serializable] public class UnityEventWithString : UnityEvent<string> {}
	[Serializable] public class UnityEventWithTwoIntegers : UnityEvent<int, int> {}
	[Serializable] public class UnityEventWithCameraPosition : UnityEvent<Mibo.CameraPosition> {}
	[Serializable] public class UnityEventWithTouch : UnityEvent<Mibo.TouchEventType> {}
	[Serializable] public class UnityEventOnPrepareMotion : UnityEvent<bool, string, float> {}
	[Serializable] public class UnityEventOnWakeUp : UnityEvent<bool, Mibo.ScoreInfoOnWakeUp, float> {}
	[Serializable] public class UnityEventOnMixUnderstandComplete : UnityEvent<bool, Mibo.ResultType, string> {}
	[Serializable] public class UnityEventOnLocalCommandComplete : UnityEvent<Mibo.VoiceRecognition> {}
	[Serializable] public class UnityEventOnLocalCommandException : UnityEvent<Mibo.ResultType, string> {}
	[Serializable] public class UnityEventOnGrammarState : UnityEvent<bool, string> {}
	[Serializable] public class GraphicRecognitionOutputDataInfo {
		public GraphicRecognitionOutputDataInfo(params Mibo.OutputData[] outputDataInfo) {
			this.outputDataInfo = outputDataInfo;
		}
		public Mibo.OutputData[] outputDataInfo;
	}
	[Serializable] public class UnityEventOnOutput : UnityEvent<GraphicRecognitionOutputDataInfo> {}

	void OnEnable() {
        
		Mibo.init();
		Mibo.onWikiServiceStart += onWikiServiceStart.Invoke;
		Mibo.onWikiServiceStop += onWikiServiceStop.Invoke;
		Mibo.onWikiServiceCrash += onWikiServiceCrash.Invoke;
		Mibo.onWikiServiceRecovery += onWikiServiceRecovery.Invoke;
		Mibo.onStartOfMotionPlay += onStartOfMotionPlay.Invoke;
		Mibo.onPauseOfMotionPlay += onPauseOfMotionPlay.Invoke;
		Mibo.onStopOfMotionPlay += onStopOfMotionPlay.Invoke;
		Mibo.onCompleteOfMotionPlay += onCompleteOfMotionPlay.Invoke;
		Mibo.onPlayBackOfMotionPlay += onPlayBackOfMotionPlay.Invoke;
		Mibo.onErrorOfMotionPlay += onErrorOfMotionPlay.Invoke;
		Mibo.onPrepareMotion += onPrepareMotion.Invoke;
		Mibo.onCameraOfMotionPlay += onCameraOfMotionPlay.Invoke;
		Mibo.onGetCameraPose += onGetCameraPose.Invoke;
		Mibo.onTouchBegan += onTouchBegan.Invoke;
		Mibo.onTouchEnd += onTouchEnd.Invoke;
		Mibo.onTap += onTap.Invoke;
		Mibo.onLongPress += onLongPress.Invoke;
		Mibo.onPIREvent += onPIREvent.Invoke;
		Mibo.onWindowSurfaceReady += onWindowSurfaceReady.Invoke;
		Mibo.onWindowSurfaceDestroy += onWindowSurfaceDestroy.Invoke;
		Mibo.onTouchEyes += onTouchEyes.Invoke;
		Mibo.onFaceSpeaker += onFaceSpeaker.Invoke;
		Mibo.onWakeup += onWakeup.Invoke;
		Mibo.onMixUnderstandComplete += onMixUnderstandComplete.Invoke;
		Mibo.onLocalCommandComplete += onLocalCommandComplete.Invoke;
		Mibo.onLocalCommandException += onLocalCommandException.Invoke;
		Mibo.onGrammarState += onGrammarState.Invoke;
		Mibo.onTTSComplete += onTTSComplete.Invoke;
		Mibo.onActionEvent += onActionEvent.Invoke;
		Mibo.onDropSensorEvent += onDropSensorEvent.Invoke;
		Mibo.onConnected += onConnected.Invoke;
		Mibo.onOutput += InvokeOnOutputEvent;
	}

	void OnDisable() {
		Mibo.onWikiServiceStart -= onWikiServiceStart.Invoke;
		Mibo.onWikiServiceStop -= onWikiServiceStop.Invoke;
		Mibo.onWikiServiceCrash -= onWikiServiceCrash.Invoke;
		Mibo.onWikiServiceRecovery -= onWikiServiceRecovery.Invoke;
		Mibo.onStartOfMotionPlay -= onStartOfMotionPlay.Invoke;
		Mibo.onPauseOfMotionPlay -= onPauseOfMotionPlay.Invoke;
		Mibo.onStopOfMotionPlay -= onStopOfMotionPlay.Invoke;
		Mibo.onCompleteOfMotionPlay -= onCompleteOfMotionPlay.Invoke;
		Mibo.onPlayBackOfMotionPlay -= onPlayBackOfMotionPlay.Invoke;
		Mibo.onErrorOfMotionPlay -= onErrorOfMotionPlay.Invoke;
		Mibo.onPrepareMotion -= onPrepareMotion.Invoke;
		Mibo.onCameraOfMotionPlay -= onCameraOfMotionPlay.Invoke;
		Mibo.onGetCameraPose -= onGetCameraPose.Invoke;
		Mibo.onTouchBegan -= onTouchBegan.Invoke;
		Mibo.onTouchEnd -= onTouchEnd.Invoke;
		Mibo.onTap -= onTap.Invoke;
		Mibo.onLongPress -= onLongPress.Invoke;
		Mibo.onPIREvent -= onPIREvent.Invoke;
		Mibo.onWindowSurfaceReady -= onWindowSurfaceReady.Invoke;
		Mibo.onWindowSurfaceDestroy -= onWindowSurfaceDestroy.Invoke;
		Mibo.onTouchEyes -= onTouchEyes.Invoke;
		Mibo.onFaceSpeaker -= onFaceSpeaker.Invoke;
		Mibo.onWakeup -= onWakeup.Invoke;
		Mibo.onMixUnderstandComplete -= onMixUnderstandComplete.Invoke;
		Mibo.onLocalCommandComplete -= onLocalCommandComplete.Invoke;
		Mibo.onLocalCommandException -= onLocalCommandException.Invoke;
		Mibo.onGrammarState -= onGrammarState.Invoke;
		Mibo.onTTSComplete -= onTTSComplete.Invoke;
		Mibo.onActionEvent -= onActionEvent.Invoke;
		Mibo.onDropSensorEvent -= onDropSensorEvent.Invoke;
		Mibo.onConnected -= onConnected.Invoke;
		Mibo.onOutput -= InvokeOnOutputEvent;
	}

	void InvokeOnOutputEvent(Mibo.OutputData[] outputData) {
		onOutput.Invoke(new GraphicRecognitionOutputDataInfo(outputData));
	}
}