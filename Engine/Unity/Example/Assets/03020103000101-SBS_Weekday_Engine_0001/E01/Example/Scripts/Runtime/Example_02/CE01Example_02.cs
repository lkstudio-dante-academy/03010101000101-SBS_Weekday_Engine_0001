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
 * - Unity 는 new 키워드를 활용해서 게임 객체를 생성하는 것이 가능하지만 일반적으로 해당
 * 방법은 잘 활용되지 않으며 Instantiate 메서드를 통한 객체 생성 방법을 주로 활용한다.
 * 
 * Unity 에서 게임 객체는 컴포넌트가 추가 되기 전까지는 아무런 역할도 수행하지 않기 때문에
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
public class CE01Example_02 : CSceneManager
{
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
	public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_02;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
	}

	/** 상태를 갱신한다 */
	public override void Update()
	{
		base.Update();

#if E02_PREFAB
		/** 스페이스 키를 눌렀을 경우 */
		if(Input.GetKeyDown(KeyCode.Space)) {
			var oTarget = Factory.CreateCloneGameObj("Target",
				m_oOriginTarget, m_oTargetRoot, new Vector3(0.0f, 360.0f, 0.0f),
				new Vector3(100.0f, 100.0f, 100.0f), Vector3.zero);
		}
#elif E02_PHYSICS
		var stPlayerPos = m_oPlayer.transform.position;
		var stDirection = m_oPlayer.transform.forward.ExToWorld(m_oPlayer.transform.parent.gameObject);

		/*
		 * Physics.Raycast 계열 메서드를 활용하면 간단하게 광선 추적 연산을 사용하는 것이
		 * 가능합니다. (즉, 해당 메서드의 결과를 통해서 특정 사물 전방에 물체가 존재하는지
		 * 여부를 판단하는 구문을 손쉽게 작성하는 것이 가능하다.)
		 * 
		 * 또한, Unity 는 Boxcast 와 같은 특정 모양으로 광선 추적 연산을 수행 할 수 있기
		 * 때문에 해당 메서드를 활용해서 다양한 충돌 예측 시뮬레이션을 작성하는 것이 가능하다.
		 * 
		 * RaycastHit 는 광선 추적을 통해서 충돌 된 사물의 정보를 보관하고 있는 구조체를 의미한다.
		 * (즉, 해당 정보를 활용하면 광선을 통해 충돌이 발생한 사물을 구별해서 그에 맞는 처리 구문을
		 * 작성하는 것이 가능하다.)
		 * 
		 * 만약, 특정 대상을 Raycast 계열 메서드의 후보에서 제외 시키고 싶다면 해당 대상의 레이어를
		 * IgnoreRaycast 를 설정하면 된다.
		 */
		// 플레이어 전방에 사물이 존재 할 경우
		if(Physics.Raycast(stPlayerPos, stDirection, out RaycastHit stRaycastHit))
		{
			Debug.LogFormat("{0} 가 전방에 존재합니다.", stRaycastHit.collider.name);
		}

		/** 이동 키를 눌렀을 경우 */
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
		{
			float fSpeed = Input.GetKey(KeyCode.UpArrow) ? 1000.0f : -1000.0f;
			m_oPlayer.transform.Translate(new Vector3(0.0f, 0.0f, fSpeed * Time.deltaTime), Space.Self);
		}

		/** 회전 키를 눌렀을 경우 */
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
		{
			float fAngle = Input.GetKey(KeyCode.LeftArrow) ? -180.0f : 180.0f;
			m_oPlayer.transform.Rotate(new Vector3(0.0f, fAngle * Time.deltaTime, 0.0f), Space.World);
		}

		/** 발사 키를 눌렀을 경우 */
		if(Input.GetKeyDown(KeyCode.Space))
		{
			var stPos = m_oPlayer.transform.position;
			var stRotate = m_oPlayer.transform.eulerAngles;

			var oRigidbody = Factory.CreateCloneGameObj<Rigidbody>("Bullet",
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
	private void HandleOnCollisionEnter(CCollisionDispatcher a_oSender, Collision a_oCollision)
	{
		Debug.LogFormat("HandleOnCollisionEnter : {0}", a_oCollision.gameObject.name);

		/*
		 * gameObject.CompareTag 메서드를 활용하면 게임 객체에 설정 된 태그를 비교하는
		 * 것이 가능하다. 또한, 태그 자체는 문자열이기 때문에 해당 메서드를 사용하지않고
		 * 직접 tag 프로퍼티를 사용해서 태그를 가져온 후 Equals 메서드로 직접 비교하는
		 * 것도 가능하다.
		 * 
		 * 단, tag 를 직접 가져 올 경우 내부적으로 불필요한 메모리 할당이 발생하기 때문에
		 * 최적화 관점에서는 CompareTag 메서드를 사용하는 것을 추천한다. (즉, 불필요한
		 * 메모리 할당은 가비지 컬렉션의 동작을 유발 시키기 때문에 이는 곧 프로그램의 성능을
		 * 저하시키는 원인이 될 수 있다는 것을 의미한다.)
		 */
		// 타겟 일 경우
		if(a_oCollision.gameObject.CompareTag("E02Target"))
		{
			/*
			 * Unity 에서 특정 객체를 제거하고 싶을 경우에는 Destroy 또는 DestroyImmediate
			 * 메서드를 사용하면 된다.
			 * 
			 * Destroy 메서드는 호출 되는 즉시 해당 대상을 제거하는 것이 아니라 일단 내부적으로
			 * 제거 해야 될 대상을 캐시 한 후 나중에 해당 대상을 안전하게 제거 할 수 있는 시점이
			 * 되었을 때 비로소 대상을 제거하는 특징이 존재한다.
			 * 
			 * 반면, DestroyImmediate 메서드는 호출 되는 즉시 해당 대상을 제거하기 때문에 일반적으로
			 * 런타임 환경에서 잘 사용되지 않는다. (즉, Unity 는 내부적으로 렌더링을 비롯한 여러
			 * 연산을 처리하기 때문에 특정 객체를 즉시 제거하는 것을 추천하지 않는다.)
			 * 
			 * 따라서, 일반적으로 런타임 환경에서는 DestroyImmediate 메서드 보다 Destroy 메서드를
			 * 사용하는 것을 추천하며 게임 객체가 아닌 텍스처와 같은 리소스를 제거 할 경우에만
			 * DestroyImmediate 메서드를 사용해야한다.
			 * 
			 * 또한, 런타임 환경이 아니라 에디터 환경에서 동작하는 스크립트에서는 특정 객체를 제거하기
			 * 위해서는 반드시 DestroyImmediate 메서드를 사용해야한다.
			 */
			GameObject.Destroy(a_oSender.gameObject);
			GameObject.Destroy(a_oCollision.gameObject);
		}
	}

	/** 충돌 진행을 처리한다 */
	private void HandleOnCollisionStay(CCollisionDispatcher a_oSender, Collision a_oCollision)
	{
		Debug.LogFormat("HandleOnCollisionStay : {0}", a_oCollision.gameObject.name);
	}

	/** 충돌 종료를 처리한다 */
	private void HandleOnCollisionExit(CCollisionDispatcher a_oSender, Collision a_oCollision)
	{
		Debug.LogFormat("HandleOnCollisionExit : {0}", a_oCollision.gameObject.name);
	}

#if UNITY_EDITOR
	/*
	 * OnDrawGizmos 메서드를 활용하면 Unity 에디터의 씬 뷰에 이미지 등을 출력하는 것이
	 * 가능하다. (즉, 프로젝트를 제작하는데 도움이 되는 여러 필요한 정보를 씬 뷰에 그리는
	 * 것이 가능하다는 것을 알 수 있다.)
	 * 
	 * 단, 해당 메서드 내부에서 사용되는 Gizmos 클래스는 Unity 의 모든 스크립트에서
	 * 공통적으로 사용하는 클래스이기 때문에 특정 연산을 수행하기 위해서 해당 클래스의 속성을
	 * 변경했다면 반드시 작업 완료 된 후에는 원래 속성 정보로 되돌려 줄 필요가 있다. (즉,
	 * 공통 자원이라는 것을 알 수 있다.)
	 * 
	 * 따라서, try ~ finally 구문을 활용하면 해당 클래스를 좀 더 안전하게 활용하는 것이
	 * 가능하다.
	 */
	/** 기즈모를 그린다 */
	public void OnDrawGizmos()
	{
		/** 플레이어가 존재 할 경우 */
		if(m_oPlayer != null)
		{
			var stPrevColor = Gizmos.color;

			try
			{
				var oParent = m_oPlayer.transform.parent.gameObject;

				var stPos = m_oPlayer.transform.position;
				var stDirection = m_oPlayer.transform.forward.ExToWorld(oParent, false);

				Gizmos.color = Color.red;
				Gizmos.DrawLine(stPos, stPos + (stDirection * 500.0f));
			}
			finally
			{
				Gizmos.color = stPrevColor;
			}
		}
	}
#endif // #if UNITY_EDITOR
	#endregion // 함수
}
