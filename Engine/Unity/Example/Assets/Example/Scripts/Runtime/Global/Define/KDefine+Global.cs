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

	// 씬 이름
	public const string G_SCENE_N_E01 = "Example_01 (Unity 기초)";
	public const string G_SCENE_N_E02 = "Example_02 (프리팹, 물리 엔진)";
	#endregion // 기본
}
