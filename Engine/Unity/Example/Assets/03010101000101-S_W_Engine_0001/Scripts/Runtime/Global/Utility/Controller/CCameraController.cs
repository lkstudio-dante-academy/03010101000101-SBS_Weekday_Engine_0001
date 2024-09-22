using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 카메라 제어자 */
public class CCameraController : CComponent
{
	#region 변수
	private Camera m_oCamera = null;

	/*
	 * 애트리뷰트란?
	 * - C# 명령문 자체에 메타 정보를 설정 할 수 있는 기능을 의미한다. (즉, 애트리뷰트를
	 * 활용하면 프로그램이 구동 중에 특정 변수 또는 메서드 등의 정보를 가져와서 여러 연산을
	 * 수행하는 것이 가능하다.)
	 * 
	 * SerializeField 란?
	 * - 기본적으로 C# 스크립트에 작성 된 맴버 변수를 Unity 에디터 상에서 설정하기 위해서는
	 * 해당 변수가 public 보호 수준 일 필요가 있다.
	 * 
	 * 하지만 public 보호 수준은 안전성을 떨어뜨리는 문제가 있기 때문에 Unity 는 
	 * SerializeField 애트리뷰트를 제공하며 해당 속성을 활용하면 private 보호 수준이라
	 * 하더라도 Unity 에디터 상에서 해당 변수를 설정하는 것이 가능하다.
	 */
	[SerializeField] private bool m_bIsPerspective = true;
	[SerializeField] private GameObject m_oScalingTarget = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		/*
		 * GetComponent 메서드는 특정 게임 객체가 지니고 있는 컴포넌트를 가져오는 역할을
		 * 수행한다. (즉, Unity 는 여러 컴포넌트를 조합해서 특정 역할을 수행하기 때문에
		 * 주어진 명령문을 실행하는 과정에서 다른 컴포넌트를 참조 할 필요가 있을 경우 해당
		 * 메서드를 활용하면 된다.)
		 * 
		 * 단, GetComponent 메서드는 빈번하게 호출 될 경우 프로그램의 성능 저하 시킬 수 있기
		 * 때문에 가능하면 해당 메서드를 호출하는 횟수를 줄이는 것이 좋다. (즉, 변수 등을
		 * 활용해서 메서드를 호출 후 해당 메서드가 반환 시키는 컴포넌트를 캐시하는 방법이
		 * 일반적으로 많이 활용되는 방법이다.)
		 */
		m_oCamera = this.GetComponent<Camera>();
	}

	/** 초기화 */
	public override void Start()
	{
		base.Start();

		this.SetupCamera();
		this.SetupScalingTarget();
	}

	/** 카메라를 설정한다 */
	private void SetupCamera()
	{
		float fFOV = KDefine.G_CAMERA_FOV;
		float fHeight = KDefine.G_DESIGN_HEIGHT;

		float fDistance = (fHeight / 2.0f) / Mathf.Tan((fFOV / 2.0f) * Mathf.Deg2Rad);
		m_oCamera.transform.localPosition = new Vector3(0.0f, 0.0f, -fDistance);

		// 원근 투영 일 경우
		if(m_bIsPerspective)
		{
			this.Setup3DCamera();
		}
		else
		{
			this.Setup2DCamera();
		}
	}

	/** 2 차원 카메라를 설정한다 */
	private void Setup2DCamera()
	{
		m_oCamera.orthographic = true;
		m_oCamera.orthographicSize = KDefine.G_DESIGN_HEIGHT / 2.0f;
	}

	/** 3 차원 카메라를 설정한다 */
	private void Setup3DCamera()
	{
		m_oCamera.fieldOfView = KDefine.G_CAMERA_FOV;
		m_oCamera.orthographic = false;
	}

	/** 비율 대상을 설정한다 */
	private void SetupScalingTarget()
	{
		var stScale = Vector3.one * Access.GetResolutionScale(KDefine.G_DESIGN_SIZE);
		m_oScalingTarget.transform.localScale = stScale;
	}
	#endregion // 함수
}
