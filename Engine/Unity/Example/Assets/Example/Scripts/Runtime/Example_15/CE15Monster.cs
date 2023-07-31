using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.AI;

/** 몬스터 */
public class CE15Monster : CE15Interactable {
	#region 변수
	[SerializeField] private float m_fAttackRange = 0.0f;
	[SerializeField] private float m_fTrackingRange = 0.0f;

#if UNITY_EDITOR
	[SerializeField] private string m_oCurState = string.Empty;
#endif // #if UNITY_EDITOR
	#endregion // 변수

	#region 프로퍼티
	public float AttackRange => m_fAttackRange;
	public float TrackingRange => m_fTrackingRange;

	public Animator Animator { get; private set; } = null;
	public NavMeshAgent NavMeshAgent { get; private set; } = null;
	public CE15StateMachine StateMachine { get; } = new CE15StateMachine();
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		this.StateMachine.SetOwner(this);

		this.Animator = this.GetComponent<Animator>();
		this.NavMeshAgent = this.GetComponent<NavMeshAgent>();
	}

	/** 초기화 */
	public virtual void Init() {
		this.StateMachine.SetState(this.CreateIdleState());
	}

	/** 상태를 갱신한다 */
	public override void Update() {
		base.Update();
		this.StateMachine.OnUpdate(Time.deltaTime);
	}

	/** 타격 되었을 경우 */
	public override void OnHit() {
		// Do Something
	}
	#endregion // 함수

	#region 접근 함수
#if UNITY_EDITOR
	/** 현재 상태를 변경한다 */
	public void SetCurState(string a_oState) {
		m_oCurState = a_oState;
	}
#endif // #if UNITY_EDITOR
	#endregion // 접근 함수

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
