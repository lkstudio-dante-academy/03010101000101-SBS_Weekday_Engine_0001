//#define E02_VAR
#define E02_CONST

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 자료형이란?
 * - 데이터를 해석하는 방법을 의미한다. (즉, 동일한 형태의 데이터라고 하더라도 자료형에
 * 의해서 해당 데이터를 처리하는 결과가 달라 질 수 있다는 것을 의미한다.)
 * 
 * 또한, 자료형은 특정 데이터를 저장하거나 읽어들일 수 있는 공간의 크기를 제한하는
 * 역할을 수행한다. (즉, 자료형에 따라 처리 가능한 데이터의 크기가 달라진다는 것을
 * 의미한다.)
 * 
 * C# 자료형 종류
 * =====> 값 형식 <=====
 * > 정수
 * - byte (1 바이트)
 * - sbyte (1 바이트)
 * 	
 * - short (2 바이트)
 * - ushort (2 바이트)
 * 	
 * - int (4 바이트)
 * - uint (4 바이트)
 * 	
 * - long (8 바이트)
 * - ulong (8 바이트)
 * 
 * > 실수
 * - float (4 바이트)
 * - double (8 바이트)
 * - decimal (16 바이트)
 * 
 * > 논리
 * - bool (1 바이트)
 * 
 * > 문자
 * - char (2 바이트)
 * 
 * =====> 참조 형식 <=====
 * > 문자열
 * - string
 * 
 * > 객체
 * - object
 * 
 * C# 자료형은 데이터 형식에 따라 값 형식과 참조 형식으로 구분된다.
 * 
 * 값 형식이란?
 * - 시스템에 의해서 자동으로 관리 되는 자료형을 의미한다. (즉, 해당 자료형의 데이터가
 * 더이상 사용되지 않을 경우 시스템에 의해서 자동으로 정리된다.)
 * 
 * 또한, 값 형식 자료형은 해당 데이터를 직접 처리하는 특징이 존재하기 때문에 해당 형식
 * 자료형 데이터를 다른 공간에 할당 (복사) 할 경우 사본이 만들어지는 특징이 존재한다.
 * 
 * 참조 형식이란?
 * - 가비지 컬렉션에 의해서 관리 되는 자료형을 의미한다. (즉, 해당 자료형의 데이터가
 * 더이상 사용되지 않을 경우 가비지 컬렉션에 의해서 자동으로 정리된다.)
 * 
 * 또한, 참조 형식 자료형 데이터는 값 형식과 달리 데이터를 직접 처리하는 것이 아니라
 * 해당 데이터를 지니고 있는 공간에 대한 참조 값 (메모리 주소) 을 제어하는 특징이 존재
 * 한다. (즉, 참조 형식 데이터를 다른 공간에 할당 할 경우 사본이 만들어지는 것이 아니라
 * 데이터를 지니고 있는 공간에 대한 참조 값만 복사 된다는 것을 알 수 있다.)
 * 
 * 정수 자료형이란?
 * - 소수점이 포함되어있지 않은 숫자 데이터를 처리 할 수 있는 자료형을 의미한다.
 * 
 * 실수 자료형이란?
 * - 소수점이 포함되어있는 숫자 데이터를 처리 할 수 있는 자료형을 의미한다. 단, float
 * 와 double 자료형은 부동 소수점 방식으로 실수 데이터를 처리하기 때문에 처리되는 데이터는
 * 미세한 오차를 지니고 있으며 해당 오차를 부동 소수점 오차라고 지칭한다.
 * 
 * 반면, decimal 자료형은 고정 소수점 방식에 비해 높은 정밀도를 자랑하지만 부동 소수점
 * 방식에 비해 처리되는 속도가 떨어지는 단점이 존재한다.
 * 
 * 정수 자료형 vs 실수 자료형
 * - 정수 자료형 데이터는 소수점을 처리 할 수 없는 대신 빠른 속도로 데이터를 처리하는 것이
 * 가능하기 때문에 정수 자료형과 실수 자료형 모두 사용 할 수 있는 상황이라면 정수 자료형을
 * 사용하는 것이 좀 더 좋은 성능을 지니는 프로그램을 제작할 수 있다.
 * 
 * 변수란?
 * - 특정 데이터를 저장하거나 읽어들일 수 있는 공간을 변수라고 한다. (즉, 변수를 활용하면
 * 프로그램이 구동 중에 필요에 따라 재사용 할 수 있는 데이터를 처리하는 것이 가능하다.)
 * 
 * C# 변수 선언 방법
 * - 자료형 + 변수 이름
 * 
 * Ex)
 * int nVal = 0;
 * float fVal = 0.0f;
 * 
 * 상수란?
 * - 일반적인 변수와 달리 한번 데이터가 할당되고 나면 더이상 해당 데이터를 변경하는 것은 불가능
 * 하고 해당 데이터를 읽어들일 수만 있는 공간을 의미한다.
 * 
 *  C# 상수 선언 방법
 *  - const 자료형 + 상수 이름
 *  
 *  C# 이름 작성 시 주의 사항
 *  - C# 에서 이름 작성 시 활용 할 수 있는 문자는 알파벳 (대/소문자), _ (언더 스코어), 숫자 (0 ~ 9)\
 *  가 존재한다. 알파벳 이외에도 한글 등을 사용하는 것이 가능하지만 프로그래밍에서 특정 대상의
 *  이름을 작성 할 때는 알파벳, _, 숫자 만을 사용하는 것이 관례이다.
 *  
 *  또한, 첫 문자는 숫자로 시작 할 수 없다는 제한이 존재한다.
 */
namespace Example.Classes.Example_02 {
	class CExample_02 {
		/** 초기화 */
		public static void Start(string[] args) {
#if E02_VAR
			/*
			 * 정수 자료형은 signed 자료형과 unsigned 자료형으로 구분된다. signed
			 * 자료형은 양수/음수를 모두 처리 할 수 있는 반면, unsigned 자료형은
			 * 양수만을 처리하는 것이 가능하다. (즉, 음수에 대한 처리가 불필요 할 경우
			 * unsigned 자료형을 활용하는 것이 가능하다.)
			 * 
			 * 또한, unsigned 자료형은 음수를 처리 할 수 없는 대신에 signed 자료형에
			 * 비해 처리 할 수 있는 양수의 크기가 2 배 더 크다는 특징이 존재한다.
			 */
			byte nByte01 = byte.MaxValue;
			sbyte nByte02 = sbyte.MaxValue;

			short nShort01 = short.MaxValue;
			ushort nShort02 = ushort.MaxValue;

			int nInt01 = int.MaxValue;
			uint nInt02 = uint.MaxValue;

			long nLong01 = long.MaxValue;
			ulong nLong02 = ulong.MaxValue;

			Console.WriteLine("=====> 정수 <=====");
			Console.WriteLine("byte : {0}, {1}", byte.MinValue, nByte01);
			Console.WriteLine("sbyte : {0}, {1}", sbyte.MinValue, nByte02);

			Console.WriteLine("\nshort : {0}, {1}", short.MinValue, nShort01);
			Console.WriteLine("ushort : {0}, {1}", ushort.MinValue, nShort02);

			Console.WriteLine("\nint : {0}, {1}", int.MinValue, nInt01);
			Console.WriteLine("uint : {0}, {1}", uint.MinValue, nInt02);

			Console.WriteLine("\nlong : {0}, {1}", long.MinValue, nLong01);
			Console.WriteLine("ulong : {0}, {1}", ulong.MinValue, nLong02);

			float fFloat = float.MaxValue;
			double dblDouble = double.MaxValue;
			decimal dmDecimal = decimal.MaxValue;

			Console.WriteLine("\n=====> 실수 <=====");
			Console.WriteLine("float : {0}, {1}", float.MinValue, fFloat);
			Console.WriteLine("double : {0}, {1}", double.MinValue, dblDouble);
			Console.WriteLine("decimal : {0}, {1}", decimal.MinValue, dmDecimal);

			bool bIsBool = true;
			char chLetter = 'A';
			string oStr = "Hello, World!";
			object oObj = 10;

			Console.WriteLine("\n=====> 기타 <=====");
			Console.WriteLine("bool : {0}", bIsBool);
			Console.WriteLine("char : {0}", chLetter);
			Console.WriteLine("string : {0}", oStr);
			Console.WriteLine("object : {0}", (int)oObj);
#elif E02_CONST
			const int nConstVal = 10;
			const float fConstVal = 3.14f;

			/*
			 * C# 상수 유형
			 * - 심볼릭 상수
			 * - 리터널 상수
			 * 
			 * 심볼릭 상수 vs 리터널 상수
			 * - 심볼릭 상수는 이름이 존재하는 반면, 리터널 상수는 이름이 따로 존재하지 않는다.
			 * (즉, 심볼릭 상수는 필요에 따라 해당 상수를 재사용 할 수 있지만 리터널 상수는
			 * 재사용하는 것이 불가능하다.)
			 */
			//nConstVal = 20;

			Console.WriteLine("=====> 상수 <=====");
			Console.WriteLine("{0}, {1}", nConstVal, fConstVal);
#endif // #if E02_VAR
		}
	}
}
