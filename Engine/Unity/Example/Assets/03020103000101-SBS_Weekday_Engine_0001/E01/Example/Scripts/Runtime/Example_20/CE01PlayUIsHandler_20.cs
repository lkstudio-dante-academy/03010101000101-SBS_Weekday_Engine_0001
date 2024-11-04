using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/** 플레이 UI 처리자 */
public class CE01PlayUIsHandler_20 : CE01UIsHandler_20
{
	/** 매개 변수 */
	public new record STParams : CE01UIsHandler_20.STParams
	{
		public Vector2Int m_stGridSize;

		#region 함수
		/** 생성자 */
		public STParams(CE01Example_20 a_oSceneManager, Vector2Int a_stGridSize) : base(a_oSceneManager)
		{
			m_stGridSize = a_stGridSize;
		}
		#endregion // 함수
	}

	#region 변수
	[SerializeField] private GameObject m_oTextRoot = null;
	[SerializeField] private GameObject m_oOriginText = null;

	[SerializeField] private GameObject m_oGridLineRoot = null;
	[SerializeField] private GameObject m_oOriginGridLine = null;

	private TMP_Text[,] m_oTexts = null;
	private List<GameObject> m_oGridLineList = new List<GameObject>();
	#endregion // 변수

	#region 프로퍼티
	public new STParams Params { get; private set; }
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public virtual void Init(STParams a_stParams)
	{
		base.Init(a_stParams);
		this.Params = a_stParams;

		this.SetupTexts();
		this.SetupGridLines();
	}

	/** 결과를 출력한다 */
	public void ShowResult(CE01Engine_20.EResult a_eResult)
	{
		CE01ResultStorage_20.Inst.Result = a_eResult;
		CSceneLoader.Inst.LoadScene(KDefine.G_N_SCENE_EXAMPLE_21, UnityEngine.SceneManagement.LoadSceneMode.Additive);
	}

	/** 상태를 갱신한다 */
	protected override void UpdateState()
	{
		base.UpdateState();

		for(int i = 0; i < m_oTexts.GetLength(0); ++i)
		{
			for(int j = 0; j < m_oTexts.GetLength(1); ++j)
			{
				var eCellState = this.Params.m_oSceneManager.Engine.CellStates[i, j];
				m_oTexts[i, j].text = this.GetCellStateStr(eCellState);
			}
		}
	}

	/** 텍스트를 설정한다 */
	private void SetupTexts()
	{
		m_oTexts = new TMP_Text[this.Params.m_stGridSize.y, this.Params.m_stGridSize.x];

		for(int i = 0; i < m_oTexts.GetLength(0); ++i)
		{
			for(int j = 0; j < m_oTexts.GetLength(1); ++j)
			{
				var stIdx = new Vector3Int(j, i, 0);

				var stOffset = new Vector3(KDefine.E20_CELL_WIDTH / 2.0f,
					KDefine.E20_CELL_HEIGHT / -2.0f, 0.0f);

				var oText = Factory.CreateCloneGameObj<TMP_Text>("Text",
					m_oOriginText, m_oTextRoot, Vector3.zero, Vector3.one, Vector3.zero);

				oText.transform.localPosition = this.Params.m_oSceneManager.Engine.GetPos(stIdx, stOffset);
				m_oTexts[i, j] = oText;
			}
		}
	}

	/** 그리드 라인을 설정한다 */
	private void SetupGridLines()
	{
		float fGridWidth = this.Params.m_stGridSize.x * KDefine.E20_CELL_WIDTH;
		float fGridHeight = this.Params.m_stGridSize.y * KDefine.E20_CELL_HEIGHT;

		var stPivotPos = new Vector3(fGridWidth / -2.0f, fGridHeight / 2.0f, 0.0f);
		var oPosListContainer = new List<List<Vector3>>();

		for(int i = 1; i < this.Params.m_stGridSize.x; ++i)
		{
			var oPosList = new List<Vector3>() {
				stPivotPos + new Vector3(i * KDefine.E20_CELL_WIDTH, 0.0f, 0.0f),
				stPivotPos + new Vector3(i * KDefine.E20_CELL_WIDTH, -fGridHeight, 0.0f)
			};

			oPosListContainer.Add(oPosList);
		}

		for(int i = 1; i < this.Params.m_stGridSize.y; ++i)
		{
			var oPosList = new List<Vector3>() {
				stPivotPos + new Vector3(0.0f, i * -KDefine.E20_CELL_HEIGHT, 0.0f),
				stPivotPos + new Vector3(fGridWidth, i * -KDefine.E20_CELL_HEIGHT, 0.0f)
			};

			oPosListContainer.Add(oPosList);
		}

		for(int i = 0; i < oPosListContainer.Count; ++i)
		{
			var oGridLine = Factory.CreateCloneGameObj<LineRenderer>("GridLine",
				m_oOriginGridLine, m_oGridLineRoot, Vector3.zero, Vector3.one, Vector3.zero);

			oGridLine.positionCount = oPosListContainer[i].Count;
			oGridLine.SetPositions(oPosListContainer[i].ToArray());
		}
	}
	#endregion // 함수

	#region 접근 함수
	/** 셀 상태 문자열을 반환한다 */
	public string GetCellStateStr(CE01Engine_20.ECellState a_eState)
	{
		switch(a_eState)
		{
			case CE01Engine_20.ECellState.PLAYER_01:
				return "O";
			case CE01Engine_20.ECellState.PLAYER_02:
				return "X";
		}

		return string.Empty;
	}
	#endregion // 접근 함수

	#region 클래스 팩토리 함수
	/** 매개 변수를 생성한다 */
	public static STParams MakeParams(CE01Example_20 a_oSceneManager,
		Vector2Int a_stGridSize)
	{
		return new STParams(a_oSceneManager, a_stGridSize);
	}
	#endregion // 클래스 팩토리 함수
}
