using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 카메라 제어자 */
public class CE01CameraController_15 : CComponent
{
	#region 변수
	[SerializeField] private float m_fHeight = 0.0f;
	[SerializeField] private float m_fDistance = 0.0f;
	[SerializeField] private GameObject m_oFollowTarget = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Start()
	{
		base.Start();
		this.SetupCameraPos(true);
	}

	/** 상태를 갱신한다 */
	public override void Update()
	{
		base.Update();
		this.SetupCameraPos();
	}

	/** 카메라 위치를 설정한다 */
	private void SetupCameraPos(bool a_bIsImmediate = false)
	{
		var stOffset = (m_oFollowTarget.transform.forward * -m_fDistance) +
			(Vector3.up * m_fHeight);

		var stPos = m_oFollowTarget.transform.position;
		var stNextPos = stPos + stOffset;

		// 즉시 적용 모드 일 경우
		if(a_bIsImmediate)
		{
			this.transform.position = stNextPos;
		}
		else
		{
			this.transform.position = Vector3.Lerp(this.transform.position,
				stNextPos, Time.deltaTime * 5.0f);
		}

		this.transform.LookAt(m_oFollowTarget.transform.position);
	}
	#endregion // 함수
}
