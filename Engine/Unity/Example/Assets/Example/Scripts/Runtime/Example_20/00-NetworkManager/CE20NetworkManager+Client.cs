using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

/** 네트워크 관리자 - 클라이언트 */
public partial class CE20NetworkManager : CSingleton<CE20NetworkManager> {
	#region 변수
	private Socket m_oSocket = null;
	private Dictionary<E20PacketType, System.Action<CE20NetworkManager, bool>> m_oCallbackDict01 = new Dictionary<E20PacketType, System.Action<CE20NetworkManager, bool>>();
	#endregion // 변수

	#region 함수
	/** 매칭 요청을 보낸다 */
	public async void SendMatchingRequest(System.Action<CE20NetworkManager, bool> a_oCallback) {
		m_oSocket = new Socket(AddressFamily.InterNetwork,
			SocketType.Stream, ProtocolType.Tcp);

		// 콜백이 존재 할 경우
		if(m_oCallbackDict01.ContainsKey(E20PacketType.MATCHING_REQUEST)) {
			m_oCallbackDict01[E20PacketType.MATCHING_REQUEST] = a_oCallback;
		} else {
			m_oCallbackDict01.Add(E20PacketType.MATCHING_REQUEST, a_oCallback);
		}

		await m_oSocket.ConnectAsync(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9080));
	}

	/** 에이전트 선택 요청을 보낸다 */
	public void SendAgentTouchRequest(Vector2Int a_stIdx) {
		//a_stPacketInfo.m_oParams
	}

	/** 매칭 응답을 처리한다 */
	private void HandleMatchingResponse(STPacketInfo a_stPacketInfo) {
		m_oCallbackDict01[E20PacketType.MATCHING_REQUEST](this, true);
	}

	/** 서버 패킷 정보를 대기한다 */
	private void WaitServerPacketInfo() {
		StartCoroutine(this.CoWaitServerPacketInfo());
	}

	/** 서버 패킷 정보를 대기한다 */
	private IEnumerator CoWaitServerPacketInfo() {
		var oBuffer = new byte[short.MaxValue];

		do {
			yield return null;

			// 수신 패킷 정보가 존재 할 경우
			if(m_oSocket != null && m_oSocket.Poll(0, SelectMode.SelectRead)) {
				int nNumBytes = m_oSocket.Receive(oBuffer, oBuffer.Length, SocketFlags.None);
				string oJSONStr = System.Text.Encoding.Default.GetString(oBuffer, 0, nNumBytes);

				var stPacketInfo = STPacketInfo.ToPacketInfo(oJSONStr);

				switch(stPacketInfo.m_eType) {
					case E20PacketType.MATCHING_RESPONSE: this.HandleMatchingResponse(stPacketInfo); break;
				}
			}
		} while(true);
	}
	#endregion // 함수
}
