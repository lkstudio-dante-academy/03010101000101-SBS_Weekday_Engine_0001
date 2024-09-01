using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 연결 리스트 */
public class CLinkedList<T>
{
	/** 노드 */
	public class CNode
	{
		public T m_tVal;

		public CNode m_oPrevNode;
		public CNode m_oNextNode;
	}

	#region 변수
	private int m_nNumVals = 0;
	private CNode m_oHeadNode = null;
	#endregion // 변수

	#region 프로퍼티
	public int Count => m_nNumVals;
	#endregion // 프로퍼티

	#region 함수
	/** 인덱서 */
	public T this[int a_nIdx]
	{
		get
		{
			var oNode = this.GetNode(a_nIdx);
			return oNode.m_tVal;
		}
		set
		{
			var oNode = this.GetNode(a_nIdx);
			oNode.m_tVal = value;
		}
	}

	/** 값을 추가한다 */
	public void Add(T a_tVal)
	{
		var oNode = this.CreateNode(a_tVal);

		// 헤드 노드가 없을 경우
		if(m_oHeadNode == null)
		{
			m_oHeadNode = oNode;
		}
		else
		{
			var oTailNode = m_oHeadNode;

			while(oTailNode.m_oNextNode != null)
			{
				oTailNode = oTailNode.m_oNextNode;
			}

			oTailNode.m_oNextNode = oNode;
			oNode.m_oPrevNode = oTailNode;
		}

		m_nNumVals += 1;
	}

	/** 값을 추가한다 */
	public void Insert(int a_nIdx, T a_tVal)
	{
		// 인덱스 유효하지 않을 경우
		if(!this.IsValidIdx(a_nIdx))
		{
			return;
		}

		var oNode = this.CreateNode(a_tVal);

		// 헤드 위치에 추가 할 경우
		if(a_nIdx <= 0)
		{
			oNode.m_oNextNode = m_oHeadNode;
			m_oHeadNode.m_oPrevNode = oNode;

			m_oHeadNode = oNode;
		}
		else
		{
			var oInsertNode = this.GetNode(a_nIdx);
			var oPrevNode = oInsertNode.m_oPrevNode;

			oNode.m_oPrevNode = oInsertNode.m_oPrevNode;
			oNode.m_oNextNode = oInsertNode;

			oPrevNode.m_oNextNode = oNode;
			oInsertNode.m_oPrevNode = oNode;
		}

		m_nNumVals += 1;
	}

	/** 값을 제거한다 */
	public void Remove(T a_tVal)
	{
		int nResult = this.Find(a_tVal);
		this.RemoveAt(nResult);
	}

	/** 값을 제거한다 */
	public void RemoveAt(int a_nIdx)
	{
		// 인덱스가 유효하지 않을 경우
		if(!this.IsValidIdx(a_nIdx))
		{
			return;
		}

		var oRemoveNode = this.GetNode(a_nIdx);

		// 헤드 노드 일 경우
		if(m_oHeadNode == oRemoveNode)
		{
			m_oHeadNode = oRemoveNode.m_oNextNode;

			// 헤드가 존재 할 경우
			if(m_oHeadNode != null)
			{
				m_oHeadNode.m_oPrevNode = null;
			}
		}
		else
		{
			var oPrevNode = oRemoveNode.m_oPrevNode;
			var oNextNode = oRemoveNode.m_oNextNode;

			oPrevNode.m_oNextNode = oNextNode;

			// 다음 노드가 존재 할 경우
			if(oNextNode != null)
			{
				oNextNode.m_oPrevNode = oPrevNode;
			}
		}

		m_nNumVals -= 1;
	}
	#endregion // 함수

	#region 접근 함수
	/** 인덱스 유효 여부를 검사한다 */
	public bool IsValidIdx(int a_nIdx)
	{
		return a_nIdx >= 0 && a_nIdx < m_nNumVals;
	}

	/** 값을 탐색한다 */
	public int Find(T a_tVal)
	{
		int nIdx = 0;
		var oCurNode = m_oHeadNode;

		while(oCurNode != null)
		{
			// 값이 존재 할 경우
			if(oCurNode.m_tVal.Equals(a_tVal))
			{
				return nIdx;
			}

			nIdx += 1;
			oCurNode = oCurNode.m_oNextNode;
		}

		return -1;
	}

	/** 노드를 반환한다 */
	private CNode GetNode(int a_nIdx)
	{
		// 인덱스가 유효하지 않을 경우
		if(!this.IsValidIdx(a_nIdx))
		{
			return null;
		}

		var oCurNode = m_oHeadNode;

		for(int i = 0; i < a_nIdx; ++i)
		{
			oCurNode = oCurNode.m_oNextNode;
		}

		return oCurNode;
	}
	#endregion // 접근 함수

	#region 팩토리 함수
	/** 노드를 생성한다 */
	private CNode CreateNode(T a_tVal)
	{
		var oNode = new CNode();
		oNode.m_tVal = a_tVal;
		oNode.m_oPrevNode = null;
		oNode.m_oNextNode = null;

		return oNode;
	}
	#endregion // 팩토리 함수
}
