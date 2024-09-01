using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/** 디바이스 메세지 수신자 */
public class CDeviceMsgReceiver : CSingleton<CDeviceMsgReceiver>
{
	#region 변수
	private Dictionary<string, System.Action<string>> m_oCallbackDict = new Dictionary<string, System.Action<string>>();
	#endregion // 변수

	#region 함수
	/** 콜백을 추가한다 */
	public void AddCallback(string a_oKey, System.Action<string> a_oCallback)
	{
		// 콜백이 존재 할 경우
		if(m_oCallbackDict.ContainsKey(a_oKey))
		{
			return;
		}

		m_oCallbackDict.Add(a_oKey, a_oCallback);
	}

	/** 콜백을 제거한다 */
	public void RemoveCallback(string a_oKey)
	{
		// 콜백이 존재 할 경우
		if(m_oCallbackDict.ContainsKey(a_oKey))
		{
			m_oCallbackDict.Remove(a_oKey);
		}
	}

	/** 디바이스 메세지를 처리한다 */
	public void HandleDeviceMsg(string a_oDeviceMsg)
	{
		var oMsgInfo = JObject.Parse(a_oDeviceMsg);

		string oCmd = (string)oMsgInfo["Cmd"];
		string oMsg = (string)oMsgInfo["Msg"];

		m_oCallbackDict.GetValueOrDefault(oCmd)?.Invoke(oMsg);
	}
	#endregion // 함수
}
