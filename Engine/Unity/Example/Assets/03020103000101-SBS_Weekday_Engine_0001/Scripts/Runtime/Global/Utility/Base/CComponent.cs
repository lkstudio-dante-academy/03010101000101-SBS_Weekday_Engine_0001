using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 게임 객체란?
 * - 컴포넌트를 담을 수 있는 그릇을 의미한다. (즉, 게임 객체 자체는 아무런 역할도 수행하지 않지만 해당 게임 객체에
 * 여러 컴포넌트를 추가함으로서 게임 객체에 역할을 부여하는 것이 가능하다.)
 * 
 * 컴포넌트란?
 * - 특정 기능을 수행하는 단위를 의미한다. (즉, 하나의 기능을 수행하는 클래스를 의미한다.)
 * 
 * 일반적으로 컴포넌트 자체는 아무런 동작을 수행하지 않지만 해당 컴포넌트를 게임 객체에 추가 시킴으로서 해당
 * 컴포넌트를 동작 시키는 것이 가능하다.
 * 
 * 따라서, Unity 여러가지 컴포넌트를 지원하며 스크립트 컴포넌트를 이용하면 특정 프로그램에 맞는 전용 컴포넌트를
 * 제작하는 것이 가능하다.
 * 
 * Unity 에서 스크립트 컴포넌트 제작 방법
 * - MonoBehaviour 클래스를 직/간접적으로 상속해야한다.
 * - 클래스 이름과 해당 클래스가 구현 되어있는 파일 이름이 동일해야한다.
 */
/** 최상단 컴포넌트 */
public class CComponent : MonoBehaviour, IUpdatable
{
	#region 변수
	private bool m_bIsDirtyUpdateState = true;
	#endregion // 변수

	#region 프로퍼티
	public bool IsDestroy { get; private set; } = false;
	public System.Action<CComponent> DestroyCallback { get; private set; } = null;
	#endregion // 프로퍼티

	#region IUpdatable
	/** 상태를 갱신한다 */
	public virtual void OnUpdate(float a_fDeltaTime)
	{
		// Do Something
	}

	/** 상태를 갱신한다 */
	public virtual void OnLateUpdate(float a_fDeltaTime)
	{
		// 상태 갱신이 필요 할 경우
		if(m_bIsDirtyUpdateState)
		{
			this.UpdateState();
			m_bIsDirtyUpdateState = false;
		}
	}

	/** 상태를 갱신한다 */
	public virtual void OnFixedUpdate(float a_fDeltaTime)
	{
		// Do Something
	}
	#endregion // IUpdatable

	#region 함수
	/*
	 * Awake vs Start 메서드
	 * - Awake 와 Start 메서드 모두 특정 객체를 초기화 시키는 용도로 사용된다.
	 * 
	 * Awake 는 해당 컴포넌트가 사용 완료되는 즉시 호출 되는 반면 Start 메서드는 사용 완료 된 
	 * 시점에 바로 호출 되는 것이 아니라 다음 프레임에 호출되는 차이점이 존재한다.
	 * 
	 * 따라서, 특정 객체를 생성과 동시에 초기화 시키는 작업을 수행하고 싶다면 Start 메서드 보다는 
	 * Awake 메서드를 사용하는 것을 추천한다.
	 * 
	 * Awake 및 Start 메서드가 호출되는 순간
	 * - 씬이 로드 되었을 경우
	 * - Game Object 에 컴포넌트를 추가 시켰을 경우
	 * 
	 * 만약, 처음부터 Game Object 가 비활성화 상태였다면 해당 Game Object 가 활성되는 순간
	 * 호출된다.
	 * 
	 * 주로 활용되는 이벤트 메서드 종류
	 * - Awake			<- 객체가 활성화 되었을 경우 (단, 활성 횟수에 상관없이 1 번만 호출)
	 * - Start			<- 객체가 활성화 되었을 경우 (단, 활성 횟수에 상관없이 1 번만 호출)
	 * - Reset			<- 에디터 상에서 컴포넌트를 추가하거나 Reset 메뉴를 선택했을 경우
	 * - Update			<- 매 프레임
	 * - LateUpdate		<- 매 프레임 (단, 모든 컴포넌트의 Update 가 호출 된 후 해당 메서드 호출)
	 * - OnEnable		<- 객체가 활성화 되었을 경우
	 * - OnDisable		<- 객체가 비활성화 되었을 경우
	 * - OnDestroy		<- 객체가 제거 되었을 경우
	 */
	/** 초기화 */
	public virtual void Awake()
	{
		// Do Something
	}

	/** 초기화 */
	public virtual void Start()
	{
		// Do Something
	}

	/*
	 * Reset 메서드는 Unity 에디터 상에서 특정 스크립트 컴포넌트를 초기화 시킬 때 호출 되는 메서드이다.
	 * 
	 * 따라서, 실행 중이 아니라 에디터 상에서 특정 컴포넌트 상태를 초기화 시키고 싶다면 해당 메서드를 활용하면
	 * 된다.
	 */
	/** 상태를 리셋한다 */
	public virtual void Reset()
	{
		// Do Something
	}

	/*
	 * OnDestroy 메서드는 해당 컴포넌트를 지니고 있는 게임 객체가 제거 되었을 때 호출 되는 메서드이다.
	 * 
	 * 따라서, 특정 객체가 제거 되었을 때 필요 한 연산이 있을 경우 해당 메서드를 활용하면 된다. 단, 해당 메서드에서
	 * 이미 제거가 완료 된 게임 객체에 접근 할 경우 내부적으로 에러 (예외) 가 발생하기 때문에 해당 메서드에서는
	 * 반드시 제어 할 객체의 존재 여부를 먼저 검사해 줄 필요가 있다.
	 */
	/** 제거 되었을 경우 */
	public virtual void OnDestroy()
	{
		this.IsDestroy = true;
		this.DestroyCallback?.Invoke(this);
	}

	/*
	 * Update 메서드는 매 프레임마다 호출 되는 메서드를 의미한다. 따라서, 해당 메서드를 활용하면 매 프레임마다
	 * 갱신 되어야하는 객체를 제어하는 것이 가능하다. (Ex. 이동, 애니메이션 처리 등등...)
	 * 
	 * 단, 해당 메서드는 매 프레임마다 호출되기 때문에 해당 몌서드 내부에서는 성능 저하를 일으키는 작업을 가능하면
	 * 지양해야한다. (Ex. GameObject.Find 메서드 호출 등등...)
	 */
	/** 상태를 갱신한다 */
	public virtual void Update()
	{
		// Do Something
	}

	/** 상태를 갱신한다 */
	public virtual void LateUpdate()
	{
		// Do Something
	}

	/** 상태를 갱신한다 */
	public virtual void FixedUpdate()
	{
		// Do Something
	}

	/** 상태를 갱신한다 */
	protected virtual void UpdateState()
	{
		// Do Something
	}

	/** 제거 수신자를 추가한다 */
	public void AddDestroyListener(System.Action<CComponent> a_oListener)
	{
		// 수신자가 없을 경우
		if(this.DestroyCallback == null)
		{
			this.DestroyCallback = a_oListener;
		}
		else
		{
			this.DestroyCallback += a_oListener;
		}
	}
	#endregion // 함수

	#region 접근 함수
	/** 상태 갱신 여부를 변경한다 */
	public void SetIsDirtyUpdateState(bool a_bIsDirty)
	{
		// 상태 갱신이 필요 할 경우
		if(!m_bIsDirtyUpdateState)
		{
			m_bIsDirtyUpdateState = a_bIsDirty;
		}
	}
	#endregion // 접근 함수
}
