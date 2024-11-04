//#define E11_AUDIO_SOURCE
#define E11_SOUND_MANAGER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Unity 에서 사운드 재생 방법
 * - AudioSource.PlayClipAtPoint 메서드 호출
 * - Audio Source 컴포넌트에 Audio Clip 을 설정 후 Play 메서드 호출
 * 
 * PlayClipAtPoint vs Audio Clip
 * - PlayClipAtPoint 메서드를 사용하면 별도의 게임 객체를 생성하지 않고 사운드를 재생하는 것이
 * 가능하다. 단, Unity 에서 사운드를 재생하기 위해서는 반드시 Audio Source 컴포넌트가 필요하기
 * 때문에 PlayClipAtPoint 메서드를 호출하는 순간 자동으로 Audio Source 컴포넌트를 포함하고 있는
 * 게임 객체가 생성되고 사라지는 단점이 존재한다. (즉, 사운드르 빈번하게 재생하면 많은 게임 객체가
 * 생성되고 제거 되기 때문에 프로그램의 성능이 저하된다는 것을 알 수 있다.)
 * 
 * 반면, Audio Source 컴포넌트를 통해 사운드를 재생하면 별도로 객체가 생성되지 않기 때문에 사운드를
 * 빈번하게 재생해도 프로그램의 성능 저하가 PlayClipAtPoint 메서드보다 적다는 장점이 존재한다.
 * 
 * 단, Audio Source 한번에 하나의 사운드만 재생하는 것이 가능하기 때문에 여러 사운드를 중첩으로
 * 재생하기 위해서는 반드시 재생하는 사운드 개수만큼 Audio Source 컴포넌트를 지닌 객체를 생성 해
 * 줄 필요가 있다.
 */
/** Example 11 */
public class CE01Example_11 : CSceneManager
{
	#region 변수
	[SerializeField] private AudioClip m_oBGSound = null;
	[SerializeField] private AudioSource m_oFXSound = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_N_SCENE_EXAMPLE_11;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
	}

	/** 상태를 갱신한다 */
	public override void Update()
	{
		base.Update();

#if E11_AUDIO_SOURCE
		// 배경음 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			AudioSource.PlayClipAtPoint(m_oBGSound, Camera.main.transform.position + Vector3.forward);
		}
		// 효과음 키를 눌렀을 경우 
		else if(Input.GetKeyDown(KeyCode.Alpha2)) {
			m_oFXSound.Play();
		}
#elif E11_SOUND_MANAGER
		// 배경음 음소거 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Space))
		{
			bool bIsMute = CSoundManager.Inst.IsMuteBGSnd;
			CSoundManager.Inst.SetIsMuteBGSnd(!bIsMute);
		}

		// 배경음 볼륨 키를 눌렀을 경우
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
		{
			float fDelta = 1.0f * Time.deltaTime;
			float fVolume = CSoundManager.Inst.BGSndVolume;

			// 볼륨 업 키를 눌렀을 경우
			if(Input.GetKey(KeyCode.UpArrow))
			{
				CSoundManager.Inst.SetVolumeBGSnd(fVolume + fDelta);
			}
			else
			{
				CSoundManager.Inst.SetVolumeBGSnd(fVolume - fDelta);
			}
		}

		// 배경음 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			CSoundManager.Inst.PlayBGSound("Example_11/E11BGSound");
		}
		// 효과음 키를 눌렀을 경우 
		else if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			CSoundManager.Inst.PlayFXSounds("Example_11/E11FXSound_01");
		}
		// 효과음 키를 눌렀을 경우 
		else if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			CSoundManager.Inst.PlayFXSounds("Example_11/E11FXSound_02");
		}
#endif // #if E11_AUDIO_SOURCE
	}
	#endregion // 함수
}
