using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/** Example 8 */
public class CE01Example_08 : CSceneManager
{
	/** 상태 */
	private enum EState
	{
		NONE = -1,
		PLAY,
		GAME_OVER,
		[HideInInspector] MAX_VAL
	}

	#region 변수
	[Header("=====> UIs <=====")]
	[SerializeField] private TMP_Text m_oTimeText = null;
	[SerializeField] private TMP_Text m_oScoreText = null;

	[Header("=====> Objs <=====")]
	[SerializeField] private GameObject m_oScoreRoot = null;
	[SerializeField] private GameObject m_oOriginScoreText = null;

	private int m_nScore = 0;
	private EState m_eState = EState.PLAY;

	private float m_fLeftPlayTime = 30.0f;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_08;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		Time.timeScale = 1.0f;
	}

	/** 상태를 갱신한다 */
	public override void Update()
	{
		base.Update();
		m_fLeftPlayTime = Mathf.Max(0.0f, m_fLeftPlayTime - Time.deltaTime);

		// 플레이 시간이 다 지났을 경우
		if(m_eState == EState.PLAY && m_fLeftPlayTime.ExIsLessEquals(0.0f))
		{
			m_eState = EState.GAME_OVER;
			CE01ResultStorage_08.Inst.Score = m_nScore;

			/*
			 * Time.timeScale 프로퍼티는 흘러간 시간 비율을 설정하는 역할을 수행한다. (즉,
			 * 해당 프로퍼티의 값이 어떤 값인지에 따라 Time.deltaTime 값이 보정 된다는 것을
			 * 알 수 있다.)
			 */
			Time.timeScale = 0.0f;

			CSceneLoader.Inst.LoadScene(KDefine.G_N_SCENE_EXAMPLE_09,
				UnityEngine.SceneManagement.LoadSceneMode.Additive);
		}

		/*
		 * 정점 변환 단계
		 * - 지역 (모델) 공간 (좌표)
		 * - 전역 (월드) 공간 (좌표)		<- 3 차원 객체
		 * - 카메라 공간 (좌표)
 		 * - 투영 공간 (좌표)
		 * - 정규 공간 (좌표)
		 * - 화면 공간 (좌표)		<- 마우스 위치
		 */

		// 마우스 버튼을 클릭했을 경우
		if(Input.GetMouseButtonDown((int)EMouseBtn.LEFT))
		{
			/*
			 * ScreenPointToRay 메서드는 화면 위치로부터 월드 공간에 존재하는 광선 정보를
			 * 가져오는 역할을 수행한다. (즉, 해당 메서드는 주로 3 차원 게임을 제작 할 때 마우스로
			 * 특정 물체가 클릭이 되었는지 판별 할 때 활용된다.)
			 * 
			 * 마우스 위치는 화면 공간을 기준으로하는 정보이기 때문에 해당 정보만을 가지고 물체의
			 * 클릭 여부를 판단하는 것은 특별한 케이스 (화면 공간 = 월드 공간) 가 아니라면 불가능하다.
			 * 
			 * 따라서, 화면 공간에 존재하는 마우스 위치 정보를 월드 공간으로 변환하거나 월드 공간에 존재하는
			 * 물체 위치를 화면 공간으로 변환해서 클릭 여부를 판단 할 수 밖에 없다.
			 * 
			 * 일반적으로 3 차원 공간 상에는 많은 물체들이 존재하기 때문에 해당 물체들을 화면 공간으로 변환
			 * 하는 것보다 마우스 위치를 월드 공간으로 변환하는 것이 효율적이다.
			 */
			var stRay = Camera.main.ScreenPointToRay(Input.mousePosition);

			// 두더지를 클릭했을 경우
			if(Physics.Raycast(stRay, out RaycastHit stRaycastHit) &&
				stRaycastHit.collider.CompareTag("E08Target"))
			{
				var oTarget = stRaycastHit.collider.GetComponent<CE01Target_08>();
				oTarget.Catch(this.OnCatchTarget);
			}
		}

		this.UpdateUIsState();
	}

	/** UI 상태를 갱신한다 */
	private void UpdateUIsState()
	{
		m_oTimeText.text = $"{m_fLeftPlayTime:0.00}";
		m_oScoreText.text = $"{m_nScore}";
	}

	/** 타겟을 캐치했을 경우 */
	private void OnCatchTarget(CE01Target_08 a_oSender, int a_nScore)
	{
		var stPos = a_oSender.transform.position.ExToLocal(m_oScoreRoot);

		var oScoreText = Factory.CreateCloneGameObj<CE01ScoreText_08>("ScoreText",
			m_oOriginScoreText, m_oScoreRoot, stPos, Vector3.one, Vector3.zero);

		oScoreText.StartTextAni(a_nScore);
		m_nScore = Mathf.Max(0, m_nScore + a_nScore);
		//CE01ResultStorage_08.Inst.Score = m_nScore;
	}
	#endregion // 함수
}
