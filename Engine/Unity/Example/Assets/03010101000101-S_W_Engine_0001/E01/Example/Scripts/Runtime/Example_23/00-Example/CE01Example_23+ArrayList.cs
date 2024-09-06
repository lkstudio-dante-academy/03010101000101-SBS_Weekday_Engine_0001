using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if E23_ARRAY_LIST
/** Example 23 - 배열 리스트 */
public partial class CE01Example_23 : CSceneManager {
	#region 변수
	private int m_nChangedIdx = -1;
	private CArrayList<int> m_oValList = new CArrayList<int>();
	#endregion // 변수

	#region 함수
	/** 인덱스를 리셋한다 */
	public void ResetIndices() {
		m_nChangedIdx = -1;
	}

	/** 추가 버튼을 눌렀을 경우 */
	public void OnTouchAddBtn() {
		m_nChangedIdx = m_oValList.Count;
		m_oValList.Add(this.GetVal());

		this.SetIsDirtyUpdateState(true);
	}

	/** 추가 버튼을 눌렀을 경우 */
	public void OnTouchInsertBtn() {
		m_nChangedIdx = this.GetIdx();
		m_oValList.Insert(this.GetIdx(), this.GetVal());

		this.SetIsDirtyUpdateState(true);
	}

	/** 제거 버튼을 눌렀을 경우 */
	public void OnTouchRemoveBtn() {
		int nResult = m_oValList.Find(this.GetVal());
		this.TryRemoveVal(nResult);
	}

	/** 제거 버튼을 눌렀을 경우 */
	public void OnTouchRemoveAtBtn() {
		this.TryRemoveVal(this.GetIdx());
	}

	/** 상태를 갱신한다 */
	protected override void UpdateState() {
		base.UpdateState();

		int nNumVals = Mathf.Max(m_oValList.Count, 
			m_oArrayListTargetRoot.transform.childCount);

		for(int i = 0; i < nNumVals; ++i) {
			CE01Target_23 oTarget = null;

			// 타겟이 존재 할 경우
			if(i < m_oArrayListTargetRoot.transform.childCount) {
				oTarget = m_oArrayListTargetRoot.transform.GetChild(i).GetComponent<CE01Target_23>();
			} else {
				oTarget = Factory.CreateCloneGameObj<CE01Target_23>("Target",
					m_oArrayListOriginTarget, m_oArrayListTargetRoot, Vector3.zero, Vector3.one, Vector3.zero);
			}

			oTarget?.gameObject.SetActive(i < m_oValList.Count);

			// 값이 존재 할 경우
			if(i < m_oValList.Count) {
				int nVal = m_oValList[i];
				oTarget.GetComponent<CE01Target_23>().SetVal(nVal);

				// 변화가 발생한 값 일 경우
				if(i == m_nChangedIdx) {
					oTarget.GetComponent<CE01Target_23>().StartFadeAni(0.0f, 1.0f);
				}
			}
		}

		this.ResetIndices();
	}

	/** 값을 제거한다 */
	private void TryRemoveVal(int a_nIdx) {
		// 인덱스가 유효하지 않을 경우
		if(!m_oValList.IsValidIdx(a_nIdx)) {
			return;
		}

		var oTarget = m_oArrayListTargetRoot.transform.GetChild(a_nIdx);

		oTarget.GetComponent<CE01Target_23>().StartFadeAni(1.0f, 0.0f, (a_oSender) => {
			oTarget.SetAsLastSibling();
			m_oValList.RemoveAt(a_nIdx);
			
			this.SetIsDirtyUpdateState(true);
		});
	}
	#endregion // 함수
}
#endif // #if E23_ARRAY_LIST
