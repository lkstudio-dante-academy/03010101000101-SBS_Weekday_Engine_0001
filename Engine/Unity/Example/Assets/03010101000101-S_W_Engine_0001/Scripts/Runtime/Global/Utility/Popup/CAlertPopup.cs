using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 알림 팝업 */
public class CAlertPopup : CPopup
{
	/** 매개 변수 */
	public struct STParams
	{
		public string m_oTitle;
		public string m_oMsg;
		public string m_oOKBtnText;
		public string m_oCancelBtnText;
	}

	#region 변수
	private System.Action<CAlertPopup, bool> m_oCallback = null;

	[Header("=====> UIs <=====")]
	[SerializeField] private Text m_oTitleText = null;
	[SerializeField] private Text m_oMsgText = null;

	[SerializeField] private Button m_oOKBtn = null;
	[SerializeField] private Button m_oCancelBtn = null;
	#endregion // 변수

	#region 프로퍼티
	public STParams Params { get; private set; }
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		// 버튼을 설정한다
		m_oOKBtn.onClick.AddListener(this.OnTouchOKBtn);
		m_oCancelBtn.onClick.AddListener(this.OnTouchCancelBtn);
	}

	/** 초기화 */
	public virtual void Init(STParams a_stParams)
	{
		this.Params = a_stParams;
		this.UpdateUIsState();
	}

	/** 알림 팝업을 출력한다 */
	public void Show(System.Action<CAlertPopup, bool> a_oCallback)
	{
		base.Show();
		m_oCallback = a_oCallback;
	}

	/** UI 상태를 갱신한다 */
	private void UpdateUIsState()
	{
		m_oTitleText.text = this.Params.m_oTitle;
		m_oMsgText.text = this.Params.m_oMsg;

		var oOKBtnText = m_oOKBtn.GetComponentInChildren<Text>();
		oOKBtnText.text = this.Params.m_oOKBtnText;

		// 취소 버튼이 유효 할 경우
		if(this.Params.m_oCancelBtnText.Length > 0)
		{
			var oCancelBtnText = m_oCancelBtn.GetComponentInChildren<Text>();
			oCancelBtnText.text = this.Params.m_oCancelBtnText;
		}
		else
		{
			m_oCancelBtn.gameObject.SetActive(false);
		}
	}

	/** 확인 버튼을 눌렀을 경우 */
	private void OnTouchOKBtn()
	{
		m_oCallback?.Invoke(this, true);
		this.Close();
	}

	/** 취소 버튼을 눌렀을 경우 */
	private void OnTouchCancelBtn()
	{
		m_oCallback?.Invoke(this, false);
		this.Close();
	}
	#endregion // 함수

	#region 클래스 함수
	/** 매개 변수를 생성한다 */
	public static STParams MakeParams(string a_oTitle,
		string a_oMsg, string a_oOKBtnText, string a_oCancelBtnText = "")
	{
		return new STParams()
		{
			m_oTitle = a_oTitle,
			m_oMsg = a_oMsg,
			m_oOKBtnText = a_oOKBtnText,
			m_oCancelBtnText = a_oCancelBtnText
		};
	}

	/** 알림 팝업을 생성한다 */
	public static CAlertPopup CreateAlertPopup(string a_oMsg,
		GameObject a_oParent, bool a_bIsEnableCancelBtn = true)
	{
		var stParams = CAlertPopup.MakeParams("알림",
			a_oMsg, "확인", a_bIsEnableCancelBtn ? "취소" : string.Empty);

		var oAlertPopup = Factory.CreateCloneGameObj<CAlertPopup>("AlertPopup",
			Resources.Load<GameObject>("Prefabs/Global/G_Prefab_AlertPopup"), a_oParent,
			Vector3.zero, Vector3.one, Vector3.zero);

		oAlertPopup.Init(stParams);
		return oAlertPopup;
	}
	#endregion // 클래스 함수
}
