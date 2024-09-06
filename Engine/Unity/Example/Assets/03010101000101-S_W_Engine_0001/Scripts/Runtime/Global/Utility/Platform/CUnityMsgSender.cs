using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 유니티 메세지 전송자 */
public class CUnityMsgSender : CSingleton<CUnityMsgSender>
{
	#region 변수
#if UNITY_EDITOR || UNITY_ANDROID
	private AndroidJavaClass m_oJavaClass = null;
	private AndroidJavaObject m_oJavaObject = null;
#endif // #if UNITY_EDITOR || UNITY_ANDROID
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

#if UNITY_EDITOR || UNITY_ANDROID
		m_oJavaClass = new AndroidJavaClass("sdlee.android.CAndroidPlugin");
		m_oJavaObject = m_oJavaClass.GetStatic<AndroidJavaObject>("m_oInst");
#endif // #if UNITY_EDITOR || UNITY_ANDROID
	}

	/** 디바이스 식별자 반환 메세지를 전송한다 */
	public void SendGetDeviceIDMsg(System.Action<string> a_oCallback)
	{
		this.SendUnitMsgToAndroid("GetDeviceID", string.Empty, a_oCallback);
	}

	/** 토스트 출력 메세지를 전송한다 */
	public void SendShowToastMsg(string a_oMsg)
	{
		this.SendUnitMsgToAndroid("ShowToast", a_oMsg, null);
	}

	/** 유니티 메세지를 안드로이드에 전송한다 */
	private void SendUnitMsgToAndroid(string a_oCmd,
		string a_oMsg, System.Action<string> a_oCallback)
	{
		// 콜백이 존재 할 경우
		if(a_oCallback != null)
		{
			CDeviceMsgReceiver.Inst.AddCallback(a_oCmd, a_oCallback);
		}

#if UNITY_EDITOR || UNITY_ANDROID
		m_oJavaObject.Call("HandleUnitMsg", a_oCmd, a_oMsg);
#endif // #if UNITY_EDITOR || UNITY_ANDROID
	}
	#endregion // 함수
}
