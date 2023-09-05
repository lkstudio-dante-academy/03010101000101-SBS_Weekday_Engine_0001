using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** NPC 제어자 */
public class CE20NonPlayerController : CE20AgentController {
	/** 매개 변수 */
	public new record STParams : CE20AgentController.STParams {
		#region 함수
		/** 생성자 */
		public STParams(CE20Engine a_oEngine,
			System.Action<CE20AgentController, Vector3Int> a_oTouchCallback) : base(a_oEngine, a_oTouchCallback) {
			// Do Something
		}
		#endregion // 함수
	}

	#region 함수
	/** 초기화 */
	public override void Init(CE20AgentController.STParams a_stParams) {
		base.Init(a_stParams);
	}
	#endregion // 함수

	#region 클래스 팩토리 함수
	/** 매개 변수를 생성한다 */
	public static STParams MakeParams(CE20Engine a_oEngine,
		System.Action<CE20AgentController, Vector3Int> a_oTouchCallback) {
		return new STParams(a_oEngine, a_oTouchCallback);
	}
	#endregion // 클래스 팩토리 함수
}
