using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public const string G_SCENE_N_E14 = "Example_14 (3D TPS - 시작)";
	public const string G_SCENE_N_E15 = "Example_15 (3D TPS - 플레이)";
	public const string G_SCENE_N_E16 = "Example_16 (3D TPS - 결과)";
	public const string G_SCENE_N_E17 = "Example_17 (스케줄링)";
	public const string G_SCENE_N_E18 = "Example_18 (네트워크)";
	public const string G_SCENE_N_E19 = "Example_19 (틱택토 - 시작)";
	public const string G_SCENE_N_E20 = "Example_20 (틱택토 - 플레이)";
	public const string G_SCENE_N_E21 = "Example_21 (틱택토 - 결과)";
	public const string G_SCENE_N_E22 = "Example_22 (플러그인 연동)";
	public const string G_SCENE_N_E23 = "Example_23 (자료구조)";
	public const string G_SCENE_N_E24 = "Example_24 (파티클)";
	#endregion // 기본
}

/** Example 20 상수 */
public static partial class KDefine {
	#region 기본
	// 단위
	public const float E20_CELL_WIDTH = 200.0f;
	public const float E20_CELL_HEIGHT = KDefine.E20_CELL_WIDTH;
	#endregion // 기본
}
