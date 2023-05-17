#define E06_SPRITE
#define E06_ANIMATION

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 스프라이트란?
 * - 3 차원 공간 상에 출력되는 2 차원 이미지를 의미한다. (즉, 스프라이트를 2 차원 게임에
 * 필요한 플레이어 등을 손쉽게 화면 상에 배치하는 것이 가능하다.)
 */
/** Example 6 */
public class CE06SceneManager : CSceneManager {
	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E06;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
	}
	#endregion // 함수
}
