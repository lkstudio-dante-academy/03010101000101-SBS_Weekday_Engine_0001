using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 타겟 */
public class CE08Target : CComponent {
	#region 변수
	private Animator m_oAnimator = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		m_oAnimator = this.GetComponent<Animator>();
	}

	/** 초기화 */
	public override void Start() {
		base.Start();
		StartCoroutine(this.TryOpen());
	}

	/** 오픈 상태로 전환한다 */
	private IEnumerator TryOpen() {
		yield return new WaitForSeconds(Random.Range(0.15f, 6.0f));
		m_oAnimator.SetTrigger("Open");
	}
	#endregion // 함수
}
