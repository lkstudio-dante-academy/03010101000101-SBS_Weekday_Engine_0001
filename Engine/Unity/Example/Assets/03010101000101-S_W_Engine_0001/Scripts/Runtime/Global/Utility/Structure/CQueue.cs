using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 큐 */
public class CQueue<T>
{
	#region 변수
	private int m_nFront = 0;
	private int m_nRear = 0;

	private T[] m_oVals = null;
	#endregion // 변수

	#region 프로퍼티
	public bool IsEmpty => m_nFront == m_nRear;
	#endregion // 프로퍼티

	#region 함수
	/** 생성자 */
	public CQueue(int a_nSize = 5)
	{
		m_oVals = new T[a_nSize];
	}

	/** 값을 추가한다 */
	public void Enqueue(T a_tVal)
	{
		// 배열이 가득 찼을 경우
		if((m_nRear + 1) % m_oVals.Length == m_nFront)
		{
			var oPrevVals = m_oVals;
			m_oVals = new T[m_oVals.Length * 2];

			int nFront = m_nFront;
			int nRear = m_nRear;

			m_nFront = 0;
			m_nRear = 0;

			for(int i = nFront; i != nRear; i = (i + 1) % oPrevVals.Length)
			{
				this.Enqueue(oPrevVals[i]);
			}
		}

		m_oVals[m_nRear] = a_tVal;
		m_nRear = (m_nRear + 1) % m_oVals.Length;
	}

	/** 값을 제거한다 */
	public T Dequeue()
	{
		int nFront = m_nFront;
		m_nFront = (m_nFront + 1) % m_oVals.Length;

		return m_oVals[nFront];
	}
	#endregion // 함수
}
