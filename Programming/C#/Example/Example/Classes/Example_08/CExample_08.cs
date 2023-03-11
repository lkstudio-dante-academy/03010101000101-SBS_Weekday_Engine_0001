//#define E08_METHOD_01
//#define E08_METHOD_02
//#define E08_METHOD_03
//#define E08_METHOD_04
//#define E08_METHOD_05
#define E08_METHOD_06

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 메서드 (함수) 란?
 * - 프로그램 명령문의 특정 부분을 그룹화시켜서 필요 할 때 재활용 할 수 있는 기능을 의미한다.
 * (즉, 메서드를 활용하면 중복되는 명령문을 최소화 시키는 것이 가능하다는 것을 알 수 있다.)
 * 
 * C# 메서드 선언 방법
 * - 반환형 + 메서드 이름 + 매개 변수
 * 
 * C# 메서드 구현 방법
 * - 반환형 + 메서드 이름 + 매개 변수 + 메서드 몸체
 * 
 * Ex)
 * // 선언
 * abstract void SomeMethod01(int a_nVal);
 * 
 * // 구현
 * void SomeMethod02(int a_nVal) {
 *		// Do Something
 * }
 * 
 * C# 메서드 유형
 * - 입력 O, 출력 O		<- int SomeMethod01(int a_nVal);
 * - 입력 O, 출력 X		<- void SomeMethod02(int a_nVal01, int a_nVal02);
 * - 입력 X, 출력 O		<- int SomeMethod03();
 * - 입력 X, 출력 X		<- void SomeMethod04();
 * 
 * 메서드 입력 vs 출력
 * - 메서드의 입력 데이터의 개수는 필요한만큼 명시하는 것이 가능하지만 출력 데이터는 하나만
 * 존재하는 차이점이 있다. (즉, 특정 메서드가 여러 데이터를 결과로 출력하기 위해서는 컬렉션 등을
 * 활용 할 필요가 있다는 것을 알 수 있다.)
 */
namespace Example.Classes.Example_08 {
	internal class CExample_08 {
#if E08_METHOD_01
		/*
		 * 메서드가 호출 될 때 입력 되는 데이터는 매개 변수로 전달 받는다. (즉, 매개 변수는 메서드 호출 시 전달되는 데이터를
		 * 저장 및 읽어들이기 위한 변수를 의미한다.)
		 */
		/** 값을 출력한다 */
		static void PrintVals(List<int> a_oValList) {
			for(int i = 0; i < a_oValList.Count; ++i) {
				Console.Write("{0}, ", a_oValList[i]);
			}

			Console.WriteLine("\n");
		}

		/** 최소, 최대 값을 반환한다 */
		static (int, int) GetMinMaxVal(List<int> a_oValList) {
			int nMinVal = int.MaxValue;
			int nMaxVal = int.MinValue;

			for(int i = 0; i < a_oValList.Count; ++i) {
				nMinVal = (nMinVal < a_oValList[i]) ? nMinVal : a_oValList[i];
				nMaxVal = (nMaxVal > a_oValList[i]) ? nMaxVal : a_oValList[i];
			}

			return (nMinVal, nMaxVal);
		}
#elif E08_METHOD_02
		/*
		 * ref 키워드는 값 형식 변수를 특정 메서드에 참조 형식으로 전달 할 때 사용하는 것이 가능하다.
		 * (즉, ref 키워드를 활용하면 다른 메서드에 존재하는 값 형식 변수에 값을 제어하는 것이 가능하다.)
		 * 
		 * C# 참조 형식 관련 키워드
		 * - ref
		 * - out
		 * 
		 * ref 키워드 vs out 키워드
		 * - ref 키워드는 단순히 참조만을 전달하는 키워드이기 때문에 초기화 되지 않은 변수를 특정 메서드에 참조 형식으로
		 * 전달하는 것이 불가능하다. (즉, 컴파일 에러가 발생한다.) C# 은 초기화 되지 않은 변수를 사용하는 것이 불가능하기
		 * 때문에 기본적으로 특정 변수를 사용하기 전에는 반드시 초기화를 해주는 것이 관례이다.
		 * 
		 * 이때, out 키워드를 활용하면 초기화 되지 않은 변수의 참조를 전달하는 것이 가능하다. out 키워드는 해당 참조
		 * 변수를 사용하기 전에 반드시 1 번 이상은 특정 데이터를 할당하겠다는 약속을 의미하기 때문에 초기화 하지 않은
		 * 변수에도 해당 키워드를 사용하는 것이 가능하다. (즉, out 참조는 참조 대상의 값을 읽어오기 전에 반드시 값을
		 * 먼저 할당 할 필요가 있다는 것을 의미하며 만약 특정 값을 할당하지 않았을 경우 컴파일 에러가 발생한다.)
		 */
		/** 값을 교환한다 */
		static void Swap(ref int a_nLhs, ref int a_nRhs) {
			int nTemp = a_nLhs;
			a_nLhs = a_nRhs;
			a_nRhs = nTemp;
		}
#elif E08_METHOD_03
		/** 값을 읽어들인다 */
		static void ReadVal(out int a_nVal) {
			Console.WriteLine(a_nVal);

			a_nVal = int.Parse(Console.ReadLine());
		}
#elif E08_METHOD_04
		/** 덧셈 결과를 반환한다 */
		static int GetSumVal(int a_nLhs, int a_nRhs) {
			return a_nLhs + a_nRhs;
		}

		/** 뺄셈 결과를 반환한다 */
		static int GetSubVal(int a_nLhs, int a_nRhs) {
			return a_nLhs - a_nRhs;
		}

		/** 곱셈 결과를 반환한다 */
		static int GetMultiplyVal(int a_nLhs, int a_nRhs) {
			return a_nLhs * a_nRhs;
		}

		/** 나눗셈 결과를 반환한다 */
		static float GetDivideVal(int a_nLhs, int a_nRhs) {
			return a_nLhs / (float)a_nRhs;
		}

		/** 수식 결과를 반환한다 */
		static bool TryGetCalcResult(int a_nLhs, int a_nRhs, char a_chOperator, out decimal a_dmOutResult) {
			switch(a_chOperator) {
				case '+': a_dmOutResult = GetSumVal(a_nLhs, a_nRhs); return true;
				case '-': a_dmOutResult = GetSubVal(a_nLhs, a_nRhs); return true;
				case '*': a_dmOutResult = GetMultiplyVal(a_nLhs, a_nRhs); return true;
				case '/': a_dmOutResult = (decimal)GetDivideVal(a_nLhs, a_nRhs); return true;
			}

			a_dmOutResult = decimal.MaxValue;
			return false;
		}
#elif E08_METHOD_05
		/*
		 * 디폴트 매개 변수란?
		 * - 특정 메서드를 호출 할 때 입력 값이 전달 되지 않으면 자동으로 지정 된 값으로 설정 되는 매개 변수를 의미한다.
		 * 따라서, 디폴트 매개 변수를 활용하면 매개 변수의 개수와 입력으로 전달 된 데이터의 개수가 일치하지 않을 수 있다.
		 * 
		 * 단, C# 메서드 호출 규약은 기본적으로 순서에 의해 입력 값이 매개 변수에 전달 되기 때문에 해당 메서드의 디폴트
		 * 매개 변수를 명시했을 경우 반드시 해당 매개 변수의 이후에도 디폴트 매개 변수로 선언 되어야한다.
		 * 
		 * 네임드 매개 변수란?
		 * - 특정 메서드를 호출 할때 입력 값을 전달 받을 매개 변수를 명시적으로 지정 할 수 있는 기능을 의미한다. (즉,
		 * 네임드 매개 변수를 활용하면 매개 변수의 순서와 입력 값의 순서를 일치하지 않아도 된다.)
		 * 
		 * 단, 일반적으로 네임드 매개 변수를 활용 할 경우에는 반드시 전달 할 값을 명시 할 때 네임드 매개 변수는 기본
		 * 전달 값이 이후에 지정해주는 것이 좋다.
		 * 
		 * Ex)
		 * void SomeMethod(int a, int b, int c = 0, int d = 0);
		 * SomeMethod(10, 20, d: 40);
		 * 
		 * 위와 같이 기본 값과 네임드 값을 서로 구분해주는 것이 좋다.
		 */
		/** 지수 값을 반환한다 */
		static decimal GetPowerVal(int a_nVal, int a_nPowerVal = 0) {
			int nVal = 1;

			for(int i = 0; i < a_nPowerVal; ++i) {
				nVal *= a_nVal;
			}

			return nVal;
		}

		/*
		 * 가변 매개 변수란?
		 * - 일반적인 메서드는 입력으로 전달 할 값을 저장 할 매개 변수의 개수가 지정되어있지만 가변 매개 변수를 활용하면
		 * 개수 제한을 해제하는 것이 가능하다. (즉, 원하는 만큼 특정 메서드에 입력 값을 전달하는 것이 가능하다.)
		 * 
		 * 단, C# 은 내부적으로 가변 매개 변수를 배열로 처리하기 때문에 가변 매개 변수를 적절하게 활용하기 위해서는
		 * 가능하면 데이터의 자료형을 일치시켜주는 것이 좋다. (즉, object 자료형으로 선언하면 모든 데이터를 처리하는
		 * 것이 가능하지만 해당 자료형은 내부적으로 박싱/언박싱이 발생 할 수 있는 여지가 있기 때문에 가능하면 
		 * 해당 자료형을 활용하는 것을 추천하지 않는다.)
		 */
		/** 합계를 반환한다 */
		static int GetSumVal(params int[] a_oVals) {
			int nSumVal = 0;

			for(int i = 0; i < a_oVals.Length; ++i) {
				nSumVal += a_oVals[i];
			}

			return nSumVal;
		}
#elif E08_METHOD_06
		/*
		 * 재귀 호출 (함수) 란?
		 * - 특정 메서드가 자기 자신을 호출하는 행위를 재귀 호출이라고 한다. (즉, 재귀 호출을 사용하면 특정 메서드가
		 * 무한히 호출되기 때문에 재귀 호출을 사용 할 경우에는 반드시 재귀 호출을 멈추는 구문을 작성 할 필요가 있다.)
		 */
		/** 팩토리얼 값을 반환한다 */
		static int GetFactorialVal(int a_nVal) {
			if(a_nVal <= 1) {
				return 1;
			}

			return a_nVal * GetFactorialVal(a_nVal - 1);
		}

		/** 합계를 반환한다 */
		static int GetSumVal(int[] a_oVals, int a_nIdx) {
			if(a_nIdx >= a_oVals.Length - 1) {
				return a_oVals[a_nIdx];
			}

			return a_oVals[a_nIdx] + GetSumVal(a_oVals, a_nIdx + 1);
		}
#endif // #if E08_METHOD_01

		/** 초기화 */
		public static void Start(string[] args) {
#if E08_METHOD_01
			var oRandom = new Random();
			List<int> oValList = new List<int>();

			for(int i = 0; i < 10; ++i) {
				oValList.Add(oRandom.Next(0, 100));
			}

			/*
			 * 메서드 호출 방법
			 * - C# 메서드는 () (메서드 호출 연산자) 를 활용해서 해당 메서드를 호출 (실행) 하는 것이 가능하다.
			 * 이때, 해당 메서드가 입력 데이터가 필요 할 경우 반드시 해당 메서드의 입력 데이터 개수만큼 전달 할 필요가 있다.
			 * (즉, 호출 시 전달되는 입력 데이터 개수와 함수가 필요하는 데이터의 개수가 일치하지 않을 경우 컴파일 에러가
			 * 발생한다.)
			 */
			Console.WriteLine("=====> 리스트 요소 <=====");
			PrintVals(oValList);

			var stResult = GetMinMaxVal(oValList);

			Console.WriteLine("\n최소 : {0}", stResult.Item1);
			Console.WriteLine("최대 : {0}", stResult.Item2);
#elif E08_METHOD_02
			int nLhs = 0;
			int nRhs = 0;

			Console.Write("정수 (2 개) 입력 : ");
			var oTokens = Console.ReadLine().Split();

			nLhs = int.Parse(oTokens[0]);
			nRhs = int.Parse(oTokens[1]);

			Console.WriteLine("=====> 교환 전 <=====");
			Console.WriteLine("{0}, {1}", nLhs, nRhs);

			Swap(ref nLhs, ref nRhs);

			Console.WriteLine("\n=====> 교환 후 <=====");
			Console.WriteLine("{0}, {1}", nLhs, nRhs);
#elif E08_METHOD_03
			int nVal;

			Console.Write("정수 입력 : ");
			ReadVal(out nVal);

			Console.WriteLine("{0}", nVal);
#elif E08_METHOD_04
			Console.Write("수식 입력 : ");
			var oTokens = Console.ReadLine().Split();

			int nLhs = int.Parse(oTokens[0]);
			int nRhs = int.Parse(oTokens[2]);

			/*
			 * 서로 다른 영역 일 경우 동일한 이름의 변수를 선언하는 것이 가능하다. (즉, 특정 영역을 포함하고 있는 상위
			 * 영역에 동일한 이름의 변수가 존재 할 경우 컴파일 에러가 발생한다는 것을 알 수 있다.)
			 * 
			 * out 키워드를 활용하면 변수를 선언함과 동시에 해당 변수의 참조 값을 특정 메서드의 입력으로 전달하는 것이
			 * 가능하다. (즉, out 키워드는 반드시 초기화가 된다는 보장이 있기 때문에 해당 방식이 가능하다는 것을 알 수
			 * 있다.)
			 * 
			 * 따라서, 해당 방식을 활용하면 메서드 호출과 동시에 변수를 선언한느 것이 가능하다.
			 * 
			 * Ex)
			 * Case 1. 변수 선언 후 참조 전달
			 * decimal dmResult = 0;
			 * TryGetCalcResult(nLhs, nRhs, chOperator, out dmResult);
			 * 
			 * Case 2. 변수 선언과 동시에 참조 전달
			 * TryGetCalcResult(nLhs, nRhs, chOperator, out decimal dmResult);
			 */
			char chOperator = char.Parse(oTokens[1]);
			bool bIsValid = TryGetCalcResult(nLhs, nRhs, chOperator, out decimal dmResult);

			// 수식이 유효하지 않을 경우
			if(!bIsValid) {
				Console.WriteLine("잘못된 수식을 입력했습니다.");
			} else {
				Console.WriteLine("결과 : {0} {1} {2} = {3}", nLhs, chOperator, nRhs, dmResult);
			}
#elif E08_METHOD_05
			Console.Write("정수 (2 개) 입력 : ");
			var oTokens = Console.ReadLine().Split();

			int nVal = int.Parse(oTokens[0]);
			int nPowerVal = int.Parse(oTokens[1]);

			Console.WriteLine("\n결과 : {0}^0 = {1}", nVal, GetPowerVal(nVal));
			Console.WriteLine("결과 : {0}^{1} = {2}", nVal, nPowerVal, GetPowerVal(nVal, nPowerVal));
			Console.WriteLine("결과 : {0}^{1} = {2}", nVal, nPowerVal, GetPowerVal(a_nPowerVal: nPowerVal, a_nVal: nVal));

			Console.WriteLine("\n=====> 합계 <=====");
			Console.WriteLine("결과 : {0}", GetSumVal(1, 2, 3));
			Console.WriteLine("결과 : {0}", GetSumVal(1, 2, 3, 4, 5, 6));
			Console.WriteLine("결과 : {0}", GetSumVal(1, 2, 3, 4, 5, 6, 7, 8, 9));
#elif E08_METHOD_06
			Console.Write("정수 입력 : ");
			int nVal = int.Parse(Console.ReadLine());

			Console.WriteLine("{0}! = {1}", nVal, GetFactorialVal(nVal));

			var oVals = new int[10];
			var oRandom = new Random();

			for(int i = 0; i < oVals.Length; ++i) {
				oVals[i] = oRandom.Next(0, 99);
			}

			Console.WriteLine("\n=====> 배열 요소 <=====");

			for(int i = 0; i < oVals.Length; ++i) {
				Console.Write("{0}, ", oVals[i]);
			}

			Console.WriteLine("\n\n합계 : {0}", GetSumVal(oVals, 0));
#endif // #if E08_METHOD_01
		}
	}
}
