using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if E23_QUEUE
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/** 노드 */
[JsonObject]
public record STNode {
	[JsonProperty("ID")] public int m_nID = 0;
	[JsonProperty("C")] public int m_nCost = 0;
	[JsonProperty("P")] public STVec3 m_stPos = Vector3.zero;

	[JsonProperty("NNIDL")] public List<int> m_oNeighborNodeIDList = new List<int>();
	[JsonIgnore] public List<STNode> m_oNeighborNodeList = new List<STNode>();
}

/** Example 23 - 큐 */
public partial class CE23SceneManager : CSceneManager {
	/** 노드 객체 정보 */
	public record STNodeObjInfo {
		public STNode m_stNode;
		public CE23Target m_oTarget = null;
	}

	#region 변수
	private int m_nLastNodeID = 0;
	private List<STNode> m_oNodeList = new List<STNode>();
	private List<STNodeObjInfo> m_oNodeObjInfoList = new List<STNodeObjInfo>();
	#endregion // 변수

	#region 프로퍼티
	private string FILE_PATH_NODES =>
		$"{Application.dataPath}/../ExternalDatas/Example_23/Nodes.json";
	#endregion // 프로퍼티

	#region 함수
	/** 노드 객체를 리셋한다 */
	public void ResetNodeObjs() {
		for(int i = 0; i < m_oNodeObjInfoList.Count; ++i) {
			Destroy(m_oNodeObjInfoList[i].m_oTarget.gameObject);
		}

		this.SetupNodeObjs();
	}

	/** 노드 추가 버튼을 눌렀을 경우 */
	public void OnTouchAddNodeBtn() {
		var stNode = this.CreateNode(m_nLastNodeID++, 0);
		stNode.m_stPos = Vector3.zero;

		var oNodeObj = this.CreateNodeObj(stNode);
		m_oNodeObjInfoList.Add(oNodeObj);
	}

	/** 노드 제거 버튼을 눌렀을 경우 */
	public void OnTouchRemoveNodeBtn() {

	}

	/** 노드를 로드한다 */
	private void LoadNodes() {
		m_oNodeList = CFunc.ReadObj<List<STNode>>(FILE_PATH_NODES);
		m_oNodeList = m_oNodeList ?? new List<STNode>();

		for(int i = 0; i < m_oNodeList.Count; ++i) {
			this.RestoreNeighborNodes(m_oNodeList[i]);
		}

		m_nLastNodeID = m_oNodeList.Max((a_stNode) => a_stNode.m_nID);
	}

	/** 노드를 저장한다 */
	private void SaveNodes() {
		for(int i = 0; i < m_oNodeList.Count; ++i) {
			this.SetupNeighborNodeIDs(m_oNodeList[i]);
		}

		CFunc.WriteObj(FILE_PATH_NODES, m_oNodeList);
	}

	/** 노드 객체를 설정한다 */
	private void SetupNodeObjs() {
		m_oNodeObjInfoList.Clear();

		for(int i = 0; i < m_oNodeList.Count; ++i) {
			var stNodeObjInfo = this.CreateNodeObj(m_oNodeList[i]);
			m_oNodeObjInfoList.Add(stNodeObjInfo);
		}

		for(int i = 0; i < m_oNodeObjInfoList.Count; ++i) {
			this.SetupNeighborNodeObjs(m_oNodeObjInfoList[i]);
		}
	}

	/** 이웃 노드 객체를 설정한다 */
	private void SetupNeighborNodeObjs(STNodeObjInfo a_stNodeObjInfo) {
		// TODO: 이웃 노드와 링크 설정 필요
	}

	/** 이웃 노드 식별자를 설정한다 */
	private void SetupNeighborNodeIDs(STNode a_stNode) {
		a_stNode.m_oNeighborNodeIDList.Clear();

		a_stNode.m_oNeighborNodeList.ForEach((a_stNeighborNode) => {
			a_stNode.m_oNeighborNodeIDList.Add(a_stNeighborNode.m_nID);
		});
	}

	/** 이웃 노드를 복원한다 */
	private void RestoreNeighborNodes(STNode a_stNode) {
		for(int i = 0; i < a_stNode.m_oNeighborNodeIDList.Count; ++i) {
			int nID = a_stNode.m_oNeighborNodeIDList[i];
			int nResult = m_oNodeList.FindIndex((a_stCompareNode) => a_stCompareNode.m_nID == nID);

			// 노드가 존재 할 경우
			if(nResult >= 0 && nResult < m_oNodeList.Count) {
				a_stNode.m_oNeighborNodeList.Add(m_oNodeList[nResult]);
			}
		}
	}
	#endregion // 함수

	#region 팩토리 함수
	/** 노드를 생성한다 */
	private STNode CreateNode(int a_nID, int a_nCost) {
		return new STNode() {
			m_nID = a_nID,
			m_nCost = a_nCost
		};
	}

	/** 노드 객체를 생성한다 */
	private STNodeObjInfo CreateNodeObj(STNode a_stNode) {
		var oTarget = CFactory.CreateCloneGameObj<CE23Target>("NodeObj",
			m_oQueueOriginTarget, m_oQueueTargetRoot, Vector3.zero, Vector3.one, Vector3.zero);

		oTarget.SetVal(a_stNode.m_nID);
		oTarget.transform.position = a_stNode.m_stPos;

		return new STNodeObjInfo() {
			m_stNode = a_stNode,
			m_oTarget = oTarget
		};
	}
	#endregion // 팩토리 함수
}
#endif // #if E23_QUEUE
