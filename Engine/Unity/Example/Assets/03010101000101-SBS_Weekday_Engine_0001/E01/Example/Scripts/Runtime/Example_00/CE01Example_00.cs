using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/** Example 0 */
public class CE01Example_00 : CSceneManager
{
	#region 변수
	[SerializeField] private GameObject m_oOriginMenuBtn = null;
	[SerializeField] private GameObject m_oScrollViewContents = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_00;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		for(int i = 1; i < SceneManager.sceneCountInBuildSettings; ++i)
		{
			int nIdx = i;
			string oScenePath = SceneUtility.GetScenePathByBuildIndex(i);

			// 버튼을 설정한다 {
			var oMenuBtn = Factory.CreateCloneGameObj<Button>("MenuBtn",
				m_oOriginMenuBtn, m_oScrollViewContents, Vector3.zero, Vector3.one, Vector3.zero);

			oMenuBtn.onClick.AddListener(() => this.OnTouchMenuBtn(nIdx));
			// 버튼을 설정한다 }

			// 텍스트 설정한다
			var oMenuBtnText = oMenuBtn.GetComponent<Text>();
			oMenuBtnText.text = Path.GetFileNameWithoutExtension(oScenePath);
		}
	}

	/** 메뉴 버튼을 눌렀을 경우 */
	private void OnTouchMenuBtn(int a_nIdx)
	{
		/*
		 * SceneManager 로 씬을 로드 할 때 씬 이름 대신에 씬 경로를 입력으로 넘겨줘도 전혀
		 * 상관 없다는 것을 알 수 있다. (즉, 별도로 씬 경로로부터 씬 이름을 분리 할 필요가 없다.)
		 */
		CSceneLoader.Inst.LoadScene(SceneUtility.GetScenePathByBuildIndex(a_nIdx));
	}
	#endregion // 함수
}
