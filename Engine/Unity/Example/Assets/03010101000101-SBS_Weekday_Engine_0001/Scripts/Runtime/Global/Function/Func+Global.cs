using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/** 전역 함수 */
public static partial class Func
{
	#region 클래스 함수
	/** 데이터를 읽어들인다 */
	public static List<byte> ReadBytes(string a_oFilePath)
	{
		// 파일이 없을 경우
		if(!File.Exists(a_oFilePath))
		{
			return null;
		}

		using(var oStream = File.Open(a_oFilePath,
			FileMode.Open, FileAccess.Read))
		{

			var oBytes = new byte[oStream.Length];
			oStream.Read(oBytes, 0, (int)oStream.Length);

			return oBytes.ToList();
		}
	}

	/** 데이터를 읽어들인다 */
	public static string ReadStr(string a_oFilePath)
	{
		// 파일이 없을 경우
		if(!File.Exists(a_oFilePath))
		{
			return string.Empty;
		}

		var oByteList = Func.ReadBytes(a_oFilePath);
		return System.Text.Encoding.Default.GetString(oByteList.ToArray());
	}

	/** 데이터를 기록한다 */
	public static void WriteBytes(string a_oFilePath, List<byte> a_oByteList)
	{
		string oDirPath = Path.GetDirectoryName(a_oFilePath);

		// 디렉토리가 없을 경우
		if(!Directory.Exists(oDirPath))
		{
			Directory.CreateDirectory(oDirPath);
		}

		using(var oStream = File.Open(a_oFilePath,
			FileMode.Create, FileAccess.Write))
		{

			var oBytes = a_oByteList.ToArray();
			oStream.Write(oBytes, 0, oBytes.Length);
		}
	}

	/** 데이터를 기록한다 */
	public static void WriteStr(string a_oFilePath, string a_oStr)
	{
		var oBytes = System.Text.Encoding.Default.GetBytes(a_oStr);
		Func.WriteBytes(a_oFilePath, oBytes.ToList());
	}
	#endregion // 클래스 함수

	#region 제네릭 클래스 함수
	/** 객체를 읽어들인다 */
	public static T ReadObj<T>(string a_oFilePath)
	{
		// 파일이 없을 경우
		if(!File.Exists(a_oFilePath))
		{
			return default;
		}

		string oJSONStr = Func.ReadStr(a_oFilePath);
		return JsonConvert.DeserializeObject<T>(oJSONStr);
	}

	/** 객체를 기록한다 */
	public static void WriteObj<T>(string a_oFilePath, T a_tObj)
	{
		string oJSONStr = JsonConvert.SerializeObject(a_tObj);
		Func.WriteStr(a_oFilePath, oJSONStr);
	}
	#endregion // 제네릭 클래스 함수
}
