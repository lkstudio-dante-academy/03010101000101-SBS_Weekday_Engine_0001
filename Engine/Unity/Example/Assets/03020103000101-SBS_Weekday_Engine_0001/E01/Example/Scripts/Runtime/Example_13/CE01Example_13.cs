using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

/*
 * 네비게이션 메쉬란?
 * - 객체가 특정 위치까지 이동 할 수 있는 경로를 계산해주는 기능을 의미한다. (즉, 네비게이션 메쉬는
 * Unity 가 지원해주는 AI 기능 중 하나로서 특정 위치까지 이동 할 수 있는 최단 거리를 계산해주는
 * 기능이란든 것을 알 수 있다.)
 * 
 * Unity 는 내부적으로 경로를 계산하기 위한 알고리즘으로 A* 를 사용하기 때문에 별도의 경로 찾기
 * 알고리즘을 모른다 하더라도 손쉽게 특정 객체를 원하는 위치로 이동 시키는 것이 가능하다.
 * 
 * 단, 네비게이션 메쉬가 경로를 탐색하기 위해서는 경로를 탐색 할 수 있는 맵 정보를 미리 제작 해둘
 * 필요가 있으며 만약 해당 정보가 존재하지 않을 경우에는 동적으로 맵 정보를 생성하는 기능 또한
 * 제공한다. (즉, 네비게이션 맵 정보는 정적으로 생성 할 수도 있지만 필요에 따라 동적으로 생성하는
 * 것도 가능하다는 것을 알 수 있다.)
 */
/** Example 13 */
public class CE01Example_13 : CSceneManager
{
	#region 변수
	private Tween m_oTween = null;

	[SerializeField] private GameObject m_oTarget = null;
	[SerializeField] private GameObject m_oObstacle = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_13;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		var stPos = m_oObstacle.transform.localPosition;

		m_oTween = m_oObstacle.transform.DOLocalMoveX(stPos.x + 500.0f, 1.5f)
			.SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
	}

	/** 상태를 갱신한다 */
	public override void Update()
	{
		base.Update();

		// 마우스 버튼을 눌렀을 경우
		if(Input.GetMouseButtonDown((int)EMouseBtn.LEFT))
		{
			var oRay = Camera.main.ScreenPointToRay(Input.mousePosition);

			bool bIsHit = Physics.Raycast(oRay, out RaycastHit stRaycastHit);
			bool bIsGround = stRaycastHit.collider.CompareTag("E13Ground");

			// 바닥을 눌렀을 경우
			if(bIsHit && (bIsGround || stRaycastHit.collider.CompareTag("E13Obstacle")))
			{
				var oNavMeshAgent = m_oTarget.GetComponent<NavMeshAgent>();
				oNavMeshAgent.SetDestination(stRaycastHit.point);
			}
		}
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy()
	{
		base.OnDestroy();
		m_oTween?.Kill();
	}
	#endregion // 함수
}
