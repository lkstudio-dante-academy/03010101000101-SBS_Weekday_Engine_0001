using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/** 마우스 버튼 */
public enum EMouseBtn {
	NONE = -1,
	LEFT,
	RIGHT,
	MIDDLE,
	[HideInInspector] MAX_VAL
}

/** 패킷 타입 */
public enum E20PacketType {
	NONE = -1,

	// 매칭
	MATCHING_REQUEST,
	MATCHING_RESPONSE,
	
	// 에이전트 선택
	AGENT_TOUCH_REQUEST,
	AGENT_TOUCH_RESPONSE,

	[HideInInspector] MAX_VAL
}

/** 2 차원 정수 벡터 */
public struct STVec2Int {
	public int m_nX;
	public int m_nY;

	#region 함수
	/** 생성자 */
	public STVec2Int(int a_nX, int a_nY) {
		m_nX = a_nX;
		m_nY = a_nY;
	}
	#endregion // 함수

	#region 클래스 함수
	/** 2 차원 벡터로 변환한다 */
	public static implicit operator Vector2Int(STVec2Int a_stSender) {
		return new Vector2Int(a_stSender.m_nX, a_stSender.m_nY);
	}
	#endregion // 클래스 함수
}

/** 패킷 정보 */
[JsonObject]
public record STPacketInfo {
	public string m_oParams;
	public E20PacketType m_eType;

	#region 함수
	/** JSON 문자열로 변환한다 */
	public string ToJSONStr() {
		return JsonConvert.SerializeObject(this);
	}
	#endregion // 함수

	#region 클래스 함수
	/** 패킷 정보로 변환한다 */
	public static STPacketInfo ToPacketInfo(string a_oJSONStr) {
		return JsonConvert.DeserializeObject<STPacketInfo>(a_oJSONStr);
	}
	#endregion // 클래스 함수
}
