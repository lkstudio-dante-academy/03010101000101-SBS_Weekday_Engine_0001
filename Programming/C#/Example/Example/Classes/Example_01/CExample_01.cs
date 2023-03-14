/*
 * 전처리기 명령어란?
 * - C# 컴파일러가 C# 으로 제작 된 명령문을 기계어로 바꾸기 전에 명령문을 튜닝 할 때 사용되는 명령어를
 * 의미한다. (즉, 전처리기 명령어는 C# 의 일부가 아니기 때문에 C# 과는 전혀 다른 문법 체계를 지니고 있다.)
 * 
 * 단, 전처리기 명령어는 모두 # 기호로 시작하는 특징이 있다. (Ex. #define, #if, #endif 등등...)
 */
#define E01_01
#define E01_02
#define E01_03

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 자료형이란?
 * - 특정 데이터를 해석하는 방법을 의미한다. (즉, 동일한 형태의 데이터라고 하더라도 자료형에 따라서
 * 처리되는 방법이 달라질 수 있다는 것을 의미한다.)
 * 
 * 또한, 자료형은 특정 데이터가 표현 할 수 있는 값의 최대/최소 범위를 제한하는 역할을 수행한다.
 * (즉, 어떤 자료형을 선택하느냐에 따라 데이터가 지닐 수 있는 최대 가짓 수가 정해진다는 것을 의미한다.)
 * 
 * C# 자료형의 종류
 * - 값 형식 자료형
 * - 참 형식 자료형
 * 
 * 값 형식 자료형이란?
 * - 시스템에 의해서 관리되는 자료형을 의미한다. (즉, 시스템에 의해서 관리되기 때문에 값 형식 데이터는
 * 사용 후 별도의 처리를 하지 않아도 된다.)
 * 
 * 또한, 값 형식 데이터는 값 자체를 제어하기 때문에 특정 값 형식 데이터를 다른 변수에 할당했을 경우
 * 원본과 동일한 데이터를 지니는 사본이 만들어지는 특징이 존재한다.
 * 
 * 참조 형식 자료형이란?
 * - 해당 형식의 자료형은 가비지 컬렉션에 관리되는 특징이 존재한다. 따라서, 해당 형식의 자료형을 무분별하게
 * 사용 할 경우 가비지 컬렉션에 의해 프로그램의 전체 성능이 저하 될 수 있다.
 * 
 * 또한, 참조 형식 데이터는 값 자체를 제어하는 것이 아니라 해당 값을 지니고 있는 대상을 가리키는 참조 값
 * (메모리 주소) 을 제어하기 때문에 특정 변수에 참조 형식 데이터를 할당했을 경우 사본이 만들어지는 것이
 * 아니라 동일한 대상을 가리키는 특징이 존재한다.
 * 
 * 클래스란?
 * - 변수와 메서드를 하위 멤버로 그룹화 시키는 사용자 정의 자료형을 의미한다. (즉, 클래스를 활용하면
 * 특정 프로그램의 목적에 맞게 사용자 (프로그래머) 만의 자료형을 정의하는 것이 가능하다.)
 * 
 * 따라서, 클래스를 통해 특정 사물을 간략화 시켜서 표현하는 것이 가능하다. (즉, 특정 사물이 지니고 있는
 * 특징 중 속성은 변수를 통해 표현하는 것이 가능하며, 해당 사물의 행위 (기능) 는 메서드 (함수) 를 통해서
 * 표현하는 것이 가능하다.)
 * 
 * 즉, 클래스는 객체지향 프로그래밍의 핵심이 되는 객체를 생성하기 위한 틀을 의미한다.
 * 
 * 객체지향 프로그래밍이란?
 * - 프로그램의 구조를 설계하는 방식 중 하나로써 특정 역할을 수행하는 객체들을 정의하고 해당 객체들의
 * 간의 관계를 통해서 프로그램의 구조를 잡아가는 방법이다.
 * 
 * C# 클래스 선언 방법
 * - class + 클래스 이름 + 맴버 (변수, 메서드, 프로퍼티 등등...)
 * 
 * Ex)
 * class CPlayer {
 *		public int m_nLV;
 *		public string m_oName;
 *		
 *		public void ShowInfo() {
 *			// Do Something
 *		}
 * }
 */
namespace Example.Classes.Example_01 {
	class CExample_01 {
#if E01_01
		/** 플레이어 */
		class CPlayer {
			private int m_nLV = 0;
			private string m_oName = string.Empty;

			public int LV {
				get {
					return m_nLV;
				}
				set {
					m_nLV = value;
				}
			}

			public string Name {
				get {
					return m_oName;
				}
				set {
					m_oName = value;
				}
			}

			/*
			 * 메서드란?
			 * - 명령문의 특정 부분을 따로 떼어내서 그룹화 시키는 기능을 의미한다. 이렇게 따로 그룹화시킨
			 * 명령문은 필요에 따라 재사용하는 것이 가능하다는 특징이 존재한다.
			 * 
			 * 따라서, 메서드를 활용하면 중복적으로 작성되는 명령문을 최소화시키는 것이 가능하다.
			 * (즉, 유지/보수 성이 증가한다는 것을 의미한다.) 
			 */
			/** 정보를 출력한다 */
			public void ShowInfo() {
				Console.WriteLine("이름 : {0}, 레벨 : {1}", m_oName, m_nLV);
			}
		}
#elif E01_02

#elif E01_03

#endif // #if E01_01

		/** 초기화 */
		public static void Start(string[] args) {
#if E01_01
			var oPlayer01 = new CPlayer();
			oPlayer01.LV = 1;
			oPlayer01.Name = "플레이어 1";

			var oPlayer02 = new CPlayer();
			oPlayer02.LV = 50;
			oPlayer02.Name = "플레이어 2";

			Console.WriteLine("=====> {0} 정보 출력 <=====", oPlayer01.Name);
			oPlayer01.ShowInfo();

			Console.WriteLine("\n=====> {0} 정보 출력 <=====", oPlayer02.Name);
			oPlayer02.ShowInfo();
#elif E01_02

#elif E01_03

#endif // #if E01_01
		}
	}
}
