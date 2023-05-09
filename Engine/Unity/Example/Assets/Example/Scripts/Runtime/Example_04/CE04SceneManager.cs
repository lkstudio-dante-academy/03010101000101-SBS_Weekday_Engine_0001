using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/** Example 4 */
public class CE04SceneManager : CSceneManager {
	/** 상태 */
	private enum EState {
		NONE = -1,
		PLAY,
		GAME_OVER,
		[HideInInspector] MAX_VAL
	}

	#region 변수
	private int m_nScore = 0;
	private EState m_eState = EState.NONE;
	private List<GameObject> m_oObstacleList = new List<GameObject>();

	[Header("=====> UIs <====")]
	[SerializeField] private TMP_Text m_oScoreText = null;

	[Header("=====> Game Object <=====")]
	[SerializeField] private GameObject m_oPlayer = null;
	[SerializeField] private GameObject m_oObstacleRoot = null;
	[SerializeField] private GameObject m_oOriginObstacle = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E04;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		m_eState = EState.PLAY;

		var oDispatcher = m_oPlayer.GetComponent<CTriggerDispatcher>();
		oDispatcher.EnterCallback = this.HandleOnTriggerEnter;
		oDispatcher.ExitCallback = this.HandleOnTriggerExit;

		StartCoroutine(this.TryCreateObstacles());
	}

	/** 상태를 갱신한다 */
	public override void Update() {
		base.Update();

		// 플레이 상태 일 경우
		if(m_eState == EState.PLAY) {
			for(int i = 0; i < m_oObstacleList.Count; ++i) {
				var stPos = m_oObstacleList[i].transform.localPosition;
				stPos += (Vector3.left * 500.0f) * Time.deltaTime;

				m_oObstacleList[i].transform.localPosition = stPos;
			}

			/** 스페이스 키를 눌렀을 경우 */
			if(Input.GetKeyDown(KeyCode.Space)) {
				var oRigidbody = m_oPlayer.GetComponent<Rigidbody>();
				oRigidbody.velocity = Vector3.zero;
				oRigidbody.angularVelocity = Vector3.zero;

				oRigidbody.AddForce(Vector3.up * 750.0f, ForceMode.VelocityChange);
			}
		}
	}

	/** UI 상태를 갱신한다 */
	private void UpdateUIsState() {
		m_oScoreText.text = $"{m_nScore}";
	}

	/** 접촉 시작 콜백을 처리한다 */
	private void HandleOnTriggerEnter(CTriggerDispatcher a_oSender,
		Collider a_oCollider) {
		// 충돌 영역 일 경우
		if(a_oCollider.CompareTag("E04CollisionArea")) {
			m_eState = EState.GAME_OVER;
			Destroy(m_oPlayer.GetComponent<Rigidbody>());

			CE04ResultStorage.Inst.Score = m_nScore;

			CSceneLoader.Inst.LoadScene(KDefine.G_SCENE_N_E05,
				UnityEngine.SceneManagement.LoadSceneMode.Additive);
		}
	}

	/** 접촉 종료 콜백을 처리한다 */
	private void HandleOnTriggerExit(CTriggerDispatcher a_oSender, 
		Collider a_oCollider) {
		// 안전 영역 일 경우
		if(a_oCollider.CompareTag("E04SafeArea")) {
			m_nScore += 1;
			this.UpdateUIsState();
		}
	}

	/** 장애물 생성을 시도한다 */
	private IEnumerator TryCreateObstacles() {
		do {
			yield return new WaitForSeconds(1.5f);
			var stPos = new Vector3((KDefine.G_DESIGN_WIDTH / 2.0f) + 250.0f, 0.0f, 0.0f);

			var oObstacle = CFactory.CreateCloneGameObj("Obstacle",
				m_oOriginObstacle, m_oObstacleRoot, stPos, Vector3.one, Vector3.zero);

			m_oObstacleList.Add(oObstacle);

			// 영역을 설정한다 {
			float fPercent = Random.Range(0.1f, 0.5f);
			float fUpHeight = KDefine.G_DESIGN_HEIGHT * fPercent;
			float fDownHeight = KDefine.G_DESIGN_HEIGHT * (0.6f - fPercent);
			float fSafeHeight = KDefine.G_DESIGN_HEIGHT * 0.4f;

			var oUpArea = oObstacle.transform.Find("UpArea");
			
			oUpArea.transform.localScale = new Vector3(oUpArea.transform.localScale.x, 
				fUpHeight, 1.0f);

			oUpArea.transform.localPosition = new Vector3(0.0f,
				(KDefine.G_DESIGN_HEIGHT / 2.0f) - (fUpHeight / 2.0f), 0.0f);

			var oDownArea = oObstacle.transform.Find("DownArea");

			oDownArea.transform.localScale = new Vector3(oDownArea.transform.localScale.x, 
				fDownHeight, 1.0f);

			oDownArea.transform.localPosition = new Vector3(0.0f,
				(KDefine.G_DESIGN_HEIGHT / -2.0f) + (fDownHeight / 2.0f), 0.0f);

			var oSafeArea = oObstacle.transform.Find("SafeArea");

			oSafeArea.transform.localPosition = new Vector3(0.0f,
				(KDefine.G_DESIGN_HEIGHT / 2.0f) - fUpHeight - (fSafeHeight / 2.0f), 0.0f);
			// 영역을 설정한다 }
		} while(m_eState == EState.PLAY);
	}
	#endregion // 함수
}
