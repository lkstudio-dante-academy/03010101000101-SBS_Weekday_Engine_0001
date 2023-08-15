using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/** Example 18 */
public class CE18SceneManager : CSceneManager {
	#region 변수
	private Thread m_oServerThread = null;
	private Thread m_oClientThread = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E18;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
	}

	/** 초기화 */
	public override void Start() {
		base.Start();

		m_oServerThread = new Thread(this.ServerMain);
		m_oClientThread = new Thread(this.ClientMain);

		m_oServerThread.Start();
		m_oClientThread.Start();
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy() {
		base.OnDestroy();

		m_oServerThread.Abort();
		m_oClientThread.Abort();
	}

	/** 서버 메인 메서드 */
	private void ServerMain() {
		for(int i = 0; i < 10000; ++i) {
			Debug.Log($"Server : {i}");
		}
	}

	/** 클라이언트 메인 메서드 */
	private void ClientMain() {
		for(int i = 0; i < 10000; ++i) {
			Debug.Log($"Client : {i}");
		}
	}
	#endregion // 함수
}
