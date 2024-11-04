using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 전역 접근자 */
public static partial class Access
{
	#region 클래스 프로퍼티
	public static float ScreenWidth
	{
		get
		{
			/*
			 * Screen 클래스를 활용하면 현재 디바이스의 너비와 높이를 가져오는 것이 가능하다. 단, 에디터 상에서
			 * 구동 중 일 때는 해당 클래스를 사용하면 올바른 디바이스 크기를 가져오는 것이 불가능하기 때문에
			 * 에디터 환경에서는 Camera 클래스를 활용해야한다.
			 */
#if UNITY_EDITOR
			return Camera.main.pixelWidth;
#else
			return Screen.width;
#endif // #if UNITY_EDITOR
		}
	}

	public static float ScreenHeight
	{
		get
		{
#if UNITY_EDITOR
			return Camera.main.pixelHeight;
#else
			return Screen.height;
#endif // #if UNITY_EDITOR
		}
	}

	public static Vector3 ScreenSize => new Vector3(ScreenWidth, ScreenHeight, 0.0f);
	#endregion // 클래스 프로퍼티

	#region 클래스 함수
	/** 해상도 너비를 반환한다 */
	public static float GetResolutionWidth(Vector3 a_stDesignSize)
	{
		float fAspect = a_stDesignSize.x / a_stDesignSize.y;
		return ScreenHeight * fAspect;
	}

	/** 해상도 비율을 반환한다 */
	public static float GetResolutionScale(Vector3 a_stDesignSize)
	{
		float fScreenWidth = Access.GetResolutionWidth(a_stDesignSize);
		return fScreenWidth.ExIsLessEquals(ScreenWidth) ? 1.0f : ScreenWidth / fScreenWidth;
	}
	#endregion // 클래스 함수
}
