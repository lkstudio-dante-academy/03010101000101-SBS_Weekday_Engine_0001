using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/** 씬 관리자 */
public abstract class CSceneManager : CComponent
{
	#region 클래스 변수
	private static Dictionary<string, CSceneManager> m_oSceneManagerDict = new Dictionary<string, CSceneManager>();
	#endregion // 클래스 변수

	#region 프로퍼티
	public abstract string SceneName { get; }

	public GameObject Base { get; private set; } = null;
	public GameObject EventSystem { get; private set; } = null;

	public GameObject UIs { get; private set; } = null;
	public GameObject PopupUIs { get; private set; } = null;

	public GameObject Objs { get; private set; } = null;
	public GameObject StaticObjs { get; private set; } = null;

	public bool IsActiveScene => this.ActiveScene.name.Equals(this.SceneName);
	public Scene ActiveScene => SceneManager.GetActiveScene();
	public virtual Vector3 Gravity => new Vector3(0.0f, -1280.0f, 0.0f);
	#endregion // 프로퍼티

	#region 클래스 프로퍼티
	public static bool IsLoadTables { get; private set; } = false;
	#endregion // 클래스 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		Physics.gravity = this.Gravity;

		CSceneManager.m_oSceneManagerDict.TryAdd(this.SceneName, this);

		// 테이블 로드가 필요 할 경우
		if(!CSceneManager.IsLoadTables)
		{
			CStrTable.Inst.LoadStrTable("Tables/Global/G_Table_Str");
		}

		/*
		 * GameObject.Find 메서드를 활용하면 씬 상에 존재하는 게임 객체를 탐색하는 것이
		 * 가능하다. 단, 해당 메서드는 액티브 씬을 대상으로만 동작하기 때문에 액티브 씬
		 * 이외에는 사용 불가능한 단점이 존재한다.
		 * 
		 * 따라서, 액티브 씬이 아닌 씬을 대상으로 특정 게임 객체를 탐색하기 위해서는
		 * Transform 컴포넌트에 존재하는 Find 메서드를 활용해야한다.
		 * 
		 * Ex)
		 * var oObj = GameObject.Find("ObjectName");
		 */
		// 최상단 객체를 설정한다 {
		var oRootObjs = this.gameObject.scene.GetRootGameObjects();

		for(int i = 0; i < oRootObjs.Length; ++i)
		{
			this.Base = this.Base ??
				(oRootObjs[i].name.Equals("Base") ? oRootObjs[i] : null);

			this.EventSystem = this.EventSystem ??
				oRootObjs[i].transform.Find("EventSystem")?.gameObject;

			this.UIs = this.UIs ??
				oRootObjs[i].transform.Find("Canvas/UIs")?.gameObject;

			this.PopupUIs = this.PopupUIs ??
				oRootObjs[i].transform.Find("Canvas/PopupUIs")?.gameObject;

			this.Objs = this.Objs ??
				oRootObjs[i].transform.Find("Objs")?.gameObject;

			this.StaticObjs = this.StaticObjs ??
				oRootObjs[i].transform.Find("StaticObjs")?.gameObject;
		}
		// 최상단 객체를 설정한다 }

		/*
		 * GetComponentInChildren 메서드는 명시 된 컴포넌트를 가져오는 역할을 수행한다.
		 * 
		 * 단, GetComponent 메서드와 해당 메서드를 호출한 게임 객체에 컴포넌트가 존재하지 않을 경우
		 * 자식 객체로부터 컴포넌트를 가져오는 차이점이 존재한다.
		 * 
		 * 또한, 자식 객체가 아니라 부모로부터 컴포넌트를 가져오고 싶다면 GetComponentInParent
		 * 메서드를 활용하면 된다.
		 */
		// 고유 컴포넌트를 설정한다
		this.Base.GetComponentInChildren<AudioListener>().enabled = this.IsActiveScene;
		this.EventSystem.GetComponentInChildren<EventSystem>().enabled = this.IsActiveScene;
	}

	/** 상태를 갱신한다 */
	public override void Update()
	{
		base.Update();

		// Esc 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Escape) && !this.SceneName.Equals(KDefine.G_N_SCENE_EXAMPLE_00))
		{
			var oAlertPopup = this.PopupUIs.GetComponentInChildren<CAlertPopup>();

			// 알림 팝업이 존재 할 경우
			if(oAlertPopup != null)
			{
				oAlertPopup.Close();
			}
			else
			{
				/*
				 * 문자열 테이블이란?
				 * - 프로그램이 동작하는 국가의 언어에 맞춰서 문장을 치환 시킬 수 있는 정보를
				 * 지니고 있는 데이터의 집합을 의미한다. (즉, 여러 국가에 서비스 되는 프로그램을
				 * 제작하기 위해서는 지역화 처리를 해줄 필요가 있으며 해당 처리는 문자열 테이블을
				 * 활용하면 좀 더 수월하게 지역화 처리를 할 수 있다.)
				 */
				oAlertPopup = CAlertPopup.CreateAlertPopup(CStrTable.Inst.GetStr("ALERT_MSG_MENU"),
					this.PopupUIs);

				oAlertPopup.Show(this.OnReceiveAlertPopupCallback);
			}
		}
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy()
	{
		base.OnDestroy();

		// 씬 관리자가 존재 할 경우
		if(CSceneManager.m_oSceneManagerDict.ContainsKey(this.SceneName))
		{
			CSceneManager.m_oSceneManagerDict.Remove(this.SceneName);
		}
	}

	/** 알림 팝업 콜백을 수신했을 경우 */
	protected virtual void OnReceiveAlertPopupCallback(CAlertPopup a_oSender,
		bool a_bIsOK)
	{
		// 확인 버튼을 눌렀을 경우
		if(a_bIsOK)
		{
			CSceneLoader.Inst.LoadScene(KDefine.G_N_SCENE_EXAMPLE_00);
		}
	}
	#endregion // 함수

	#region 제네릭 함수
	/** 씬 관리자를 반환한다 */
	public static T GetSceneManager<T>(string a_oSceneName) where T : CSceneManager
	{
		return CSceneManager.m_oSceneManagerDict.GetValueOrDefault(a_oSceneName) as T;
	}
	#endregion // 제네릭 함수
}
