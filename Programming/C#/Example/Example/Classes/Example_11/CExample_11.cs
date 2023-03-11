//#define E11_DELEGATE_01
//#define E11_DELEGATE_02
#define E11_DELEGATE_03

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 델리게이트란?
 * - 메서드를 간접적으로 호출해 줄 수 있는 기능을 의미한다. (즉, 델리게이트를 활용하면 메서드를 데이터처럼 취급하는 것이
 * 가능하다.)
 * 
 * 따라서, 델리게이트를 통해서 특정 메서드를 다른 메서드의 입력으로 전달하거나 특정 메서드가 반환 값으로 다른 메서드를 반환
 * 하는 것이 가능하다는 것을 알 수 있다. (즉, C/C++ 의 함수 포인터와 유사한 개념이다.)
 * 
 * C# 델리게이트 선언 방법
 * - delegate + 반환형 + 델리게이트 이름 + 입력 (매개 변수)
 * 
 * Ex)
 * delegate void SomeDelegateA(void);			<- 입력 X, 출력 X
 * delegate void SomeDelegateB(int a_nVal);		<- 입력 O, 출력 X
 * delegate int SomeDelegateC(volid);			<- 입력 X, 출력 O
 * delegate int SomeDelegateD(int a_nVal);		<- 입력 O, 출력 O
 * 
 * 람다란?
 * - 이름이 없는 메서드를 의미하며 일반적인 메서드와 달리 람다는 다른 메서드 구문 내에 구현하는 것이 가능한 특징이 존재한다.
 * 따라서, 람다를 활용하면 재활용성이 떨어지는 일회성 메서드를 선언 및 호출하는 것이 가능하다.
 * 
 * 또한, 람다는 특정 메서드 내부에서 구현되기 때문에 람다 내부에서는 해당 람다를 구현한 외부 영역에 존재하는 변수에 접근하는
 * 것이 가능하다. (즉, 메서드 입력으로 데이터를 전달하지 않더라도 외부에 존재하는 데이터를 사용하는 것이 가능하다는 것을 알
 * 수 있다.)
 * 
 * C# 람다 유형
 * - 람다식
 * - 람다문
 * 
 * Ex)
 * (int a_nLhs, int a_nRhs) => a_nLhs.CompareTo(a_nRhs);				<- 람다문
 * (int a_nLhs, int a_nRhs) => { return a_nLhs.CompareTo(a_nRhs); }		<- 람다식
 * 
 * 람다 내부 구문이 한 줄로 처리되는 단순한 구문 일 경우에는 { } (영역 연산자) 명시를 생략하는 것이 가능하며, 해당 방식으로
 * 선언 된 람다를 람다문이라고 한다. (또한, 람다문에는 return 키워드도 생략을 할 필요가 있다.)
 */
namespace Example.Classes.Example_11 {
	internal class CExample_11 {
#if E11_DELEGATE_01
		public delegate int COMPARE_METHOD<T>(T a_tLhs, T a_tRhs);

		/** 오름차순으로 비교한다 */
		private static int CompareByAscending<T>(T a_tLhs, T a_tRhs) where T : IComparable {
			return a_tLhs.CompareTo(a_tRhs);
		}

		/** 내림차순으로 비교한다 */
		private static int CompareByDescending<T>(T a_tLhs, T a_tRhs) where T : IComparable {
			return a_tRhs.CompareTo(a_tLhs);
		}

		/** 값을 정렬한다 */
		private static void SortVals<T>(List<T> a_oValList, COMPARE_METHOD<T> a_oCompare) where T : IComparable {
			for(int i = 0; i < a_oValList.Count - 1; ++i) {
				int nCompareIdx = i;

				for(int j = i + 1; j < a_oValList.Count; ++j) {
					nCompareIdx = (a_oCompare(a_oValList[nCompareIdx], a_oValList[j]) < 0) ? nCompareIdx : j;
				}

				T tTemp = a_oValList[i];
				a_oValList[i] = a_oValList[nCompareIdx];
				a_oValList[nCompareIdx] = tTemp;
			}
		}
#elif E11_DELEGATE_02
		/*
		 * C# 에서 특정 메서드 유형을 데이터처럼 사용하기 위해서는 반드시 해당 메서드 유형의 델리게이트를 선언 할 필요가
		 * 있다.
		 * 
		 * 따라서, C# 은 불필요한 델리게이트 선언을 방지하기 위해서 Action / Func 델리게이트를 지원한다.
		 * 
		 * Action 델리게이트는 반환 값이 없는 메서드를 처리하기 위한 델리게이트이며 Func 델리게이트는 Action 델리게이트와
		 * 달리 반환 값이 존재하는 메서드를 처리 할 수 있다는 차이점이 존재한다.
		 * 
		 * Ex)
		 * Action<int> oAction;		<- 정수 데이터 1 개 입력, 출력 X
		 * Func<int, int> oFunc;	<- 정수 데이터 1 개 입력, 정수 데이터 출력
		 */
		public delegate void EVENT_METHOD(string a_oEvent);

		/** 이벤트를 처리한다 */
		private static void HandleEvent01(string a_oEvent) {
			Console.WriteLine("HandleEvent01 호출!");
		}

		/** 이벤트를 처리한다 */
		private static void HandleEvent02(string a_oEvent) {
			Console.WriteLine("HandleEvent02 호출!");
		}

		/** 이벤트를 처리한다 */
		private static void HandleEvent03(string a_oEvent) {
			Console.WriteLine("HandleEvent03 호출!");
		}
#elif E11_DELEGATE_03
		/** 값을 정렬한다 */
		private static void SortVals<T>(List<T> a_oValList, Func<T, T, int> a_oCompare) where T : IComparable {
			for(int i = 0; i < a_oValList.Count - 1; ++i) {
				int nCompareIdx = i;

				for(int j = i + 1; j < a_oValList.Count; ++j) {
					nCompareIdx = (a_oCompare(a_oValList[nCompareIdx], a_oValList[j]) < 0) ? nCompareIdx : j;
				}

				T tTemp = a_oValList[i];
				a_oValList[i] = a_oValList[nCompareIdx];
				a_oValList[nCompareIdx] = tTemp;
			}
		}
#endif // E11_DELEGATE_01

		/** 초기화 */
		public static void Start(string[] args) {
#if E11_DELEGATE_01
			var oRandom = new Random();
			var oValList = new List<int>();

			for(int i = 0; i < 10; ++i) {
				oValList.Add(oRandom.Next(0, 100));
			}

			Console.WriteLine("=====> 정렬 전 <=====");

			for(int i = 0; i < oValList.Count; ++i) {
				Console.Write("{0}, ", oValList[i]);
			}

			SortVals(oValList, CompareByAscending);
			Console.WriteLine("\n\n=====> 정렬 후 - 오름차순 <=====");

			for(int i = 0; i < oValList.Count; ++i) {
				Console.Write("{0}, ", oValList[i]);
			}

			SortVals(oValList, CompareByDescending);
			Console.WriteLine("\n\n=====> 정렬 후 - 내림차순 <=====");

			for(int i = 0; i < oValList.Count; ++i) {
				Console.Write("{0}, ", oValList[i]);
			}
#elif E11_DELEGATE_02
			/*
			 * 델리게이트 체인이란?
			 * - 델리게이트 변수에 1 개 이상의 메서드를 지정 할 수 있는 기능을 의미한다. (즉, 델리게이트 체인을 활용하면
			 * 특정 이벤트가 발생했을 때 해당 이벤트를 처리 할 여러 메서드를 하나의 변수로 관리하는 것이 가능하다는 것을
			 * 알 수 있다.)
			 * 
			 * 따라서, 델리게이트 변수에 특정 메서드를 추가하거나 제거하는 것이 가능하며 해당 변수에 특정 메서드를 할당했을
			 * 경우에는 이전에 저장 된 체인 정보가 제거된다는 특징이 존재한다.
			 * 
			 * (즉, += / -= 연산자를 사용해서 메서드를 추가하거나 제거하는 것이 가능하며, = 연산자를 사용해서 메서드를 할당
			 * 하는 것이 가능하다.)
			 */
			EVENT_METHOD oEventHandlers = HandleEvent01;
			oEventHandlers += HandleEvent02;
			oEventHandlers += HandleEvent03;

			Console.WriteLine("=====> 델리게이트 체인 호출 결과 - 1 <=====");
			oEventHandlers("Some Event");

			oEventHandlers -= HandleEvent02;

			Console.WriteLine("\n=====> 델리게이트 체인 호출 결과 - 2 <=====");
			oEventHandlers("Some Event");

			oEventHandlers = HandleEvent03;

			Console.WriteLine("\n=====> 델리게이트 체인 호출 결과 - 3 <=====");
			oEventHandlers("Some Event");
#elif E11_DELEGATE_03
			var oRandom = new Random();
			var oValList = new List<int>();

			Action oPrintVals = () => {
				/*
				 * 람다는 특정 메서드 내부에서 선언되기 때문에 선언 된 람다 내부에서 외부에 존재하는 지역 변수에 접근 할
				 * 수 있는 특징이 존재한다. (즉, 람다를 호출 할 때 전달되는 입력 값을 최소화 시키는 것이 가능하다.)
				 */
				for(int i = 0; i < oValList.Count; ++i) {
					Console.Write("{0}, ", oValList[i]);
				}

				Console.WriteLine();
			};

			for(int i = 0; i < 10; ++i) {
				oValList.Add(oRandom.Next(0, 100));
			}

			Console.WriteLine("=====> 정렬 전 <=====");
			oPrintVals();

			SortVals(oValList, (int a_nLhs, int a_nRhs) => {
				return a_nLhs.CompareTo(a_nRhs);
			});

			Console.WriteLine("\n=====> 정렬 후 - 오름차순 <=====");
			oPrintVals();
			
			/*
			 * 무명 메서드란?
			 * - 람다가 지원 되기 이전 버전에서 사용되던 이름 없는 메서드를 의미하며, 해당 메서드를 활용하면 람다와 마찬가지로
			 * 재활용성이 떨어지는 일회성 메서드를 구현하는 것이 가능하다.
			 * 
			 * 단, 지금은 대부분 C# 컴파일러가 람다를 지원하기 때문에 과거 버전과의 호환성 때문에 존재하는 기능이며 현재는 잘
			 * 사용되지 않는 기능이다.
			 */
			SortVals(oValList, delegate (int a_nLhs, int a_nRhs) {
				return a_nRhs.CompareTo(a_nLhs);
			});

			Console.WriteLine("\n=====> 정렬 후 - 내림차순 <=====");
			oPrintVals();
#endif // E11_DELEGATE_01
		}
	}
}
