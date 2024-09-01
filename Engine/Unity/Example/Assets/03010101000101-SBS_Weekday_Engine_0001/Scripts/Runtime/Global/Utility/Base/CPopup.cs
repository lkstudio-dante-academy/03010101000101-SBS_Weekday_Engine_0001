using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/** 팝업 */
public abstract class CPopup : CComponent
{
	#region 변수
	private Tween m_oShowAni = null;
	private Tween m_oCloseAni = null;
	#endregion // 변수

	#region 프로퍼티
	protected GameObject Contents { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		this.Contents = this.transform.Find("Contents").gameObject;
		this.Contents.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
	}

	/** 애니메이션을 리셋한다 */
	public void ResetAnimations()
	{
		m_oShowAni?.Kill();
		m_oCloseAni?.Kill();
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy()
	{
		base.OnDestroy();
		this.ResetAnimations();
	}

	/** 팝업을 출력한다 */
	public void Show()
	{
		this.ResetAnimations();
		m_oShowAni = this.Contents.transform.DOScale(Vector3.one, 0.15f).SetAutoKill();
	}

	/** 팝업을 닫는다 */
	public void Close()
	{
		this.ResetAnimations();

		m_oCloseAni = this.Contents.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 0.15f).SetAutoKill();
		m_oCloseAni.onComplete = this.OnCompleteCloseAni;
	}

	/** 닫기 애니메이션이 완료 되었을 경우 */
	private void OnCompleteCloseAni()
	{
		Destroy(this.gameObject);
	}
	#endregion // 함수
}
