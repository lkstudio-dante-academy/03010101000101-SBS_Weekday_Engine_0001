using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 사운드 관리자 */
public class CSoundManager : CSingleton<CSoundManager>
{
	#region 변수
	private CSound m_oBGSound = null;
	private Dictionary<string, List<CSound>> m_oFXSoundDictContainer = new Dictionary<string, List<CSound>>();
	#endregion // 변수

	#region 프로퍼티
	public bool IsMuteBGSnd => m_oBGSound.IsMute;
	public bool IsMuteFXSnds { get; private set; } = false;

	public float BGSndVolume => m_oBGSound.Volume;
	public float FXSndsVolume { get; private set; } = 0.0f;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		m_oBGSound = Factory.CreateCloneGameObj<CSound>("BGSound",
			Resources.Load<GameObject>("Prefabs/Global/G_Prefab_BGM"), this.gameObject, Vector3.zero, Vector3.one, Vector3.zero);
	}

	/** 배경음 음소거 여부를 변경한다 */
	public void SetIsMuteBGSnd(bool a_bIsMute)
	{
		m_oBGSound.SetIsMute(a_bIsMute);
	}

	/** 효과음 음소거 여부를 변경한다 */
	public void SetIsMuteFXSnds(bool a_bIsMute)
	{
		this.IsMuteFXSnds = a_bIsMute;
		this.EnumerateFXSnds((a_oSnd) => a_oSnd.SetIsMute(a_bIsMute));
	}

	/** 배경음 볼륨을 변경한다 */
	public void SetVolumeBGSnd(float a_fVolume)
	{
		m_oBGSound.SetVolume(Mathf.Clamp01(a_fVolume));
	}

	/** 효과음 볼륨을 변경한다 */
	public void SetVolumeFXSnds(float a_fVolume)
	{
		this.FXSndsVolume = Mathf.Clamp01(a_fVolume);
		this.EnumerateFXSnds((a_oSnd) => a_oSnd.SetVolume(this.FXSndsVolume));
	}

	/** 배경음을 재생한다 */
	public void PlayBGSound(string a_oSoundPath, bool a_bIsLoop = true)
	{
		m_oBGSound.Play(a_oSoundPath, a_bIsLoop);
	}

	/** 효과음을 재생한다 */
	public void PlayFXSounds(string a_oSoundPath, bool a_bIsLoop = false)
	{
		var oFXSounds = this.FindPlayableFXSounds(a_oSoundPath);
		oFXSounds?.Play(a_oSoundPath, a_bIsLoop);

		this.SetIsMuteFXSnds(this.IsMuteFXSnds);
		this.SetVolumeFXSnds(this.FXSndsVolume);
	}

	/** 재생 가능한 효과음을 탐색한다 */
	private CSound FindPlayableFXSounds(string a_oSoundPath)
	{
		// 사운드 리스트가 없을 경우
		if(!m_oFXSoundDictContainer.TryGetValue(a_oSoundPath, out List<CSound> oSoundList))
		{
			oSoundList = new List<CSound>();
		}

		m_oFXSoundDictContainer.TryAdd(a_oSoundPath, oSoundList);

		// 새로운 사운드 생성이 불가능 할 경우
		if(oSoundList.Count >= 10)
		{
			for(int i = 0; i < oSoundList.Count; ++i)
			{
				// 재생 중이 아닐 경우
				if(!oSoundList[i].IsPlaying)
				{
					return oSoundList[i];
				}
			}

			return null;
		}

		var oFXSounds = Factory.CreateCloneGameObj<CSound>("FXSounds",
			Resources.Load<GameObject>("Prefabs/Global/G_Prefab_SFX"), this.gameObject, Vector3.zero, Vector3.one, Vector3.zero);

		oSoundList.Add(oFXSounds);
		return oFXSounds;
	}

	/** 효과음을 탐색한다 */
	private void EnumerateFXSnds(System.Action<CSound> a_oCallback)
	{
		foreach(var stKeyVal in m_oFXSoundDictContainer)
		{
			for(int i = 0; i < stKeyVal.Value.Count; ++i)
			{
				a_oCallback?.Invoke(stKeyVal.Value[i]);
			}
		}
	}
	#endregion // 함수
}
