using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 결과 저장소 */
public class CE20ResultStorage : CSingleton<CE20ResultStorage> {
	#region 프로퍼티
	public CE20Engine.EResult Result { get; set; } = CE20Engine.EResult.NONE;
	#endregion // 프로퍼티

	#region 함수
	/** 상태를 리셋한다 */
	public override void Reset() {
		base.Reset();
		this.Result = CE20Engine.EResult.NONE;
	}
	#endregion // 함수

	#region 접근 함수
	/** 결과 문자열을 반환한다 */
	public string GetResultStr() {
		// 승리 일 경우
		if(this.Result == CE20Engine.EResult.WIN) {
			return "승리";
		}

		return (this.Result == CE20Engine.EResult.LOSE) ? "패배" : "무승부";
	}
	#endregion // 접근 함수
}
