/*
 * 전처리기 명령어란?
 * - C# 컴파일러가 C# 으로 제작 된 명령문을 기계어로 바꾸기 전에 명령문을 튜닝 할 때 사용되는 명령어를
 * 의미한다. (즉, 전처리기 명령어는 C# 의 일부가 아니기 때문에 C# 과는 전혀 다른 문법 체계를 지니고 있다.)
 * 
 * 단, 전처리기 명령어는 모두 # 기호로 시작하는 특징이 있다. (Ex. #define, #if, #endif 등등...)
 */
//#define E01_CLASS_01
//#define E01_CLASS_02
//#define E01_CLASS_03
#define E01_CLASS_04

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
namespace Example._03010101000101_S_W_Engine_0001.E01.Example.Classes.Runtime.Example_01
{
	class CE01Example_01
	{
#if E01_CLASS_01
		/** 플레이어 */
		class CPlayer
		{
			private string m_oName = string.Empty;

			/*
			 * 프로퍼티란?
			 * - C# 에서 제공하는 접근자 메서드를 좀 더 쉽게 구현 할 수 있는 기능을 의미한다. 따라서,
			 * 프로퍼티를 활용하면 일반적인 변수에 접근 하 듯이 객체 존재하는 맴버를 제어하는 것이
			 * 가능하다.
			 * 
			 * 또한, 프로퍼티는 Getter 와 Setter 중 필요에 따라 모두 구현하거나 둘 중 하나를 생략하는
			 * 것이 가능하다. (즉, Getter 만 필요 할 경우 Setter 구현 할 필요가 없다는 것을 알 수 있다.)
			 * 
			 * 자동 구현 프로퍼티란?
			 * - 일반적인 프로퍼티는 특정 데이터를 보관하거나 읽어 들일 수 있는 맴버 변수가 필요하지만
			 * 자동 구현 프로퍼티를 활용하면 맴버 변수 선언을 생략하는 것이 가능하다. (즉, 프로퍼티만으로
			 * 데이터를 저장하거나 읽어 들 일 수 있다는 것을 알 수 있다.)
			 * 
			 * 단, 특정 데이터를 저장하기 위해서는 반드시 해당 데이터를 저장하기 위한 공간 (변수) 이 필요하기
			 * 때문에 C# 컴파일러는 자동 구현 프로퍼티가 구현되면 해당 프로퍼티를 통해서 제어 될 데이터를
			 * 저장하기 위한 변수를 자동으로 생성하는 특징이 있다.
			 */
			public int LV { get; set; } = 0;

			public string Name
			{
				get
				{
					return m_oName;
				}
				set
				{
					m_oName = value;
				}
			}

			/*
			 * 생성자란?
			 * - 객체가 생성되는 과정에서 호출되는 특별한 이름을 지니는 메서드를 의미한다. (즉, 생성자의
			 * 역할은 생성 된 객체를 초기화하는 것이다.)
			 * 
			 * 또한, 객체 생성이 완료되기 위해서는 반드시 생성자가 호출 될 필요가 있다. 만약, 객체가 생성되는
			 * 과정에서 생성자가 별도로 호출되지 않았을 경우 이는 곧 객체 생성이 완료되지 않았다는 것을
			 * 의미한다.
			 * 
			 * 따라서, 특정 클래스 내부에 생성자가 존재하지 않을 경우 C# 컴파일러에 의해서 자동으로 아무런
			 * 매개 변수도 전달 받지 않는 기본 생성자가 추가 된다. 단, 클래스 내부에 생성자가 1 개라도 존재
			 * 할 경우 컴파일러는 기본 생성자가 필요하지 않다고 판단하기 때문에 더이상 기본 생성자를 자동으로
			 * 추가 시켜주지 않는 특징이 있다.
			 * 
			 * 위임 생성자란?
			 * - 생성자가 같은 클래스 내부에 있는 다른 생성자를 호출 할 수 있는 기능을 의미한다. 따라서, 위임
			 * 생성자를 활용하면 객체를 초기화 시키는 구문의 중복을 최소화 시키는 것이 가능하다.
			 * 
			 * PS)
			 * C++ 은 특정 메모리를 동적으로 할당 할 수 있는 방법으로 크게 malloc 함수와 new 키워드를
			 * 제공한다.
			 * 
			 * malloc 함수는 C 부터 존재하던 함수로써 해당 함수를 활용하면 동적으로 데이터를 할당하는 것이
			 * 가능하지만 객체는 동적으로 생성 할 수 없다. 이는 C 에는 클래스의 개념이 없기 때문에 malloc
			 * 함수 또한 클래스에 대응되는 기능이 별도로 구현되어 있지 않기 때문이다.
			 * 
			 * 따라서, C++ 에서 객체를 동적으로 생성하기 위해서는 반드시 new 키워드를 사용해야한다. (즉,
			 * new 키워드를 통해서 객체를 동적으로 생성하면 컴파일러에 의해서 해당 객체의 생성자가 호출되며
			 * 이는 곧 정상적으로 객체가 생성 되었다는 것을 의미한다.)
			 */
			/** 생성자 */
			public CPlayer() : this(string.Empty, 0)
			{
				// Do Something
			}

			/** 생성자 */
			public CPlayer(string a_oName, int a_nLV)
			{
				this.LV = a_nLV;
				m_oName = a_oName;
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
			public void ShowInfo()
			{
				Console.WriteLine("이름 : {0}, 레벨 : {1}", m_oName, this.LV);
			}
		}
#elif E01_CLASS_02
		/*
		 * 상속이란?
		 * - 부모 클래스가 지니고 있는 맴버 (변수, 메서드, 프로퍼티 등등...) 를 자식 클래스가 물려받는
		 * 것을 의미한다. (즉, 상속으로 활용하면 특정 클래스 간에 부모/자식 관계를 형성하는 것이 가능하며
		 * 자식 클래스에서는 부모 클래스가 지니고 있는 맴버를 활용하는 것이 가능하다.)
		 * 
		 * 따라서, 상속을 활용하면 클래스 간에 중복적으로 명시되는 구문을 최소화 시키는 것이 가능하다.
		 * (즉, 클래스 간에 공통적으로 존재하는 특징은 부모 클래스에 구현 후 해당 클래스를 상속함으로써
		 * 요구사항 변경에 따르는 변화에 좀 더 수월하게 대처하는 것이 가능하다.)
		 * 
		 * 올바른 상속 구조
		 * - 기본적으로 C# 을 포함한 모든 객체지향 프로그래밍 언어는 특정 클래스 간에 상속을 제한하는
		 * 규약이 별도로 존재하지 않기 때문에 상속을 무분별하게 활용하면 오히려 프로그램의 구조가 복잡해지는
		 * 단점이 존재한다.
		 * 
		 * 따라서, 특정 클래스를 올바른 구조로 상속을 하기 위해서는 크게 2 가지의 규칙이 있으며 해당 규칙을
		 * 만족한다면 일반적으로 바람직한 방향으로 상속이 진행되고 있다고 봐도 괜찮다.
		 * 
		 * 올바른 상속 구조에는 is a 의 관계와 has a 의 관계가 있으며 이 중 has a 는 상속으로 이루어진
		 * 포함 관계가 변하지 않는 단점이 있기 때문에 is a 의 관계만을 가지고 상속 구조를 잡아가는 것을
		 * 추천한다. (즉, has a 관계는 클래스의 맴버를 통해서도 충분히 유연하게 표현 할 수 있기 때문에
		 * 굳이 has a 의 관계를 상속에 적용하지 않아도 된다.)
		 */
		/** 부모 클래스 */
		public class CBase
		{
			public int m_nVal = 0;
			public float m_fVal = 0.0f;

			/** 생성자 */
			public CBase() : this(0, 0.0f)
			{
				// Do Something
			}

			/** 생성자 */
			public CBase(int a_nVal, float a_fVal)
			{
				m_nVal = a_nVal;
				m_fVal = a_fVal;
			}

			/** 정보를 출력한다 */
			public void ShowInfo()
			{
				Console.WriteLine("=====> 부모 클래스 정보 <=====");
				Console.WriteLine("정수 : {0}, 실수 : {1}", m_nVal, m_fVal);
			}
		}

		/** 자식 클래스 */
		public class CDerived : CBase
		{
			public string m_oStr = string.Empty;

			/** 생성자 */
			public CDerived() : this(0, 0.0f, string.Empty)
			{
				// Do Something
			}

			/*
			 * 자식 클래스를 통해 객체를 생성했을 경우 자식 객체의 생성자는 컴파일러에 의해서 호출된다.
			 * 단, 자식 클래스의 객체가 생성 완료되기 위해서는 반드시 부모 클래스의 생성자가 호출 될
			 * 필요가 있으며 부모 클래스의 생성자는 자식 클래스의 생성자에서 호출해줘야한다.
			 * 
			 * 만약, 자식 클래스의 생성자에서 부모 클래스의 생성자를 호출하는 구문이 존재하지 않을 경우
			 * 컴파일러에 의해서 자동으로 부모 클래스의 기본 생성자를 호출하는 구문이 추가된다.
			 * 
			 * 따라서, 부모 클래스에 기본 생성자가 존재하지 않을 경우에는 반드시 자식 클래스의 생성자에서
			 * 부모 클래스의 생성자를 호출하는 구문을 명시적으로 작성해 줄 필요가 있다.
			 */
			/** 생성자 */
			public CDerived(int a_nVal, float a_fVal, string a_oStr) : base(a_nVal, a_fVal)
			{
				m_oStr = a_oStr;
			}

			/** 정보를 출력한다 */
			public new void ShowInfo()
			{
				/*
				 * base 키워드는 부모 클래스를 지칭하는 키워드를 의미한다. 따라서, 부모 클래스가 지니고
				 * 메서드 중에 자식 클래스의 메서드와 동일한 이름을 지닌 메서드를 호출하기 위해서는
				 * 반드시 base 키워드를 명시함으로써 부모 클래스의 메서드를 호출하겠다는 것을 컴파일러에게
				 * 알려줘야한다.
				 */
				base.ShowInfo();

				Console.WriteLine("=====> 자식 클래스 정보 <=====");
				Console.WriteLine("문자열 : {0}", m_oStr);
			}
		}
#elif E01_CLASS_03
		/*
		 * 다형성이란?
		 * - 특정 대상을 바라보는 시야에 따라 서로 다른 대상을 인지하는 것을 의미한다. (즉, 대상은 하나지만
		 * 바라보는 시야에 해당 대상이 여러 형태를 지닌다는 것을 알 수 있다.)
		 * 
		 * 객체지향 프로그래밍에서는 클래스의 상속 관계를 이용해서 다형성을 흉내내는 것이 가능하다.
		 * (즉, 자식 클래스로부터 생성 된 객체를 부모 클래스의 자료형으로 참조하는 것이 가능하다는 것을
		 * 알 수 있다.)
		 * 
		 * 따라서, 자식 클래스 객체를 부모 클래스의 시야로 바라봄에 따라 해당 객체를 부모 클래스 객체로
		 * 인지하는 것이 가능하다.
		 * 
		 * 가상 메서드란?
		 * - 해당 메서드가 호출 될 때 실행 되는 메서드를 동적으로 바인딩 할 수 있는 메서드를 의미한다.
		 * (즉, 가상 메서드를 다형성에 활용하면 특정 객체를 가리키는 참조 형에 상관 없이 항상 동일한
		 * 결과를 만들어내는 것이 가능하다.)
		 * 
		 * 순수 가상 메서드란?
		 * - 순수 가상 메서드 (또는 추상 메서드) 는 구현부가 존재하지 않는 메서드를 의미한다. (즉, 메서드
		 * 선언부만 존재하기 때문에 해당 메서드를 특정 클래스가 하나라도 지니고 있을 경우 해당 클래스는
		 * 객체화 시킬 수 없는 추상 클래스가 된다는 것을 알 수 있다.)
		 * 
		 * 따라서, 추상 클래스를 객체화 시키기 위해서는 해당 클래스를 상속한 자식 클래스에서 추상 메서드를
		 * 구현함으로써 간접적으로 객체화 시키는 것이 가능하다.
		 * 
		 * 만약, 자식 클래스에서도 부모 클래스에 존재하는 추상 메서드를 구현하지 않았을 경우 해당 클래스
		 * 또한 객체화 시킬 수 없는 추상 클래스가 되는 특징이 존재한다.
		 */
		/** 색상 */
		private enum EColor
		{
			NONE = -1,
			RED,
			GREEN,
			BLUE,
			MAX_VAL
		}

		/** 도형 */
		private abstract class CShape
		{
			protected EColor Color { get; private set; } = EColor.NONE;

			/** 생성자 */
			public CShape(EColor a_eColor)
			{
				this.Color = a_eColor;
			}

			/** 색상을 반환한다 */
			public string GetColor()
			{
				var oColors = new List<string>() {
					"빨간색", "녹색", "파란색"
				};

				return oColors[(int)this.Color];
			}

			/** 그린다 */
			public abstract void Draw();
		}

		/** 삼각형 */
		private class CTriangle : CShape
		{
			/** 생성자 */
			public CTriangle(EColor a_eColor) : base(a_eColor)
			{
				// Do Something
			}

			/** 그린다 */
			public override void Draw()
			{
				Console.WriteLine("{0} 삼각형을 그렸습니다.", this.GetColor());
			}
		}

		/** 사각형 */
		private class CRectangle : CShape
		{
			/** 생성자 */
			public CRectangle(EColor a_eColor) : base(a_eColor)
			{
				// Do Something
			}

			/** 그린다 */
			public override void Draw()
			{
				Console.WriteLine("{0} 사각형을 그렸습니다.", this.GetColor());
			}
		}

		/** 캔버스 */
		private class CCanvas
		{
			private List<CShape> m_oShapeList = new List<CShape>();

			/** 도형을 추가한다 */
			public void AddShape(CShape a_oShape)
			{
				m_oShapeList.Add(a_oShape);
			}

			/** 모든 도형을 그린다 */
			public void DrawAllShapes()
			{
				for(int i = 0; i < m_oShapeList.Count; ++i)
				{
					m_oShapeList[i].Draw();
				}
			}
		}

		/** 그림판 어플리케이션 */
		private class CPaintApp
		{
			/** 메뉴 */
			private enum EMenu
			{
				NONE = -1,
				ADD_TRIANGLE,
				ADD_RECTANGLE,
				DRAW_ALL_SHAPES,
				EXIT,
				MAX_VAL
			}

			private CCanvas m_oCanvas = new CCanvas();

			/** 구동시킨다 */
			public void Run()
			{
				EMenu eMenu = EMenu.NONE;

				do
				{
					this.PrintMenus();
					Console.Write("\n메뉴 선택 : ");

					eMenu = (EMenu)(int.Parse(Console.ReadLine()) - 1);

					switch(eMenu)
					{
						case EMenu.ADD_TRIANGLE:
						case EMenu.ADD_RECTANGLE:
						{
							var oShape = this.CreateShape(eMenu);
							m_oCanvas.AddShape(oShape);

							break;
						}
						case EMenu.DRAW_ALL_SHAPES:
							m_oCanvas.DrawAllShapes();
							break;
					}

					Console.WriteLine();
				} while(eMenu != EMenu.EXIT);
			}

			/** 메뉴를 출력한다 */
			private void PrintMenus()
			{
				Console.WriteLine("=====> 메뉴 <=====");
				Console.WriteLine("1. 삼각형 추가");
				Console.WriteLine("2. 사각형 추가");
				Console.WriteLine("3. 모든 도형 그리기");
				Console.WriteLine("4. 종료");
			}

			/*
			 * 팩토리 메서드란?
			 * - 특정 객체를 생성하는 역할을 담당하는 메서드를 의미한다. (즉, 팩토리 메서드를 활용하면
			 * 객체가 생성되는 과정에서 발생하는 초기화 등의 구문을 특정 메서드에서 작성함으로써 중복을
			 * 줄이는 것이 가능하다.)
			 */
			/** 도형을 생성한다 */
			private CShape CreateShape(EMenu a_eMenu)
			{
				var oRandom = new Random();
				var eColor = (EColor)oRandom.Next((int)EColor.RED, (int)EColor.MAX_VAL);

				switch(a_eMenu)
				{
					case EMenu.ADD_TRIANGLE:
						return new CTriangle(eColor);
					case EMenu.ADD_RECTANGLE:
						return new CRectangle(eColor);
				}

				return null;
			}
		}
#elif E01_CLASS_04
		/*
		 * 인터페이스란?
		 * - 특정 사물 간에 상호작용을 일으킬 수 있는 요소 (수단) 을 의미한다.
		 * (즉, 프로그래밍에서 인터페이스라는 것은 특정 클래스가 지니고 있는 메서드를
		 * 의미한다.)
		 * 
		 * 객체지향 프로그래밍은 사물 간에 상호작용을 통해서 프로그램의 목적을 달성하는
		 * 설계 방식이기 때문에 특정 명령 구문을 단순히 프로그래밍 관점으로 해석하는
		 * 것이 아니라 특정 대상 (의인화) 사람의 관점에 가깝게 끌어 올려서 사물과
		 * 사물간에 요청과 해당 요청에 결과를 처리하는 방향으로 프로그램의 구조를
		 * 설계 할 필요가 있다.
		 * 
		 * 따라서, 프로그래밍에서 인터페이스란 함수 (메서드) 의 목록을 의미한다.
		 * 
		 * C# 인터페이스 선언 방법
		 * - interface + 인터페이스 이름 + 인터페이스 맴버 (메서드)
		 * 
		 * Ex)
		 * interface IOutput {
		 *		void OutputDatas();
		 * }
		 * 
		 * 인터페이스는 단순한 함수 (메서드) 의 목록이기 때문에 해당 메서드의 구현부를
		 * 추가하는 것은 불가능하며 선언만 추가 할 수 있다.
		 * 
		 * 또한, 특정 클래스가 인터페이스를 따를 경우 해당 클래스에서는 반드시 인터페이스에
		 * 존재하는 모든 메서드를 구현해줘야한다.
		 * 
		 * 만약, 해당 클래스가 인터페이스에 존재하는 메서드 중 하나로 구현하지 않았을 경우
		 * 해당 클래스는 객체화 시키는 것이 불가능하다. (즉, 컴파일 에러가 발생한다.)
		 */
		/** 복사 인터페이스 */
		private interface ICloneable
		{
			/** 사본 객체를 반환한다 */
			object Clone();
		}

		/** 데이터 */
		private class CData : ICloneable
		{
			public int Val { get; set; } = 0;
			public string Str { get; set; } = string.Empty;

			/** 생성자 */
			public CData(int a_nVal, string a_oStr)
			{
				this.Val = a_nVal;
				this.Str = a_oStr;
			}

			/** 사본 객체를 반환한다 */
			public virtual object Clone()
			{
				/*
				 * 값 형식 데이터는 할당해주는 것으로 간단하게 사본을 만들어내는 것이
				 * 가능하지만 참조 형식 데이터는 단순히 할당만 할 경우 동일한 대상을
				 * 가리키는 얕은 복사가 이루어지기 때문에 참조 형식 데이터는 깊은
				 * 복사를 수행 할 수 있게 Clone 과 메서드를 사용 할 필요가 있다.
				 */
				return new CData(this.Val, (string)this.Str.Clone());
			}
		}
#endif // #if E01_CLASS_01

		/** 초기화 */
		public static void Start(string[] args)
		{
#if E01_CLASS_01
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
#elif E01_CLASS_02
			CBase oBase = new CBase();
			oBase.m_nVal = 10;
			oBase.m_fVal = 3.14f;

			CBase oDerived = new CDerived();
			oDerived.m_nVal = 20;
			oDerived.m_fVal = 3.14f;
			//oDerived.m_oStr = "자식 클래스";

			oBase.ShowInfo();

			Console.WriteLine("\n");
			oDerived.ShowInfo();
#elif E01_CLASS_03
			var oPaintApp = new CPaintApp();
			oPaintApp.Run();
#elif E01_CLASS_04
			var oData = new CData(10, "ABC");
			var oCloneDataA = (CData)oData.Clone();
			var oCloneDataB = oData;

			/*
			 * 사본 B 는 원본은 단순히 얕은 복사했기 때문에 사본 B 의 데이터를 변경
			 * 할 경우 원본에도 영향을 미치는 것을 알 수 있다.
			 * 
			 * 반면, 사본 A 는 깊은 복사를 수행했기 때문에 사본 A 의 데이터를 변경
			 * 해도 원본에는 전혀 영향을 미치지 않는다는 것을 알 수 있다.
			 */
			oCloneDataA.Val = 20;
			oCloneDataB.Val = 30;

			Console.WriteLine("=====> 원본 데이터 <=====");
			Console.WriteLine("{0}, {1}", oData.Val, oData.Str);

			Console.WriteLine("\n=====> 사본 데이터 - A <=====");
			Console.WriteLine("{0}, {1}", oCloneDataA.Val, oCloneDataA.Str);

			Console.WriteLine("\n=====> 사본 데이터 - B <=====");
			Console.WriteLine("{0}, {1}", oCloneDataB.Val, oCloneDataB.Str);
#endif // #if E01_CLASS_01
		}
	}
}
