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

	#region 함수
	public override void OnStateEnter() {
		this.Owner.Animator.ResetTrigger("Attack");

#if UNITY_EDITOR
		this.Owner.SetCurState(this.GetType().Name);
#endif // #if UNITY_EDITOR
	}
	#endregion // 함수
}

/** 몬스터 대기 상태 */
public class CE15MonsterIdleState : CE15MonsterState {
	#region 함수
	/** 상태가 시작 되었을 경우 */
	public override void OnStateEnter() {
		base.OnStateEnter();
		this.Owner.NavMeshAgent.isStopped = true;
	}

	/** 상태를 갱신한다 */
	public override void OnStateUpdate(float a_fDeltaTime) {
		var stDelta = this.Player.transform.position - this.Owner.transform.position;

		// 플레이어 공격이 가능 할 경우
		if(stDelta.magnitude.ExIsLessEquals(this.Owner.AttackRange)) {
			this.Owner.StateMachine.SetState(this.Owner.CreateAttackState());
		}
		// 플레이어 추적이 가능 할 경우
		else if(stDelta.magnitude.ExIsLessEquals(this.Owner.TrackingRange)) {
			this.Owner.StateMachine.SetState(this.Owner.CreateTrackingState());
		}
	}
	#endregion // 함수
}

/** 몬스터 추적 상태 */
public class CE15MonsterTrackingState : CE15MonsterState {
	#region 함수
	/** 상태가 시작 되었을 경우 */
	public override void OnStateEnter() {
		base.OnStateEnter();
		this.Owner.NavMeshAgent.isStopped = false;

		this.Owner.Animator.SetBool("IsTracking", true);
	}

	/** 상태가 종료 되었을 경우 */
	public override void OnStateExit() {
		base.OnStateExit();
		this.Owner.Animator.SetBool("IsTracking", false);
	}

	/** 상태를 갱신한다 */
	public override void OnStateUpdate(float a_fDeltaTime) {
		var stPos = this.Player.transform.position;
		var stDelta = stPos - this.Owner.transform.position;

		this.Owner.NavMeshAgent.SetDestination(stPos);

		// 플레이어 공격이 가능 할 경우
		if(stDelta.magnitude.ExIsLessEquals(this.Owner.AttackRange)) {
			this.Owner.StateMachine.SetState(this.Owner.CreateAttackState());
		}
	}
	#endregion // 함수
}

/** 몬스터 공격 상태 */
public class CE15MonsterAttackState : CE15MonsterState {
	#region 변수
	private bool m_bIsEnableAttack = false;
	private float m_fUpdateSkipTime = 0.0f;
	#endregion // 변수

	#region 함수
	/** 상태가 시작 되었을 경우 */
	public override void OnStateEnter() {
		base.OnStateEnter();
		this.Owner.NavMeshAgent.isStopped = true;

		m_bIsEnableAttack = true;
		m_fUpdateSkipTime = 0.0f;
	}

	/** 상태를 갱신한다 */
	public override void OnStateUpdate(float a_fDeltaTime) {
		m_fUpdateSkipTime += a_fDeltaTime;

		// 플레이어 공격이 가능 할 경우
		if(m_bIsEnableAttack && m_fUpdateSkipTime.ExIsGreatEquals(0.5f)) {
			m_bIsEnableAttack = false;
			this.Owner.Animator.SetTrigger("Attack");
		}
	}
	#endregion // 함수
}
