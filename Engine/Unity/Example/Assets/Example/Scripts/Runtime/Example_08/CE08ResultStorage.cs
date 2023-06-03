using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 결과 저장소 */
public class CE08ResultStorage : CSingleton<CE08ResultStorage> {
	#region 프로퍼티
	public int Score { get; set; } = 0;
	#endregion // 프로퍼티
}
