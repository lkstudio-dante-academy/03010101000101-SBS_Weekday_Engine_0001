using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Example 24 */
public class CE01Example_24 : CSceneManager
{
	#region 변수
	[SerializeField] private GameObject m_oParticleSystem = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_24;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		var oDispatcher = m_oParticleSystem.GetComponent<CEventDispatcher>();
		oDispatcher.ParticleCallback = this.HandleOnParticleCollision;
	}

	/** 상태를 갱신한다 */
	public override void Update()
	{
		base.Update();

		// 스페이스 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Space))
		{
			var oParticle = m_oParticleSystem.GetComponent<ParticleSystem>();

			// 재생 중 일 경우
			if(oParticle.isEmitting)
			{
				oParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
			}
			else
			{
				oParticle.Play(true);
			}
		}
	}

	/** 파티클 충돌을 처리한다 */
	private void HandleOnParticleCollision(CEventDispatcher a_oSender,
		GameObject a_oGameObj)
	{
		Debug.Log($"HandleOnParticleCollision: {a_oGameObj.name}");
	}
	#endregion // 함수
}
