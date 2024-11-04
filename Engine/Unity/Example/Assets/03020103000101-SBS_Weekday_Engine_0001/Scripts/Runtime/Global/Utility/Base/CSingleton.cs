using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 싱글턴 패턴이란?
 * - 프로그램 상에서 유일한 객체를 생성하기 위한 패턴을 의미한다. (즉, 패턴이라는 것은 프로그램 구조를 의미한다.)
 * 
 * 일반적으로 객체는 new 키워드를 통해서 원하는 만큼 생성하는 것이 가능하지만 싱글턴 활용하면 객체를 생성 할 수 있는
 * 방법을 제한시키는 것이 가능하다. (즉, 싱글턴 패턴은 객체 지향 프로그래밍 언어에서 전역 변수와 같은 개념을 의미한다.)
 * 
 * 따라서, 싱글턴 패턴을 활용하면 Unity 에서 씬 간에 데이터를 공유하는 구문을 수월하게 작성하는 것이 가능하다.
 * (즉, 일반적으로 Unity 에서는 씬이 전환 될 때 기존 씬에 존재하는 모든 게임 객체를 제거하는 특징이 존재하기 때문에
 * 씬 간에 데이터를 전달하기 위해서는 클래스 변수, 파일, 싱글턴 패턴 등을 활용해야한다.)
 */
/** 싱글턴 */
public class CSingleton<T> : CComponent where T : CSingleton<T>
{
	#region 변수
	private static T m_tInst = null;
	#endregion // 변수

	/*
	 * 프로퍼티란?
	 * - 접근자 메서드를 좀 더 쉽게 구현하도록 지원되는 기능을 의미한다.
	 */
	#region 클래스 프로퍼티
	public static T Inst
	{
		get
		{
			// 인스턴스가 없을 경우
			if(CSingleton<T>.m_tInst == null)
			{
				var oGameObj = new GameObject(typeof(T).Name);
				CSingleton<T>.m_tInst = oGameObj.AddComponent<T>();
			}

			return CSingleton<T>.m_tInst;
		}
	}
	#endregion // 클래스 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		Debug.Assert(CSingleton<T>.m_tInst == null);

		CSingleton<T>.m_tInst = this as T;
		DontDestroyOnLoad(this.gameObject);
	}
	#endregion // 함수

	#region 클래스 함수
	/** 인스턴스를 생성한다 */
	public static T Create()
	{
		return CSingleton<T>.Inst;
	}
	#endregion // 클래스 함수
}
