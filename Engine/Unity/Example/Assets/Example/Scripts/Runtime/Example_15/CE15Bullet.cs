using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 총알 */
public class CE15Bullet : CComponent {
	#region 변수
	private Rigidbody m_oRigidbody = null;
	private TrailRenderer m_oTrail = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		m_oTrail = this.GetComponentInChildren<TrailRenderer>();
		m_oRigidbody = this.GetComponent<Rigidbody>();
	}

	/** 초기화 */
	public virtual void Init() {
		m_oTrail.Clear();
		m_oRigidbody.velocity = Vector3.zero;
	}

	/** 충돌이 시작 되었을 경우 */
	public void OnTriggerEnter(Collider a_oCollider) {
		/*
		 * LayerMask 클래스를 이용하면 특정 레이어 번호 및 비트 마스크를
		 * 가져오는 것이 가능하다. (즉, 레이어는 태그와 달리 32 비트 정수를
		 * 이용해서 물체를 식별하기 때문에 원하는 만큼 레이어를 추가하는
		 * 것이 불가능하다는 것을 알 수 있다.)
		 * 
		 * 유니티의 모든 게임 객체는 해당 객체가 속해 있는 레이어가 지정되어
		 * 있기 때문에 layer 프로퍼티를 사용하면 언제든지 게임 객체가 속한
		 * 레이어를 가져오는 것이 가능하다. 
		 * 
		 * 단, layer 프로퍼티를 통해서 가져오는 값은 비트 마스크 값이 아닌
		 * 레이어 번호이기 때문애 레이어 값을 통해 특정 물체를 식별하는데
		 * 주의가 필요하다. (즉, 비트 마스크가 아니기 때문에 비트 & 연산자를
		 * 통해 레이어를 식별하는 것이 아니라 == (동등) 연산자를 통해서 레이어를
		 * 식별해야한다는 것을 알 수 있다.)
		 */
		int nFXLayer = LayerMask.NameToLayer("TransparentFX");

		// 효과 레이어가 아닐 경우
		if(a_oCollider.gameObject.layer != nFXLayer) {
			var oInteractable = a_oCollider.GetComponentInParent<CE15Interactable>();
			oInteractable?.OnHit();

			var oSceneManager = CSceneManager.GetSceneManager<CE15SceneManager>(KDefine.G_SCENE_N_E15);
			oSceneManager.ObjsPoolManager.DespawnObj<CE15Bullet>(this.gameObject, this.OnCompleteDespawnObj);
		}
	}

	/** 비활성화가 완료 되었을 경우 */
	private void OnCompleteDespawnObj(object a_oObj) {
		(a_oObj as GameObject).SetActive(false);
	}
	#endregion // 함수
}
