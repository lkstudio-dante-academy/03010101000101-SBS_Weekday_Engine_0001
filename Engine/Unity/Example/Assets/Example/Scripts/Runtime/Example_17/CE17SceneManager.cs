using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** Example 17 */
public class CE17SceneManager : CSceneManager {
	#region 변수
	[SerializeField] private Text m_oTimeText = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E17;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
	}

	/** 상태를 갱신한다 */
	public override void OnUpdate(float a_fDeltaTime) {
		base.OnUpdate(a_fDeltaTime);
		m_oTimeText.text = $"{a_fDeltaTime}";
	}

	/** 컴포넌트 추가 버튼을 눌렀을 경우 */
	public void OnTouchAddComponentBtn() {
		CScheduleManager.Inst.AddComponent(this);
	}

	/** 컴포넌트 제거 버튼을 눌렀을 경우 */
	public void OnTouchRemoveComponentBtn() {
		CScheduleManager.Inst.RemoveComponent(this);
	}
	#endregion // 함수
}
