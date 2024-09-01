using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 갱신 인터페이스 */
public interface IUpdatable
{
	#region 인터페이스
	/** 상태를 갱신한다 */
	public void OnUpdate(float a_fDeltaTime);

	/** 상태를 갱신한다 */
	public void OnLateUpdate(float a_fDeltaTime);

	/** 상태를 갱신한다 */
	public void OnFixedUpdate(float a_fDeltaTime);
	#endregion // 인터페이스
}
