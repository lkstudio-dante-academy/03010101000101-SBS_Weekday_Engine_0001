using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

/** 점수 텍스트 */
public class CE01ScoreText_08 : CComponent
{
	#region 변수
	private Tween m_oAni = null;
	private TMP_Text m_oText = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		m_oText = this.GetComponent<TMP_Text>();
		(m_oText as TextMeshPro).ExSetSortingOrder(KDefine.G_N_LAYER_DEF, 1);
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy()
	{
		base.OnDestroy();
		m_oAni?.Kill();
	}

	/** 텍스트 애니메이션을 시작한다 */
	public void StartTextAni(int a_nScore)
	{
		m_oText.text = string.Format("{0}{1}", (a_nScore >= 0) ? "+" : string.Empty, a_nScore);
		m_oText.color = (a_nScore >= 0) ? Color.white : Color.red;

		m_oAni = this.transform.DOLocalMoveY(this.transform.localPosition.y + 50.0f, 0.5f);
		m_oAni.SetAutoKill().OnComplete(this.OnCompleteTextAni);
	}

	/** 텍스트 애니메이션이 완료되었을 경우 */
	private void OnCompleteTextAni()
	{
		m_oAni?.Kill();
		Destroy(this.gameObject);
	}
	#endregion // 함수
}
