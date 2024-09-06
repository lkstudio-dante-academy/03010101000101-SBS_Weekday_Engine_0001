using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

/** 네트워크 관리자 */
public partial class CE01NetworkManager_20 : CSingleton<CE01NetworkManager_20>
{
	#region 함수
	/** 초기화 */
	public override void Start()
	{
		base.Start();
		this.WaitServerPacketInfo();
	}

	/** 패킷 정보를 전송한다 */
	private void SendPacketInfo(Socket a_oSocket, STPacketInfo a_stPacketInfo)
	{
		string oJSONStr = a_stPacketInfo.ToJSONStr();
		var oBytes = System.Text.Encoding.Default.GetBytes(oJSONStr);

		a_oSocket.Send(oBytes, oBytes.Length, SocketFlags.None);
	}
	#endregion // 함수
}
