using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

/*
 * 자료구조란?
 * - 특정 데이터를 저장하고 표현하는 방법을 의미한다. (즉, 자료구조는 데이터를
 * 효율적으로 다루는 방법이라는 것을 알 수 있다.)
 * 
 * 자료구조를 활용하면 여러 데이터를 좀 더 효율적으로 저장 및 제어하는 것이
 * 가능하기 때문에 해당 분야는 프로그래밍의 기본 요소 중 하나이다.
 * 
 * 자료구조 종류
 * - 리스트 (배열/연결 리스트)
 * - 스택/큐
 * - 이진 트리 (+ 밸런스 이진 트리)
 * - 힙
 * - 그래프
 * 
 * 리스트란?
 * - 관리되고 있는 데이터의 순서가 존재하는 자료구조를 의미한다. (즉, 리스트
 * 자료구조를 활용하면 특정 위치에 데이터를 추가하거나 가져오는 것이 가능하다는
 * 것을 알 수 있다.)
 * 
 * 배열 리스트 vs 연결 리스트
 * - 배열 리스트는 배열을 기반으로 데이터의 순차성을 만들어내기 때문에 임의
 * 접근을 통해 특정 위치에 존재하는 데이터에 빠르게 접근하는 것이 가능하다.
 * 
 * 단, 배열의 원리를 이용해서 데이터의 순서를 만들어내기 때문에 특정 위치에
 * 존재하는 데이터를 제거하거나 추가 시킬 경우 데이터의 이동 및 복사 발생하는
 * 단점이 존재한다. (즉, 데이터의 삽입/삭제 연산이 빈번하게 발생 할 경우
 * 배열 리스트가 느리게 동작한다는 것을 알 수 있다.)
 * 
 * 반면, 연결 리스트는 참조에 의해서 데이터의 순차성을 만들어내기 때문에 특정
 * 위치에 존재하는 데이터에 접근하기 위해서는 항상 첫 데이터부터 순차적으로
 * 접근해야 되는 단점이 존재한다. (즉, 임의 접근이 불가능하다는 것을 알 수
 * 있다.)
 * 
 * 단, 연결 리스트는 참조에 의해 데이터의 순서를 만들어내기 때문에 특정 위치에
 * 데이터가 추가되거나 삭제 된다 하더라도 데이터의 이동이 발생하지 않는다.
 * (즉, 삽입/삭제가 빈번한 경우 연결 리스트는 좋은 성능을 지닌다는 것을 알 수
 * 있다.)
 * 
 * 스택이란?
 * - LIFO (Last In First Out) 순서로 데이터를 관리하는 구조를 의미한다.
 * (즉, 스택은 데이터의 입/출력 순서가 엄격하게 제한되기 때문에 다른 컬렉션과
 * 달리 특정 위치에 존재하는 데이터에 자유롭게 접근하는 것이 불가능하다.)
 * 
 * 큐란?
 * - FIFO (First In First Out) 순서로 데이터를 관리하는 구조를 의미한다.
 * (즉, 큐 또한 스택과 마찬가지로 특정 위치에 존재하는 데이터에 자유롭게
 * 접근하는 것이 불가능하다.)
 */
/** Example 23 */
public partial class CE01Example_23 : CSceneManager
{
	#region 변수
	[SerializeField] private Image m_oBGImg = null;
	[SerializeField] private LineRenderer m_oQueueLine = null;

	[Header("=====> UIs <=====")]
	[SerializeField] private InputField m_oIdxInput = null;
	[SerializeField] private InputField m_oValInput = null;

	[SerializeField] private InputField m_oStartInput = null;
	[SerializeField] private InputField m_oEndInput = null;

	[Header("=====> Game Objects <=====")]
	[SerializeField] private GameObject m_oArrayListTargetRoot = null;
	[SerializeField] private GameObject m_oArrayListOriginTarget = null;

	[SerializeField] private GameObject m_oQueueLineRoot = null;
	[SerializeField] private GameObject m_oQueueOriginLine = null;

	[SerializeField] private GameObject m_oMoveableTarget = null;
	[SerializeField] private GameObject m_oQueueTargetRoot = null;
	[SerializeField] private GameObject m_oQueueOriginTarget = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_23;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		CScheduleManager.Inst.AddComponent(this);

#if E23_QUEUE
		m_oMoveableTarget.SetActive(false);

		var oDispatcher = m_oBGImg.GetComponent<CTouchDispatcher>();
		oDispatcher.SetBeginCallback(this.HandleOnTouchBegin);
		oDispatcher.SetMoveCallback(this.HandleOnTouchMove);
		oDispatcher.SetEndCallback(this.HandleOnTouchEnd);
#endif // #if E23_QUEUE
	}

	/** 초기화 */
	public override void Start()
	{
		base.Start();

#if E23_QUEUE
		this.LoadNodes();
		this.ResetNodeObjs();
#endif // #if E23_QUEUE
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy()
	{
		base.OnDestroy();

#if E23_QUEUE
		m_oMoveAni?.Kill();
#endif // #if E23_QUEUE
	}
	#endregion // 함수

	#region 접근 함수
	/** 인덱스를 반환한다 */
	private int GetIdx()
	{
		return int.TryParse(m_oIdxInput.text, out int nIdx) ? nIdx : 0;
	}

	/** 값을 반환한다 */
	private int GetVal()
	{
		return int.TryParse(m_oValInput.text, out int nVal) ? nVal : 0;
	}
	#endregion // 접근 함수
}
