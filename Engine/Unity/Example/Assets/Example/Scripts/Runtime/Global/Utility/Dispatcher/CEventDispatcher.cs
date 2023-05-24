using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 이벤트 전파자 */
public class CEventDispatcher : CComponent {
	#region 프로퍼티
	public System.Action<CEventDispatcher, string> EventCallback { get; set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 이벤트를 수신했을 경우 */
	public void OnReceiveEvent(string a_oEvent) {
		this.EventCallback?.Invoke(this, a_oEvent);
	}
	#endregion // 함수
}
