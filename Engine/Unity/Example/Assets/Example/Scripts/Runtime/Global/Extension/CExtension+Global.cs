using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 전역 확장 메서드 */
public static partial class CExtension {
	#region 클래스 함수
	/** 작음 여부를 검사한다 */
	public static bool ExIsLess(this float a_fSender, float a_fRhs) {
		return a_fSender < a_fRhs - float.Epsilon;
	}

	/** 작거나 같음 여부를 검사한다 */
	public static bool ExIsLessEquals(this float a_fSender, float a_fRhs) {
		return a_fSender.ExIsLess(a_fRhs) || a_fSender.ExIsEquals(a_fRhs);
	}

	/** 큰 여부를 검사한다 */
	public static bool ExIsGreate(this float a_fSender, float a_fRhs) {
		return a_fSender > a_fRhs + float.Epsilon;
	}

	/** 크거나 같음 여부를 검사한다 */
	public static bool ExIsGreateEquals(this float a_fSender, float a_fRhs) {
		return a_fSender.ExIsGreate(a_fRhs) || a_fSender.ExIsEquals(a_fRhs);
	}

	/** 같음 여부를 검사한다 */
	public static bool ExIsEquals(this float a_fSender, float a_fRhs) {
		return Mathf.Approximately(a_fSender, a_fRhs);
	}

	/** 월드 => 로컬로 변환한다 */
	public static Vector3 ExToLocal(this Vector3 a_stSender, 
		GameObject a_oParent, bool a_bIsCoord = true) {
		var stVec4 = new Vector4(a_stSender.x, a_stSender.y, a_stSender.z, a_bIsCoord ? 1.0f : 0.0f);
		return a_oParent.transform.worldToLocalMatrix * stVec4;
	}

	/** 로컬 => 월드로 변환한다 */
	public static Vector3 ExToWorld(this Vector3 a_stSender,
		GameObject a_oParent, bool a_bIsCoord = true) {
		var stVec4 = new Vector4(a_stSender.x, a_stSender.y, a_stSender.z, a_bIsCoord ? 1.0f : 0.0f);
		return a_oParent.transform.localToWorldMatrix * stVec4;
	}
	#endregion // 클래스 함수
}
