using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** Example 22 */
public class CE01Example_22 : CSceneManager
{
	#region 변수
	[SerializeField] private Text m_oDeviceIDText = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_22;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
	}

	/** 디바이스 식별자 반환 버튼을 눌렀을 경우 */
	public void OnTouchGetDeviceIDBtn()
	{
		CUnityMsgSender.Inst.SendGetDeviceIDMsg(this.OnReceiveDeviceID);
	}

	/** 토스트 메세지 출력 버튼을 눌렀을 경우 */
	public void OnTouchShowToastMsgBtn()
	{
		CUnityMsgSender.Inst.SendShowToastMsg("Toast Message");
	}

	/** 디바이스 식별자를 수신했을 경우 */
	private void OnReceiveDeviceID(string a_oMsg)
	{
		m_oDeviceIDText.text = a_oMsg;
	}
	#endregion // 함수
}
