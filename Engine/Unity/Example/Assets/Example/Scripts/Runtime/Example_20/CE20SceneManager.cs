using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Example 20 */
public class CE20SceneManager : CSceneManager {
	/** 상태 */
	private enum EState {
		NONE = -1,
		PLAY,
		MACHING,
		[HideInInspector] MAX_VAL
	}

	#region 변수
	[SerializeField] private GameObject m_oPlayUIs = null;
	[SerializeField] private GameObject m_oMatchingUIs = null;

	private EState m_eState = EState.MACHING;
	private CE20UIsHandler m_oPlayUIsHandler = null;
	private CE20UIsHandler m_oMatchingUIsHandler = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E20;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		CScheduleManager.Inst.AddComponent(this);

		// UI 처리자를 설정한다 {
		m_oPlayUIsHandler = this.GetComponent<CE20PlayUIsHandler>();
		m_oMatchingUIsHandler = this.GetComponent<CE20MatchingUIsHandler>();

		(m_oPlayUIsHandler as CE20PlayUIsHandler).Init(CE20PlayUIsHandler.MakeParams(this));
		(m_oMatchingUIsHandler as CE20MatchingUIsHandler).Init(CE20MatchingUIsHandler.MakeParams(this));
		// UI 처리자를 설정한다 }
	}

	/** 상태를 갱신한다 */
	public override void OnUpdate(float a_fDeltaTime) {
		base.OnUpdate(a_fDeltaTime);

		// UI 처리자를 갱신한다
		m_oPlayUIsHandler.OnUpdate(a_fDeltaTime);
		m_oMatchingUIsHandler.OnUpdate(a_fDeltaTime);
	}

	/** 상태를 갱신한다 */
	protected override void UpdateState() {
		base.UpdateState();

		// UI 를 갱신한다
		m_oPlayUIs.SetActive(m_eState == EState.PLAY);
		m_oMatchingUIs.SetActive(m_eState == EState.MACHING);
	}
	#endregion // 함수
}
