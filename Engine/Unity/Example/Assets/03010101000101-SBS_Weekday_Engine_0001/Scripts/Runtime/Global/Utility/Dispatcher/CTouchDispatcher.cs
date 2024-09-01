using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/** 터치 전달자 */
public class CTouchDispatcher : CComponent, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	#region 프로퍼티
	public System.Action<CTouchDispatcher, PointerEventData> BeginCallback { get; private set; } = null;
	public System.Action<CTouchDispatcher, PointerEventData> MoveCallback { get; private set; } = null;
	public System.Action<CTouchDispatcher, PointerEventData> EndCallback { get; private set; } = null;
	#endregion // 프로퍼티

	#region IPointerDownHandler
	/** 터치가 시작 되었을 경우 */
	public void OnPointerDown(PointerEventData a_oEventData)
	{
		this.BeginCallback?.Invoke(this, a_oEventData);
	}
	#endregion // IPointerDownHandler

	#region IPointerUpHandler
	/** 터치가 종료 되었을 경우 */
	public void OnPointerUp(PointerEventData a_oEventData)
	{
		this.EndCallback?.Invoke(this, a_oEventData);
	}
	#endregion // IPointerUpHandler

	#region IDragHandler
	/** 터치가 이동 되었을 경우 */
	public void OnDrag(PointerEventData a_oEventData)
	{
		this.MoveCallback?.Invoke(this, a_oEventData);
	}
	#endregion // IDragHandler

	#region 접근 함수
	/** 시작 콜백을 변경한다 */
	public void SetBeginCallback(System.Action<CTouchDispatcher, PointerEventData> a_oCallback)
	{
		this.BeginCallback = a_oCallback;
	}

	/** 이동 콜백을 변경한다 */
	public void SetMoveCallback(System.Action<CTouchDispatcher, PointerEventData> a_oCallback)
	{
		this.MoveCallback = a_oCallback;
	}

	/** 종료 콜백을 변경한다 */
	public void SetEndCallback(System.Action<CTouchDispatcher, PointerEventData> a_oCallback)
	{
		this.EndCallback = a_oCallback;
	}
	#endregion // 접근 함수
}
