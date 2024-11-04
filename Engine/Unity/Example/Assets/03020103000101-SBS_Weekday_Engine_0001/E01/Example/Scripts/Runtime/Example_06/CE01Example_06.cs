//#define E06_SPRITE
#define E06_ANIMATION

#if E06_ANIMATION
//#define E06_ANIMATION_TWEEN
#define E06_ANIMATION_LEGACY
#define E06_ANIMATION_MECANIM
#endif // #if E06_ANIMATION

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
 * 스프라이트란?
 * - 3 차원 공간 상에 출력되는 2 차원 이미지를 의미한다. (즉, 스프라이트를 2 차원 게임에
 * 필요한 플레이어 등을 손쉽게 화면 상에 배치하는 것이 가능하다.)
 * 
 * 애니메이션이란?
 * - 특정 물체의 상태를 시간에 따라 변화시켜서 생동감을 주는 것을 의미한다. (즉, 시간의 흐름에
 * 따라 특정 물체의 이미지를 변경하면 이를 스프라이트 (시퀀스) 애니메이션이라고 한다.)
 * 
 * Unity 애니메이션 연출 방법
 * - 트윈 애니메이션
 * - 키 프레임 애니메이션
 * - 메카님 애니메이션
 * 
 * 트윈 애니메이션이란?
 * - 특정 물체의 상태 값을 시간이 흐름에 따라 보간함으로서 애니메이션을 연출하는 방법을 의미한다.
 * (즉, 트윈 애니메이션은 복잡한 애니메이션을 연출하는데는 적합하지 않지만 단순한 애니메이션은
 * 손쉽게 처리 할 수 있다는 장점이 존재한다.)
 * 
 * 키 프레임 (레거시) 애니메이션이란?
 * - 특정 물체의 상태 값을 키의 형태로 저장해서 키와 키 사이의 값을 보간함으로서 애니메이션을
 * 연출하는 방법을 의미한다. (단, 트윈 애니메이션과 달리 동적으로 변하는 애니메이션을 연출하기에는
 * 적합하지 않다.)
 * 
 * 키 프레임 애니메이션은 특정 물체의 값을 미리 키의 형태로 저장 할 필요가 있기 때문에 실행 중에
 * 물체의 상태가 변경되는 상황에 적용하는 것은 불가능하지만 복잡한 형태의 애니메이션을 처리 할 수
 * 있다는 장점이 존재한다.
 * 
 * 메카님 애니메이션이란?
 * - 애니메이션의 상태를 관리하는 시스템을 메카님이라고 한다. (즉, 메카님 자체는 애니메이션이
 * 아니라 특정 대상의 상태를 관리하기 위한 FSM (Finite State Machine) 이라는 것을 알 수 있다.)
 * 
 * 따라서, 메카님 시스템을 이용하면 특정 대상의 상태에 따라 애니메이션을 자동으로 전환 시키는 것이
 * 가능하다. (즉, 기존 레거시 방식에서는 특정 대상의 애니메이션을 전환하기 하기 이를 직접 스크립트로
 * 작성 할 필요가 있었지만 메카님 시스템이 도입되고 나서 부터는 더이상 수동으로 애니메이션을 전환 할
 * 필요가 없다.)
 */
/** Example 6 */
public class CE01Example_06 : CSceneManager
{
	#region 변수
	[Header("=====> Tween Animation <=====")]
	[SerializeField] private GameObject m_oTweenAniTarget = null;
	private Sequence m_oSequence = null;

	[Header("=====> Legacy Animation <=====")]
	[SerializeField] private GameObject m_oLegacyAniTarget = null;

	[Header("=====> Mecanim Animation <=====")]
	[SerializeField] private GameObject m_oMecanimAniTarget = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_06;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

#if E06_ANIMATION_LEGACY
		var oDispatcher = m_oLegacyAniTarget.GetComponent<CEventDispatcher>();
		oDispatcher.EventCallback = this.HandleOnEvent;
#endif // #if E06_ANIMATION_LEGACY
	}

	/** 상태를 갱신한다 */
	public override void Update()
	{
		base.Update();

#if E06_ANIMATION_TWEEN
		// 스페이스 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Space)) {
			m_oTweenAniTarget.transform.localPosition = Vector3.zero;
			m_oTweenAniTarget.transform.localEulerAngles = Vector3.zero;

			var stRotate = new Vector3(0.0f, 0.0f, -360.0f);

			/*
			 * DOTween 은 Sequence 를 활용해서 여러 애니메이션을 차례대로 실행하거나 동시에 실행하는 것이
			 * 가능하다. (즉, Join 메서드를 사용하면 여러 애니메이션을 동시에 실행 할 수 있고 Append 메서드를
			 * 사용하면 애니메이션을 차례대로 실행하는 것이 가능하다.)
			 */
			var oAni = DOTween.Sequence().SetAutoKill();
			oAni.Join(m_oTweenAniTarget.transform.DOLocalMoveX(450.0f, 1.0f));
			oAni.Join(m_oTweenAniTarget.transform.DOLocalRotate(stRotate, 1.0f, RotateMode.LocalAxisAdd));

			m_oSequence?.Kill();
			m_oSequence = oAni;
		}
#elif E06_ANIMATION_MECANIM
		// 스페이스 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Space))
		{
			var oAnimator = m_oMecanimAniTarget.GetComponent<Animator>();
			oAnimator.SetTrigger("Run");
		}
#endif // #if E06_ANIMATION_TWEEN
	}

	/** 이벤트를 처리한다 */
	private void HandleOnEvent(CEventDispatcher a_oSender, string a_oEvent)
	{
		Debug.LogFormat("HandleOnEvent: {0}", a_oEvent);
	}
	#endregion // 함수
}
