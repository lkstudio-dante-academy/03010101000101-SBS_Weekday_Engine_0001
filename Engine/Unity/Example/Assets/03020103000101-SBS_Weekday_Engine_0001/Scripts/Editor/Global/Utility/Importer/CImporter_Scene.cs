using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 에디터 스크립트란?
 * - 에디터 환경에서만 동작하는 스크립트를 의미한다. (즉, 에디터 스크립트는 실제 실행 중인 런타임
 * 빌드 환경에는 포함되지 않는다는 것을 알 수 있다.)
 * 
 * Unity 는 에디터 스크립트를 제공하며 해당 스크립트를 활용하면 Unity 에디터를 통한 작업 환경을
 * Unity 가 지원하는 범위 내에서 마음대로 커스터마이징하는 것이 가능하다. (즉, 에디터 스크립트를
 * 활용하면 특정 프로젝트에 맞는 최적화 된 개발 환경을 구축하는 것이 가능하다.)
 * 
 * 단, 모든 스크립트게 에디터 스크립트가 될 수는 없으면 특정 스크립트가 에디터 스크립트가 되기
 * 위해서는 해당 스크립트가 반드시 Editor 폴더 하위에 존재해야한다. (즉, Unity 는 Editor 폴더를
 * 포함하여 특별한 의미로 사용되는 몇몇 폴더가 존재한다는 것을 알 수 있다.)
 * 
 * 또한, 에디터 스크립트는 에디터 환경에서만 동작하는 스크립트이기 때문에 가능하면 UNITY_EDITOR
 * 심볼을 이용해서 확실하게 에디터 환경에서만 동작하는 스크립트라는 것을 명시하는 것이 좋은
 * 습관이다.
 */
#if UNITY_EDITOR
using System.IO;
using UnityEditor;

/**
 * 씬 임포터
 */
[InitializeOnLoad]
public static partial class CEditor_Importer_Scene
{
	#region 클래스 함수
	/** 생성자 */
	static CEditor_Importer_Scene()
	{
		// 플레이 모드 일 경우
		if(EditorApplication.isPlaying)
		{
			return;
		}

		EditorApplication.projectChanged -= CEditor_Importer_Scene.ImportScenes_All;
		EditorApplication.projectChanged += CEditor_Importer_Scene.ImportScenes_All;
	}

	/** 모든 씬을 추가한다 */
	public static void ImportScenes_All()
	{
		/*
		 * AssetDatabase 클래스란?
		 * - Unity 에 포함 된 모든 에셋을 관리하는 클래스를 의미한다. (즉, 해당 클래스를 활용하면
		 * 특정 에셋을 로드해서 제어하는 것이 가능하다.)
		 * 
		 * Unity 는 내부적으로 에셋을 관리하기 위해 각 에셋마다 고유한 식별자를 부여하며 해당
		 * 식별자를 GUID (Global Unique Identifier) 라고 한다.
		 */
		var oGUIDs = AssetDatabase.FindAssets(KEditor_Define.G_PATTERN_NAME_SCENE, new string[]
		{
			KEditor_Define.G_P_DIR_SEARCH_SCENE
		});

		var oListEditorBuildSettingsScenes = new List<EditorBuildSettingsScene>();

		for(int i = 0; i < oGUIDs.Length; ++i)
		{
			string oPath_File = AssetDatabase.GUIDToAssetPath(oGUIDs[i]);
			string oExtension_File = Path.GetExtension(oPath_File);

			// 씬 추가가 불가능 할 경우
			if(!oExtension_File.Equals(KEditor_Define.G_E_FILE_SCENE))
			{
				continue;
			}

			var oEditorBuildSettingsScene = new EditorBuildSettingsScene(oPath_File, true);
			oListEditorBuildSettingsScenes.Add(oEditorBuildSettingsScene);
		}

		EditorBuildSettings.scenes = oListEditorBuildSettingsScenes.ToArray();
	}
	#endregion // 클래스 함수
}
#endif // #if UNITY_EDITOR
