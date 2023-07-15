using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 카메라 제어자 */
public class CE15CameraController : CComponent {
	#region 변수
	[SerializeField] private float m_fHeight = 0.0f;
	[SerializeField] private float m_fDistance = 0.0f;
	[SerializeField] private GameObject m_oFollowTarget = null;
	#endregion // 변수

	#region 함수
	/** 상태를 갱신한다 */
	public override void Update() {
		base.Update();
		this.SetupCameraPos();
	}

	/** 카메라 위치를 설정한다 */
	private void SetupCameraPos() {
		var stOffset = (m_oFollowTarget.transform.forward * -m_fDistance) +
			(Vector3.up * m_fHeight);

		var stPos = m_oFollowTarget.transform.position;

		this.transform.position = stPos + stOffset;
		this.transform.LookAt(m_oFollowTarget.transform.position);
	}
	#endregion // 함수
}
