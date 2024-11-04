using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 스택 */
public class CStack<T>
{
	#region 변수
	private int m_nTop = 0;
	private T[] m_oVals = null;
	#endregion // 변수

	#region 프로퍼티
	public int Count => m_nTop;
	#endregion // 프로퍼티

	#region 함수
	/** 생성자 */
	public CStack(int a_nSize = 5)
	{
		m_oVals = new T[a_nSize];
	}

	/** 값을 추가한다 */
	public void Push(T a_tVal)
	{
		// 배열이 가득 찼을 경우
		if(m_nTop >= m_oVals.Length)
		{
			System.Array.Resize(ref m_oVals, m_oVals.Length * 2);
		}

		m_oVals[m_nTop++] = a_tVal;
	}

	/** 값을 제거한다 */
	public T Pop()
	{
		return m_oVals[--m_nTop];
	}
	#endregion // 함수
}
