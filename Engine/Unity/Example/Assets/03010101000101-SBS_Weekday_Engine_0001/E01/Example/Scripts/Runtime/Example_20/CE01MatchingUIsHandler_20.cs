using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 매칭 UI 처리자 */
public class CE01MatchingUIsHandler_20 : CE01UIsHandler_20
{
	/** 상태 */
	private enum EState
	{
		NONE = -1,
		WAIT,
		MATCHING,
		[HideInInspector] MAX_VAL
	}

	/** 매개 변수 */
	public new record STParams : CE01UIsHandler_20.STParams
	{
		#region 함수
		/** 생성자 */
		public STParams(CE01Example_20 a_oSceneManager) : base(a_oSceneManager)
		{
			// Do Something
		}
		#endregion // 함수
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
	public override void Awake()
	{
		base.Awake();

		CE01NetworkManager_20.Inst.AddCallback(E20PacketType.MATCHING_RESPONSE,
			this.OnRecieveMatchingResponse);
	}

	/** 초기화 */
	public virtual void Init(STParams a_stParams)
	{
		base.Init(a_stParams);
		this.Params = a_stParams;
	}

	/** 매칭 버튼을 눌렀을 경우 */
	public void OnTouchMatchingBtn()
	{
		// 대기 상태 일 경우
		if(m_eState == EState.WAIT)
		{
			m_eState = EState.MATCHING;
			CE01NetworkManager_20.Inst.SendMatchingRequest();
		}
	}

	/** 매칭 응답을 수신했을 경우 */
	private void OnRecieveMatchingResponse(CE01NetworkManager_20 a_oSender,
		STPacketInfo a_stPacketInfo)
	{
		m_eState = EState.WAIT;

		var oSceneManager = CSceneManager.GetSceneManager<CE01Example_20>(KDefine.G_N_SCENE_EXAMPLE_20);
		oSceneManager.OnMatchingSuccess(int.Parse(a_stPacketInfo.m_oParams));
	}

	/** 상태를 갱신한다 */
	protected override void UpdateState()
	{
		base.UpdateState();

		// 텍스트를 갱신한다
		m_oStateText.gameObject.SetActive(m_eState == EState.MATCHING);

		// 버튼을 갱신한다
		m_oMatchingBtn.interactable = m_eState == EState.WAIT;
	}
	#endregion // 함수

	#region 클래스 팩토리 함수
	/** 매개 변수를 생성한다 */
	public new static STParams MakeParams(CE01Example_20 a_oSceneManager)
	{
		return new STParams(a_oSceneManager);
	}
	#endregion // 클래스 팩토리 함수
}
