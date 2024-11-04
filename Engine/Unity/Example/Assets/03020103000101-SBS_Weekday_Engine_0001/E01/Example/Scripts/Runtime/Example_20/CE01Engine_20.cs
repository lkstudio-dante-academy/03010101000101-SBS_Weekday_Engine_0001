using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/** 틱택토 엔진 */
public class CE01Engine_20 : CComponent
{
	/** 결과 */
	public enum EResult
	{
		NONE = -1,
		WIN,
		LOSE,
		DRAW,
		[HideInInspector] MAX_VAL
	}

	/** 셀 상태 */
	public enum ECellState
	{
		NONE = -1,
		EMPTY,
		PLAYER_01,
		PLAYER_02,
		[HideInInspector] MAX_VAL
	}

	/** 매개 변수 */
	public record STParams
	{
		public GameObject m_oPlayObjsRoot = null;
		public CE01Example_20 m_oSceneManager = null;
		public CE01PlayUIsHandler_20 m_oPlayUIsHandler = null;
	}

	#region 변수
	/*
	 * Bounds 구조체는 OBB 또는 AABB 정보를 제어하는 역할을 수행한다.
	 * (즉, 해당 구조체를 활용하면 2 차원 또는 3 차원 공간 상에 경계 상자를
	 * 데이터로 표현하는 것이 가능하다는 것을 알 수 있다.)
	 */
	private Bounds m_stGridBounds;
	private ECellState[,] m_oCellStates = null;

	private CE01AgentController_20 m_oPlayerController = null;
	private CE01AgentController_20 m_oNonPlayerController = null;
	#endregion // 변수

	#region 프로퍼티
	public STParams Params { get; private set; } = null;
	public bool IsPlaying { get; private set; } = false;

	public ECellState[,] CellStates => m_oCellStates;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		// 에이전트 제어자를 설정한다 {
		m_oPlayerController = Factory.CreateGameObj<CE01PlayerController_20>("PlayerController",
			null, Vector3.zero, Vector3.one, Vector3.zero);

		m_oNonPlayerController = Factory.CreateGameObj<CE01NonPlayerController_20>("NonPlayerController",
			null, Vector3.zero, Vector3.one, Vector3.zero);
		// 에이전트 제어자를 설정한다 }
	}

	/** 초기화 */
	public override void Start()
	{
		base.Start();

		// 그리드 영역을 설정한다 {
		float fGridWidth = this.Params.m_oPlayUIsHandler.Params.m_stGridSize.x * KDefine.E20_CELL_WIDTH;
		float fGridHeight = this.Params.m_oPlayUIsHandler.Params.m_stGridSize.y * KDefine.E20_CELL_HEIGHT;

		m_stGridBounds = new Bounds(Vector3.zero, new Vector3(fGridWidth, fGridHeight, 0.0f));
		// 그리드 영역을 설정한다 }

		// 셀 상태를 설정한다 {
		m_oCellStates = new ECellState[this.Params.m_oPlayUIsHandler.Params.m_stGridSize.y,
			this.Params.m_oPlayUIsHandler.Params.m_stGridSize.x];

		for(int i = 0; i < m_oCellStates.GetLength(0); ++i)
		{
			for(int j = 0; j < m_oCellStates.GetLength(1); ++j)
			{
				m_oCellStates[i, j] = ECellState.EMPTY;
			}
		}
		// 셀 상태를 설정한다 }
	}

	/** 초기화 */
	public virtual void Init(STParams a_stParams)
	{
		this.Params = a_stParams;

		// 전달자를 설정한다 {
		var oPlayUIs = a_stParams.m_oSceneManager.UIs.transform.Find("PlayUIs");

		var oDispatcher = oPlayUIs.GetComponentInChildren<CTouchDispatcher>();
		oDispatcher.SetBeginCallback(this.HandleOnTouchBegin);
		oDispatcher.SetEndCallback(this.HandleOnTouchEnd);
		// 전달자를 설정한다 }

		// 에이전트 제어자를 설정한다 {
		m_oPlayerController.transform.SetParent(this.Params.m_oPlayObjsRoot.transform, false);
		(m_oPlayerController as CE01PlayerController_20).Init(CE01PlayerController_20.MakeParams(this, this.OnReceiveAgentTouchCallback));

		m_oNonPlayerController.transform.SetParent(this.Params.m_oPlayObjsRoot.transform, false);
		(m_oNonPlayerController as CE01NonPlayerController_20).Init(CE01NonPlayerController_20.MakeParams(this, this.OnReceiveAgentTouchCallback));
		// 에이전트 제어자를 설정한다 }
	}

	/** 게임 플레이를 시작한다 */
	public void Play(int a_nPlayerOrder)
	{
		this.IsPlaying = true;

		m_oPlayerController.SetIsEnableTouch(a_nPlayerOrder == 1);
		m_oNonPlayerController.SetIsEnableTouch(a_nPlayerOrder == 2);
	}

	/** 게임 플레이를 종료한다 */
	public void FinishPlay(EResult a_eResult)
	{
		m_oPlayerController.SetIsEnableTouch(false);
		m_oNonPlayerController.SetIsEnableTouch(false);

		this.Params.m_oPlayUIsHandler.ShowResult(a_eResult);
	}

	/** 에이전트 터치 콜백을 수신했을 경우 */
	private void OnReceiveAgentTouchCallback(object[] a_oParams)
	{
		this.OnReceiveAgentTouchCallback(a_oParams[0] as CE01AgentController_20,
			(STVec3Int)a_oParams[1]);
	}

	/** 에이전트 터치 콜백을 수신했을 경우 */
	private void OnReceiveAgentTouchCallback(CE01AgentController_20 a_oSender,
		Vector3Int a_stIdx)
	{
		// 빈 셀 일 경우
		if(m_oCellStates[a_stIdx.y, a_stIdx.x] == ECellState.EMPTY)
		{
			var eCellState = (a_oSender == m_oPlayerController) ?
				ECellState.PLAYER_01 : ECellState.PLAYER_02;

			m_oCellStates[a_stIdx.y, a_stIdx.x] = eCellState;
			this.Params.m_oSceneManager.SetIsDirtyUpdateState(true);

			bool bIsEnableTouchPlayer = m_oPlayerController.IsEnableTouch;
			bool bIsEnableTouchNonPlayer = m_oNonPlayerController.IsEnableTouch;

			m_oPlayerController.SetIsEnableTouch(!bIsEnableTouchPlayer);
			m_oNonPlayerController.SetIsEnableTouch(!bIsEnableTouchNonPlayer);

			var eResult = this.GetResult(eCellState);

			// 승패 여부가 판정 되었을 경우
			if(eResult != EResult.NONE)
			{
				this.FinishPlay(eResult);
			}

			// 플레이어 제어자 일 경우
			if(eCellState == ECellState.PLAYER_01)
			{
				CE01NetworkManager_20.Inst.SendAgentTouchRequest(a_stIdx);
			}
		}
	}

	/** 무승부 여부를 검사한다 */
	private bool IsDraw()
	{
		for(int i = 0; i < m_oCellStates.GetLength(0); ++i)
		{
			for(int j = 0; j < m_oCellStates.GetLength(1); ++j)
			{
				// 빈 셀이 존재 할 경우
				if(m_oCellStates[i, j] == ECellState.EMPTY)
				{
					return false;
				}
			}
		}

		return true;
	}

	/** 결과를 반환한다 */
	private EResult GetResult(ECellState a_eState)
	{
		int nNumCols = m_oCellStates.GetLength(1);

		var oIdxInfoList = new List<(Vector3Int, Vector3Int)>();
		oIdxInfoList.Add((Vector3Int.zero, Vector3Int.up + Vector3Int.right));
		oIdxInfoList.Add((new Vector3Int(nNumCols - 1, 0, 0), Vector3Int.up + Vector3Int.left));

		for(int i = 0; i < m_oCellStates.GetLength(0); ++i)
		{
			var stStartIdx = new Vector3Int(0, i, 0);
			oIdxInfoList.Add((stStartIdx, Vector3Int.right));
		}

		for(int i = 0; i < m_oCellStates.GetLength(1); ++i)
		{
			var stStartIdx = new Vector3Int(i, 0, 0);
			oIdxInfoList.Add((stStartIdx, Vector3Int.up));
		}

		for(int i = 0; i < oIdxInfoList.Count; ++i)
		{
			int nNumCells = this.GetNumCellsOnLine(a_eState,
				oIdxInfoList[i].Item1, oIdxInfoList[i].Item2);

			// 라인을 모두 채웠을 경우
			if(nNumCells >= m_oCellStates.GetLength(0) ||
				nNumCells >= m_oCellStates.GetLength(1))
			{
				return (a_eState == ECellState.PLAYER_01) ? EResult.WIN : EResult.LOSE;
			}
		}

		return this.IsDraw() ? EResult.DRAW : EResult.NONE;
	}

	/** 셀 개수를 반환한다 */
	private int GetNumCellsOnLine(ECellState a_eState,
		Vector3Int a_stStartIdx, Vector3Int a_stOffset)
	{
		int nNumCells = 0;

		while(true)
		{
			nNumCells += (m_oCellStates[a_stStartIdx.y, a_stStartIdx.x] == a_eState) ? 1 : 0;
			a_stStartIdx += a_stOffset;

			bool bIsValidIdx01 = a_stStartIdx.x >= 0 && a_stStartIdx.x < m_oCellStates.GetLength(1);
			bool bIsValidIdx02 = a_stStartIdx.y >= 0 && a_stStartIdx.y < m_oCellStates.GetLength(0);

			// 배열을 벗어났을 경우
			if(!bIsValidIdx01 || !bIsValidIdx02)
			{
				break;
			}
		}

		return nNumCells;
	}

	/** 터치 시작을 처리한다 */
	private void HandleOnTouchBegin(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData)
	{
		var stPos = a_oEventData.ExGetLocalPos(this.Params.m_oPlayObjsRoot);

		// 그리드 영역 일 경우
		if(this.IsContainsInGridBounds(stPos))
		{
			m_oPlayerController.HandleOnTouchBegin(a_oSender, a_oEventData);
			m_oNonPlayerController.HandleOnTouchBegin(a_oSender, a_oEventData);
		}
	}

	/** 터치 종료를 처리한다 */
	private void HandleOnTouchEnd(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData)
	{
		m_oPlayerController.HandleOnTouchEnd(a_oSender, a_oEventData);
		m_oNonPlayerController.HandleOnTouchEnd(a_oSender, a_oEventData);
	}
	#endregion // 함수

	#region 접근 함수
	/** 그리드 영역 여부를 검사한다 */
	public bool IsContainsInGridBounds(Vector3 a_stPos)
	{
		return m_stGridBounds.Contains(a_stPos);
	}

	/** 기준 위치를 반환한다 */
	public Vector3 GetPivot()
	{
		float fGridWidth = this.Params.m_oPlayUIsHandler.Params.m_stGridSize.x * KDefine.E20_CELL_WIDTH;
		float fGridHeight = this.Params.m_oPlayUIsHandler.Params.m_stGridSize.y * KDefine.E20_CELL_HEIGHT;

		return new Vector3(fGridWidth / -2.0f, fGridHeight / 2.0f, 0.0f);
	}

	/** 위치를 반환한다 */
	public Vector3 GetPos(Vector3Int a_stIdx, Vector3 a_stOffset)
	{
		var stPos = new Vector3(a_stIdx.x * KDefine.E20_CELL_WIDTH,
			a_stIdx.y * -KDefine.E20_CELL_HEIGHT, 0.0f);

		return this.GetPivot() + stPos + a_stOffset;
	}

	/** 인덱스를 반환한다 */
	public Vector3Int GetIdx(Vector3 a_stPos)
	{
		float fDeltaX = a_stPos.x - this.GetPivot().x;
		float fDeltaY = a_stPos.y - this.GetPivot().y;

		return new Vector3Int((int)(fDeltaX / KDefine.E20_CELL_WIDTH),
			(int)(fDeltaY / -KDefine.E20_CELL_HEIGHT), 0);
	}
	#endregion // 접근 함수

	#region 클래스 팩토리 함수
	/** 매개 변수를 생성한다 */
	public static STParams MakeParams(GameObject a_oPlayObjsRoot,
		CE01Example_20 a_oSceneManager, CE01PlayUIsHandler_20 a_oPlayUIsHandler)
	{
		return new STParams()
		{
			m_oPlayObjsRoot = a_oPlayObjsRoot,
			m_oSceneManager = a_oSceneManager,
			m_oPlayUIsHandler = a_oPlayUIsHandler
		};
	}
	#endregion // 클래스 팩토리 함수
}
