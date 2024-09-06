using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/** 에이전트 제어자 */
public abstract class CE01AgentController_20 : CComponent
{
	/** 매개 변수 */
	public record STParams
	{
		public CE01Engine_20 m_oEngine = null;
		public System.Action<CE01AgentController_20, Vector3Int> m_oTouchCallback = null;

		#region 함수
		/** 생성자 */
		public STParams(CE01Engine_20 a_oEngine,
			System.Action<CE01AgentController_20, Vector3Int> a_oTouchCallback)
		{
			m_oEngine = a_oEngine;
			m_oTouchCallback = a_oTouchCallback;
		}
		#endregion // 함수
	}

	#region 프로퍼티
	public STParams Params { get; private set; } = null;
	public bool IsEnableTouch { get; private set; } = false;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public virtual void Init(STParams a_stParams)
	{
		this.Params = a_stParams;
	}

	/** 터치 시작을 처리한다 */
	public virtual void HandleOnTouchBegin(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData)
	{
		// Do Something
	}

	/** 터치 종료를 처리한다 */
	public virtual void HandleOnTouchEnd(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData)
	{
		// Do Something
	}
	#endregion // 함수

	#region 접근 함수
	/** 터치 가능 여부를 변경한다 */
	public void SetIsEnableTouch(bool a_bIsEnable)
	{
		this.IsEnableTouch = a_bIsEnable;
	}
	#endregion // 접근 함수
}
