using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/** 몬스터 정보 */
public class CE01MonsterInfo_15
{
	#region 변수
	[JsonProperty("ID")] public int m_nID = 0;
	[JsonProperty("HP")] public int m_nHP = 0;
	[JsonProperty("ATK")] public int m_nATK = 0;
	[JsonProperty("AttackRange")] public int m_nAttackRange = 0;
	[JsonProperty("TrackingRange")] public int m_nTrackingRange = 0;
	#endregion // 변수
}

/** 데이터 테이블 */
public class CE01DataTable_15 : CSingleton<CE01DataTable_15>
{
	#region 프로퍼티
	public Dictionary<int, CE01MonsterInfo_15> MonsterInfoDict = new Dictionary<int, CE01MonsterInfo_15>();
	#endregion // 프로퍼티

	#region 함수
	/** 데이터 테이블을 로드한다 */
	public void LoadDataTable()
	{
		this.LoadDataTable("Example_15/E01Table_MonsterInfo_15");
	}

	/** 데이터 테이블을 로드한다 */
	public void LoadDataTable(string a_oFilePath)
	{
		var oTextAsset = Resources.Load<TextAsset>(a_oFilePath);
		var oDataInfos = JObject.Parse(oTextAsset.text);
		var oMonsterInfos = oDataInfos["Monster"] as JArray;

		for(int i = 0; i < oMonsterInfos.Count; ++i)
		{
			var oJSONStr = oMonsterInfos[i].ToString();
			var oMonsterInfo = JsonConvert.DeserializeObject<CE01MonsterInfo_15>(oJSONStr);

			this.MonsterInfoDict.TryAdd(oMonsterInfo.m_nID, oMonsterInfo);
		}
	}
	#endregion // 함수

	#region 접근 함수
	/** 몬스터 정보를 반환한다 */
	public bool TryGetMonsterInfo(int a_nID,
		out CE01MonsterInfo_15 a_oOutMonsterInfo)
	{
		a_oOutMonsterInfo = this.MonsterInfoDict.GetValueOrDefault(a_nID);
		return a_oOutMonsterInfo != null;
	}
	#endregion // 접근 함수
}
