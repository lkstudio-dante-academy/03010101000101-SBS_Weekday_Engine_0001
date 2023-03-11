//#define E10_GENERIC_01
//#define E10_GENERIC_02
#define E10_GENERIC_03

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

/*
 * 제네릭이란?
 * - 메서드 또는 클래스를 구현 할 때 자료형을 결정하지 않고 구현 할 수 있는 기능을 의미한다. (즉, 제네릭을 활용하면 자료형에
 * 상관없이 동일한 동작을 수행하는 메서드 또는 클래스를 구현하는 것이 가능하다.)
 * 
 * 따라서, 제네릭 메서드 또는 클래스를 구현하기 위해서는 제네릭 형식 인자를 사용 할 필요가 있으며 해당 형식 인자는 메서드 또는
 * 클래스가 사용되는 시점에 자료형을 대체 된다는 특징이 존재한다.
 * 
 * 즉, 메서드는 해당 메서드가 호출되는 시점에 자료형이 대체되며 클래스는 해당 클래스를 통해 객체가 생성되는 시점에 자료형이
 * 대체된다는 것을 알 수 있다.
 * 
 * C# 제네릭 메서드 구현 방법
 * - 반환형 + 메서드 이름 + 제네릭 형식 인자 + 매개 변수 + 메서드 몸체
 * 
 * C# 제네릭 클래스 구현 방법
 * - class + 클래스 이름 + 제네릭 형식 인자 + 클래스 맴버 (변수, 메서드)
 * 
 * Ex)
 * void SomeMethod<T1, T2>(T1 a_tValA, T2 a_tVal2);
 * 
 * class SomeClass<T1, T2> {
 *		T1 m_tValA;
 *		T2 m_tValB;
 *		
 *		void SomeMethodA(T1 a_tValA, T2 a_tValB);	<- 제네릭 클래스의 형식 인자를 사용해서 제네릭 메서드 구현 가능
 *		void SomeMethodB<T>(T a_tValA, T1 a_tValB);	<- 제네릭 클래스 안에 제네릭 메서드 구현 가능
 * }
 */
namespace Example.Classes.Example_10 {
	internal class CExample_10 {
#if E10_GENERIC_01
		/** 값을 교환한다 */
		private static void Swap<T>(ref T a_tLhs, ref T a_tRhs) {
			T tTemp = a_tLhs;
			a_tLhs = a_tRhs;
			a_tRhs = tTemp;
		}
#elif E10_GENERIC_02
		/** 배열 */
		private class CArray<T> {
			private int m_nSize = 0;
			private T[] m_tVals = null;

			public int NumVals { get; private set; } = 0;

			/*
			 * 인덱서란?
			 * - 객체를 대상으로 인덱스 연산자를 사용 할 경우 호출 되는 프로퍼티를 의미한다. (즉, 인덱서를 활용하면 객체를
			 * 대상으로 배열과 같은 컬렉션처럼 특정 데이터를 가져오고 변경하는 것이 가능하다.)
			 */
			/** 인덱서 */
			public T this[int a_nIdx] {
				get {
					return this.GetVal(a_nIdx);
				}
				set {
					this.SetVal(a_nIdx, value);
				}
			}

			/** 생성자 */
			public CArray(int a_nSize = 5) {
				m_nSize = a_nSize;
				this.NumVals = 0;

				m_tVals = new T[a_nSize];
			}

			/** 값을 추가한다 */
			public void AddVal(T a_tVal) {
				// 배열이 가득 찼을 경우
				if(this.NumVals >= m_nSize) {
					m_nSize *= 2;
					Array.Resize(ref m_tVals, m_nSize);
				}

				m_tVals[this.NumVals++] = a_tVal;
			}

			/** 값을 추가한다 */
			public void AddVal(int a_nIdx, T a_tVal) {
				// 배열이 가득 찼을 경우
				if(this.NumVals >= m_nSize) {
					m_nSize *= 2;
					Array.Resize(ref m_tVals, m_nSize);
				}

				for(int i = this.NumVals; i > a_nIdx; --i) {
					m_tVals[i] = m_tVals[i - 1];
				}

				this.NumVals += 1;
				m_tVals[a_nIdx] = a_tVal;
			}

			/** 값을 제거한다 */
			public void RemoveVal(T a_tVal) {
				for(int i = 0; i < this.NumVals; ++i) {
					// 값이 존재 할 경우
					if(m_tVals[i].Equals(a_tVal)) {
						this.RemoveValAt(i);
						break;
					}
				}
			}

			/** 값을 제거한다 */
			public void RemoveValAt(int a_nIdx) {
				Debug.Assert(this.IsValidIdx(a_nIdx));

				for(int i = a_nIdx; i < this.NumVals - 1; ++i) {
					m_tVals[i] = m_tVals[i + 1];
				}

				this.NumVals -= 1;
			}

			/** 값을 가져온다 */
			public T GetVal(int a_nIdx) {
				/*
				 * Debug.Assert 메서드는 조건이 거짓 일 경우 프로그램을 중단 시키는 역할을 수행한다. (즉, 해당 메서드는
				 * 개발 (Debug) 환경에서만 동작하기 때문에 개발 중에 잘못된 데이터에 의해 오작동하는 경우를 검사 할 때
				 * 사용하는 것이 가능하다.)
				 */
				Debug.Assert(this.IsValidIdx(a_nIdx));
				return m_tVals[a_nIdx];
			}

			/** 값을 변경한다 */
			public void SetVal(int a_nIdx, T a_tVal) {
				Debug.Assert(this.IsValidIdx(a_nIdx));
				m_tVals[a_nIdx] = a_tVal;
			}

			/** 인덱스 유효 여부를 검사한다 */
			private bool IsValidIdx(int a_nIdx) {
				return a_nIdx >= 0 && a_nIdx < this.NumVals;
			}
		}

		/** 값을 출력한다 */
		private static void PrintVals<T>(CArray<T> a_oValList) {
			for(int i = 0; i < a_oValList.NumVals; ++i) {
				Console.Write("{0}, ", a_oValList[i]);
			}

			Console.WriteLine();
		}
#elif E10_GENERIC_03
		/*
		 * 인터페이스란?
		 * - 특정 대상과 상호 작용을 할 수 있는 수단을 의미하며 C# 인터페이스는 단순한 메서드의 집합 (목록) 을 의미한다.
		 * 
		 * C# 인터페이스 특징
		 * - C# 인터페이스는 단순한 메서드의 목록이기 때문에 메서드의 선언만 명시하는 것이 가능하다. (즉, 실질적으로 해당
		 * 메서드가 어떻게 동작할지는 알 수 없다는 것을 의미한다.)
		 * 
		 * 따라서, 해당 인터페이스를 특정 클래스가 따를 경우 (즉, 인터페이스는 상속한다는 표현을 사용하지 않는다.) 특정
		 * 클래스는 반드시 해당 인터페이스에 명시 된 모든 메서드를 구현해줘야한다.
		 * 
		 * 만약, 하나라도 메서드를 구현하지 않았을 경우 해당 클래스는 직접 객체화 시킬 수 없는 추상 클래스가 된다는
		 * 것을 의미한다.
		 * 
		 * 추상 클래스란?
		 * - 직접 객체화 시킬 수 없는 클래스를 의미한다. 따라서, 해당 클래스를 통해 객체가 필요 할 경우 해당 클래스를
		 * 직/간접적으로 상속하는 클래스를 사용해야한다. (즉, 추상 클래스는 자식 클래스를 통해 간접적으로 객체화가 가능
		 * 하다는 것을 알 수 있다.)
		 * 
		 * 반대로 객체화 시킬 수 있는 일반적인 클래스는 구체 클래스라고 지칭한다.
		 */
		/** 출력 인터페이스 */
		private interface IOutput {
			void OutputVals();
		}

		/*
		 * abstract 키워드는 해당 클래스가 직접 객체화 시킬 수 없는 추상 클래스라는 것을 컴파일러에게 알리는 역할을 수행
		 * 한다.
		 * 
		 * C# 은 특정 클래스가 추상 클래스가 되기 위해서는 반드시 해당 키워드를 명시하도록 강제하고 있기 때문에 클래스 
		 * 선언을 통해 해당 클래스 객체화 가능 여부를 알 수 있다.
		 */
		/** 데이터 */
		private class CData : IOutput {
			private int m_nVal = 0;
			private float m_fVal = 0.0f;

			/** 생정자 */
			public CData(int a_nVal, float a_fVal) {
				m_nVal = a_nVal;
				m_fVal = a_fVal;
			}

			/*
			 * 추상 메서드란?
			 * - 몸체가 존재하지 않는 메서드를 의미한다. 따라서, 특정 클래스가 추상 메서드를 하나라도 가지고 있을 경우
			 * 해당 클래스는 직접 객체화 시킬 수 없는 추상 클래스라는 것을 의미한다.
			 * 
			 * 따라서, 특정 클래스가 따르고 있는 인터페이스를 구현하지 않는 경우에는 반드시 해당 메서드를 추상 메서드로
			 * 선언해서 자식 클래스가 구현하도록 강제 할 필요가 있다.
			 * 
			 * 만약, 자식 클래스에서 부모 클래스가 지니고 있는 추상 메서드를 구현하지 않는다면 자식 클래스 또한 직접 객체화
			 * 시킬 수 없는 추상 클래스가 된다는 특징이 존재한다. (즉, 클래스에 하나라도 추상 메서드가 존재 할 경우에는
			 * 반드시 자식 클래스 어딘가에서 해당 메서드를 구현해야지만 간접적으로 객체화 시킬 수 있다는 것을 알 수 있다.)
			 */
			//public abstract void OutputVals();

			/** 값을 출력한다 */
			public void OutputVals() {
				Console.WriteLine("정수 : {0}, 실수 : {1}", m_nVal, m_fVal);
			}
		}

		/*
		 * C# 제네릭 형식 인자 범위 제한 방법
		 * - where T : struct			<- 값 형식 자료형만 허용
		 * - where T : class			<- 참조 형식 자료형만 허용
		 * - where T : CSomeClass		<- 해당 클래스이거나 직/간접적으로 상속한 클래스만 허용
		 * - where T : ISomeInterface	<- 해당 인터페이스를 직/간접적으로 따르는 자료형만 허용
		 */
		/** 배열 */
		private class CArray<T> where T : struct {
			private int m_nSize = 0;
			private T[] m_tVals = null;

			public int NumVals { get; private set; } = 0;

			/*
			 * 인덱서란?
			 * - 객체를 대상으로 인덱스 연산자를 사용 할 경우 호출 되는 프로퍼티를 의미한다. (즉, 인덱서를 활용하면 객체를
			 * 대상으로 배열과 같은 컬렉션처럼 특정 데이터를 가져오고 변경하는 것이 가능하다.)
			 */
			/** 인덱서 */
			public T this[int a_nIdx] {
				get {
					return this.GetVal(a_nIdx);
				}
				set {
					this.SetVal(a_nIdx, value);
				}
			}

			/** 생성자 */
			public CArray(int a_nSize = 5) {
				m_nSize = a_nSize;
				this.NumVals = 0;

				m_tVals = new T[a_nSize];
			}

			/** 값을 추가한다 */
			public void AddVal(T a_tVal) {
				// 배열이 가득 찼을 경우
				if(this.NumVals >= m_nSize) {
					m_nSize *= 2;
					Array.Resize(ref m_tVals, m_nSize);
				}

				m_tVals[this.NumVals++] = a_tVal;
			}

			/** 값을 추가한다 */
			public void AddVal(int a_nIdx, T a_tVal) {
				// 배열이 가득 찼을 경우
				if(this.NumVals >= m_nSize) {
					m_nSize *= 2;
					Array.Resize(ref m_tVals, m_nSize);
				}

				for(int i = this.NumVals; i > a_nIdx; --i) {
					m_tVals[i] = m_tVals[i - 1];
				}

				this.NumVals += 1;
				m_tVals[a_nIdx] = a_tVal;
			}

			/** 값을 제거한다 */
			public void RemoveVal(T a_tVal) {
				for(int i = 0; i < this.NumVals; ++i) {
					// 값이 존재 할 경우
					if(m_tVals[i].Equals(a_tVal)) {
						this.RemoveValAt(i);
						break;
					}
				}
			}

			/** 값을 제거한다 */
			public void RemoveValAt(int a_nIdx) {
				Debug.Assert(this.IsValidIdx(a_nIdx));

				for(int i = a_nIdx; i < this.NumVals - 1; ++i) {
					m_tVals[i] = m_tVals[i + 1];
				}

				this.NumVals -= 1;
			}

			/** 값을 가져온다 */
			public T GetVal(int a_nIdx) {
				/*
				 * Debug.Assert 메서드는 조건이 거짓 일 경우 프로그램을 중단 시키는 역할을 수행한다. (즉, 해당 메서드는
				 * 개발 (Debug) 환경에서만 동작하기 때문에 개발 중에 잘못된 데이터에 의해 오작동하는 경우를 검사 할 때
				 * 사용하는 것이 가능하다.)
				 */
				Debug.Assert(this.IsValidIdx(a_nIdx));
				return m_tVals[a_nIdx];
			}

			/** 값을 변경한다 */
			public void SetVal(int a_nIdx, T a_tVal) {
				Debug.Assert(this.IsValidIdx(a_nIdx));
				m_tVals[a_nIdx] = a_tVal;
			}

			/** 인덱스 유효 여부를 검사한다 */
			private bool IsValidIdx(int a_nIdx) {
				return a_nIdx >= 0 && a_nIdx < this.NumVals;
			}
		}

		/** 값을 출력한다 */
		private static void PrintVals<T>(T a_oTarget) where T : IOutput {
			a_oTarget.OutputVals();
		}

		/** 값을 출력한다 */
		private static void PrintVals<T>(CArray<T> a_oValList) where T : struct {
			for(int i = 0; i < a_oValList.NumVals; ++i) {
				Console.Write("{0}, ", a_oValList[i]);
			}

			Console.WriteLine();
		}
#endif // E10_GENERIC_01

		/** 초기화 */
		public static void Start(string[] args) {
#if E10_GENERIC_01
			int nLhs = 10;
			int nRhs = 20;

			float fLhs = 10.0f;
			float fRhs = 20.0f;

			string oLhs = "10";
			string oRhs = "20";

			Console.WriteLine("=====> 교환 전 <=====");
			Console.WriteLine("{0}, {1}", nLhs, nRhs);
			Console.WriteLine("{0}, {1}", fLhs, fRhs);
			Console.WriteLine("{0}, {1}", oLhs, oRhs);

			Swap(ref nLhs, ref nRhs);
			Swap(ref fLhs, ref fRhs);
			Swap(ref oLhs, ref oRhs);

			Console.WriteLine("\n=====> 교환 후 <=====");
			Console.WriteLine("{0}, {1}", nLhs, nRhs);
			Console.WriteLine("{0}, {1}", fLhs, fRhs);
			Console.WriteLine("{0}, {1}", oLhs, oRhs);
#elif E10_GENERIC_02
			/*
			 * 제네릭 클래스는 제네릭 메서드와 달리 제네릭 형식 인자를 생략하는 것이 불가능하다. (즉, 제네릭 클래스는 반드시
			 * 형식 인자를 명시해줘야한다는 것을 알 수 있다.)
			 */
			CArray<int> oValList = new CArray<int>();

			for(int i = 0; i < 10; ++i) {
				oValList.AddVal(i + 1);
			}

			Console.WriteLine("=====> 배열 요소 <=====");
			PrintVals(oValList);

			oValList.AddVal(0, 100);

			Console.WriteLine("\n=====> 배열 요소 - 추가 후 <=====");
			PrintVals(oValList);

			oValList.RemoveVal(100);
			oValList.RemoveValAt(oValList.NumVals - 1);

			Console.WriteLine("\n=====> 배열 요소 - 삭제 후 <=====");
			PrintVals(oValList);

			Console.WriteLine();
#elif E10_GENERIC_03
			CData oData = new CData(10, 3.14f);
			CArray<int> oValList = new CArray<int>();

			Console.WriteLine("=====> 데이터 <=====");
			PrintVals(oData);

			/*
			 * CArray 제네릭 클래스 형식 인자는 값 형식으로 제한되었기 때문에 string 과 같은 참조 형식 자료형은 사용이
			 * 불가능하다는 것을 알 수 있다.
			 */
			//CArray<string> oStrList = new CArray<string>();

			for(int i = 0; i < 10; ++i) {
				oValList.AddVal(i + 1);
			}

			Console.WriteLine("\n=====> 배열 요소 <=====");
			PrintVals(oValList);
#endif // E10_GENERIC_01
		}
	}
}
