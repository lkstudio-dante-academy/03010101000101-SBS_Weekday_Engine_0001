using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/** 틱택토 엔진 */
public class CE20Engine : CComponent {
	/** 셀 상태 */
	public enum ECellState {
		NONE = -1,
		EMPTY,
		PLAYER_01,
		PLAYER_02,
		[HideInInspector] MAX_VAL
	}

	/** 매개 변수 */
	public record STParams {
		public GameObject m_oPlayObjsRoot = null;
		public CE20SceneManager m_oSceneManager = null;
		public CE20PlayUIsHandler m_oPlayUIsHandler = null;
	}

	#region 변수
	/*
	 * Bounds 구조체는 OBB 또는 AABB 정보를 제어하는 역할을 수행한다.
	 * (즉, 해당 구조체를 활용하면 2 차원 또는 3 차원 공간 상에 경계 상자를
	 * 데이터로 표현하는 것이 가능하다는 것을 알 수 있다.)
	 */
	private Bounds m_stGridBounds;
	private ECellState[,] m_oCellStates = null;

	private CE20AgentController m_oPlayerController = null;
	private CE20AgentController m_oNonPlayerController = null;
	#endregion // 변수

	#region 프로퍼티
	public STParams Params { get; private set; } = null;
	public bool IsPlaying { get; private set; } = false;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		// 에이전트 제어자를 설정한다 {
		m_oPlayerController = CFactory.CreateGameObj<CE20PlayerController>("PlayerController",
			null, Vector3.zero, Vector3.one, Vector3.zero);

		m_oNonPlayerController = CFactory.CreateGameObj<CE20NonPlayerController>("NonPlayerController",
			null, Vector3.zero, Vector3.one, Vector3.zero);
		// 에이전트 제어자를 설정한다 }
	}

	/** 초기화 */
	public override void Start() {
		base.Start();

		// 그리드 영역을 설정한다 {
		float fGridWidth = this.Params.m_oPlayUIsHandler.Params.m_stGridSize.x * KDefine.E20_CELL_WIDTH;
		float fGridHeight = this.Params.m_oPlayUIsHandler.Params.m_stGridSize.y * KDefine.E20_CELL_HEIGHT;

		m_stGridBounds = new Bounds(Vector3.zero, new Vector3(fGridWidth, fGridHeight, 0.0f));
		// 그리드 영역을 설정한다 }

		// 셀 상태를 설정한다 {
		m_oCellStates = new ECellState[this.Params.m_oPlayUIsHandler.Params.m_stGridSize.y, 
			this.Params.m_oPlayUIsHandler.Params.m_stGridSize.x];

		for(int i = 0; i < m_oCellStates.GetLength(0); ++i) {
			for(int j = 0; j < m_oCellStates.GetLength(1); ++j) {
				m_oCellStates[i, j] = ECellState.EMPTY;
			}
		}
		// 셀 상태를 설정한다 }
	}

	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		this.Params = a_stParams;

		// 전달자를 설정한다 {
		var oPlayUIs = a_stParams.m_oSceneManager.UIs.transform.Find("PlayUIs");

		var oDispatcher = oPlayUIs.GetComponentInChildren<CTouchDispatcher>();
		oDispatcher.SetBeginCallback(this.HandleOnTouchBegin);
		oDispatcher.SetEndCallback(this.HandleOnTouchEnd);
		// 전달자를 설정한다 }

		// 에이전트 제어자를 설정한다 {
		m_oPlayerController.transform.SetParent(this.Params.m_oPlayObjsRoot.transform, false);
		(m_oPlayerController as CE20PlayerController).Init(CE20PlayerController.MakeParams(this, this.OnReceiveAgentTouchCallback));

		m_oNonPlayerController.transform.SetParent(this.Params.m_oPlayObjsRoot.transform, false);
		(m_oNonPlayerController as CE20NonPlayerController).Init(CE20NonPlayerController.MakeParams(this, this.OnReceiveAgentTouchCallback));
		// 에이전트 제어자를 설정한다 }



		m_oPlayerController.SetIsEnableTouch(true);
	}

	/** 게임 플레이를 시작한다 */
	public void Play() {
		this.IsPlaying = true;
	}

	/** 에이전트 터치 콜백을 수신했을 경우 */
	private void OnReceiveAgentTouchCallback(CE20AgentController a_oSender,
		Vector3Int a_stIdx) {
		// 빈 셀 일 경우
		if(m_oCellStates[a_stIdx.y, a_stIdx.x] == ECellState.EMPTY) {

		}
	}

	/** 터치 시작을 처리한다 */
	private void HandleOnTouchBegin(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData) {
		var stPos = a_oEventData.ExGetLocalPos(this.Params.m_oPlayObjsRoot);

		// 그리드 영역 일 경우
		if(this.IsContainsInGridBounds(stPos)) {
			m_oPlayerController.HandleOnTouchBegin(a_oSender, a_oEventData);
			m_oNonPlayerController.HandleOnTouchBegin(a_oSender, a_oEventData);
		}
	}

	/** 터치 종료를 처리한다 */
	private void HandleOnTouchEnd(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData) {
		m_oPlayerController.HandleOnTouchEnd(a_oSender, a_oEventData);
		m_oNonPlayerController.HandleOnTouchEnd(a_oSender, a_oEventData);
	}
	#endregion // 함수

	#region 접근 함수
	/** 그리드 영역 여부를 검사한다 */
	public bool IsContainsInGridBounds(Vector3 a_stPos) {
		return m_stGridBounds.Contains(a_stPos);
	}

	/** 기준 위치를 반환한다 */
	public Vector3 GetPivot() {
		float fGridWidth = this.Params.m_oPlayUIsHandler.Params.m_stGridSize.x * KDefine.E20_CELL_WIDTH;
		float fGridHeight = this.Params.m_oPlayUIsHandler.Params.m_stGridSize.y * KDefine.E20_CELL_HEIGHT;

		return new Vector3(fGridWidth / -2.0f, fGridHeight / 2.0f, 0.0f);
	}

	/** 인덱스를 반환한다 */
	public Vector3Int GetIdx(Vector3 a_stPos) {
		float fDeltaX = a_stPos.x - this.GetPivot().x;
		float fDeltaY = a_stPos.y - this.GetPivot().y;

		return new Vector3Int((int)(fDeltaX / KDefine.E20_CELL_WIDTH),
			(int)(fDeltaY / -KDefine.E20_CELL_HEIGHT), 0);
	}
	#endregion // 접근 함수

	#region 클래스 팩토리 함수
	/** 매개 변수를 생성한다 */
	public static STParams MakeParams(GameObject a_oPlayObjsRoot,
		CE20SceneManager a_oSceneManager, CE20PlayUIsHandler a_oPlayUIsHandler) {
		return new STParams() {
			m_oPlayObjsRoot = a_oPlayObjsRoot,
			m_oSceneManager = a_oSceneManager,
			m_oPlayUIsHandler = a_oPlayUIsHandler
		};
	}
	#endregion // 클래스 팩토리 함수
}
