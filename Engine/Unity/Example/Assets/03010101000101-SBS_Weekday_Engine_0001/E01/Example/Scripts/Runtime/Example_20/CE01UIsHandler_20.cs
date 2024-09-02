using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** UI 처리자 */
public class CE01UIsHandler_20 : CComponent
{
	/** 매개 변수 */
	public record STParams
	{
		public CE01Example_20 m_oSceneManager = null;

		#region 함수
		/** 생성자 */
		public STParams(CE01Example_20 a_oSceneManager)
		{
			m_oSceneManager = a_oSceneManager;
		}
		#endregion // 함수
	}

	#region 프로퍼티
	public STParams Params { get; private set; }
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public virtual void Init(STParams a_stParams)
	{
		this.Params = a_stParams;
	}
	#endregion // 함수

	#region 클래스 팩토리 함수
	/** 매개 변수를 생성한다 */
	public static STParams MakeParams(CE01Example_20 a_oSceneManager)
	{
		return new STParams(a_oSceneManager);
	}
	#endregion // 클래스 팩토리 함수
}
