using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

/** 스탯 종류 */
public enum EE01StatKinds_15
{
	NONE = -1,
	HP,
	ATK,
	MAX_HP,
	[HideInInspector] MAX_VAL
}

/** 스탯 처리자 */
public class CE01StatHandler_15 : CComponent
{
	#region 변수
	private Dictionary<EE01StatKinds_15, int> m_oStatValDict = new Dictionary<EE01StatKinds_15, int>();
	#endregion // 변수

	#region 함수
	/** 스탯을 증가시킨다 */
	public void IncrStatVal(EE01StatKinds_15 a_eKinds,
		int a_nVal, int a_nMinVal = 0, int a_nMaxVal = int.MaxValue)
	{

		int nVal = this.GetStat(a_eKinds, a_nVal);
		this.SetStat(a_eKinds, nVal + a_nVal, a_nMinVal, a_nMaxVal);
	}
	#endregion // 함수

	#region 접근 함수
	/** 사망 여부를 검사한다 */
	public bool IsDie()
	{
		return this.GetStat(EE01StatKinds_15.HP) <= 0;
	}

	/** 스탯을 반환한다 */
	public int GetStat(EE01StatKinds_15 a_eKinds, int a_nDefVal = 0)
	{
		return m_oStatValDict.GetValueOrDefault(a_eKinds, a_nDefVal);
	}

	/** 스탯 비율을 반환한다 */
	public float GetStatValPercent(EE01StatKinds_15 a_eLhsKinds,
		EE01StatKinds_15 a_eRhsKinds)
	{
		int nLhs = this.GetStat(a_eLhsKinds);
		return nLhs / (float)this.GetStat(a_eRhsKinds, 1);
	}

	/** 스탯을 변경한다 */
	public void SetStat(EE01StatKinds_15 a_eKinds,
		int a_nVal, int a_nMinVal = 0, int a_nMaxVal = int.MaxValue)
	{

		int nVal = Mathf.Clamp(a_nVal, a_nMinVal, a_nMaxVal);

		// 값이 존재 할 경우
		if(m_oStatValDict.ContainsKey(a_eKinds))
		{
			m_oStatValDict[a_eKinds] = nVal;
		}
		else
		{
			m_oStatValDict.Add(a_eKinds, nVal);
		}
	}
	#endregion // 접근 함수
}
