using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

/** 타겟 */
public class CE01Target_23 : CComponent
{
	#region 변수
	[SerializeField] private TMP_Text m_oText = null;
	[SerializeField] private MeshRenderer m_oMeshRenderer = null;
	[SerializeField] private SpriteRenderer m_oSpriteRenderer = null;

	private Tween m_oFadeAni = null;
	#endregion // 변수

	#region 프로퍼티
	public TMP_Text Text => m_oText;
	public MeshRenderer MeshRenderer => m_oMeshRenderer;
	public SpriteRenderer SpriteRenderer => m_oSpriteRenderer;
	#endregion // 프로퍼티

	#region 함수
	/** 제거 되었을 경우 */
	public override void OnDestroy()
	{
		base.OnDestroy();
		m_oFadeAni?.Kill();
	}

	/** 페이드 애니메이션을 시작한다 */
	public void StartFadeAni(float a_fStartAlpha,
		float a_fEndAlpha, System.Action<CE01Target_23> a_oCallback = null)
	{
		var stColor = m_oMeshRenderer.material.color;
		stColor.a = a_fStartAlpha;

		m_oFadeAni?.Kill();
		m_oMeshRenderer.material.color = stColor;

		m_oFadeAni = m_oMeshRenderer.material.DOFade(a_fEndAlpha, 0.25f).SetAutoKill();
		m_oFadeAni.OnComplete(() => a_oCallback?.Invoke(this));
	}
	#endregion // 함수

	#region 접근 함수
	/** 값을 변경한다 */
	public void SetVal(int a_nVal)
	{
		m_oText.text = $"{a_nVal}";
	}
	#endregion // 접근 함수
}
