using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 타이머 */
public class CTimer : CComponent
{
	/** 매개 변수 */
	public struct STParams
	{
		public int m_nTimes;
		public float m_fDelay;
		public float m_fInterval;
		public System.Action<CTimer> m_oCallback;
	}

	#region 변수
	private bool m_bIsDelay = true;

	private int m_nTimes = 0;
	private float m_fUpdateSkipTime = 0.0f;
	#endregion // 변수

	#region 프로퍼티
	public STParams Params { get; private set; }
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public virtual void Init(STParams a_stParams)
	{
		this.Params = a_stParams;
		CScheduleManager.Inst.AddComponent(this);
	}

	/** 상태를 갱신한다 */
	public override void OnUpdate(float a_fDeltaTime)
	{
		base.OnUpdate(a_fDeltaTime);
		m_fUpdateSkipTime += a_fDeltaTime;

		// 딜레이 상태 일 경우
		if(m_bIsDelay)
		{
			this.HandleDelayState();
		}
		else
		{
			this.HandleIntervalState();
		}
	}

	/** 지연 상태를 처리한다 */
	private void HandleDelayState()
	{
		// 지연 시간이 지났을 경우
		if(m_fUpdateSkipTime.ExIsGreatEquals(this.Params.m_fDelay))
		{
			m_bIsDelay = false;
			m_fUpdateSkipTime -= this.Params.m_fDelay;
		}
	}

	/** 타이머 간격 상태를 처리한다 */
	private void HandleIntervalState()
	{
		// 타이머 간격 시간이 지났을 경우
		if(m_fUpdateSkipTime.ExIsGreatEquals(this.Params.m_fInterval))
		{
			m_nTimes += 1;
			m_fUpdateSkipTime -= this.Params.m_fInterval;

			this.Params.m_oCallback?.Invoke(this);

			// 반복 횟수가 지났을 경우
			if(m_nTimes >= this.Params.m_nTimes)
			{
				Destroy(this.gameObject);
			}
		}
	}
	#endregion // 함수

	#region 클래스 팩토리 함수
	/** 매개 변수를 생성한다 */
	public static STParams MakeParams(int a_nTimes,
		float a_fInterval, System.Action<CTimer> a_oCallback, float a_fDelay = 0.0f)
	{
		return new STParams()
		{
			m_nTimes = a_nTimes,
			m_fDelay = a_fDelay,
			m_fInterval = a_fInterval,
			m_oCallback = a_oCallback
		};
	}
	#endregion // 클래스 팩토리 함수
}
