using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 매칭 UI 처리자 */
public class CE20MatchingUIsHandler : CE20UIsHandler {
	/** 상태 */
	private enum EState {
		NONE = -1,
		WAIT,
		MATCHING,
		[HideInInspector] MAX_VAL
	}

	/** 매개 변수 */
	public new struct STParams {
		public CE20UIsHandler.STParams m_stBaseParams;
	}

	#region 변수
	[SerializeField] private Text m_oStateText = null;
	[SerializeField] private Button m_oMatchingBtn = null;

	private EState m_eState = EState.WAIT;
	#endregion // 변수

	#region 프로퍼티
	public new STParams Params { get; private set; }
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		base.Init(a_stParams.m_stBaseParams);
		this.Params = a_stParams;
	}

	/** 매칭 버튼을 눌렀을 경우 */
	public void OnTouchMachingBtn() {
		// Do Something
	}

	/** 상태를 갱신한다 */
	protected override void UpdateState() {
		base.UpdateState();

		// 텍스트를 갱신한다
		m_oStateText.gameObject.SetActive(m_eState == EState.MATCHING);

		// 버튼을 갱신한다
		m_oMatchingBtn.interactable = m_eState == EState.WAIT;
	}
	#endregion // 함수

	#region 클래스 팩토리 함수
	/** 매개 변수를 생성한다 */
	public new static STParams MakeParams(CE20SceneManager a_oSceneManager) {
		return new STParams() {
			m_stBaseParams = CE20UIsHandler.MakeParams(a_oSceneManager)
		};
	}
	#endregion // 클래스 팩토리 함수
}
