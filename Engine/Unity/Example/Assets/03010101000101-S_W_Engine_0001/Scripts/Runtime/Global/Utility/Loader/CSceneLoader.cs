using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** 씬 로더 */
public class CSceneLoader : CSingleton<CSceneLoader>
{
	#region 함수
	/** 씬을 로드한다 */
	public void LoadScene(string a_oName, LoadSceneMode a_eSceneMode = LoadSceneMode.Single)
	{
		SceneManager.LoadScene(a_oName, a_eSceneMode);
	}

	/** 씬을 비동기 로드한다 */
	public void LoadSceneAsync(string a_oName,
		System.Action<CSceneLoader, float, bool> a_oCallback, LoadSceneMode a_eSceneMode = LoadSceneMode.Single)
	{
		/*
		 * StartCoroutine 메서드는 특정 메서드를 코루틴 방식으로 동작시키는 역할을 수행한다. (즉, 해당 메서드를 활용하면 특정 연산을
		 * 수행하기 위해서 상호 협력 관계로 동작하는 메서드 구문을 만들어내는 것이 가능하다.)
		 * 
		 * Unity 는 멀티 쓰레드 환경에 안전하지 않기 때문에 서브 쓰레드를 이용하면 비동기 처리를 추천하지 않는다. (즉, 서브 쓰레드에서
		 * Unity 와 관련 된 구문을 작성 할 경우 내부적으로 예외가 발생한다.)
		 * 
		 * 따라서, Unity 는 비동기 처리 구문을 작성 할 수 있게 코루틴 기능을 지원한다. (즉, Unity 에서 지원하는 비동기 처리 구문들은
		 * 모두 코루틴을 사용한다는 것을 알 수 있다.)
		 * 
		 * 코루틴이란?
		 * - 서브 루틴과 달리 특정 메서드가 호출이 종료 된 후 해당 메서드를 다시 실행했을때 이전 종료 된 위치부터 다시 이어서 명령문을
		 * 처리 할 수 있는 동작 방식을 의미한다. (즉, 일반적인 메서드는 종료 된 후 다시 실행 되었을 때 처음부터 실행되는 특징이 존재하며
		 * 해당 방식처럼 동작하는 구문을 서브 루틴이라고 한다.)
		 * 
		 * Unity 에서 코루틴 작성하는 방법
		 * - StartCoroutine 메서드 사용
		 * - 코루틴에서는 반드시 IEnumerator 인터페이스를 반환
		 * - 반드시 한번 이상 yield return 구문 호출
		 * 
		 * 위의 3 가지 사항 중에 하나라도 만족하지 않을 경우 코루틴이 제대로 동작하지 않는다. (즉, StartCoroutine 메서드 호출을 제외하고는
		 * 문법적으로 반드시 지켜야되는 사항이라는 것을 알 수 있다.)
		 */
		StartCoroutine(this.DOLoadSceneAsync(a_oName, a_oCallback, a_eSceneMode));
	}

	/** 씬을 비동기 로드한다 */
	public IEnumerator DOLoadSceneAsync(string a_oName,
		System.Action<CSceneLoader, float, bool> a_oCallback, LoadSceneMode a_eSceneMode)
	{
		var oAsyncOperation = SceneManager.LoadSceneAsync(a_oName, a_eSceneMode);

		do
		{
			/*
			 * 코루틴 내부에서는 서브 루틴에 활용되는 return 키워드를 사용하는 것이 불가능하기 때문에 코루틴을 종료하기 위해서는 반드시
			 * yield return 키워드를 사용해야한다. (즉, yield return 종료 된 코루틴을 필요에 따라 해당 구문 이후부터 다시 이어서 실행
			 * 하는 것이 가능하다는 것을 알 수 있다.)
			 */
			yield return new WaitForEndOfFrame();
			a_oCallback?.Invoke(this, oAsyncOperation.progress, false);
		} while(!oAsyncOperation.isDone);

		yield return new WaitForEndOfFrame();
		a_oCallback?.Invoke(this, oAsyncOperation.progress, true);
	}
	#endregion // 함수
}
