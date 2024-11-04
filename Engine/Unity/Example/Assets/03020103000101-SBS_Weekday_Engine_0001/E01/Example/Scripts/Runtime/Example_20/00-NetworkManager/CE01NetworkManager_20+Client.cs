using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

/** 네트워크 관리자 - 클라이언트 */
public partial class CE01NetworkManager_20 : CSingleton<CE01NetworkManager_20>
{
	#region 변수
	private Socket m_oSocket = null;
	private Dictionary<E20PacketType, System.Action<CE01NetworkManager_20, STPacketInfo>> m_oCallbackDict = new Dictionary<E20PacketType, System.Action<CE01NetworkManager_20, STPacketInfo>>();
	#endregion // 변수

	#region 함수
	/** 콜백을 추가한다 */
	public void AddCallback(E20PacketType a_eType,
		System.Action<CE01NetworkManager_20, STPacketInfo> a_oCallback)
	{
		m_oCallbackDict.TryAdd(a_eType, a_oCallback);
	}

	/** 콜백을 제거한다 */
	public void RemoveCallback(E20PacketType a_eType)
	{
		// 콜백이 존재 할 경우
		if(m_oCallbackDict.ContainsKey(a_eType))
		{
			m_oCallbackDict.Remove(a_eType);
		}
	}

	/** 매칭 요청을 보낸다 */
	public async void SendMatchingRequest()
	{
		m_oSocket = new Socket(AddressFamily.InterNetwork,
			SocketType.Stream, ProtocolType.Tcp);

		await m_oSocket.ConnectAsync(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9080));
	}

	/** 에이전트 선택 요청을 보낸다 */
	public void SendAgentTouchRequest(Vector3Int a_stIdx)
	{
		STVec3Int stIdx = a_stIdx;

		var stPacketInfo = new STPacketInfo()
		{
			m_eType = E20PacketType.AGENT_TOUCH_REQUEST,
			m_oParams = stIdx.ToJSONStr()
		};

		this.SendPacketInfo(m_oSocket, stPacketInfo);
	}

	/** 매칭 응답을 처리한다 */
	private void HandleMatchingResponse(STPacketInfo a_stPacketInfo)
	{
		m_oCallbackDict.GetValueOrDefault(E20PacketType.MATCHING_RESPONSE)?.Invoke(this, a_stPacketInfo);
	}

	/** 에이전트 터치 응답을 처리한다 */
	private void HandleAgentTouchResponse(STPacketInfo a_stPacketInfo)
	{
		m_oCallbackDict.GetValueOrDefault(E20PacketType.AGENT_TOUCH_RESPONSE)?.Invoke(this, a_stPacketInfo);
	}

	/** 서버 패킷 정보를 대기한다 */
	private void WaitServerPacketInfo()
	{
		StartCoroutine(this.CoWaitServerPacketInfo());
	}

	/** 서버 패킷 정보를 대기한다 */
	private IEnumerator CoWaitServerPacketInfo()
	{
		var oBuffer = new byte[short.MaxValue];

		do
		{
			yield return null;

			// 수신 패킷 정보가 존재 할 경우
			if(m_oSocket != null && m_oSocket.Poll(0, SelectMode.SelectRead))
			{
				int nNumBytes = m_oSocket.Receive(oBuffer, oBuffer.Length, SocketFlags.None);
				string oJSONStr = System.Text.Encoding.Default.GetString(oBuffer, 0, nNumBytes);

				var stPacketInfo = STPacketInfo.ToPacketInfo(oJSONStr);

				switch(stPacketInfo.m_eType)
				{
					case E20PacketType.MATCHING_RESPONSE:
						this.HandleMatchingResponse(stPacketInfo);
						break;
					case E20PacketType.AGENT_TOUCH_RESPONSE:
						this.HandleAgentTouchResponse(stPacketInfo);
						break;
				}
			}
		} while(true);
	}
	#endregion // 함수
}
