using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 몬스터 상태 */
public class CE15MonsterState : CE15State {
	#region 프로퍼티
	public CE15Player Player {
		get {
			var oSceneManager = CSceneManager.GetSceneManager<CE15SceneManager>(KDefine.G_SCENE_N_E15);
			return oSceneManager.Player;
		}
	}
	#endregion // 프로퍼티
}

/** 몬스터 대기 상태 */
public class CE15MonsterIdleState : CE15MonsterState {
	#region 함수
	/** 상태가 시작 되었을 경우 */
	public override void OnStateEnter() {
		this.Owner.NavMeshAgent.isStopped = true;
	}
	#endregion // 함수
}

/** 몬스터 추적 상태 */
public class CE15MonsterTrackingState : CE15MonsterState {
	#region 함수
	/** 상태가 시작 되었을 경우 */
	public override void OnStateEnter() {
		this.Owner.NavMeshAgent.isStopped = false;
	}

	/** 상태를 갱신한다 */
	public override void OnStateUpdate(float a_fDeltaTime) {
		var stPos = this.Player.transform.position;
		this.Owner.NavMeshAgent.SetDestination(stPos);
	}
	#endregion // 함수
}

/** 몬스터 공격 상태 */
public class CE15MonsterAttackState : CE15MonsterState {

}
