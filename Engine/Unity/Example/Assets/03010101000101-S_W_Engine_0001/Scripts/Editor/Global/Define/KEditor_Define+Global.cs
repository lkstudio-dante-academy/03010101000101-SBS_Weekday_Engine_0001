using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

/**
 * 에디터 상수
 */
public static partial class KEditor_Define
{
	#region 컴파일 상수
	// 기타
	public const string G_E_FILE_SCENE = ".unity";
	public const string G_PATTERN_NAME_SCENE = "t:Example t:Scene";

	// 경로
	public const string G_P_DIR_SEARCH_SCENE = "Assets/03010101000101-S_W_Engine_0001/E01/Example/Scenes";
	#endregion // 컴파일 상수

	#region 런타임 상수
	public static readonly List<string> G_LIST_PACKAGES_IMPORT = new List<string>()
	{
		// Do Something
	};
	#endregion // 런타임 상수
}
#endif // #if UNITY_EDITOR
