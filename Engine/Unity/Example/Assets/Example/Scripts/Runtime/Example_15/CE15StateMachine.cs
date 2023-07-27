using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 상태 */
public class CE15State {
	#region 프로퍼티
	public CE15Monster Owner { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 상태가 시작 되었을 경우 */
	public virtual void OnStateEnter() {
		// Do Something
	}

	/** 상태가 종료 되었을 경우 */
	public virtual void OnStateExit() {
		// Do Something
	}

	/** 상태를 갱신한다 */
	public virtual void OnStateUpdate(float a_fDeltaTime) {
		// Do Something
	}

	/** 소유자를 변경한다 */
	public void SetOwner(CE15Monster a_oOwner) {
		this.Owner = a_oOwner;
	}
	#endregion // 함수
}

/** 상태 머신 */
public class CE15StateMachine {
	#region 프로퍼티
	public CE15State State { get; private set; } = null;
	public CE15Monster Owner { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 상태를 갱신한다 */
	public void OnUpdate(float a_fDeltaTime) {
		this.State?.OnStateUpdate(a_fDeltaTime);
	}
	
	/** 상태를 변경한다 */
	public void SetState(CE15State a_oState) {
		// 상태 변경이 가능 할 경우
		if(this.State != a_oState) {
			a_oState?.SetOwner(this.Owner);

			var oPrevState = this.State;
			oPrevState?.OnStateExit();

			this.State = a_oState;
			this.State?.OnStateEnter();
		}
	}

	/** 소유자를 변경한다 */
	public void SetOwner(CE15Monster a_oOwner) {
		this.Owner = a_oOwner;
	}
	#endregion // 함수
}
