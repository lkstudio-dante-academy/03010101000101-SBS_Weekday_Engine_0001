using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 몬스터 상태 행동 처리 */
public class CE01MonsterStateBehaviour_15 : StateMachineBehaviour
{
	#region 프로퍼티
	public System.Action<CE01MonsterStateBehaviour_15, Animator, AnimatorStateInfo, int> Callback { get; set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 상태가 종료 되었을 경우 */
	public override void OnStateExit(Animator a_oSender,
		AnimatorStateInfo a_stStateInfo, int a_nLayerIdx)
	{
		this.Callback?.Invoke(this, a_oSender, a_stStateInfo, a_nLayerIdx);
	}
	#endregion // 함수
}
