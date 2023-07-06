using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 마우스 버튼 */
public enum EMouseBtn {
	NONE = -1,
	LEFT,
	RIGHT,
	MIDDLE,
	[HideInInspector] MAX_VAL
}

/** 전역 상수 */
public static partial class KDefine {
	#region 기본
	// 단위 {
	public const float G_CAMERA_FOV = 45.0f;
	public const float G_DESIGN_WIDTH = 1280.0f;
	public const float G_DESIGN_HEIGHT = 720.0f;

	public static readonly Vector3 G_DESIGN_SIZE = new Vector3(G_DESIGN_WIDTH, G_DESIGN_HEIGHT, 0.0f);
	// 단위 }

	// 레이어 이름
	public const string G_LAYER_N_DEF = "Default";

	// 씬 이름
	public const string G_SCENE_N_E00 = "Example_00 (메뉴)";
	public const string G_SCENE_N_E01 = "Example_01 (Unity 기초)";
	public const string G_SCENE_N_E02 = "Example_02 (프리팹, 물리 엔진)";
	public const string G_SCENE_N_E03 = "Example_03 (플래피 버드 - 시작)";
	public const string G_SCENE_N_E04 = "Example_04 (플래피 버드 - 플레이)";
	public const string G_SCENE_N_E05 = "Example_05 (플래피 버드 - 결과)";
	public const string G_SCENE_N_E06 = "Example_06 (스프라이트, 애니메이션)";
	public const string G_SCENE_N_E07 = "Example_07 (두더지 잡기 - 시작)";
	public const string G_SCENE_N_E08 = "Example_08 (두더지 잡기 - 플레이)";
	public const string G_SCENE_N_E09 = "Example_09 (두더지 잡기 - 결과)";
	public const string G_SCENE_N_E10 = "Example_10 (UGUI)";
	public const string G_SCENE_N_E11 = "Example_11 (사운드)";
	public const string G_SCENE_N_E12 = "Example_12 (쉐이더)";
	public const string G_SCENE_N_E13 = "Example_13 (네비게이션 메쉬)";
	#endregion // 기본
}
