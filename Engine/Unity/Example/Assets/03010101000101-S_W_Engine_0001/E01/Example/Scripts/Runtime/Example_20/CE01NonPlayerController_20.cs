using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/** NPC 제어자 */
public class CE01NonPlayerController_20 : CE01AgentController_20
{
	/** 매개 변수 */
	public new record STParams : CE01AgentController_20.STParams
	{
		#region 함수
		/** 생성자 */
		public STParams(CE01Engine_20 a_oEngine,
			System.Action<CE01AgentController_20, Vector3Int> a_oTouchCallback) : base(a_oEngine, a_oTouchCallback)
		{
			// Do Something
		}
		#endregion // 함수
	}

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		CE01NetworkManager_20.Inst.AddCallback(E20PacketType.AGENT_TOUCH_RESPONSE,
			this.OnReceiveAgentTouchResponse);
	}

	/** 초기화 */
	public override void Init(CE01AgentController_20.STParams a_stParams)
	{
		base.Init(a_stParams);
	}

	/** 에이전트 터치 응답을 수신했을 경우 */
	private void OnReceiveAgentTouchResponse(CE01NetworkManager_20 a_oSender,
		STPacketInfo a_stPacketInfo)
	{
		var stIdx = STVec3Int.ToVec3Int(a_stPacketInfo.m_oParams);

		/*
		 * SendMessage 및 BroadcastMessage 메서드를 활용하면 특정 게임 객체가
		 * 지니고 있는 컴포넌트의 메서드를 호출하는 것이 가능하다. (즉, 해당 메서드를
		 * 활용하면 특정 컴포넌트에 직접 접근하지 않고 메서드를 호출하는 것이 가능하다
		 * 는 것을 알 수 있다.)
		 * 
		 * 단, 해당 메서드를 통해서 호출 할 수 있는 메서드 유형은 매개 변수를 1 개 
		 * 이하로 받는 메서드만 호출 할 수 있기 때문에 여러 매개 변수를 전달 받는 
		 * 메서드를 호출하는 것이 불가능하다.
		 */
		this.Params.m_oEngine.gameObject.SendMessage("OnReceiveAgentTouchCallback", new object[]
		{
			this, stIdx
		}, SendMessageOptions.DontRequireReceiver);
	}
	#endregion // 함수

	#region 클래스 팩토리 함수
	/** 매개 변수를 생성한다 */
	public static STParams MakeParams(CE01Engine_20 a_oEngine,
		System.Action<CE01AgentController_20, Vector3Int> a_oTouchCallback)
	{
		return new STParams(a_oEngine, a_oTouchCallback);
	}
	#endregion // 클래스 팩토리 함수
}
