//#define E03_DELEGATE_01
//#define E03_DELEGATE_02
//#define E03_DELEGATE_03
#define E03_DELEGATE_04

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * partial 키워드란?
 * - 특정 클래스 또는 구조체를 여러 파일에 나누어서 구현 할 수 있게 해주는 키워드를 의미한다.
 * (즉, 해당 키워드를 활용하면 복잡한 명령문으로 이루어진 큰 클래스를 특정 분류에 따라 여러 파일에
 * 나누어 작성함으로서 코드에 대한 유지보수성을 증가시킬 수 있다는 것을 알 수 있다.)
 * 
 * 델리게이트란?
 * - 메서드를 데이터처럼 특정 메서드에 입력으로 전달하거나 출력으로 메서드를 반환 할 수 있게 해주는 
 * 기능을 의미한다. (즉, 델리게이트를 활용하면 특정 발생되는 이벤트에 따라 객체의 상태를 처리하는
 * 콜백 구조로 프로그램을 설계하는 것이 가능하다.)
 * 
 * 또한, C# 은 델리게이트를 활용해서 람다 (Lambda) 또는 무명 메서드를 구현하는 것이 가능하다.
 * 
 * 람다 및 무명 메서드란?
 * - 일반적인 메서드와 달리 이름이 존재하지 않는 메서드를 의미한다. 따라서, 람다 및 무명 메서드를
 * 활용하면 재사용성이 떨어지는 일회성 메서드를 손쉽게 구현하는 것이 가능하다.
 * 
 * 또한, 람다 및 무명 메서드는 다른 메서드 내부에서 구현되는 내장 메서드이기 때문에 해당 메서드가
 * 선언 된 영역에 존재하는 지역 변수에 접근하는 것이 가능하다. (즉, 지역 변수에 접근하기 위해서
 * 별도의 데이터를 전달 할 필요가 없다는 것을 의미한다.)
 * 
 * C# 델리게이트 선언 방법
 * - delegate + 반환 형 + 델리게이트 이름 + 매개 변수
 * 
 * Ex)
 * delegate void SomeDelegate(int, int)
 * 
 * 위의 경우 정수 2 개를 입력으로 받고 출력은 존재하지 않는 메서드에 대한 델리게이트를 선언 한
 * 것이다.
 * 
 * C# 람다 구현 방법
 * - 매개 변수 + 람다 몸체
 * 
 * Ex)
 * (int a_nLhs, int a_nRhs) => a_nLhs + a_nRhs					<- 람다식 (식 형식)
 * (int a_nLhs, int a_nRhs) => { return a_nLhs + a_nRhs; }		<- 람다문 (문 형식)
 * 
 * C# 람다는 식 형식과 문 형식 의 형태를 제공한다. 
 * 
 * 식 형식으로 주로 한줄로 처리되는 간단한 람다 메서드를 구현 할 때 활용되며, 문 형식은 여러
 * 라인을 지니는 복잡한 명령문을 지니는 람다 메서드를 구현 할 때 활용된다.
 * 
 * 또한, C# 람다는 입력으로 전달되는 자료형의 매개 변수를 생략하는 것이 가능하다.
 * 따라서, (a_nLhs, a_nRhs) 와 같은 매개 변수의 이름만 명시하는 것이 가능하다는 것을 알 수 있다.
 * 
 * C# 무명 메서드 구현 방법
 * - delegate + 매개 변수 + 메서드 몸체
 * 
 * Ex)
 * delegate (int a_nLhs, int a_nRhs) {
 *		// Do Something
 * }
 * 
 * 무명 메서드는 C# 이 람다를 지원하기 이전에 사용하던 일회성 메서드이기 때문에 현재는 잘 활용되지
 * 않는다. (즉, 해당 메서드는 C# 과거 버전과의 호환성을 위해서 존재 할 뿐 현재는 대부분 람다를
 * 사용하는 것이 일반적이다.)
 * 
 * C# 이 지원하는 델리게이트 종류
 * - Action		<- 반환 값이 없는 메서드에 대한 델리게이트
 * - Func		<- 반환 값이 존재하는 메서드에 대한 델리게이트
 * 
 * Ex)
 * Action<int, float>		<- 정수 1 개, 실수 1 개를 입력으로 받는 메서드에 대한 델리게이트
 * Func<int, float>			<- 정수 1 개를 입력으로 받고 실수를 출력하는 메서드에 대한 델리게이트
 */
/** 확장 클래스 */
public static partial class CExtension
{
	/** 값을 오름차순 정렬한다 */
	public static void ExSort<T>(this List<T> a_oSender,
		Func<T, T, int> a_oCompare)
	{
		for(int i = 1; i < a_oSender.Count; ++i)
		{
			int j = 0;
			T tCompareVal = a_oSender[i];

			for(j = i - 1; j >= 0 && a_oCompare(a_oSender[j], tCompareVal) > 0; --j)
			{
				a_oSender[j + 1] = a_oSender[j];
			}

			a_oSender[j + 1] = tCompareVal;
		}
	}
}

namespace Example._03010101000101_S_W_Engine_0001.E01.Example.Classes.Runtime.Example_03
{
	/** Example 3 */
	class CE01Example_03
	{
#if E03_DELEGATE_01
		/** 오름차순으로 비교한다 */
		public static int CompareByAscending<T>(T a_nLhs, T a_nRhs) where T : IComparable
		{
			return a_nLhs.CompareTo(a_nRhs);
		}

		/** 내림차순으로 비교한다 */
		public static int CompareByDescending<T>(T a_nLhs, T a_nRhs) where T : IComparable
		{
			return a_nRhs.CompareTo(a_nLhs);
		}
#elif E03_DELEGATE_02
		/*
		 * PS)
		 * C# 과거 버전 사용 시 주의 사항
		 * - 제네릭이 아닌 델리게이트 일반 메서드를 제어하는 것이 가능하며 제네릭 델리게이트는
		 * 제네릭 메서드를 제어 할 수 있다. (즉, 제네릭과 제네렉이 아닌 델리게이트를 구분해서
		 * 메서드를 제어해야 한다는 것을 알 수 있다.)
		 */
		/** 비교 델리게이트 */
		public delegate int Compare(int a_nLhs, int a_nRhs);

		/** 계산 델리게이트 */
		public delegate decimal Calculator(int a_nLhs, int a_nRhs);

		/** 값을 비교한다 */
		public static int CompareInt(int a_nLhs, int a_nRhs)
		{
			return a_nLhs - a_nRhs;
		}

		/** 덧셈 결과를 반환한다 */
		public static decimal GetSumVal(int a_nLhs, int a_nRhs)
		{
			return a_nLhs + a_nRhs;
		}

		/** 뺄셈 결과를 반환한다 */
		public static decimal GetSubVal(int a_nLhs, int a_nRhs)
		{
			return a_nLhs - a_nRhs;
		}

		/** 곱셈 결과를 반환한다 */
		public static decimal GetMultiplyVal(int a_nLhs, int a_nRhs)
		{
			return a_nLhs * a_nRhs;
		}

		/** 나눗셈 결과를 반환한다 */
		public static decimal GetDivideVal(int a_nLhs, int a_nRhs)
		{
			return a_nLhs / (decimal)a_nRhs;
		}

		/** 계산 메서드를 반환한다 */
		public static Calculator GetCalc(char a_chOperator)
		{
			switch(a_chOperator)
			{
				case '+':
					return GetSumVal;
				case '-':
					return GetSubVal;
				case '*':
					return GetMultiplyVal;
				case '/':
					return GetDivideVal;
			}

			return null;
		}
#elif E03_DELEGATE_03
		/*
		 * 일반적인 메서드는 람다와 달리 다른 지역에 존재하는 지역 변수에 접근하는 것이 불가능하다.
		 * 따라서, 특정 메서드가 동작하기 위해 필요한 데이터가 있다면 해당 데이터를 일반적으로
		 * 매개 변수를 통해서 전달해주는 것이 기본적인 구조이다.
		 */
		/** 값을 비교한다 */
		//public static bool IsEquals(int a_nVal) {
		//	return a_nVal == nVal;
		//}

		/** 람다를 반환한다 */
		public static Action GetAction(int a_nVal)
		{
			/*
			 * 람다 메서드는 해당 메서드가 구현되어있는 지역 변수에 접근하는 것이 가능하며
			 * 만약 해당 지역이 더이상 유효하지 않더라도 람다 메서드 내부에서는 여전히 필요에 따라
			 * 해당 지역에 존재했던 지역 변수를 사용하는 것이 가능하다.
			 * 
			 * (즉, 개념적으로 람다 메서드가 외부에 존재하는 지역 변수의 사본을 가지고 있는 개념과
			 * 비슷한 의미이다.)
			 * 
			 * 따라서, 특정 고수준 언어에서는 람다를 클로저라고도 부른다. (즉, 클로저라는 단어는 외부에서는
			 * 닫혀 있는 영역이지만 내부에서 여전히 열려 있는 영역이라는 의미를 내포하고 있다.)
			 */
			return () =>
			{
				Console.WriteLine("람다 메서드 호출 : {0}", a_nVal);
			};
		}
#elif E03_DELEGATE_04
		/** 출력 델리게이트 */
		public delegate int Printer();
#endif // E03_DELEGATE_01

		/** 초기화 */
		public static void Start(string[] args)
		{
#if E03_DELEGATE_01
			var oRandom = new Random();

			var oValList01 = new List<int>();
			var oValList02 = new List<float>();

			for(int i = 0; i < 5; ++i)
			{
				oValList01.Add(oRandom.Next(1, 100));
				oValList02.Add((float)(oRandom.NextDouble() * 100.0));
			}

			Console.WriteLine("=====> 정렬 전 <=====");

			for(int i = 0; i < oValList01.Count; ++i)
			{
				Console.Write("{0}, ", oValList01[i]);
			}

			Console.WriteLine();

			for(int i = 0; i < oValList02.Count; ++i)
			{
				Console.Write("{0:0.00}, ", oValList02[i]);
			}

			oValList01.ExSort(CompareByDescending);
			oValList02.ExSort(CompareByDescending);

			Console.WriteLine("\n\n=====> 정렬 후 <=====");

			for(int i = 0; i < oValList01.Count; ++i)
			{
				Console.Write("{0}, ", oValList01[i]);
			}

			Console.WriteLine();

			for(int i = 0; i < oValList02.Count; ++i)
			{
				Console.Write("{0:0.00}, ", oValList02[i]);
			}

			Console.WriteLine();
#elif E03_DELEGATE_02
			int nLhs = 0;
			int nRhs = 0;

			Console.Write("정수 (2 개) 입력 : ");
			var oTokens = Console.ReadLine().Split();

			nLhs = int.Parse(oTokens[0]);
			nRhs = int.Parse(oTokens[1]);

			Compare oCompare = CompareInt;

			Console.WriteLine("\n=====> 비교 결과 <=====");
			Console.WriteLine("{0} Compare {1} = {2}", nLhs, nRhs, oCompare(nLhs, nRhs));

			Console.Write("\n수식 입력 (+, -, *, /) : ");
			oTokens = Console.ReadLine().Split();

			nLhs = int.Parse(oTokens[0]);
			nRhs = int.Parse(oTokens[2]);

			char chOperator = char.Parse(oTokens[1]);
			Calculator oCalc = GetCalc(chOperator);

			// 수식이 올바를 경우
			if(oCalc != null)
			{
				decimal dmResult = oCalc(nLhs, nRhs);
				Console.WriteLine("{0} {1} {2} = {3}", nLhs, chOperator, nRhs, dmResult);
			}
			else
			{
				Console.WriteLine("잘못된 수식을 입력했습니다.");
			}
#elif E03_DELEGATE_03
			var oRandom = new Random();
			var oValList = new List<int>();

			for(int i = 0; i < 10; ++i)
			{
				oValList.Add(oRandom.Next(1, 100));
			}

			Console.WriteLine("=====> 리스트 요소 <=====");

			for(int i = 0; i < oValList.Count; ++i)
			{
				Console.Write("{0}, ", oValList[i]);
			}

			Console.Write("\n\n정수 입력 : ");
			int.TryParse(Console.ReadLine(), out int nVal);

			/*
			 * 람다 메서드는 해당 메서드가 구현 된 영역에 존재하는 지역 변수를 사용하는 것이
			 * 가능하다. (즉, 람다 메서드 또한 특정 메서드의 일부로 인식한다는 것을 알 수 있다.)
			 */
			int nIdx = oValList.FindIndex((int a_nVal) =>
			{
				return a_nVal == nVal;
			});

			Action oAction01 = () =>
			{
				Console.WriteLine("람다 메서드 호출!");
			};

			Action oAction02 = GetAction(nVal);

			/*
			int nIdx = oValList.FindIndex(delegate (int a_nVal) {
				return a_nVal == nVal;
			});
			*/

			Console.WriteLine("결과 : {0}\n", (nIdx >= 0) ? "탐색 성공" : "탐색 실패");

			oAction01();
			oAction02();
#elif E03_DELEGATE_04
			/*
			 * 델리게이트 체인을 활용하면 특정 델리게이트 변수가 여러 메서드를 제어하는 것이 가능하다.
			 * 
			 * 이때, 해당 델리게이트 변수를 통해 메서드를 호출하면 변수가 지니고 있던 메서드가 추가
			 * 된 순서에 따라 순차적으로 호출 된다는 특징이 존재한다.
			 * 
			 * 따라서, 해당 특징을 활용하면 프로그램이 수행 중에 특정 작업을 여러 단계에 걸쳐서 수행 할
			 * 필요가 있을 경우 델리게이트 체인을 통해 좀 더 수월하게 각 메서드를 호출해주는 것이 가능하다.
			 * 
			 * 또한, 델리게이트 변수에 = (할당 연산자) 를 사용 할 경우 기존에 지니고 있던 체인 정보는 리셋이
			 * 된다는 특징이 존재하기 때문에 체인 정보를 유지하기 위해서는 = (할당 연산자) 가 아닌 +, - 
			 * 연산자를 활용해서 특정 메서드를 추가하거나 제거해야한다.
			 * 
			 * 만약, 델리게이트 체인에서 관리되고 있는 메서드가 반환 값이 존재 할 경우에는 가장 마지막에 추가 된
			 * 메서드의 반환 값만 유효하며 나머지 메서드의 반환 값은 무시가 된다는 특징이 존재한다.
			 * 
			 * 따라서, 해당 결과를 유도한 경우가 아니라면 델리게이트 체인에는 가능하면 반환 값이 존재하지 않는
			 * 메서드를 추가하거나 제거하는 것을 추천한다.
			 */
			Printer oPrinter = () =>
			{
				Console.WriteLine("첫번째 메서드 호출!");
				return 1;
			};

			oPrinter += () =>
			{
				Console.WriteLine("두번째 메서드 호출!");
				return 2;
			};

			oPrinter += () =>
			{
				Console.WriteLine("세번째 메서드 호출!");
				return 3;
			};

			oPrinter = () =>
			{
				Console.WriteLine("네번째 메서드 호출!");
				return 4;
			};

			Console.WriteLine("=====> 델리게이트 호출 결과 <=====");
			Console.WriteLine("{0}", oPrinter());
#endif // E03_DELEGATE_01
		}
	}
}
