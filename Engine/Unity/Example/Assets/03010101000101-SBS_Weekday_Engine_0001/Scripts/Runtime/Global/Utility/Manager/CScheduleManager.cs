using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 스케줄 관리자 */
public class CScheduleManager : CSingleton<CScheduleManager>
{
	#region 변수
	private List<CComponent> m_oComponentList = new List<CComponent>();
	private List<CComponent> m_oAddComponentList = new List<CComponent>();
	private List<CComponent> m_oRemoveComponentList = new List<CComponent>();

	private Dictionary<string, CTimer> m_oTimerDict = new Dictionary<string, CTimer>();
	#endregion // 변수

	#region 함수
	/** 상태를 리셋한다 */
	public override void Reset()
	{
		base.Reset();
		m_oTimerDict.Clear();

		m_oComponentList.Clear();
		m_oAddComponentList.Clear();
		m_oRemoveComponentList.Clear();
	}

	/** 상태를 갱신한다 */
	public override void Update()
	{
		base.Update();

		for(int i = 0; i < m_oComponentList.Count; ++i)
		{
			// 상태 갱신이 가능 할 경우
			if(this.IsUpdateable(m_oComponentList[i]))
			{
				m_oComponentList[i].OnUpdate(Time.deltaTime);
			}
		}
	}

	/** 상태를 갱신한다 */
	public override void LateUpdate()
	{
		base.LateUpdate();

		for(int i = 0; i < m_oComponentList.Count; ++i)
		{
			// 제거 된 상태 일 경우
			if(m_oComponentList[i].IsDestroy)
			{
				this.RemoveComponent(m_oComponentList[i]);
			}
			// 상태 갱신이 가능 할 경우
			else if(this.IsUpdateable(m_oComponentList[i]))
			{
				m_oComponentList[i].OnLateUpdate(Time.deltaTime);
			}
		}

		this.UpdateComponentsState();
	}

	/** 상태를 갱신한다 */
	public override void FixedUpdate()
	{
		base.FixedUpdate();

		for(int i = 0; i < m_oComponentList.Count; ++i)
		{
			// 상태 갱신이 가능 할 경우
			if(this.IsUpdateable(m_oComponentList[i]))
			{
				m_oComponentList[i].OnFixedUpdate(Time.fixedDeltaTime);
			}
		}
	}

	/** 타이머를 추가한다 */
	public void AddTimer(string a_oKey,
		int a_nTimes, float a_fInterval, System.Action<CTimer> a_oCallback, float a_fDelay = 0.0f)
	{
		// 타이머가 없을 경우
		if(!m_oTimerDict.ContainsKey(a_oKey))
		{
			var stParams = CTimer.MakeParams(a_nTimes,
				a_fInterval, a_oCallback, a_fDelay);

			var oTimer = Factory.CreateGameObj<CTimer>("Timer",
				this.gameObject, Vector3.zero, Vector3.one, Vector3.zero);

			oTimer.Init(stParams);
			oTimer.AddDestroyListener(this.OnDestroyTimer);

			m_oTimerDict.Add(a_oKey, oTimer);
		}
	}

	/** 타이머를 제거한다 */
	public void RemoveTimer(string a_oKey)
	{
		// 타이머가 존재 할 경우
		if(m_oTimerDict.TryGetValue(a_oKey, out CTimer oTimer))
		{
			m_oTimerDict.Remove(a_oKey);

			// 타이머 제거가 필요 할 경우
			if(!oTimer.IsDestroy)
			{
				Destroy(oTimer.gameObject);
			}
		}
	}

	/** 컴포넌트를 추가한다 */
	public void AddComponent(CComponent a_oComponent)
	{
		// 컴포넌트 추가가 가능 할 경우
		if(!m_oComponentList.Contains(a_oComponent) &&
			!m_oAddComponentList.Contains(a_oComponent))
		{
			m_oAddComponentList.Add(a_oComponent);
		}
	}

	/** 컴포넌트를 제거한다 */
	public void RemoveComponent(CComponent a_oComponent)
	{
		// 컴포넌트 제거가 가능 할 경우
		if(m_oComponentList.Contains(a_oComponent) &&
			!m_oRemoveComponentList.Contains(a_oComponent))
		{
			m_oRemoveComponentList.Add(a_oComponent);
		}
	}

	/** 컴포넌트 상태를 갱신한다 */
	private void UpdateComponentsState()
	{
		for(int i = 0; i < m_oAddComponentList.Count; ++i)
		{
			var oComponent = m_oAddComponentList[i];
			m_oComponentList.Add(oComponent);
		}

		for(int i = 0; i < m_oRemoveComponentList.Count; ++i)
		{
			var oComponent = m_oRemoveComponentList[i];
			m_oComponentList.Remove(oComponent);
		}

		m_oAddComponentList.Clear();
		m_oRemoveComponentList.Clear();
	}

	/** 타이머가 제거 되었을 경우 */
	private void OnDestroyTimer(CComponent a_oSender)
	{
		var oTimer = a_oSender as CTimer;
		var stResult = m_oTimerDict.ExFindVal((a_oTimer) => a_oTimer == oTimer);

		// 타이머가 존재 할 경우
		if(stResult.Item2)
		{
			this.RemoveTimer(stResult.Item1);
		}
	}
	#endregion // 함수

	#region 접근 함수
	/** 갱신 가능 여부를 검사한다 */
	private bool IsUpdateable(CComponent a_oComponent)
	{
		bool bIsValid = !a_oComponent.IsDestroy;
		return bIsValid && a_oComponent.enabled && a_oComponent.gameObject.activeInHierarchy;
	}
	#endregion // 접근 함수
}
