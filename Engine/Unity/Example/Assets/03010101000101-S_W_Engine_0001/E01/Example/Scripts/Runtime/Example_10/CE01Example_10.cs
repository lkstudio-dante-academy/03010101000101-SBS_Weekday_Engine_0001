//#define E10_IMGUI
#define E10_UNITY_GUI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Unity UI 시스템 종류
 * - ImGUI			<- Unity 초기 버전부터 활용되던 UI 시스템
 * - Unity GUI		<- 현재 가장 많이 활용되는 UI 시스템
 * - UI Toolkit		<- 차세대 UI 시스템 (단, 현재 개발 진행 중)
 * 
 * ImGUI vs Unity GUI
 * - ImGUI 는 Unity 초기부터 현재 버전까지 활용되는 UI 시스템이기 때문에 해당 시스템을 활용하면
 * 모든 Unity 버전에서 구동되는 UI 를 제작하는 것이 가능하다.
 * 
 * 단, ImGUI 는 Unity 에디터 상에 눈으로 보고 배치하는 것이 불가능하며 100 % 스크립트만으로 처리
 * 되기 때문에 사용하기 굉장히 비효율적이고 까다롭다는 단점이 존재한다.
 * 
 * 따라서, Unity 는 Unity GUI 를 개발했으며 해당 시스템은 현재까지도 널리 활용되는 UI 시스템이다.
 * 
 * Unity GUI 는 UI 위젯들을 직접 눈으로 보고 배치 할 수 있기 때문에 ImGUI 에 비해서 UI 를 효율적으로
 * 제작하는 것이 가능하다.
 * 
 * 단, Unity 는 구동 시에 디스플레이 보여지는 UI 뿐만 아니라 Unity 에디터 자체의 UI 도 필요에 따라
 * 커스텀하게 변경 할 수 있는 기능을 제공하며 해당 기능은 오직 ImGUI 만으로 통해서 사용하는 것이
 * 가능하다. (즉, Unity GUI 와 UI Toolkit 은 에디터 자체의 UI 를 제어하는 기능은 제공하지 않는다는
 * 것을 알 수 있다.)
 */
/** Example 10 */
public class CE01Example_10 : CSceneManager
{
	#region 변수
	[Header("=====> Unity GUI <=====")]
	[SerializeField] private List<Toggle> m_oToggleList = new List<Toggle>();
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_10;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

#if E10_UNITY_GUI
		for(int i = 0; i < m_oToggleList.Count; ++i)
		{
			m_oToggleList[i].onValueChanged.AddListener(this.OnTouchToggle);
		}
#endif // #if E10_UNITY_GUI
	}

	/** GUI 를 그린다 */
	public void OnGUI()
	{
#if E10_IMGUI
		var stRect = new Rect(0.0f, 0.0f, 100.0f, 50.0f);

		// 버튼을 눌렀을 경우
		if(GUI.Button(stRect, "버튼")) {
			Debug.Log("버튼이 눌렸습니다!");
		}
#endif // #if E10_IMGUI
	}

	/** 토글을 눌렀을 경우 */
	public void OnTouchToggle(bool a_bIsOn)
	{
		Debug.LogFormat("OnTouchToggle: {0}", a_bIsOn);
	}
	#endregion // 함수
}
