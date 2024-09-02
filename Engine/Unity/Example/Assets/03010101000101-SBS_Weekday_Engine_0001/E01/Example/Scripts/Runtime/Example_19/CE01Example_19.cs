using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Example 19 */
public class CE01Example_19 : CSceneManager
{
	#region 프로퍼티
	public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_19;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
	}

	/** 시작하기 버튼을 눌렀을 경우 */
	public void OnTouchPlayBtn()
	{
		CSceneLoader.Inst.LoadScene(KDefine.G_N_SCENE_EXAMPLE_20);
	}
	#endregion // 함수
}
