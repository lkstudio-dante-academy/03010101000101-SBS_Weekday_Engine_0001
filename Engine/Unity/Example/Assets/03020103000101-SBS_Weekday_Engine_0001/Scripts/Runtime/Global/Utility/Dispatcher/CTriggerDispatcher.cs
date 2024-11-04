using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 접촉 전파자 */
public class CTriggerDispatcher : CComponent
{
	#region 프로퍼티
	public System.Action<CTriggerDispatcher, Collider> EnterCallback { get; set; } = null;
	public System.Action<CTriggerDispatcher, Collider> StayCallback { get; set; } = null;
	public System.Action<CTriggerDispatcher, Collider> ExitCallback { get; set; } = null;

	public System.Action<CTriggerDispatcher, Collider2D> Enter2DCallback { get; set; } = null;
	public System.Action<CTriggerDispatcher, Collider2D> Stay2DCallback { get; set; } = null;
	public System.Action<CTriggerDispatcher, Collider2D> Exit2DCallback { get; set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 접촉이 시작 되었을 경우 */
	public void OnTriggerEnter(Collider a_oCollider)
	{
		this.EnterCallback?.Invoke(this, a_oCollider);
	}

	/** 접촉이 진행 중 일 경우 */
	public void OnTriggerStay(Collider a_oCollider)
	{
		this.StayCallback?.Invoke(this, a_oCollider);
	}

	/** 접촉이 종료 되었을 경우 */
	public void OnTriggerExit(Collider a_oCollider)
	{
		this.ExitCallback?.Invoke(this, a_oCollider);
	}

	/** 접촉이 시작 되었을 경우 */
	public void OnTriggerEnter2D(Collider2D a_oCollider)
	{
		this.Enter2DCallback?.Invoke(this, a_oCollider);
	}

	/** 접촉이 진행 중 일 경우 */
	public void OnTriggerStay2D(Collider2D a_oCollider)
	{
		this.Stay2DCallback?.Invoke(this, a_oCollider);
	}

	/** 접촉이 종료 되었을 경우 */
	public void OnTriggerExit2D(Collider2D a_oCollider)
	{
		this.Exit2DCallback?.Invoke(this, a_oCollider);
	}
	#endregion // 함수
}
