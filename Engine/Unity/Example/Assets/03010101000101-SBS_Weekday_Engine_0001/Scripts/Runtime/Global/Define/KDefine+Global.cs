using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 전역 상수 */
public static partial class KDefine
{
	#region 기본
	// 단위 {
	public const float G_CAMERA_FOV = 45.0f;
	public const float G_DESIGN_WIDTH = 1280.0f;
	public const float G_DESIGN_HEIGHT = 720.0f;

	public static readonly Vector3 G_DESIGN_SIZE = new Vector3(G_DESIGN_WIDTH, 
		G_DESIGN_HEIGHT, 0.0f);
	// 단위 }

	// 레이어
	public const string G_N_LAYER_DEF = "Default";

	// 씬 이름
	public const string G_N_SCENE_EXAMPLE_00 = "E01Example_00 (메뉴)";
	public const string G_N_SCENE_EXAMPLE_01 = "E01Example_01 (Unity 기초)";
	public const string G_N_SCENE_EXAMPLE_02 = "E01Example_02 (프리팹, 물리 엔진)";
	public const string G_N_SCENE_EXAMPLE_03 = "E01Example_03 (플래피 버드 - 시작)";
	public const string G_N_SCENE_EXAMPLE_04 = "E01Example_04 (플래피 버드 - 플레이)";
	public const string G_N_SCENE_EXAMPLE_05 = "E01Example_05 (플래피 버드 - 결과)";
	public const string G_N_SCENE_EXAMPLE_06 = "E01Example_06 (스프라이트, 애니메이션)";
	public const string G_N_SCENE_EXAMPLE_07 = "E01Example_07 (두더지 잡기 - 시작)";
	public const string G_N_SCENE_EXAMPLE_08 = "E01Example_08 (두더지 잡기 - 플레이)";
	public const string G_N_SCENE_EXAMPLE_09 = "E01Example_09 (두더지 잡기 - 결과)";
	public const string G_N_SCENE_EXAMPLE_10 = "E01Example_10 (UGUI)";
	public const string G_N_SCENE_EXAMPLE_11 = "E01Example_11 (사운드)";
	public const string G_N_SCENE_EXAMPLE_12 = "E01Example_12 (쉐이더)";
	public const string G_N_SCENE_EXAMPLE_13 = "E01Example_13 (네비게이션 메쉬)";
	public const string G_N_SCENE_EXAMPLE_14 = "E01Example_14 (3D TPS - 시작)";
	public const string G_N_SCENE_EXAMPLE_15 = "E01Example_15 (3D TPS - 플레이)";
	public const string G_N_SCENE_EXAMPLE_16 = "E01Example_16 (3D TPS - 결과)";
	public const string G_N_SCENE_EXAMPLE_17 = "E01Example_17 (스케줄링)";
	public const string G_N_SCENE_EXAMPLE_18 = "E01Example_18 (네트워크)";
	public const string G_N_SCENE_EXAMPLE_19 = "E01Example_19 (틱택토 - 시작)";
	public const string G_N_SCENE_EXAMPLE_20 = "E01Example_20 (틱택토 - 플레이)";
	public const string G_N_SCENE_EXAMPLE_21 = "E01Example_21 (틱택토 - 결과)";
	public const string G_N_SCENE_EXAMPLE_22 = "E01Example_22 (플러그인 연동)";
	public const string G_N_SCENE_EXAMPLE_23 = "E01Example_23 (자료구조)";
	public const string G_N_SCENE_EXAMPLE_24 = "E01Example_24 (파티클)";
	#endregion // 기본
}

/** Example 20 상수 */
public static partial class KDefine
{
	#region 기본
	// 단위
	public const float E20_CELL_WIDTH = 200.0f;
	public const float E20_CELL_HEIGHT = KDefine.E20_CELL_WIDTH;
	#endregion // 기본
}
