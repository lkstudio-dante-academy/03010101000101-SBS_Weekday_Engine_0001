using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** Example 21 */
public class CE01Example_21 : CSceneManager
{
	#region 변수
	[SerializeField] private Text m_oResultText = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_21;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		m_oResultText.text = string.Format("결과 : {0}", CE01ResultStorage_20.Inst.GetResultStr());
	}

	/** 다시하기 버튼을 눌렀을 경우 */
	public void OnTouchRetryBtn()
	{
		CSceneLoader.Inst.LoadScene(KDefine.G_N_SCENE_EXAMPLE_20);
	}

	/** 그만두기 버튼을 눌렀을 경우 */
	public void OnTouchLeaveBtn()
	{
		CSceneLoader.Inst.LoadScene(KDefine.G_N_SCENE_EXAMPLE_19);
	}
	#endregion // 함수
}
