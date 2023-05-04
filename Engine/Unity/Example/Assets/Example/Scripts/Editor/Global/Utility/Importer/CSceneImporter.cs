using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;

/** 씬 추가자 */
[InitializeOnLoad]
public static partial class CSceneImporter {
	#region 클래스 함수
	/** 생성자 */
	static CSceneImporter() {
		EditorApplication.projectChanged -= CSceneImporter.OnUpdateProjectState;
		EditorApplication.projectChanged += CSceneImporter.OnUpdateProjectState;
	}

	/** 프로젝트 디렉토리 상태가 갱신 되었을 경우 */
	private static void OnUpdateProjectState() {
		/*
		 * GUID (Global Unique Identifier) 란?
		 * - 대상마다 중복되지 않는 고유한 식별자 데이터를 의미하며 유니티는 내부적으로 존재하는 모든 에셋에 GUID 가 부여 되어있다.
		 * (즉, GUID 를 활용하면 특정 에셋의 이름과 저장 된 위치를 모르더라도 해당 에셋을 가져오는 것이 가능하다.)
		 * 
		 * 유니티는 모든 에셋에 대한 정보를 관리 및 제어 할 수 있도록 AssetDatabase 클래스를 제공하며 해당 클래스를 활용하면 에셋을
		 * 대상으로 여러 연산을 처리하는 것이 가능하다. (Ex. 특정 에셋 탐색 및 제거 등등...)
		 */
		var oAssetGUIDs = AssetDatabase.FindAssets("Example_", new string[] {
			"Assets/Example/Scenes"
		});

		var oEditorSceneList = new List<EditorBuildSettingsScene>();

		for(int i = 0; i < oAssetGUIDs.Length; ++i) {
			var oAssetPath = AssetDatabase.GUIDToAssetPath(oAssetGUIDs[i]);

			// 씬 에셋 일 경우
			if(Path.GetFileName(oAssetPath).Contains(".unity")) {
				oEditorSceneList.Add(new EditorBuildSettingsScene(oAssetPath, true));
			}
		}

		EditorBuildSettings.scenes = oEditorSceneList.ToArray();
	}
	#endregion // 클래스 함수
}
#endif // #if UNITY_EDITOR
