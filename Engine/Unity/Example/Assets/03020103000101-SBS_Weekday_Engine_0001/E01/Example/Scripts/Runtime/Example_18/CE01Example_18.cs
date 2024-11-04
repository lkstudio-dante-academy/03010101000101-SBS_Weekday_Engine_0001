//#define E18_THREAD
//#define E18_NETWORK_01
#define E18_NETWORK_02

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

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
public partial class CE01Example_18 : CSceneManager
{
	#region 변수
	[Header("=====> 네트워크 Part 2 <=====")]
	[SerializeField] private InputField m_oInput = null;

	[SerializeField] private GameObject m_oOriginText = null;
	[SerializeField] private GameObject m_oScrollViewContents = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_18;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
	}
	#endregion // 함수
}

#if E18_THREAD
/** Example 18 - 쓰레드 */
public partial class CE01Example_18 : CSceneManager {
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
		/*
		 * lock 키워드는 쓰레드 간에 동일한 메모리를 수정 할 때 발생하는
		 * 데이터 레이싱 현상을 방지하는 역할을 수행한다. (즉, 쓰레드는
		 * 동작 중에 언제라도 다른 쓰레드에게 CPU 사용 권한이 넘어 갈 수
		 * 있기 때문에 쓰레드가 공유하는 메모리에 접근 할 경우에는 반드시
		 * 동기화 처리를 해줘야한다는 것을 알 수 있다.)
		 * 
		 * 단, 동기화 처리 시 사용 되는 공유 자원은 사용이 완료 되었을 경우
		 * 반드시 다시 소유 권한을 반납해야한다.
		 * 
		 * 만약, 특정 쓰레드가 공유 자원을 소유한 후 해당 자원을 반환하지
		 * 않을 경우 다른 쓰레드가 자원을 무한정 기다리는 데드락 현상에
		 * 빠질 수 있다.
		 * 
		 * 따라서, lock 키워드를 활용하면 공유 자원을 가져온 후 lock 키워드로
		 * 선언 영역을 벗어 날 경우 자동으로 자원이 반납된다는 것을 알 수 있다.
		 */
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
public partial class CE01Example_18 : CSceneManager {
#region 변수
	private Thread m_oServerThread = null;
	private Thread m_oClientThread = null;
#endregion // 변수

#region 함수
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

	/** 서버 메인 */
	private void ServerMain() {
		var oServerSocket = new Socket(AddressFamily.InterNetwork,
			SocketType.Stream, ProtocolType.Tcp);

		oServerSocket.Bind(new IPEndPoint(IPAddress.Any, 9080));
		oServerSocket.Listen(byte.MaxValue);

		var oSocket = oServerSocket.Accept();
		var oBuffer = new byte[short.MaxValue];

		do {
			int nNumBytes = oSocket.Receive(oBuffer, oBuffer.Length, SocketFlags.None);

			// 연결이 종료 되었을 경우
			if(nNumBytes <= 0) {
				oSocket.Close();
				break;
			}

			string oMsg = System.Text.Encoding.Default.GetString(oBuffer,
				0, nNumBytes);

			UnityEngine.Debug.Log($"서버 수신 메세지 : {oMsg}");
			oSocket.Send(oBuffer, nNumBytes, SocketFlags.None);
		} while(true);

		oServerSocket.Close();
	}

	/** 클라이언트 메인 */
	private void ClientMain() {
		var oSocket = new Socket(AddressFamily.InterNetwork,
			SocketType.Stream, ProtocolType.Tcp);

		oSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9080));

		// 연결 되었을 경우
		if(oSocket.Connected) {
			string oMsg = "Hello, World!";
			oSocket.Send(System.Text.Encoding.Default.GetBytes(oMsg), SocketFlags.None);

			var oBuffer = new byte[short.MaxValue];
			int nNumBytes = oSocket.Receive(oBuffer, oBuffer.Length, SocketFlags.None);

			oMsg = System.Text.Encoding.Default.GetString(oBuffer, 0, nNumBytes);
			UnityEngine.Debug.Log($"클라이언트 수신 메세지 : {oMsg}");

			oSocket.Shutdown(SocketShutdown.Both);
			oSocket.Close();
		}
	}
#endregion // 함수
}
#elif E18_NETWORK_02
/** Example 18 - 네트워크 Part 2 */
public partial class CE01Example_18 : CSceneManager
{
	#region 변수
	private Thread m_oServerThread = null;
	private Thread m_oClientThread = null;

	private List<Socket> m_oClientList = new List<Socket>();
	private Queue<string> m_oSendMsgQueue = new Queue<string>();
	private Queue<string> m_oReceiveMsgQueue = new Queue<string>();
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Start()
	{
		base.Start();
		StartCoroutine(this.TryReceiveMsg());

		m_oServerThread = new Thread(this.ServerMain);
		m_oClientThread = new Thread(this.ClientMain);

		m_oServerThread.Start();
		m_oClientThread.Start();

		// 입력 필드를 설정한다
		m_oInput.onEndEdit.AddListener(this.OnEndInputText);
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy()
	{
		base.OnDestroy();

		m_oServerThread.Abort();
		m_oClientThread.Abort();
	}

	/** 전송 버튼을 눌렀을 경우 */
	public void OnTouchSendBtn()
	{
		// 텍스트가 존재 할 경우
		if(m_oInput.text.Length >= 1)
		{
			m_oSendMsgQueue.Enqueue(m_oInput.text);
			m_oInput.SetTextWithoutNotify(string.Empty);
		}
	}

	/** 텍스트 입력을 종료했을 경우 */
	private void OnEndInputText(string a_oStr)
	{
		// 전송 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Return))
		{
			this.OnTouchSendBtn();
		}
	}

	/** 메세지 수신을 시도한다 */
	private IEnumerator TryReceiveMsg()
	{
		do
		{
			int nTimes = 0;
			yield return new WaitForSeconds(0.1f);

			while(nTimes < 10 && m_oReceiveMsgQueue.Count >= 1)
			{
				nTimes += 1;
				string oMsg = m_oReceiveMsgQueue.Dequeue();

				// 텍스트를 설정한다 {
				var oText = Factory.CreateCloneGameObj<Text>("Text",
					m_oOriginText, m_oScrollViewContents, Vector3.zero, Vector3.one, Vector3.zero);

				oText.text = oMsg;
				// 텍스트를 설정한다 }
			}

			var oLayoutGroup = m_oScrollViewContents.GetComponent<LayoutGroup>();
			LayoutRebuilder.MarkLayoutForRebuild(oLayoutGroup.transform as RectTransform);
		} while(true);
	}

	/** 서버 메인 */
	private void ServerMain()
	{
#if UNITY_EDITOR
		var oServerSocket = new Socket(AddressFamily.InterNetwork,
			SocketType.Stream, ProtocolType.Tcp);

		oServerSocket.Bind(new IPEndPoint(IPAddress.Any, 9080));
		oServerSocket.Listen(byte.MaxValue);

		do
		{
			var oSocket = oServerSocket.Accept();

			// 연결 되었을 경우
			if(oSocket.Connected)
			{
				m_oClientList.Add(oSocket);
				this.TryReceiveClientMsg(oSocket);

				var oEndPoint = oSocket.RemoteEndPoint as IPEndPoint;
				string oWelcomMsg = $"[{oEndPoint.Port}] 입장했습니다.";

				var oBytes = System.Text.Encoding.Default.GetBytes(oWelcomMsg);

				for(int i = 0; i < m_oClientList.Count; ++i)
				{
					m_oClientList[i].Send(oBytes, oBytes.Length, SocketFlags.None);
				}
			}
		} while(m_oClientList.Count >= 1);

		oServerSocket.Close();
#endif // #if UNITY_EDITOR
	}

	/** 클라이언트 메세지 수신을 시도한다 */
	private async void TryReceiveClientMsg(Socket a_oSocket)
	{
		var oBuffer = new byte[short.MaxValue];
		var oTask = a_oSocket.ReceiveAsync(oBuffer, SocketFlags.None);

		await oTask;
		int nBytes = oTask.Result;

		// 연결이 종료 되었을 경우
		if(nBytes <= 0)
		{
			a_oSocket.Close();
		}
		else
		{
			for(int i = 0; i < m_oClientList.Count; ++i)
			{
				m_oClientList[i].Send(oBuffer, nBytes, SocketFlags.None);
			}

			this.TryReceiveClientMsg(a_oSocket);
		}
	}

	/** 클라이언트 메인 */
	private void ClientMain()
	{
		var oSocket = new Socket(AddressFamily.InterNetwork,
			SocketType.Stream, ProtocolType.Tcp);

		oSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9080));

		// 연결 되었을 경우
		if(oSocket.Connected)
		{
			this.TryReceiveServerMsg(oSocket);

			do
			{
				// 전송 메세지가 존재 할 경우
				if(m_oSendMsgQueue.Count >= 1)
				{
					string oMsg = m_oSendMsgQueue.Dequeue();
					var oBytes = System.Text.Encoding.Default.GetBytes(oMsg);

					oSocket.Send(oBytes, oBytes.Length, SocketFlags.None);
				}
			} while(true);
		}
	}

	/** 서버 메세지 수신을 시도한다 */
	private async void TryReceiveServerMsg(Socket a_oSocket)
	{
		var oBuffer = new byte[short.MaxValue];
		var oTask = a_oSocket.ReceiveAsync(oBuffer, SocketFlags.None);

		await oTask;

		int nNumBytes = oTask.Result;
		string oMsg = System.Text.Encoding.Default.GetString(oBuffer, 0, nNumBytes);

		m_oReceiveMsgQueue.Enqueue(oMsg);
		this.TryReceiveServerMsg(a_oSocket);
	}
	#endregion // 함수
}
#endif // #if E18_THREAD
