using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Example 15 */
public class CE15SceneManager : CSceneManager {
	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E15;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
	}
	#endregion // 함수
}
