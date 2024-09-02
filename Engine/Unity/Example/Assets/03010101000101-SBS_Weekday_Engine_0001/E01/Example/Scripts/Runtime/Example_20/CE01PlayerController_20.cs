using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/** 플레이어 제어자 */
public class CE01PlayerController_20 : CE01AgentController_20
{
	/** 매개 변수 */
	public new record STParams : CE01AgentController_20.STParams
	{
		#region 함수
		/** 생성자 */
		public STParams(CE01Engine_20 a_oEngine,
			System.Action<CE01AgentController_20, Vector3Int> a_oTouchCallback) : base(a_oEngine, a_oTouchCallback)
		{
			// Do Something
		}
		#endregion // 함수
	}

	#region 변수
	private Vector3Int m_stTouchStartIdx = new Vector3Int(-1, -1, -1);
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Init(CE01AgentController_20.STParams a_stParams)
	{
		base.Init(a_stParams);
	}

	/** 터치 시작을 처리한다 */
	public override void HandleOnTouchBegin(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData)
	{
		base.HandleOnTouchBegin(a_oSender, a_oEventData);

		// 터치 가능 상태 일 경우
		if(this.IsEnableTouch)
		{
			var stPos = a_oEventData.ExGetLocalPos(this.gameObject);
			m_stTouchStartIdx = this.Params.m_oEngine.GetIdx(stPos);
		}
	}

	/** 터치 종료를 처리한다 */
	public override void HandleOnTouchEnd(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData)
	{
		base.HandleOnTouchEnd(a_oSender, a_oEventData);

		// 터치 가능 상태 일 경우
		if(this.IsEnableTouch)
		{
			var stPos = a_oEventData.ExGetLocalPos(this.gameObject);
			var stTouchIdx = this.Params.m_oEngine.GetIdx(stPos);

			// 시작 위치와 동일 할 경우
			if(stTouchIdx.Equals(m_stTouchStartIdx))
			{
				this.Params.m_oTouchCallback(this, stTouchIdx);
			}
		}

		m_stTouchStartIdx = new Vector3Int(-1, -1, -1);
	}
	#endregion // 함수

	#region 클래스 팩토리 함수
	/** 매개 변수를 생성한다 */
	public static STParams MakeParams(CE01Engine_20 a_oEngine,
		System.Action<CE01AgentController_20, Vector3Int> a_oTouchCallback)
	{
		return new STParams(a_oEngine, a_oTouchCallback);
	}
	#endregion // 클래스 팩토리 함수
}
