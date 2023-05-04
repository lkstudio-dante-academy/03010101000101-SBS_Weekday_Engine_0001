using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Example 3 */
public class CE03SceneManager : CSceneManager {
	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E03;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
	}

	/** 시작하기 버튼을 눌렀을 경우 */
	public void OnTouchPlayBtn() {
		CSceneLoader.Inst.LoadScene(KDefine.G_SCENE_N_E04);
	}
	#endregion // 함수
}
