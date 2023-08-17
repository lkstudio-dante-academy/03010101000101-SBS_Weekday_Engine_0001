//#define E18_THREAD
#define E18_NETWORK_01

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Net.Sockets;
using UnityEngine;

/*
 * 쓰레드란?
 * - 프로그램이 동작 할 수 있게 CPU 의 작업 시간을 할당 받는 기본 단위를 의미한다. (즉,
 * 프로그램은 쓰레드를 CPU 에 의해 명령문 실행 됨으로서 프로그램이 동작한다는 것을 알 수
 * 있다.)
 * 
 * 따라서, 프로그램이 실행 되면 자동으로 하나의 쓰레드가 생성 되며 해당 쓰레드를 메인 쓰레드
 * 라고 한다. 또한, 필요에 의해서 쓰레드를 컴퓨터 자원이 허락하는 한 생성하는 것이 가능하며
 * 2 개 이상의 쓰레드를 생성하면 프로그램의 명령문을 병렬로 처리하는 것이 가능하다.
 */
/** Example 18 */
public partial class CE18SceneManager : CSceneManager {
	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E18;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
	}
	#endregion // 함수
}

#if E18_THREAD
/** Example 18 - 쓰레드 */
public partial class CE18SceneManager : CSceneManager {
#region 변수
	private int m_nCount = 0;
	private object m_oKey = new object();
	private Stopwatch m_oStopwatch = new Stopwatch();

	private bool m_bIsCompleteServerThread = false;
	private bool m_bIsCompleteClientThread = false;

	private Thread m_oServerThread = null;
	private Thread m_oClientThread = null;
#endregion // 변수

#region 함수
	/** 초기화 */
	public override void Start() {
		base.Start();
		m_oStopwatch.Restart();

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
		lock(m_oKey) {
			for(int i = 0; i < 1000000; ++i) {
				m_nCount += 1;
			}
		}

		m_bIsCompleteServerThread = true;
		this.TryShowThreadCountingResult();
	}

	/** 클라이언트 메인 메서드 */
	private void ClientMain() {
		lock(m_oKey) {
			for(int i = 0; i < 1000000; ++i) {
				m_nCount += 1;
			}
		}

		m_bIsCompleteClientThread = true;
		this.TryShowThreadCountingResult();
	}

	/** 쓰레드 카운팅 결과를 출력한다 */
	private void TryShowThreadCountingResult() {
		// 쓰레드가 모두 완료 되었을 경우
		if(m_bIsCompleteServerThread && m_bIsCompleteClientThread) {
			m_oStopwatch.Stop();
			UnityEngine.Debug.Log($"TryShowThreadCountingResult : {m_nCount}, {m_oStopwatch.Elapsed.TotalSeconds}");

			m_oStopwatch.Restart();

			for(int i = 0; i < 2000000; ++i) {
				m_nCount += 1;
			}

			m_oStopwatch.Stop();
			UnityEngine.Debug.Log($"TryShowThreadCountingResult Single : {m_nCount}, {m_oStopwatch.Elapsed.TotalSeconds}");
		}
	}
#endregion // 함수
}
#elif E18_NETWORK_01
/** Example 18 - 네트워크 Part 1 */
public partial class CE18SceneManager : CSceneManager {
	#region 변수

	#endregion // 변수
}
#endif // #if E18_THREAD
