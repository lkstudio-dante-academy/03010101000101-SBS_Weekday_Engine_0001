using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Example 1 */
public class CExample_01 : CSceneManager
{
	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E01;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
	}
	#endregion // 함수
}
