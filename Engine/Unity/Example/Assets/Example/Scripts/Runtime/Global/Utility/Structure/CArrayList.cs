using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 배열 리스트 */
public class CArrayList<T> {
	#region 변수
	private int m_nNumVals = 0;
	private T[] m_oVals = null;
	#endregion // 변수

	#region 프로퍼티
	public int Count => m_nNumVals;
	#endregion // 프로퍼티

	#region 함수
	/** 인덱서 */
	public T this[int a_nIdx] {
		get {
			return m_oVals[a_nIdx];
		}
		set {
			m_oVals[a_nIdx] = value;
		}
	}

	/** 생성자 */
	public CArrayList(int a_nSize = 5) {
		m_oVals = new T[a_nSize];
	}

	/** 값을 추가한다 */
	public void Add(T a_tVal) {
		// 배열이 가득 찼을 경우
		if(m_nNumVals >= m_oVals.Length) {
			System.Array.Resize(ref m_oVals, m_oVals.Length * 2);
		}

		m_oVals[m_nNumVals++] = a_tVal;
	}

	/** 값을 추가한다 */
	public void Insert(int a_nIdx, T a_tVal) {

	}

	/** 값을 제거한다 */
	public void Remove(T a_tVal) {

	}

	/** 값을 제거한다 */
	public void RemoveAt(int a_nIdx) {

	}
	#endregion // 함수
}
