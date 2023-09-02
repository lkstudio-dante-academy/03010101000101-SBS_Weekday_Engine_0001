using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 플레이 UI 처리자 */
public class CE20PlayUIsHandler : CE20UIsHandler {
	/** 매개 변수 */
	public new record STParams : CE20UIsHandler.STParams {
		public Vector2Int m_stGridSize;

		#region 함수
		/** 생성자 */
		public STParams(CE20SceneManager a_oSceneManager, Vector2Int a_stGridSize) : base(a_oSceneManager) {
			m_stGridSize = a_stGridSize;
		}
		#endregion // 함수
	}

	#region 변수
	[SerializeField] private GameObject m_oGridLineRoot = null;
	[SerializeField] private GameObject m_oOriginGridLine = null;

	private List<GameObject> m_oGridLineList = new List<GameObject>();
	#endregion // 변수

	#region 프로퍼티
	public new STParams Params { get; private set; }
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		base.Init(a_stParams);
		this.Params = a_stParams;

		this.SetupGridLines();
	}

	/** 그리드 라인을 설정한다 */
	private void SetupGridLines() {
		float fGridWidth = this.Params.m_stGridSize.x * KDefine.E20_CELL_WIDTH;
		float fGridHeight = this.Params.m_stGridSize.y * KDefine.E20_CELL_HEIGHT;

		var stPivotPos = new Vector3(fGridWidth / -2.0f, fGridHeight / 2.0f, 0.0f);
		var oPosListContainer = new List<List<Vector3>>();

		for(int i = 1; i < this.Params.m_stGridSize.x; ++i) {
			var oPosList = new List<Vector3>() {
				stPivotPos + new Vector3(i * KDefine.E20_CELL_WIDTH, 0.0f, 0.0f),
				stPivotPos + new Vector3(i * KDefine.E20_CELL_WIDTH, -fGridHeight, 0.0f)
			};

			oPosListContainer.Add(oPosList);
		}

		for(int i = 1; i < this.Params.m_stGridSize.y; ++i) {
			var oPosList = new List<Vector3>() {
				stPivotPos + new Vector3(0.0f, i * -KDefine.E20_CELL_HEIGHT, 0.0f),
				stPivotPos + new Vector3(fGridWidth, i * -KDefine.E20_CELL_HEIGHT, 0.0f)
			};

			oPosListContainer.Add(oPosList);
		}

		for(int i = 0; i < oPosListContainer.Count; ++i) {
			var oGridLine = CFactory.CreateCloneGameObj<LineRenderer>("GridLine",
				m_oOriginGridLine, m_oGridLineRoot, Vector3.zero, Vector3.one, Vector3.zero);

			oGridLine.positionCount = oPosListContainer[i].Count;
			oGridLine.SetPositions(oPosListContainer[i].ToArray());
		}
	}
	#endregion // 함수

	#region 클래스 팩토리 함수
	/** 매개 변수를 생성한다 */
	public static STParams MakeParams(CE20SceneManager a_oSceneManager, 
		Vector2Int a_stGridSize) {
		return new STParams(a_oSceneManager, a_stGridSize);
	}
	#endregion // 클래스 팩토리 함수
}
