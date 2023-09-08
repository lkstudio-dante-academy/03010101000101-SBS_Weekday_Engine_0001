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
}
