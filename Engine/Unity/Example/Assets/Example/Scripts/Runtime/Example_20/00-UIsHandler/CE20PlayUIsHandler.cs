using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 플레이 UI 처리자 */
public class CE20PlayUIsHandler : CE20UIsHandler {
	/** 매개 변수 */
	public new struct STParams {
		public CE20UIsHandler.STParams m_stBaseParams;
	}

	#region 프로퍼티
	public new STParams Params { get; private set; }
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		base.Init(a_stParams.m_stBaseParams);
		this.Params = a_stParams;
	}
	#endregion // 함수

	#region 클래스 팩토리 함수
	/** 매개 변수를 생성한다 */
	public new static STParams MakeParams(CE20SceneManager a_oSceneManager) {
		return new STParams() {
			m_stBaseParams = CE20UIsHandler.MakeParams(a_oSceneManager)
		};
	}
	#endregion // 클래스 팩토리 함수
}
