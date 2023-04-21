using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 씬 관리자 */
public abstract class CSceneManager : CComponent {
	#region 프로퍼티
	public abstract string SceneName { get; }
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
	}
	#endregion // 함수
}
