using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if E23_QUEUE
using UnityEngine.EventSystems;
using DG.Tweening;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/** 노드 */
[JsonObject]
public record STE01Node_23
{
	[JsonProperty("ID")] public int m_nID = 0;
	[JsonProperty("C")] public int m_nCost = 0;
	[JsonProperty("P")] public STVec3 m_stPos = Vector3.zero;

	[JsonProperty("NNIDL")] public List<int> m_oNeighborNodeIDList = new List<int>();
	[JsonIgnore] public List<STE01Node_23> m_oNeighborNodeList = new List<STE01Node_23>();
}

/** Example 23 - 큐 */
public partial class CE01Example_23 : CSceneManager
{
	/** 노드 객체 정보 */
	public record STE01NodeObjInfo_23
	{
		public STE01Node_23 m_stNode;
		public CE01Target_23 m_oTarget = null;
		public List<LineRenderer> m_oLineList = new List<LineRenderer>();
	}

	/** 정점 정보 */
	public record STE01VertexInfo_23
	{
		public int m_nCost = 0;
		public STE01NodeObjInfo_23 m_oCurNodeObjInfo = null;
		public STE01VertexInfo_23 m_stPrevVertexInfo = null;
	}

	#region 변수
	private int m_nLastNodeID = 0;

	private Tween m_oMoveAni = null;
	private CE01Target_23 m_oSelTarget = null;
	private List<STE01Node_23> m_oNodeList = new List<STE01Node_23>();
	private List<STE01NodeObjInfo_23> m_oNodeObjInfoList = new List<STE01NodeObjInfo_23>();
	#endregion // 변수

	#region 프로퍼티
	private string FILE_PATH_NODES =>
		$"{Application.dataPath}/../ExternalDatas/Example_23/Nodes.json";
	#endregion // 프로퍼티

	#region 함수
	/** 노드 객체를 리셋한다 */
	public void ResetNodeObjs()
	{
		for(int i = 0; i < m_oNodeObjInfoList.Count; ++i)
		{
			for(int j = 0; j < m_oNodeObjInfoList[i].m_oLineList.Count; ++j)
			{
				Destroy(m_oNodeObjInfoList[i].m_oLineList[j]);
			}

			Destroy(m_oNodeObjInfoList[i].m_oTarget.gameObject);
		}

		m_oSelTarget = null;
		this.SetupNodeObjs();
	}

	/** 노드 추가 버튼을 눌렀을 경우 */
	public void OnTouchAddNodeBtn()
	{
		var stNode = this.CreateNode(m_nLastNodeID++, 0);
		stNode.m_stPos = Vector3.zero;

		var oNodeObj = this.CreateNodeObj(stNode);

		m_oNodeList.Add(stNode);
		m_oNodeObjInfoList.Add(oNodeObj);
	}

	/** 노드 제거 버튼을 눌렀을 경우 */
	public void OnTouchRemoveNodeBtn()
	{
		// 선택 된 노드가 없을 경우
		if(m_oSelTarget == null)
		{
			return;
		}

		int nResult = m_oNodeObjInfoList.FindIndex((a_stNodeObjInfo) => a_stNodeObjInfo.m_oTarget == m_oSelTarget);

		if(nResult >= 0 && nResult < m_oNodeObjInfoList.Count)
		{
			var stNode = m_oNodeObjInfoList[nResult].m_stNode;
			var oNeighborNodeList = stNode.m_oNeighborNodeList;

			for(int i = 0; i < oNeighborNodeList.Count; ++i)
			{
				oNeighborNodeList[i].m_oNeighborNodeList.Remove(stNode);
			}

			m_oNodeList.Remove(stNode);
			this.ResetNodeObjs();
		}
	}

	/** 저장 버튼을 눌렀을 경우 */
	public void OnTouchSaveBtn()
	{
		m_oNodeList.Clear();

		for(int i = 0; i < m_oNodeObjInfoList.Count; ++i)
		{
			m_oNodeList.Add(m_oNodeObjInfoList[i].m_stNode);
		}

		this.SaveNodes();
	}

	/** 경로 탐색 시작 버튼을 눌렀을 경우 */
	public void OnTouchStartPathFindingBtn()
	{
		m_oMoveableTarget.SetActive(true);

		bool bIsValid01 = int.TryParse(m_oStartInput.text, out int nStartID);
		bool bIsValid02 = int.TryParse(m_oEndInput.text, out int nEndID);

		// 위치를 입력하지 않았을 경우
		if(!bIsValid01 || !bIsValid02)
		{
			return;
		}

		int nStartIdx = m_oNodeObjInfoList.FindIndex((a_stNodeObjInfo) =>
			a_stNodeObjInfo.m_stNode.m_nID == nStartID);

		int nEndIdx = m_oNodeObjInfoList.FindIndex((a_stNodeObjInfo) =>
			a_stNodeObjInfo.m_stNode.m_nID == nEndID);

		// 노드 정보가 없을 경우
		if(nStartIdx < 0 || nEndIdx < 0)
		{
			return;
		}

		var oPosList = this.FindPath(nStartIdx, nEndIdx);

		// 경로가 존재 할 경우
		if(oPosList.Count >= 1)
		{
			var oSequence = DOTween.Sequence();
			m_oMoveableTarget.transform.position = oPosList[0];

			for(int i = 1; i < oPosList.Count; ++i)
			{
				var stPos = oPosList[i];
				var oMoveAni = m_oMoveableTarget.transform.DOMove(stPos, 1.0f).SetAutoKill().SetEase(Ease.Linear);

				oSequence.Append(oMoveAni);
			}

			m_oMoveAni?.Kill();
			m_oMoveAni = oSequence;
		}
	}

	/** 경로를 탐색한다 */
	private List<Vector3> FindPath(int a_nStartIdx, int a_nEndIdx)
	{
		var oPosList = new List<Vector3>();
		var oOpenVertexList = new List<STE01VertexInfo_23>();
		var oCloseVertexList = new List<STE01VertexInfo_23>();

		var oCurVertex = this.CreateVertex(a_nStartIdx, 0);
		oOpenVertexList.Add(oCurVertex);

		var oEndNodeObjInfo = m_oNodeObjInfoList[a_nEndIdx];

		while(oOpenVertexList.Count >= 1)
		{
			oCurVertex = oOpenVertexList[0];

			oOpenVertexList.RemoveAt(0);
			oCloseVertexList.Add(oCurVertex);

			// 목적지 일 경우
			if(oCurVertex.m_oCurNodeObjInfo.m_stNode.m_nID ==
				oEndNodeObjInfo.m_stNode.m_nID)
			{
				var oVertexStack = new CStack<Vector3>();

				while(oCurVertex != null)
				{
					oVertexStack.Push(oCurVertex.m_oCurNodeObjInfo.m_oTarget.transform.position);
					oCurVertex = oCurVertex.m_stPrevVertexInfo;
				}

				while(oVertexStack.Count >= 1)
				{
					oPosList.Add(oVertexStack.Pop());
				}

				return oPosList;
			}

			var oNeighborNodeList = oCurVertex.m_oCurNodeObjInfo.m_stNode.m_oNeighborNodeList;

			for(int i = 0; i < oNeighborNodeList.Count; ++i)
			{
				int nIdx = m_oNodeObjInfoList.FindIndex((a_stNodeObjInfo) =>
					a_stNodeObjInfo.m_stNode == oNeighborNodeList[i]);

				var oNeighborVertex = this.CreateVertex(nIdx, oCurVertex.m_nCost + 1);
				oNeighborVertex.m_stPrevVertexInfo = oCurVertex;

				int nCloseVertexIdx = oCloseVertexList.FindIndex((a_stVertexInfo) =>
					a_stVertexInfo.m_oCurNodeObjInfo == oNeighborVertex.m_oCurNodeObjInfo);

				// 새로운 최단 경로가 나왔을 경우
				if(nCloseVertexIdx >= 0 && oCloseVertexList[nCloseVertexIdx].m_nCost > oNeighborVertex.m_nCost)
				{
					oCloseVertexList[nCloseVertexIdx].m_nCost = oNeighborVertex.m_nCost;
					oCloseVertexList[nCloseVertexIdx].m_stPrevVertexInfo = oCurVertex;
				}
				else
				{
					oOpenVertexList.Add(oNeighborVertex);
				}
			}
		}

		return oPosList;
	}

	/** 노드를 로드한다 */
	private void LoadNodes()
	{
		m_oNodeList = Func.ReadObj<List<STE01Node_23>>(FILE_PATH_NODES);
		m_oNodeList = m_oNodeList ?? new List<STE01Node_23>();

		for(int i = 0; i < m_oNodeList.Count; ++i)
		{
			this.RestoreNeighborNodes(m_oNodeList[i]);
		}

		// 노드가 존재 할 경우
		if(m_oNodeList.Count >= 1)
		{
			m_nLastNodeID = m_oNodeList.Max((a_stNode) => a_stNode.m_nID);
			m_nLastNodeID += 1;
		}
	}

	/** 노드를 저장한다 */
	private void SaveNodes()
	{
		for(int i = 0; i < m_oNodeList.Count; ++i)
		{
			this.SetupNeighborNodeIDs(m_oNodeList[i]);
		}

		Func.WriteObj(FILE_PATH_NODES, m_oNodeList);
	}

	/** 노드 객체를 설정한다 */
	private void SetupNodeObjs()
	{
		m_oNodeObjInfoList.Clear();

		for(int i = 0; i < m_oNodeList.Count; ++i)
		{
			var stNodeObjInfo = this.CreateNodeObj(m_oNodeList[i]);
			m_oNodeObjInfoList.Add(stNodeObjInfo);
		}

		for(int i = 0; i < m_oNodeObjInfoList.Count; ++i)
		{
			this.SetupNeighborNodeObjs(m_oNodeObjInfoList[i]);
		}
	}

	/** 이웃 노드 객체를 갱신한다 */
	private void UpdateNeighborNodeObjs(STE01NodeObjInfo_23 a_stNodeObjInfo)
	{
		var oNeighborNodeList = a_stNodeObjInfo.m_stNode.m_oNeighborNodeList;

		for(int i = 0; i < a_stNodeObjInfo.m_oLineList.Count; ++i)
		{
			var oLine = a_stNodeObjInfo.m_oLineList[i];

			int nResult = m_oNodeObjInfoList.FindIndex((a_stCompareNodeObjInfo) =>
				a_stCompareNodeObjInfo.m_stNode == oNeighborNodeList[i]);

			var oPosList = new List<Vector3>() {
				a_stNodeObjInfo.m_oTarget.transform.position,
				m_oNodeObjInfoList[nResult].m_oTarget.transform.position
			};

			oLine.positionCount = oPosList.Count;
			oLine.SetPositions(oPosList.ToArray());
		}
	}

	/** 이웃 노드 객체를 설정한다 */
	private void SetupNeighborNodeObjs(STE01NodeObjInfo_23 a_stNodeObjInfo)
	{
		var oNeighborNodeList = a_stNodeObjInfo.m_stNode.m_oNeighborNodeList;

		for(int i = 0; i < oNeighborNodeList.Count; ++i)
		{
			var oLine = Factory.CreateCloneGameObj<LineRenderer>("Line",
				m_oQueueOriginLine, m_oQueueLineRoot, Vector3.zero, Vector3.one, Vector3.zero);

			int nResult = m_oNodeObjInfoList.FindIndex((a_stCompareNodeObjInfo) =>
				a_stCompareNodeObjInfo.m_stNode == oNeighborNodeList[i]);

			var oPosList = new List<Vector3>() {
				a_stNodeObjInfo.m_oTarget.transform.position,
				m_oNodeObjInfoList[nResult].m_oTarget.transform.position
			};

			oLine.positionCount = oPosList.Count;
			oLine.SetPositions(oPosList.ToArray());

			a_stNodeObjInfo.m_oLineList.Add(oLine);
		}
	}

	/** 이웃 노드 식별자를 설정한다 */
	private void SetupNeighborNodeIDs(STE01Node_23 a_stNode)
	{
		a_stNode.m_oNeighborNodeIDList.Clear();

		a_stNode.m_oNeighborNodeList.ForEach((a_stNeighborNode) =>
		{
			a_stNode.m_oNeighborNodeIDList.Add(a_stNeighborNode.m_nID);
		});
	}

	/** 이웃 노드를 복원한다 */
	private void RestoreNeighborNodes(STE01Node_23 a_stNode)
	{
		for(int i = 0; i < a_stNode.m_oNeighborNodeIDList.Count; ++i)
		{
			int nID = a_stNode.m_oNeighborNodeIDList[i];
			int nResult = m_oNodeList.FindIndex((a_stCompareNode) => a_stCompareNode.m_nID == nID);

			// 노드가 존재 할 경우
			if(nResult >= 0 && nResult < m_oNodeList.Count)
			{
				a_stNode.m_oNeighborNodeList.Add(m_oNodeList[nResult]);
			}
		}
	}

	/** 터치 시작을 처리한다 */
	private void HandleOnTouchBegin(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData)
	{

		var stRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		bool bIsHit = Physics.Raycast(stRay, out RaycastHit stRaycastHit);

		m_oSelTarget = null;

		// 터치 된 노드가 존재 할 경우
		if(bIsHit)
		{
			m_oSelTarget = stRaycastHit.collider.GetComponentInParent<CE01Target_23>();
			m_oQueueLine.positionCount = 0;
		}
	}

	/** 터치 이동을 처리한다 */
	private void HandleOnTouchMove(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData)
	{

		// 선택 된 노드가 없을 경우
		if(m_oSelTarget == null)
		{
			return;
		}

		var stPos = a_oEventData.ExGetWorldPos();
		int nResult = m_oNodeObjInfoList.FindIndex((a_stNodeObjInfo) => a_stNodeObjInfo.m_oTarget == m_oSelTarget);

		// 노드 정보가 없을 경우
		if(nResult < 0 || nResult >= m_oNodeObjInfoList.Count)
		{
			return;
		}

		// 마우스 왼쪽 버튼을 눌렀을 경우
		if(Input.GetMouseButton((int)EMouseBtn.LEFT))
		{
			m_oSelTarget.transform.position = stPos;
			m_oNodeObjInfoList[nResult].m_stNode.m_stPos = stPos;

			for(int i = 0; i < m_oNodeObjInfoList.Count; ++i)
			{
				this.UpdateNeighborNodeObjs(m_oNodeObjInfoList[i]);
			}
		}
		// 마우스 오른쪽 버튼을 눌렀을 경우
		else if(Input.GetMouseButton((int)EMouseBtn.RIGHT))
		{
			var oPosList = new List<Vector3>() {
				m_oSelTarget.transform.position,
				stPos
			};

			m_oQueueLine.positionCount = oPosList.Count;
			m_oQueueLine.SetPositions(oPosList.ToArray());
		}
	}

	/** 터치 종료를 처리한다 */
	private void HandleOnTouchEnd(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData)
	{

		this.HandleOnTouchMove(a_oSender, a_oEventData);
		m_oQueueLine.positionCount = 0;

		// 마우스 오른쪽 버튼 입력 종료가 아닐 경우
		if(!Input.GetMouseButtonUp((int)EMouseBtn.RIGHT))
		{
			return;
		}

		var stRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		bool bIsHit = Physics.Raycast(stRay, out RaycastHit stRaycastHit);

		// 터치 된 노드가 존재 할 경우
		if(bIsHit)
		{
			var oHitTarget = stRaycastHit.collider.GetComponentInParent<CE01Target_23>();

			// 목적지 노드가 없을 경우
			if(oHitTarget == null || m_oSelTarget == oHitTarget)
			{
				return;
			}

			int nSrc = m_oNodeObjInfoList.FindIndex((a_stNodeObjInfo) => a_stNodeObjInfo.m_oTarget == m_oSelTarget);
			int nDest = m_oNodeObjInfoList.FindIndex((a_stNodeObjInfo) => a_stNodeObjInfo.m_oTarget == oHitTarget);

			// 출발지 or 목적지 노드가 없을 경우
			if(nSrc < 0 || nDest < 0)
			{
				return;
			}

			var stSrcNode = m_oNodeObjInfoList[nSrc].m_stNode;
			var stDestNode = m_oNodeObjInfoList[nDest].m_stNode;

			// 목적지 노드와 연결 되지 않았을 경우
			if(!stSrcNode.m_oNeighborNodeList.Contains(stDestNode))
			{
				stSrcNode.m_oNeighborNodeList.Add(stDestNode);
			}

			// 출발지 노드와 연결 되지 않았을 경우
			if(!stDestNode.m_oNeighborNodeList.Contains(stSrcNode))
			{
				stDestNode.m_oNeighborNodeList.Add(stSrcNode);
			}

			this.ResetNodeObjs();
		}
	}
	#endregion // 함수

	#region 팩토리 함수
	/** 노드를 생성한다 */
	private STE01Node_23 CreateNode(int a_nID, int a_nCost)
	{
		return new STE01Node_23()
		{
			m_nID = a_nID,
			m_nCost = a_nCost
		};
	}

	/** 노드 객체를 생성한다 */
	private STE01NodeObjInfo_23 CreateNodeObj(STE01Node_23 a_stNode)
	{
		var oTarget = Factory.CreateCloneGameObj<CE01Target_23>("NodeObj",
			m_oQueueOriginTarget, m_oQueueTargetRoot, Vector3.zero, Vector3.one, Vector3.zero);

		oTarget.SetVal(a_stNode.m_nID);
		oTarget.transform.position = a_stNode.m_stPos;

		return new STE01NodeObjInfo_23()
		{
			m_stNode = a_stNode,
			m_oTarget = oTarget
		};
	}

	/** 정점을 생성한다 */
	private STE01VertexInfo_23 CreateVertex(int a_nIdx, int a_nCost)
	{
		return new STE01VertexInfo_23()
		{
			m_nCost = a_nCost,
			m_oCurNodeObjInfo = m_oNodeObjInfoList[a_nIdx]
		};
	}
	#endregion // 팩토리 함수
}
#endif // #if E23_QUEUE
