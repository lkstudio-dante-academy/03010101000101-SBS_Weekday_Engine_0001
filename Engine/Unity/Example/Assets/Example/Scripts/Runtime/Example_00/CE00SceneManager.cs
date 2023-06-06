using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** Example 0 */
public class CE00SceneManager : CSceneManager {
	#region 변수
	[SerializeField] private GameObject m_oOriginMenuBtn = null;
	[SerializeField] private GameObject m_oScrollViewContents = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E00;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
	}
	#endregion // 함수
}
