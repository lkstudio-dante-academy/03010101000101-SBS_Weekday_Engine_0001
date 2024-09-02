//#define E02_GENERIC_01
//#define E02_GENERIC_02
#define E02_GENERIC_03

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 제네릭이란?
 * - 메서드 또는 클래스를 구현 할 때 자료형을 결정하지 않고 구현 할 수 있는 기능을
 * 의미한다.
 * 
 * 따라서, 제네릭을 활용하면 같은 동작을 하지만 자료형이 다르다는 이유만으로 중복적으로
 * 구현되는 메서드 또는 클래스를 최소화 시키는 것이 가능하다.
 * 
 * 즉, 제네릭은 자료형에 상관없이 동일한 동작을 수행하는 메서드 또는 클래스를 구현하는
 * 것이 주된 목적이라는 것을 알 수 있다.
 * 
 * Ex)
 * void GenericMethod<T>(T a_tVal);
 * 
 * class CGenericClass<T> {
 *		// Do Something
 * }
 * 
 * 단, 제네릭 또한 메서드 또는 클래스를 구현하는 문법을 지켜야되기 때문에 기존에 자료형이
 * 명시되는 곳을 무엇인가로 채워넣을 필요가 있다는 것을 알 수 있다.
 * 
 * 이때 활용되는 것이 바로 제네릭 형식 인자이다.
 * 
 * 제네릭 형식 인자는 기존 자료형 자리에 명시 할 수 있으며 해당 형식 인자는 실제 메서드
 * 또는 클래스가 사용 될 때 구체적으로 자료형으로 대체가 된다. (즉, 제네릭 형식 인자는
 * 자료형 자리를 잠시 대체하는 임시 자료형이라는 것을 알 수 있다.)
 * 
 * 제네릭 형식 인자의 개수는 제한이 없기 때문에 프로그램의 목적에 따라 원하는 만큼 형식
 * 인자를 사용하는 것이 가능하다.
 * 
 * 정적 클래스란?
 * - 추상 클래스와 마찬가지로 객체화 시킬 수 없는 클래스를 의미한다. (단, 추상 클래스는 직접적으로
 * 객체화 시키는 것은 불가능했지만 자식 클래스를 통해 간접적으로 객체화 시키는 것이 가능했던 반면,
 * 정적 클래스는 자식 클래스를 통해서도 객체화 시키는 것이 불가능하다.)
 * 
 * 정적 클래스는 일반적인 클래스와 달리 정적 계열 맴버만을 지닐 수 있기 때문에 해당 클래스는 상속
 * 자체는 의미가 없으며 상속을 지원하지도 않는 특징이 있다. (즉, 정적 클래스를 상속 할려고 하면
 * 컴파일 에러가 발생한다는 것을 알 수 있다.)
 * 
 * C# 정적 클래스 구현 방법
 * - static + class + 클래스 이름 + 클래스 맴버 (정적 변수, 정적 메서드 등등...)
 * 
 * Ex)
 * private static class CExtension {
 *		private static int m_nVal = 0;
 *		
 *		public static void SomeMethod() {
 *			// Do Something
 *		}
 * }
 * 
 * C# 정적 클래스 사용처
 * - 상수 클래스 선언
 * - 싱글턴 패턴 대체
 * - 확장 메서드 구현
 * 
 * 확장 메서드란?
 * - 특정 클래스가 지니고 있는 기능을 메서드를 통해서 확장 시킬 수 있는 기능을 의미한다. 
 * 
 * 일반적으로 특정 클래스가 지니고 있는 기능을 확장하기 위해서는 상속을 사용하지만 C# 은 확장
 * 메서드를 제공하기 때문에 특정 클래스를 상속하지 않고도 해당 클래스의 기능을 확장하는 것이
 * 가능하다.
 * 
 * 즉, 확장 메서드를 활용하면 제 3 자가 만든 클래스에 새로운 기능을 추가하는 것도 가능하다는
 * 것을 알 수 있다. (즉, 라이브러리 형태로 제공되는 클래스도 확장 메서드를 통해 프로그램의 목적에
 * 맞는 기능을 추가하는 것이 가능하다.)
 * 
 * 단, 확장 메서드는 클래스 내부에 존재하는 메서드가 아니기 때문에 해당 클래스의 protected 보호
 * 수준 이상의 맴버에는 접근 할 수 없다는 제약이 존재한다.
 * 
 * 따라서, 외부에서 접근 불가능한 클래스는 확장 메서드를 구현한다 하더라도 별달리 할 수 있는 동작이
 * 없다는 것을 알 수 있다.
 */
/** 확장 클래스 */
public static partial class CExtension
{
	/** 값을 오름차순 정렬한다 */
	public static void ExSort(this List<int> a_oSender)
	{
		for(int i = 1; i < a_oSender.Count; ++i)
		{
			int j = 0;
			int nCompareVal = a_oSender[i];

			for(j = i - 1; j >= 0 && a_oSender[j] >= nCompareVal; --j)
			{
				a_oSender[j + 1] = a_oSender[j];
			}

			a_oSender[j + 1] = nCompareVal;
		}
	}

	/** 값을 출력한다 */
	public static void ExPrint(this List<int> a_oSender)
	{
		for(int i = 0; i < a_oSender.Count; ++i)
		{
			Console.Write("{0}, ", a_oSender[i]);
		}

		Console.WriteLine();
	}
}

namespace Example._03010101000101_SBS_Weekday_Engine_0001.E01.Example.Classes.Runtime.Example_02
{
	/** Example 2 */
	class CE01Example_02
	{
#if E02_GENERIC_01
		/*
		 * 일반적으로 제네릭 메서드와 클래스는 모든 자료형에 동작하도록 명령문이 구성
		 * 되어있어야한다.
		 * 
		 * 따라서, 특정 자료형에만 동적하도록 제네릭 메서드와 클래스를 구현하기 위해서는
		 * where 키워드를 사용해서 특정 자료형에만 동적하도록 제네릭 형식 인자를 제한
		 * 시킬 필요가 있다는 것을 알 수 있다.
		 * 
		 * C# 제네릭 형식 인자 제한 종류
		 * - where T : class			<- 참조 형식 자료형으로 제한
		 * - where T : struct			<- 값 형식 자료형으로 제한
		 * - where T : CSomeClass		<- 해당 클래스를 직/간접적으로 상속한 자료형으로 제한
		 * - where T : ISomeInterface	<- 해당 인터페이스를 따르는 자료형으로 제한
		 */
		/** 동등 여부를 검사한다 */
		private static bool IsEquals<T>(T a_tLhs, T a_tRhs) where T : IEquatable<T>
		{
			return a_tLhs.Equals(a_tRhs);
		}

		/** 값을 교환한다 */
		private static void Swap<T>(ref T a_tLhs, ref T a_tRhs)
		{
			T tTemp = a_tLhs;
			a_tLhs = a_tRhs;
			a_tRhs = tTemp;
		}
#elif E02_GENERIC_02
		/** 배열 리스트 */
		private class CArrayList<T>
		{
			private int m_nNumVals = 0;
			private T[] m_tVals = null;

			/** 생성자 */
			public CArrayList(int a_nSize = 5)
			{
				m_tVals = new T[a_nSize];
			}

			/*
			 * 인덱서란?
			 * - 객체를 대상으로 [ ] (인덱스 연산자) 를 사용 할 수 있게 해주는 기능을 의미한다.
			 * (즉, 인덱서를 활용하면 객채를 대상으로 배열과 유사한 방법으로 특정 위치에 존재하는
			 * 데이터를 제어하는 것이 가능하다.)
			 * 
			 * 또한, 인덱서는 구현부는 프로퍼티와 동일하지만 필요에 따라 N 개 이상의 입력을 받아
			 * 들일 수 있기 때문에 해당 특징을 활용하면 다차원 배열과 같은 객체를 구현하는 것이
			 * 가능하다.
			 */
			/** 인덱서 */
			public T this[int a_nIdx]
			{
				get
				{
					return this.GetVal(a_nIdx);
				}
				set
				{
					this.SetVal(a_nIdx, value);
				}
			}

			/** 개수를 반환한다 */
			public int GetNumVals()
			{
				return m_nNumVals;
			}

			/** 값을 반환한다 */
			public T GetVal(int a_nIdx)
			{
				return m_tVals[a_nIdx];
			}

			/** 값을 변경한다 */
			public void SetVal(int a_nIdx, T a_tVal)
			{
				m_tVals[a_nIdx] = a_tVal;
			}

			/** 값을 추가한다 */
			public void AddVal(T a_tVal)
			{
				// 배열이 가득 찼을 경우
				if(m_nNumVals >= m_tVals.Length)
				{
					var oNewVals = new T[m_tVals.Length * 2];

					for(int i = 0; i < m_tVals.Length; ++i)
					{
						oNewVals[i] = m_tVals[i];
					}

					m_tVals = oNewVals;
				}

				m_tVals[m_nNumVals++] = a_tVal;
			}

			/** 값을 제거한다 */
			public void RemoveVal(T a_tVal)
			{
				for(int i = 0; i < m_nNumVals; ++i)
				{
					// 동일한 값이 존재 할 경우
					if(m_tVals[i].Equals(a_tVal))
					{
						this.RemoveValAt(i);
						break;
					}
				}
			}

			/** 값을 제거한다 */
			public void RemoveValAt(int a_nIdx)
			{
				for(int i = a_nIdx; i < m_nNumVals - 1; ++i)
				{
					m_tVals[i] = m_tVals[i + 1];
				}

				m_nNumVals -= 1;
			}
		}
#elif E02_GENERIC_03

#endif // E02_GENERIC_01

		/** 초기화 */
		public static void Start(string[] args)
		{
#if E02_GENERIC_01
			Console.Write("정수 (2 개) 입력 : ");
			var oTokens = Console.ReadLine().Split();

			int.TryParse(oTokens[0], out int nLhs);
			int.TryParse(oTokens[1], out int nRhs);

			Console.WriteLine("=====> 교환 전 <=====");
			Console.WriteLine("{0}, {1}", nLhs, nRhs);

			/*
			 * 일반적으로 제네릭 메서드를 호출 할 경우 제네릭 형식 인자의 자료형을
			 * 명시하지 않아도 호출이 가능하다.
			 * 
			 * 단, 해당 경우는 메서드에 전달 되는 데이터를 기반으로 C# 컴파일러가
			 * 자동으로 자료형을 추측하기 때문에 만약 메서드의 매개 변수만으로는
			 * 제네릭 형식 인자의 자료형을 결정 할 수 없을 경우에는 반드시 해당 자료형을
			 * 명시해줘야한다.
			 * 
			 * (즉, 일반적으로 메서드를 통해서 호출 되는 여러 기능들 (오버로딩, 오버라이딩
			 * 등등...) 은 모두 메서드의 매개 변수 정보를 기반으로 동작한다는 것을 알 수
			 * 있다.)
			 */
			Swap(ref nLhs, ref nRhs);
			//Swap<int>(ref nLhs, ref nRhs);

			Console.WriteLine("\n=====> 교환 후 <=====");
			Console.WriteLine("{0}, {1}", nLhs, nRhs);

			Console.WriteLine("\n=====> 동등 비교 결과 <=====");
			Console.WriteLine("{0} == {1} : {2}", 10, 20, IsEquals(10, 20));
			Console.WriteLine("{0} == {1} : {2}", 10, 20, IsEquals(10.0f, 20.0f));
			Console.WriteLine("{0} == {1} : {2}", 10, 20, IsEquals("A", "A"));
#elif E02_GENERIC_02
			var oValList01 = new CArrayList<int>();
			var oValList02 = new CArrayList<string>();

			for(int i = 0; i < 15; ++i)
			{
				oValList01.AddVal(i + 1);
				oValList02.AddVal($"{i + 1}");
			}

			Console.WriteLine("=====> 리스트 요소 <=====");

			for(int i = 0; i < oValList01.GetNumVals(); ++i)
			{
				Console.Write("{0}, ", oValList01[i]);
			}

			Console.WriteLine("\n");
			oValList02.RemoveVal("3");

			oValList02[0] = $"{100}";

			for(int i = 0; i < oValList02.GetNumVals(); ++i)
			{
				Console.Write("{0}, ", oValList02[i]);
			}

			Console.WriteLine("\n");
#elif E02_GENERIC_03
			var oRandom = new Random();
			var oValList = new List<int>();

			for(int i = 0; i < 20; ++i)
			{
				oValList.Add(oRandom.Next(1, 100));
			}

			Console.WriteLine("=====> 정렬 전 <=====");
			oValList.ExPrint();

			oValList.ExSort();

			Console.WriteLine("\n=====> 정렬 후 <=====");
			oValList.ExPrint();
#endif // E02_GENERIC_01
		}
	}
}

