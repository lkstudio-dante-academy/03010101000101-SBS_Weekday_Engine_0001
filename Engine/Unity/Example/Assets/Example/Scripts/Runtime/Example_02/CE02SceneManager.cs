//#define E02_PREFAB
#define E02_PHYSICS

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 프리팹이란?
 * - 컴포넌트를 포함하고 있는 게임 객체를 필요에 따라 재사용 할 수 있게 에셋 형태로 저장 시킬
 * 수 있는 기능을 의미한다. (즉, 프리팹을 활용하면 사본 객체를 좀 더 수월하게 생성 및 관리
 * 하는 것이 가능하다.)
 * 
 * 프리팹과 프리팹을 통해 생성 된 사본 객체들은 원본 프리팹과 동기화를 시키는 것이 가능하기
 * 때문에 만약 프리팹 또는 사본 객체에 변화가 발생 할 경우 해당 사항을 원본 프리팹과 동기화
 * 시킴으로서 다른 사본 객체도 해당 변화를 손쉽게 적용하는 것이 가능하다.
 * 
 * 구동 중에 게임 객체 생성 방법
 * - new 키워드 사용
 * - Instantiate 메서드 사용
 * 
 * new 키워드 vs Instantiate 메서드
 * - 유니티는 new 키워드를 활용해서 게임 객체를 생성하는 것이 가능하지만 일반적으로 해당
 * 방법은 잘 활용되지 않으며 Instantiate 메서드를 통한 객체 생성 방법을 주로 활용한다.
 * 
 * 유니티에서 게임 객체는 컴포넌트가 추가 되기 전까지는 아무런 역할도 수행하지 않기 때문에
 * new 키워드를 통한 게임 객체 생성은 반드시 컴포넌트를 추가하는 구문을 같이 작성 해줄 필요가
 * 있다. 
 * 
 * 반면, Instantiate 메서드는 원본 게임 객체를 기반으로 사본 게임 객체를 생성하는 메서드이기
 * 때문에 해당 메서드를 통해 게임 객체를 생성하면 원본과 동일한 컴포넌트를 지닌 사본 게임 객체를
 * 생성하는 것이 가능하다. (즉, 일반적으로 사본 게임 객체를 생성 후 별도의 컴포넌트 추가 작업이
 * 불필요하다는 것을 알 수 있다.)
 * 
 * 따라서, Instantiate 메서드는 활용하기 위해서는 원본 게임 객체가 필요하며 해당 원본 게임 
 * 객체는 프리팹을 전달하는 것이 가능하다.
 */
/** Example 2 */
public class CE02SceneManager : CSceneManager {
	#region 변수
	[Header("=====> 프리팹 <=====")]
	[SerializeField] private GameObject m_oTargetRoot = null;
	[SerializeField] private GameObject m_oOriginTarget = null;

	[Header("=====> 물리 엔진 <=====")]
	[SerializeField] private GameObject m_oPlayer = null;
	[SerializeField] private GameObject m_oBulletRoot = null;
	[SerializeField] private GameObject m_oOriginBullet = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E02;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
	}

	/** 상태를 갱신한다 */
	public override void Update() {
		base.Update();

#if E02_PREFAB
		/** 스페이스 키를 눌렀을 경우 */
		if(Input.GetKeyDown(KeyCode.Space)) {
			var oTarget = CFactory.CreateCloneGameObj("Target",
				m_oOriginTarget, m_oTargetRoot, new Vector3(0.0f, 360.0f, 0.0f),
				new Vector3(100.0f, 100.0f, 100.0f), Vector3.zero);
		}
#elif E02_PHYSICS
		/** 이동 키를 눌렀을 경우 */
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)) {
			float fSpeed = Input.GetKey(KeyCode.UpArrow) ? 1000.0f : -1000.0f;
			m_oPlayer.transform.Translate(new Vector3(0.0f, 0.0f, fSpeed * Time.deltaTime), Space.Self);
		}

		/** 회전 키를 눌렀을 경우 */
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) {
			float fAngle = Input.GetKey(KeyCode.LeftArrow) ? -180.0f : 180.0f;
			m_oPlayer.transform.Rotate(new Vector3(0.0f, fAngle * Time.deltaTime, 0.0f), Space.World);
		}

		/** 발사 키를 눌렀을 경우 */
		if(Input.GetKeyDown(KeyCode.Space)) {
			var stPos = m_oPlayer.transform.position;
			var stRotate = m_oPlayer.transform.eulerAngles;

			var oRigidbody = CFactory.CreateCloneGameObj<Rigidbody>("Bullet",
				m_oOriginBullet, m_oBulletRoot, stPos.ExToLocal(m_oBulletRoot), Vector3.one,
				stRotate.ExToLocal(m_oBulletRoot, false));

			var oParent = m_oPlayer.transform.parent.gameObject;

			oRigidbody.AddForce(m_oPlayer.transform.forward.ExToWorld(oParent, false) * 1500.0f, 
				ForceMode.VelocityChange);

			var oDispatcher = oRigidbody.GetComponent<CCollisionDispatcher>();
			oDispatcher.EnterCallback = this.HandleOnCollisionEnter;
			oDispatcher.StayCallback = this.HandleOnCollisionStay;
			oDispatcher.ExitCallback = this.HandleOnCollisionExit;
		}
#endif // #if E02_PREFAB
	}

	/** 충돌 시작을 처리한다 */
	private void HandleOnCollisionEnter(CCollisionDispatcher a_oSender, Collision a_oCollision) {
		// Do Something
	}

	/** 충돌 진행을 처리한다 */
	private void HandleOnCollisionStay(CCollisionDispatcher a_oSender, Collision a_oCollision) {
		// Do Something
	}

	/** 충돌 종료를 처리한다 */
	private void HandleOnCollisionExit(CCollisionDispatcher a_oSender, Collision a_oCollision) {
		// Do Something
	}
	#endregion // 함수
}
