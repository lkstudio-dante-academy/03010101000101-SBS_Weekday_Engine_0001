using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

/** 네트워크 관리자 */
public class CE20NetworkManager : CSingleton<CE20NetworkManager> {
	#region 변수
	private Socket m_oSocket = null;
	private Thread m_oWorkerThread = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Start() {
		base.Start();

		m_oWorkerThread = new Thread(this.WorkerMain);
		m_oWorkerThread.Start();
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy() {
		base.OnDestroy();
		m_oWorkerThread.Abort();
	}

	/** 매칭 요청을 보낸다 */
	public void SendMatchingRequest(System.Action<CE20NetworkManager, bool> a_oCallback) {
		// TODO: 서버 기반 통신 구조로 변경 필요
		a_oCallback?.Invoke(this, true);
	}

	/** 작업자 쓰레드 메인 메서드 */
	private void WorkerMain() {
		do {
			// Do Something
		} while(true);
	}
	#endregion // 함수
}
