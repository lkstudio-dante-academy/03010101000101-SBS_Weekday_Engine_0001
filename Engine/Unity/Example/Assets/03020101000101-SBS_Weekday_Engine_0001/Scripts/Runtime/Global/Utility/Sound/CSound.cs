using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 사운드 */
public class CSound : CComponent
{
	#region 변수
	private AudioSource m_oAudioSource = null;
	#endregion // 변수

	#region 프로퍼티
	public bool IsMute => m_oAudioSource.mute;
	public bool IsPlaying => m_oAudioSource.isPlaying;

	public float Volume => m_oAudioSource.volume;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		m_oAudioSource = this.GetComponentInChildren<AudioSource>();
		m_oAudioSource.playOnAwake = false;
	}

	/** 음소거 여부를 변경한다 */
	public void SetIsMute(bool a_bIsMute)
	{
		m_oAudioSource.mute = a_bIsMute;
	}

	/** 볼륨을 변경한다 */
	public void SetVolume(float a_fVolume)
	{
		m_oAudioSource.volume = Mathf.Clamp01(a_fVolume);
	}

	/** 사운드를 재생한다 */
	public void Play(string a_oSoundPath, bool a_bIsLoop, bool a_bIs3DSound = false)
	{
		m_oAudioSource.loop = a_bIsLoop;
		m_oAudioSource.clip = Resources.Load<AudioClip>(a_oSoundPath);
		m_oAudioSource.spatialBlend = a_bIs3DSound ? 1.0f : 0.0f;

		m_oAudioSource.Play();
	}
	#endregion // 함수
}
