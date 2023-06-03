using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** Example 9 */
public class CE09SceneManager : CSceneManager {
	#region 변수
	[SerializeField] private Text m_oScoreText = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E09;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		m_oScoreText.text = string.Format("점수 : {0}", CE08ResultStorage.Inst.Score);
	}

	/** 다시하기 버튼을 눌렀을 경우 */
	public void OnTouchRetryBtn() {
		CSceneLoader.Inst.LoadScene(KDefine.G_SCENE_N_E08);
	}

	/** 그만두기 버튼을 눌렀을 경우 */
	public void OnTouchLeaveBtn() {
		CSceneLoader.Inst.LoadScene(KDefine.G_SCENE_N_E07);
	}
	#endregion // 함수
}
