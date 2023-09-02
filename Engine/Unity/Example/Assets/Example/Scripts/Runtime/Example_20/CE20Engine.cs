using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/** 틱택토 엔진 */
public class CE20Engine : CComponent {
	/** 매개 변수 */
	public record STParams {
		public CE20SceneManager m_oSceneManager = null;
		public CE20PlayUIsHandler m_oPlayUIsHandler = null;

		#region 함수
		/** 생성자 */
		public STParams(CE20SceneManager a_oSceneManager) {
			m_oSceneManager = a_oSceneManager;
		}
		#endregion // 함수
	}

	#region 변수

	#endregion // 변수

	#region 프로퍼티
	public STParams Params { get; private set; } = null;
	public bool IsPlaying { get; private set; } = false;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		this.Params = a_stParams;

		// 전달자를 설정한다 {
		var oPlayUIs = a_stParams.m_oSceneManager.UIs.transform.Find("PlayUIs");

		var oDispatcher = oPlayUIs.GetComponentInChildren<CTouchDispatcher>();
		oDispatcher.SetBeginCallback(this.HandleOnTouchBegin);
		oDispatcher.SetEndCallback(this.HandleOnTouchEnd);
		// 전달자를 설정한다 }
	}

	/** 게임 플레이를 시작한다 */
	public void Play() {
		this.IsPlaying = true;
	}

	/** 터치 시작을 처리한다 */
	private void HandleOnTouchBegin(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData) {

	}

	/** 터치 종료를 처리한다 */
	private void HandleOnTouchEnd(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData) {

	}

	/** 기준 위치를 반환한다 */
	private Vector3 GetPivot() {
		float fGridWidth = this.Params.m_oPlayUIsHandler.Params.m_stGridSize.x * KDefine.E20_CELL_WIDTH;
		float fGridHeight = this.Params.m_oPlayUIsHandler.Params.m_stGridSize.y * KDefine.E20_CELL_HEIGHT;

		return new Vector3(fGridWidth / -2.0f, fGridHeight / 2.0f, 0.0f);
	}

	/** 인덱스를 반환한다 */
	private Vector3Int GetIdx(Vector3 a_stPos) {
		float fDeltaX = a_stPos.x - this.GetPivot().x;
		float fDeltaY = a_stPos.y - this.GetPivot().y;

		return new Vector3Int((int)(fDeltaX / KDefine.E20_CELL_WIDTH),
			(int)(fDeltaY / KDefine.E20_CELL_HEIGHT), 0);
	}
	#endregion // 함수

	#region 클래스 팩토리 함수
	/** 매개 변수를 생성한다 */
	public static STParams MakeParams(CE20SceneManager a_oSceneManager) {
		return new STParams(a_oSceneManager);
	}
	#endregion // 클래스 팩토리 함수
}
