using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 플레이어 */
public class CE01Player_15 : CComponent
{
	#region 변수
	private bool m_bIsDirtyUpdateUIsState = true;

	private Animation m_oAnimation = null;
	private CE01StatHandler_15 m_oStatHandler = null;

	[Header("=====> UIs <=====")]
	[SerializeField] private Image m_oGaugeImg = null;

	[Header("=====> Game Objects <=====")]
	[SerializeField] private GameObject m_oMuzzleFlash = null;
	[SerializeField] private GameObject m_oBulletSpawnPos = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		m_oAnimation = this.GetComponent<Animation>();
		m_oMuzzleFlash.SetActive(false);

		m_oStatHandler = this.gameObject.AddComponent<CE01StatHandler_15>();
		m_oStatHandler.SetStat(EE01StatKinds_15.HP, 10);
		m_oStatHandler.SetStat(EE01StatKinds_15.ATK, 1);
		m_oStatHandler.SetStat(EE01StatKinds_15.MAX_HP, 10);
	}

	/** 상태를 갱신한다 */
	public override void Update()
	{
		base.Update();
		var oSceneManager = CSceneManager.GetSceneManager<CE01Example_15>(KDefine.G_N_SCENE_EXAMPLE_15);

		// 게임 종료 상태 일 경우
		if(oSceneManager.IsGameOver())
		{
			return;
		}

		// UI 상태 갱신이 필요 할 경우
		if(m_bIsDirtyUpdateUIsState)
		{
			this.UpdateUIsState();
			m_bIsDirtyUpdateUIsState = false;
		}

		/*
        Axis 계열 메서드는 입력 장치의 입력 여부에 따라 -1 ~ 1 범위 사이 값을
        반환한다. (즉, 아무런 입력도 없다면 0 을 반환하며 입력이 존재 할 경우
        입력 방향에 따라 음수 또는 양수를 반환한다는 것을 알 수 있다.)

        단, Axis 메서드는 입력 방향에 따라 값이 서서히 변경되기 때문에 해당 값을
        바로 이동 처리에 활용하면 입력이 종료 되어도 제어하는 물체가 일정 시간 동안
        여전히 움직이는 단점이 존재한다.

        따라서, 입력에 따라 바로 반응하는 물체를 제작하기 위해서는 AxisRaw 메서드를
        활용해야한다. (즉, AxisRaw 메서드는 입력 여부에 따라 -1, 0, 1 값 중 하나를
        반환한다는 것을 알 수 있다.)
        */
		float fVertical = Input.GetAxisRaw("Vertical");
		float fHorizontal = Input.GetAxisRaw("Horizontal");

		//var stDirection = (this.transform.forward * fVertical) +
		//	(this.transform.right * fHorizontal);

		var stDirection = (Vector3.forward * fVertical) +
			(Vector3.right * fHorizontal);

		/*
		 * Translate 메서드를 활용하면 물체를 특정 위치로 이동 시키는 것이 가능하다.
		 * 
		 * 단, 해당 메서드는 내부적으로 공간 변환에 대한 기능을 지원하기 때문에 해당
		 * 설정에 따라 올바른 위치 정보를 명시 할 필요가 있다. (즉, Space.Self 는
		 * 내부적으로 로컬 공간으로 변환하기 때문에 명시되는 위치 정보는 월드 공간의
		 * 위치 정보를 명시해줘야한다는 것을 알 수 있다.)
		 */
		//this.transform.localPosition += stDirection.normalized * 350.0f * Time.deltaTime;
		this.transform.Translate(stDirection.normalized * 350.0f * Time.deltaTime, Space.Self);

		// 마우스 버튼을 눌렀을 경우
		if(Input.GetMouseButton((int)EMouseBtn.RIGHT))
		{
			float fMouseX = Input.GetAxisRaw("Mouse X");
			this.transform.Rotate(Vector3.up * fMouseX * 270.0f * Time.deltaTime);
		}

		// 입력이 없을 경우
		if(fVertical.ExIsEquals(0.0f) && fHorizontal.ExIsEquals(0.0f))
		{
			m_oAnimation.CrossFade("Idle", 0.15f);
		}
		else
		{
			// 수직 입력이 존재 할 경우
			if(!fVertical.ExIsEquals(0.0f))
			{
				m_oAnimation.CrossFade(fVertical.ExIsGreat(0.0f) ? "RunF" : "RunB", 0.15f);
			}
			else
			{
				m_oAnimation.CrossFade(fHorizontal.ExIsGreat(0.0f) ? "RunR" : "RunL", 0.15f);
			}
		}
	}

	/** 충돌이 시작 되었을 경우 */
	public void OnTriggerEnter(Collider a_oCollider)
	{
		var oMonster = a_oCollider.GetComponentInParent<CE01Monster_15>();
		bool bIsHitPoint = a_oCollider.CompareTag("E15HitPoint");

		// 타격 되었을 경우
		if(bIsHitPoint && (oMonster != null && oMonster.IsAttack()))
		{
			this.OnHit();
		}
	}

	/** 타격 되었을 경우 */
	private void OnHit()
	{
		// 타격 가능 할 경우
		if(!m_oStatHandler.IsDie())
		{
			m_oStatHandler.IncrStatVal(EE01StatKinds_15.HP, -1);
			m_bIsDirtyUpdateUIsState = true;

			// 사망했을 경우
			if(m_oStatHandler.IsDie())
			{
				var oSceneManager = CSceneManager.GetSceneManager<CE01Example_15>(KDefine.G_N_SCENE_EXAMPLE_15);
				oSceneManager.HandleOnDeathPlayer();
			}
		}
	}

	/** 총알을 발사한다 */
	public void Fire(GameObject a_oOriginBullet, GameObject a_oBulletRoot)
	{
		var oBullet = this.CreateBullet(a_oOriginBullet, a_oBulletRoot);
		oBullet.transform.forward = this.transform.forward;
		oBullet.transform.position = m_oBulletSpawnPos.transform.position;

		oBullet.Init();

		StopCoroutine("StartMuzzleFlash");
		StartCoroutine(this.StartMuzzleFlash());

		var oRigidbody = oBullet.GetComponent<Rigidbody>();
		oRigidbody.AddForce(this.transform.forward * 2500.0f, ForceMode.VelocityChange);
	}

	/** UI 상태를 갱신한다 */
	private void UpdateUIsState()
	{
		// 이미지를 갱신한다
		m_oGaugeImg.fillAmount = m_oStatHandler.GetStatValPercent(EE01StatKinds_15.HP,
			EE01StatKinds_15.MAX_HP);
	}

	/** 발사 효과를 시작한다 */
	private IEnumerator StartMuzzleFlash()
	{
		float fOffsetU = Random.Range(0, 2) * 0.5f;
		float fOffsetV = Random.Range(0, 2) * 0.5f;

		var oMeshRenderer = m_oMuzzleFlash.GetComponent<MeshRenderer>();
		oMeshRenderer.material.mainTextureOffset = new Vector2(fOffsetU, fOffsetV);

		m_oMuzzleFlash.SetActive(true);
		yield return new WaitForSeconds(0.05f);

		m_oMuzzleFlash.SetActive(false);
	}

	/** 총알을 생성한다 */
	private CE01Bullet_15 CreateBullet(GameObject a_oOriginBullet, GameObject a_oBulletRoot)
	{
		var oSceneManager = CSceneManager.GetSceneManager<CE01Example_15>(KDefine.G_N_SCENE_EXAMPLE_15);

		var oBullet = oSceneManager.ObjsPoolManager.SpawnObj<CE01Bullet_15>(() =>
		{
			return Factory.CreateCloneGameObj("Bullet",
				a_oOriginBullet, a_oBulletRoot, Vector3.zero, Vector3.one, Vector3.zero);
		}) as GameObject;

		oBullet.SetActive(true);
		return oBullet.GetComponent<CE01Bullet_15>();
	}
	#endregion // 함수
}
