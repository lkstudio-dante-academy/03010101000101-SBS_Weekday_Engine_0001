using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * using 키워드를 활용하면 특정 네임 스페이스에 포함 되어있는 클래스 등에 새로운 이름을 부여하는
 * 것이 가능하다. (즉, using 키워드는 별칭을 작성하는데 활용 된다는 것을 알 수 있다.)
 */
using StrDict = System.Collections.Generic.Dictionary<string, string>;

/** 문자열 테이블 */
public class CStrTable : CSingleton<CStrTable>
{
	#region 변수
	private Dictionary<SystemLanguage, StrDict> m_oStrDictContainer = new Dictionary<SystemLanguage, StrDict>();
	#endregion // 변수

	#region 함수
	/** 문자열을 반환한다 */
	public string GetStr(string a_oKey)
	{
		return this.GetStr(Application.systemLanguage, a_oKey);
	}

	/** 문자열을 반환한다 */
	public string GetStr(SystemLanguage a_eLanguage, string a_oKey)
	{
		var oStrDict = m_oStrDictContainer.GetValueOrDefault(a_eLanguage);
		return (oStrDict != null) ? oStrDict.GetValueOrDefault(a_oKey, a_oKey) : a_oKey;
	}

	/** 문자열 테이블을 로드한다 */
	public void LoadStrTable(string a_oFilePath)
	{
		/*
		 * Unity 에서 Resources 폴더로부터 텍스트 파일을 읽어들이고 싶다면 해당 에셋의 자료형을
		 * TextAsset 으로 지정하면 된다. (즉, TextAsset 문자열 기반으로 된 에셋을 제어 할 수 있는
		 * 자료형이라는 것을 알 수 있다.
		 */
		var oTextAsset = Resources.Load<TextAsset>(a_oFilePath);

		/*
		 * Split 메서드는 특정 문자를 기준으로 문자열을 분리시키는 역할을 수행한다. 만약, 해당 메서드에
		 * 아무런 입력 데이터도 전달하지 않으면 공백을 기준으로 문자열을 분리시키는 특징이 존재한다.
		 */
		var oStrLines = oTextAsset.text.Split("\n");
		var oHeader = oStrLines[0].Split(",");

		for(int i = 1; i < oStrLines.Length; ++i)
		{
			var oStrs = oStrLines[i].Split(",");

			// 데이터가 유효 할 경우
			if(oStrs.Length >= 2)
			{
				string oKey = oStrs[0];

				for(int j = 1; j < oStrs.Length; ++j)
				{
					/* Case 1
					SystemLanguage eLanguage = SystemLanguage.Unknown;

					switch(oHeader[j]) {
						case "Korean": eLanguage = SystemLanguage.Korean; break;
						case "English": eLanguage = SystemLanguage.English; break;
					}

					if(eLanguage != SystemLanguage.Unknown) {
						var oStrDict = m_oStrDictContainer.GetValueOrDefault(eLanguage)
							?? new StrDict();

						oStrDict.Add(oKey, oStrs[j]);

						if(!m_oStrDictContainer.ContainsKey(eLanguage)) {
							m_oStrDictContainer.Add(eLanguage, oStrDict);
						}
					}
					*/

					// 시스템 언어가 유효 할 경우
					if(System.Enum.TryParse(oHeader[j], out SystemLanguage eLanguage))
					{
						var oStrDict = m_oStrDictContainer.GetValueOrDefault(eLanguage)
							?? new StrDict();

						oStrDict.TryAdd(oKey, oStrs[j]);
						m_oStrDictContainer.TryAdd(eLanguage, oStrDict);
					}
				}
			}
		}
	}
	#endregion // 함수
}
