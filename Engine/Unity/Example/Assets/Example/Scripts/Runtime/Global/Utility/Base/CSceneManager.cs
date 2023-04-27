using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 씬 관리자 */
public abstract class CSceneManager : CComponent {
	#region 프로퍼티
	public abstract string SceneName { get; }
	public virtual Vector3 Gravity => new Vector3(0.0f, -1280.0f, 0.0f);
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		Physics.gravity = this.Gravity;
	}
	#endregion // 함수
}
