using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** Example 21 */
public class CE21SceneManager : CSceneManager {
	#region 변수
	[SerializeField] private Text m_oResultText = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E21;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		m_oResultText.text = string.Format("결과 : {0}", CE20ResultStorage.Inst.GetResultStr());
	}

	/** 다시하기 버튼을 눌렀을 경우 */
	public void OnTouchRetryBtn() {
		CSceneLoader.Inst.LoadScene(KDefine.G_SCENE_N_E20);
	}

	/** 그만두기 버튼을 눌렀을 경우 */
	public void OnTouchLeaveBtn() {
		CSceneLoader.Inst.LoadScene(KDefine.G_SCENE_N_E19);
	}
	#endregion // 함수
}
