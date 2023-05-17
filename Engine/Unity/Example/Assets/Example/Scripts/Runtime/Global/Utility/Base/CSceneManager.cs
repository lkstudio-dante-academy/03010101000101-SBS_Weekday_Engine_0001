using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/** 씬 관리자 */
public abstract class CSceneManager : CComponent {
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

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		Physics.gravity = this.Gravity;

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

		for(int i = 0; i < oRootObjs.Length; ++i) {
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
	#endregion // 함수
}
