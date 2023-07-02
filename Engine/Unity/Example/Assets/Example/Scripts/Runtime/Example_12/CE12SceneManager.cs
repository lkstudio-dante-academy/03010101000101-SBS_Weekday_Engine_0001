using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 쉐이더란?
 * - GPU 상에서 구동되는 프로그램을 의미한다. (즉, 쉐이더를 활용하면 특정 물체를 화면 상에 그리기
 * 위해서 거치는 단계를 커스텀하게 제어하는 것이 가능하다.)
 * 
 * 렌더링 파이프라인 주요 연산
 * - 정점 연산				<- 정점 쉐이더에 의해서 처리
 * - 래스터라이즈 연산		<- GPU 전용 (즉, 커스텀하게 제어하는 것이 불가능하다.)
 * - 픽셀 연산				<- 픽셀 (프래그먼트) 쉐이더에 의해서 처리
 * 
 * 정점 연산이란?
 * - 3 차원 상에 존재하는 위치 정보를 2 차원으로 변환하는 역할을 수행한다. (즉, 정점 연산 거치고
 * 나면 3 차원 데이터는 2 차원 데이터로 변환된다는 것을 알 수 있다.)
 * 
 * 래스터라이즈 연산이란?
 * - 정점 연산은 거치는 2 차원 정보를 기반으로 실제 디스플레이 상에 그려질 픽셀을 계산하는 역할을
 * 수행한다. (즉, 래스터라이즈 연산을 거치고 나면 3 차원 데이터가 완벽한 2 차원 데이터가 된다는 
 * 것을 의미한다.)
 * 
 * 픽셀 (프래그먼트) 연산이란?
 * - 래스터라이즈 연산에 의해서 계산 된 각 픽셀의 색상을 계산하는 역할을 수행한다. (즉, 픽셀 
 * 쉐이더를 거치고 나면 최종적으로 화면 상에 출력 될 색상 정보가 결정된다는 것을 알 수 있다.)
 * 
 * 유니티 쉐이더 제작 방법
 * - 쉐이더 랩 제작			<- 간단한 문법으로 쉐이더 제작 가능 (단, 만들어낼 수 있는 효과가 제한적)
 * - 서피스 쉐이더 제작		<- 최소한의 지식만으로 다양한 효과 만들어 낼 수 있다.
 * - 정점/픽셀 쉐이더 제작	<- 고퀄리티의 효과를 만들어 낼 수 있지만 전문적인 지식을 요구
 */
/** Example 12 */
public class CE12SceneManager : CSceneManager {
	#region 변수
	private float m_fCutout = 0.0f;
	private float m_fSkipTime = 0.0f;
	private float m_Refraction = 0.0f;

	[SerializeField] private GameObject m_oSphereRoot = null;
	[SerializeField] private MeshRenderer m_oQuadMeshRenderer = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E12;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
	}

	/** 상태를 갱신한다 */
	public override void Update() {
		base.Update();

		/*
		* Material 객체의 Set 계열 메서드를 활용하면 Properties 영역에 명시한 속성을 변경하는 것이 가능하다. (즉, 프로그램이 실행 중에
		* 속성을 변경함으로서 다양한 효과를 연출하는 것이 가능하다.)
		*/
		m_fSkipTime += Time.deltaTime;
		m_oQuadMeshRenderer.material.SetFloat("_Refraction", Mathf.Sin(m_fSkipTime * 5.0f) / 2.0f);

		// 투명도 조절 키를 눌렀을 경우
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)) {
			float fOffset = Input.GetKey(KeyCode.UpArrow) ? 1.0f : -1.0f;
			m_fCutout = Mathf.Clamp01(m_fCutout + (fOffset * Time.deltaTime));
		}

		for(int i = 0; i < m_oSphereRoot.transform.childCount; ++i) {
			var oTrans = m_oSphereRoot.transform.GetChild(i);
			oTrans.Rotate(Vector3.up * 90.0f * Time.deltaTime, Space.World);

			var oRenderer = oTrans.GetComponent<MeshRenderer>();
			oRenderer.material.SetFloat("_Cutout", m_fCutout);
		}
	}
	#endregion // 함수
}
