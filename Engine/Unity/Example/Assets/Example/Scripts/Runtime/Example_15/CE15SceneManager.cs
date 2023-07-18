using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Example 15 */
public class CE15SceneManager : CSceneManager {
	#region 변수
	[SerializeField] private CE15Player m_oPlayer = null;
	[SerializeField] private GameObject m_oBulletRoot = null;
	[SerializeField] private GameObject m_oOriginBullet = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E15;
	public CE15ObjsPoolManager ObjsPoolManager { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		this.ObjsPoolManager = CFactory.CreateGameObj<CE15ObjsPoolManager>("ObjsPoolManager",
			this.gameObject, Vector3.zero, Vector3.one, Vector3.zero);
	}

	/** 상태를 갱신한다 */
	public override void Update() {
		base.Update();

		// 스페이스 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Space)) {
			m_oPlayer.Fire(m_oOriginBullet, m_oBulletRoot);
		}
	}
	#endregion // 함수
}
