using UnityEngine;

/// <summary>
/// Android java utility.
/// </summary>
public static class AndroidJavaUtility {

	/// <summary>
	/// Gets current activity of the application on android device.
	/// </summary>
	/// <value>Current activity.</value>
	public static AndroidJavaObject currentActivity {
		get {
#if UNITY_ANDROID && !UNITY_EDITOR
			if (_currentActivity == null) {
				_currentActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			}
#endif
			return _currentActivity;
		}
	}

	static AndroidJavaObject _currentActivity = null;
}
