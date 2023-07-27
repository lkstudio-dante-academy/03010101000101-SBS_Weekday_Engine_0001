using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/** 몬스터 */
public class CE15Monster : CE15Interactable {
	#region 프로퍼티
	public NavMeshAgent NavMeshAgent { get; private set; } = null;
	public CE15StateMachine StateMachine { get; } = new CE15StateMachine();
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		this.StateMachine.SetOwner(this);

		this.NavMeshAgent = this.GetComponent<NavMeshAgent>();
		this.NavMeshAgent.isStopped = true;
	}

	/** 초기화 */
	public override void Start() {
		base.Start();
		this.StateMachine.SetState(new CE15MonsterIdleState());
	}

	/** 타격 되었을 경우 */
	public override void OnHit() {
		// Do Something
	}
	#endregion // 함수

	#region 팩토리 함수
	/** 대기 상태를 생성한다 */
	public CE15MonsterIdleState CreateIdleState() {
		return this.CreateState<CE15MonsterIdleState>();
	}

	/** 추적 상태를 생성한다 */
	public CE15MonsterTrackingState CreateTrackingState() {
		return this.CreateState<CE15MonsterTrackingState>();
	}

	/** 공격 상태를 생성한다 */
	public CE15MonsterAttackState CreateAttackState() {
		return this.CreateState<CE15MonsterAttackState>();
	}

	/** 상태를 생성한다 */
	private T CreateState<T>() where T : class, new() {
		var oSceneManager = CSceneManager.GetSceneManager<CE15SceneManager>(KDefine.G_SCENE_N_E15);

		return oSceneManager.ObjsPoolManager.SpawnObj<T>(() => {
			return new T();
		}) as T;
	}
	#endregion // 팩토리 함수
}
