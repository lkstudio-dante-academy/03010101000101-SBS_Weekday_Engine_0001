using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Graphic Raycaster 란?
 * - Unity 가 제공하는 이벤트 시스템에 반응하는 객체를 탐색하는 역할을 수행하는
 * 컴포넌트를 의미한다. (즉, 해당 컴포넌트가 부착 된 캔버스 하위에 존재하는
 * UI 관련 컴포넌트는 Unity 가 제공하는 각종 입력 이벤트에 반응하는 것이 가능하다는
 * 것을 알 수 있다.)
 * 
 * 또한, Unity 가 제공하는 각종 이벤트 인터페이스를 활용하면 이벤트 시스템에
 * 반응하는 커스텀한 스크립트를 제작하는 것이 가능하다. 
 * 
 * 단, Graphic Raycaster 는 UI 관련 컴포넌트에만 동작하기 때문에 
 * 스프라이트와 같은 메시 기반 컴포넌트가 Unity 이벤트 시스템에 반응하기 
 * 위해서는 별도의 Raycaster 컴포넌트를 카메라에 추가 시켜 줄 필요가 있다. 
 * (즉, 2 차원 객체가 이벤트를 처리하기 위해서는 Physics Raycaster 2D 
 * 컴포넌트가 필요하며 3 차원 객체가 이벤트를 처리하기 위해서는 
 * Physics Raycaster 컴포넌트가 필요하다.)
 */
/** Example 20 */
public class CE01Example_20 : CSceneManager
{
	/** 상태 */
	private enum EState
	{
		NONE = -1,
		PLAY,
		MATCHING,
		[HideInInspector] MAX_VAL
	}

	/*
	 * System.Serializable 속성은 구조체 또는 클래스를 Unity 인스펙터 상에
	 * 노출 시키는 역할을 수행한다. (즉, 해당 속성을 활용하면 관련 된 게임 객체를
	 * 하나의 묶음으로 제어하는 것이 가능하다.)
	 */
	/** UI 정보 */
	[System.Serializable]
	public struct STUIsInfo
	{
		public GameObject m_oUIs;
		public GameObject m_oObjs;
	}

	#region 변수
	[SerializeField] private STUIsInfo m_oPlayUIsInfo;
	[SerializeField] private STUIsInfo m_oMatchingUIsInfo;

	private EState m_eState = EState.MATCHING;
	private CE01UIsHandler_20 m_oPlayUIsHandler = null;
	private CE01UIsHandler_20 m_oMatchingUIsHandler = null;
	private CE01Engine_20 m_oEngine = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_20;
	public CE01Engine_20 Engine => m_oEngine;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		CScheduleManager.Inst.AddComponent(this);

		// 엔진을 설정한다
		m_oEngine = this.GetComponentInChildren<CE01Engine_20>();

		// UI 처리자를 설정한다
		m_oPlayUIsHandler = this.GetComponentInChildren<CE01PlayUIsHandler_20>();
		m_oMatchingUIsHandler = this.GetComponentInChildren<CE01MatchingUIsHandler_20>();
	}

	/** 초기화 */
	public override void Start()
	{
		base.Start();
		this.SetIsDirtyUpdateState(true);

		// 엔진을 설정한다
		m_oEngine.Init(CE01Engine_20.MakeParams(m_oPlayUIsInfo.m_oObjs, this, m_oPlayUIsHandler as CE01PlayUIsHandler_20));

		// UI 처리자를 설정한다 {
		var stGridSize = new Vector2Int(3, 3);

		(m_oPlayUIsHandler as CE01PlayUIsHandler_20).Init(CE01PlayUIsHandler_20.MakeParams(this, stGridSize));
		(m_oMatchingUIsHandler as CE01MatchingUIsHandler_20).Init(CE01MatchingUIsHandler_20.MakeParams(this));
		// UI 처리자를 설정한다 }

#if UNITY_EDITOR
		CE01NetworkManager_20.Inst.RunServerSocket();
#endif // #if UNITY_EDITOR
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy()
	{
		base.OnDestroy();

#if UNITY_EDITOR
		CE01NetworkManager_20.Inst.StopServerSocket();
#endif // #if UNITY_EDITOR
	}

	/** 상태를 갱신한다 */
	public override void OnUpdate(float a_fDeltaTime)
	{
		base.OnUpdate(a_fDeltaTime);

		// UI 처리자를 갱신한다
		m_oPlayUIsHandler.OnUpdate(a_fDeltaTime);
		m_oMatchingUIsHandler.OnUpdate(a_fDeltaTime);
	}

	/** 상태를 갱신한다 */
	public override void OnLateUpdate(float a_fDeltaTime)
	{
		base.OnLateUpdate(a_fDeltaTime);

		// UI 처리자를 갱신한다
		m_oPlayUIsHandler.OnLateUpdate(a_fDeltaTime);
		m_oMatchingUIsHandler.OnLateUpdate(a_fDeltaTime);
	}

	/** 매칭에 성공했을 경우 */
	public void OnMatchingSuccess(int a_nPlayerOrder)
	{
		m_eState = EState.PLAY;
		m_oEngine.Play(a_nPlayerOrder);

		this.SetIsDirtyUpdateState(true);
	}

	/** 상태를 갱신한다 */
	protected override void UpdateState()
	{
		base.UpdateState();

		// 객체를 갱신한다 {
		m_oPlayUIsInfo.m_oUIs.SetActive(m_eState == EState.PLAY);
		m_oPlayUIsInfo.m_oObjs.SetActive(m_eState == EState.PLAY);

		m_oMatchingUIsInfo.m_oUIs.SetActive(m_eState == EState.MATCHING);
		m_oMatchingUIsInfo.m_oObjs.SetActive(m_eState == EState.MATCHING);
		// 객체를 갱신한다 }

		m_oPlayUIsHandler.SetIsDirtyUpdateState(true);
		m_oMatchingUIsHandler.SetIsDirtyUpdateState(true);
	}
	#endregion // 함수
}
