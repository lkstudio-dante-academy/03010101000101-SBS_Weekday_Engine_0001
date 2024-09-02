using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 타겟 */
public class CE01Target_08 : CComponent
{
	#region 변수
	[Header("=====> Controller <=====")]
	[SerializeField] private RuntimeAnimatorController m_oController01 = null;
	[SerializeField] private RuntimeAnimatorController m_oController02 = null;

	private bool m_bIsEnableCatch = false;
	private Animator m_oAnimator = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		m_oAnimator = this.GetComponent<Animator>();

		// 이벤트 전파자를 설정한다
		var oDispatcher = this.GetComponent<CEventDispatcher>();
		oDispatcher.EventCallback = this.HandleOnEvent;
	}

	/** 초기화 */
	public override void Start()
	{
		base.Start();
		this.HandleOnEvent(null, string.Empty);
	}

	/** 타겟을 캐치한다 */
	public void Catch(System.Action<CE01Target_08, int> a_oCallback)
	{
		// 캐치 가능 할 경우
		if(m_bIsEnableCatch)
		{
			m_oAnimator.SetTrigger("Catch");
			m_oAnimator.ResetTrigger("Open");

			m_bIsEnableCatch = false;
			a_oCallback(this, (m_oAnimator.runtimeAnimatorController == m_oController01) ? 10 : -20);
		}
	}

	/** 이벤트를 처리한다 */
	private void HandleOnEvent(CEventDispatcher a_oSender, string a_oEvent)
	{
		StartCoroutine(this.TryOpen());
	}

	/** 오픈 상태로 전환한다 */
	private IEnumerator TryOpen()
	{
		/*
		 * WaitForSeconds vs WaitForSecondsRealtime
		 * - WaitForSeconds 내부적으로 흘러간 시간을 계산하기 위해서 Time.deltaTime 을 사용하는
		 * 반면, WaitForSecondsRealtime 내부적으로 Time.unscaledDeltaTime 을 사용한다는 차이점
		 * 이 존재한다. (즉, WaitForSeconds 는 Time.timeScale 에 영향을 받는다는 것을 알 수 있다.)
		 */
		yield return new WaitForSeconds(Random.Range(0.15f, 6.0f));
		int nSelTarget = Random.Range(0, 2);

		// A 두더지 일 경우
		if(nSelTarget <= 0)
		{
			m_oAnimator.runtimeAnimatorController = m_oController01;
		}
		else
		{
			m_oAnimator.runtimeAnimatorController = m_oController02;
		}

		m_oAnimator.SetTrigger("Open");
		m_oAnimator.ResetTrigger("Catch");

		m_bIsEnableCatch = true;
	}
	#endregion // 함수
}
