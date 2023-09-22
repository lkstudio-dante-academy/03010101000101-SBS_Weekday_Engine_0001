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

/** 3 차원 정수 벡터 */
public struct STVec3Int {
	public int m_nX;
	public int m_nY;
	public int m_nZ;

	#region 함수
	/** 생성자 */
	public STVec3Int(int a_nX, int a_nY, int a_nZ) {
		m_nX = a_nX;
		m_nY = a_nY;
		m_nZ = a_nZ;
	}

	/** JSON 문자열로 변환한다 */
	public string ToJSONStr() {
		return JsonConvert.SerializeObject(this);
	}
	#endregion // 함수

	#region 클래스 함수
	/** 3 차원 벡터로 변환한다 */
	public static STVec3Int ToVec3Int(string a_oJSONStr) {
		return JsonConvert.DeserializeObject<STVec3Int>(a_oJSONStr);
	}

	/** 3 차원 벡터로 변환한다 */
	public static implicit operator Vector3Int(STVec3Int a_stSender) {
		return new Vector3Int(a_stSender.m_nX, a_stSender.m_nY, a_stSender.m_nZ);
	}

	/** 3 차원 벡터로 변환한다 */
	public static implicit operator STVec3Int(Vector3Int a_stSender) {
		return new STVec3Int(a_stSender.x, a_stSender.y, a_stSender.y);
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
