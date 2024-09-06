using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 전역 팩토리 */
public static partial class Factory
{
	#region 클래스 함수
	/** 게임 객체를 생성한다 */
	public static GameObject CreateGameObj(string a_oName,
		GameObject a_oParent, Vector3 a_stPos, Vector3 a_stScale, Vector3 a_stRoate,
		bool a_bIsStayWorldPos = false)
	{
		/*
		 * ?. (널 조건부 연산자 or 널 조건부 맴버 지정 연산자) 는 좌항의 참조 값이 널 일
		 * 경우 널 값을 반환하며 널이 아니라면 해당 변수가 참조하고 있는 대상의 하위 맴버를
		 * 가져오는 역할을 수행한다.
		 */
		var oGameObj = new GameObject(a_oName);
		oGameObj.transform.SetParent(a_oParent?.transform, a_bIsStayWorldPos);

		oGameObj.transform.localScale = a_stScale;
		oGameObj.transform.localPosition = a_stPos;
		oGameObj.transform.localEulerAngles = a_stRoate;

		return oGameObj;
	}

	/** 사본 게임 객체를 생성한다 */
	public static GameObject CreateCloneGameObj(string a_oName,
		GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stPos,
		Vector3 a_stScale, Vector3 a_stRoate, bool a_bIsStayWorldPos = false)
	{
		var oGameObj = GameObject.Instantiate(a_oOrigin, Vector3.zero, Quaternion.identity);
		oGameObj.name = a_oName;
		oGameObj.transform.SetParent(a_oParent?.transform, a_bIsStayWorldPos);

		oGameObj.transform.localScale = a_stScale;
		oGameObj.transform.localPosition = a_stPos;
		oGameObj.transform.localEulerAngles = a_stRoate;

		return oGameObj;
	}
	#endregion // 클래스 함수

	#region 제네릭 클래스 함수
	/** 게임 객체를 생성한다 */
	public static T CreateGameObj<T>(string a_oName,
		GameObject a_oParent, Vector3 a_stPos, Vector3 a_stScale, Vector3 a_stRoate,
		bool a_bIsStayWorldPos = false) where T : Component
	{
		var oGameObject = Factory.CreateGameObj(a_oName,
			a_oParent, a_stPos, a_stScale, a_stRoate, a_bIsStayWorldPos);

		/*
		 * ?? (널 병합 연산자) 는 해당 연산자를 기준으로 좌항의 참조 값이 널이 아니라면
		 * 좌항의 참조 값을 반환하고 좌항의 참조 값이 널이라면 우항의 참조 값을 반환한다.
		 */
		return oGameObject.GetComponent<T>() ?? oGameObject.AddComponent<T>();
	}

	/** 사본 게임 객체를 생성한다 */
	public static T CreateCloneGameObj<T>(string a_oName,
		GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stPos,
		Vector3 a_stScale, Vector3 a_stRoate,
		bool a_bIsStayWorldPos = false) where T : Component
	{
		var oGameObject = Factory.CreateCloneGameObj(a_oName,
			a_oOrigin, a_oParent, a_stPos, a_stScale, a_stRoate, a_bIsStayWorldPos);

		return oGameObject.GetComponent<T>() ?? oGameObject.AddComponent<T>();
	}
	#endregion // 제네릭 클래스 함수
}
