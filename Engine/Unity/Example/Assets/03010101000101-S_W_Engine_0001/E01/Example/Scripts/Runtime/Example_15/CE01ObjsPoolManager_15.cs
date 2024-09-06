using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 풀 리스트 */
public class CE01PoolList_15<T>
{
	public List<T> m_oList = new List<T>();
	public Queue<T> m_oQueue = new Queue<T>();
}

/** 객체 풀 관리자 */
public class CE01ObjsPoolManager_15 : CComponent
{
	#region 변수
	private Dictionary<System.Type, CE01PoolList_15<object>> m_oPoolListDict = new Dictionary<System.Type, CE01PoolList_15<object>>();
	#endregion // 변수

	#region 함수
	/** 객체를 활성화한다 */
	public object SpawnObj<T>(System.Func<object> a_oCreator)
	{
		var oPoolList = m_oPoolListDict.GetValueOrDefault(typeof(T)) ?? new CE01PoolList_15<object>();
		var oObj = (oPoolList.m_oQueue.Count >= 1) ? oPoolList.m_oQueue.Dequeue() : a_oCreator();

		// 중복 객체가 아닐 경우
		if(!oPoolList.m_oList.Contains(oObj))
		{
			oPoolList.m_oList.Add(oObj);
		}

		m_oPoolListDict.TryAdd(typeof(T), oPoolList);
		return oObj;
	}

	/** 객체를 비활성화한다 */
	public void DespawnObj<T>(object a_oObj, System.Action<object> a_oCallback)
	{
		// 객체 풀이 존재 할 경우
		if(m_oPoolListDict.TryGetValue(typeof(T), out CE01PoolList_15<object> oPoolList))
		{
			oPoolList.m_oList.Remove(a_oObj);
			oPoolList.m_oQueue.Enqueue(a_oObj);

			a_oCallback?.Invoke(a_oObj);
		}
	}
	#endregion // 함수
}
