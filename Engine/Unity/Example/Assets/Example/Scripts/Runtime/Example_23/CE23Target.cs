using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/** 타겟 */
public class CE23Target : CComponent {
	#region 변수
	[SerializeField] private TMP_Text m_oText = null;
	#endregion // 변수

	#region 접근 함수
	/** 값을 변경한다 */
	public void SetVal(int a_nVal) {
		m_oText.text = $"{a_nVal}";
	}
	#endregion // 접근 함수
}
