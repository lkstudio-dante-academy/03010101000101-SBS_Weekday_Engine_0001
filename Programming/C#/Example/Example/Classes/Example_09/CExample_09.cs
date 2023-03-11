//#define E08_CLASS_01
//#define E08_CLASS_02
//#define E08_CLASS_03
#define E08_CLASS_04

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 클래스란?
 * - 데이터와 메서드의 집합을 표현하는 사용자 정의 자료형을 의미한다. (즉, 클래스를 활용하면 객체 지향 프로그래밍에서
 * 핵심이 되는 사물 (객체) 을 표현하는 것이 가능하다.)
 * 
 * 클래스는 객체 지향 프로그래밍에서 핵심이 되는 도구로써 특정 사물의 특징 (속성) 과 행위 (기능) 를 변수와 메서드로
 * 표현함으로써 사물 간에 관계를 통해 프로그램 구조를 설계하는 것이 가능하다.
 * 
 * C# 클래스 정의 방법
 * - class + 클래스 이름 + 클래스 맴버 (변수, 메서드)
 * 
 * Ex)
 * class CPCharacter {
 *		private int m_nHP;
 *		private int m_nLV;
 *		private int m_nATK;
 *		
 *		// 공격한다
 *		public void Attack(CMonster a_oTarget);
 *		
 *		// 점프한다
 *		public void Jump(Vector3 a_stVelocity);
 *		
 *		// 아이템을 획득한다
 *		public void GetItem(CItem a_oTarget);
 * }
 * 
 * CPCharacter oCharacterA = new CPCharacter();
 * CPCharacter oCharacterB = oCharacterA;
 * 
 * 상속이란?
 * - 특정 클래스가 지니고 있는 특징 (변수, 메서드) 를 물려주는 기능을 의미한다. (즉, 상속을 활용하면 특정 클래스가 지니고
 * 있는 기능을 확장하는 것이 가능하다.)
 * 
 * 또한, 특정 클래스를 상속했을 경우 해당 클래스와는 부모/자식의 관계를 형성하게 된다. 따라서, 자식 클래스는 부모 클래스가
 * 지니고 있는 변수와 메서드를 사용하는 것이 가능하다.
 * 
 * 단, C# 은 상속 할 부모 클래스를 하나만 지정 할 수 있다. (즉, 여러 부모 클래스를 지정하는 다중 상속을 C# 은 지원하지
 * 않는다.)
 * 
 * C# 클래스 상속 방법
 * - 자식 클래스 이름 + 부모 클래스 이름
 * 
 * Ex)
 * class CParent {
 *		// Do Somethong
 * }
 * 
 * class CChild : CParent {
 *		// Do Something
 * }
 * 
 * 위의 경우 Child 클래스는 Parent 클래스를 상속한다고 표현한다. (즉, 부모/자식 관계를 형성하게 된다.)
 * 따라서, Child 클래스는 Parent 클래스가 지니고 있는 맴버 (변수, 메서드) 를 사용하는 것이 가능하다.
 * 
 * 다형성이란?
 * - 특정 대상이 상황에 따라 다양한 형태를 지니는 개념을 의미한다. (즉, 객체 지향 프로그래밍에서는 상속과 가상 메서드를 이용해서 다형성을 흉내내는
 * 것이 가능하다.)
 * 
 * 특정 클래스가 상속 관계에 있을 때 자식 클래스 객체는 부모 클래스 형태로 참조하는 것이 가능하다. (즉, is a 의 관계가 성립되면 할당 가능한 것을
 * 알 수 있다.)
 * 
 * 이때, 부모 클래스의 형태는 하나지만 해당 형태에 어떤 대상을 참조시키냐에 따라서 여러 형태를 지니는 것이 가능하다. 따라서, 해당 특징을 이용한
 * 여러 클래스를 통해 구조를 설계 할 때 반복적으로 등장하는 구문을 단일화 시키는 것이 가능하다. (즉, 클래스 별로 제어하는 것이 아니라 해당 클래스들이
 * 상속하고 있는 공통 부모 클래스로 제어함으로써 구문을 단순화 시킬 수 있다.)
 * 
 * 다형성 관련 용어 정리
 * - 부모 클래스 -> 자식 클래스 : 다운 캐스팅
 * - 자식 클래스 -> 부모 클래스 : 업 캐스팅
 * 
 * 업 캐스팅은 안전한 반면 다운 캐스팅은 주의 할 필요가 있다. (즉, 클래스 상속 관계에 있을 때 자식 클래스 객체는 항상 부모를 포함하고 있지만
 * 부모 클래스 객체는 자식을 포함하고 있는지 확신 할 수 없기 때문에 다운 캐스팅을 할 때는 주의 할 필요가 있다.)
 * 
 * 따라서, 좀 더 안전한 다운 캐스팅을 하기 위해서는 is 또는 as 키워드를 사용하는 것이 좋다.
 * 
 * Ex)
 * class CParent { }
 * class CChild : CParent { }
 * 
 * CParent oParentA = new CChild();
 * CParent oParentB = new CParent();
 * 
 * oParentA is CChild		<- True
 * oParentB is CChild		<- False
 * 
 * oParentA as CChild		<- CChild 객체 참조 값
 * oParentB as CChild		<- null 값
 * 
 * 즉, is 키워드는 다운 캐스팅 또는 업 캐스팅이 가능 할 경우 해당 결과를 참 또는 거짓으로 돌려주는 반면 as 키워드는 해당 객체의 참조 값이 결과로
 * 반환된다는 것을 알 수 있다.
 * 
 * 따라서, is 키워드는 값 형식과 참조 형식에 모두 사용 할 수 있는 반면 as 키워드는 참조 형식 자료형에만 사용하는 것이 가능하다는 것을 알 수 있다.
 */
namespace Example.Classes.Example_09 {
	internal class CExample_09 {
#if E08_CLASS_01
		/*
		 * 접근 제어 지시자란?
		 * - 클래스 맴버 (변수, 메서드) 에 접근 할 수 있는 범위를 제한 할 수 있는 키워드를 의미한다. (즉, 접근 제어 지시자를
		 * 활용하면 클래스의 특정 맴버를 좀 더 안정하게 제어하는 것이 가능하다.)
		 * 
		 * 객체 지향 프로그래밍에서는 정보 은닉 (캡슐화), 상속, 다형성이라고하는 3 가지 요소가 존재하며 접근 제어 지시자는
		 * 정보 은닉과 연관되어 있다.
		 * 
		 * 즉, 정보 은닉은 클래스 맴버의 범위를 제한함으로써 특정 클래스를 제어 할 수 있는 수단을 제어하는 것을 의미한다.
		 * 
		 * 따라서, 정보 은닉 개념을 활용하면 외부에 노출하기 민감한 데이터를 좀 더 제한 된 방식으로 제어함으로써 안정성을
		 * 높히는 것이 가능하다.
		 * 
		 * C# 접근 제어 지시자 (한정자) 종류
		 * - public			<- 클래스 내부/외부에서 모두 접근 가능
		 * - private		<- 클래스 내부에서만 접근 가능
		 * - protected		<- 클래스 내부와 자식 클래스에서만 접근 가능
		 * 
		 * 객체 지향 프로그래밍에서 맴버 변수는 private, 맴버 메서드는 public 으로 제한하는 것이 일반적이 관례이다. 따라서,
		 * 클래스 외부에서 특정 객체의 맴버에 접근하기 위해서는 해당 역할을 수행하는 메서드를 별도로 구현해야하며 해당 메서드는
		 * 접근자 메서드라고 지칭된다. (즉, 특정 맴버 변수의 데이터를 가져오는 메서드는 Getter, 특정 맴버 변수의 데이터를
		 * 변경하는 것은 Setter 라고 한다.)
		 * 
		 * 또한, 클래스 맴버에 별도로 접근 제어 지시자를 명시하지 않을 경우 컴파일러에 의해서 자동으로 private 수준으로 지정
		 * 되는 특징이 존재한다.
		 */
		/** 플레이어블 캐릭터 */
		class CPCharacter {
			private int m_nLV;
			private int m_nHP;

			private string m_oName;

			/*
			 * 자동 구현 프로퍼티를 활용하면 프로퍼티를 좀 더 수월하게 구현하는 것이 가능하다. (즉, 자동 구현 프로퍼티를
			 * 활용하면 컴파일러에 의해서 맴버 변수가 자동으로 선언되기 때문에 외부에 공개 할 맴버 변수를 좀 더 수월하게
			 * 선언 및 제어하는 것이 가능하다.)
			 */
			public string ID { get; private set; }

			/*
			 * 프로퍼티란?
			 * - 클래스의 특정 맴버 변수에 접근하기 위해서 C# 에서 제공하는 기능을 의미한다. 객체 지향 프로그래밍에서
			 * 정통적인 방식으로는 특정 맴버 변수를 제어하기 위해서 접근자 함수 (메서드) 를 구현하는 것이 일반적인 관례이지만
			 * C# 에서는 프로퍼티를 활용함으로써 불필요한 접근자 함수 구현을 방지하는 것이 가능하다.
			 */
			public int LV {
				get {
					return m_nLV;
				} set {
					m_nLV = value;
				}
			}

			public int HP {
				get {
					return m_nHP;
				}
				set {
					m_nHP = value;
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
			 * 생성자란?
			 * - 객체가 생성 될 때 가장 처음 호출되는 메서드를 의미한다. (즉, 해당 생성자는 프로그래머가 명시적으로 호출
			 * 하는 것이 불가능하며 생성자는 객체가 생성 될 때 컴파일러에 의해서 자동으로 호출 된다는 것을 알 수 있다.)
			 * 
			 * 따라서, 생성자를 활용하면 객체를 생성과 동시에 특정 데이터로 맴버 변수를 초기화하는 것이 가능하다.
			 * 
			 * 또한, 객체가 생성되기 위해서는 반드시 생성자가 호출되어야하기 때문에 특정 클래스에 생성자가 존재하지 않을
			 * 경우 컴파일러에 의해서 자동으로 기본 생성자가 구현되는 특징이 존재한다.
			 * 
			 * 단, 프로그래머가 별도의 생성자를 구현했을 경우에는 컴파일러가 더이상 기본 생성자를 자동으로 구현해주지 않기
			 * 때문에 만약 기본 생성자가 필요 할 경우 명시적으로 구현해줘야한다.
			 */
			/** 생성자 */
			public CPCharacter() {
				// Do Something
			}

			/*
			 * 위임 생성자란?
			 * - 생성자 내부에서 다른 생성자를 호출 할 수 있는 기능을 의미한다. (즉, 위임 생성자를 활용하면 특정 객체를
			 * 초기화하는 과정을 통일 시키는 것이 가능하다.)
			 * 
			 * 따라서, 위임 생성자를 활용하면 객체가 생성되는 과정에서 발생하는 중복을 최소화하는 것이 가능하다.
			 * 
			 * default 키워드란?
			 * - default 키워드는 특정 자료형의 기본 값을 의미하며, 해당 키워드를 활용하면 변수를 선언 할 때 C# 내부에
			 * 설정되어있는 기본 값으로 설정하는 것이 가능하다.
			 * 
			 * 또한, C# 생성자는 맴버 변수를 default 값으로 설정하는 특징이 존재하기 때문에 특정 객체를 생성 후 아무 데이터도
			 * 설정하지 않았을 경우 자동으로 default 값으로 설정 된다는 것을 알 수 있다.
			 */
			/** 생성자 */
			public CPCharacter(int a_nLV, int a_nHP) : this(a_nLV, a_nHP, "Unknown") {
				// Do Something
			}

			/** 생성자 */
			public CPCharacter(int a_nLV, int a_nHP, string a_oName) {
				m_nLV = a_nLV;
				m_nHP = a_nHP;
				m_oName = a_oName;
			}

			/** 레벨을 반환한다 */
			public int GetLV() {
				return m_nLV;
			}

			/** 체력을 반환한다 */
			public int GetHP() {
				return m_nHP;
			}

			/** 이름을 반환한다 */
			public string GetName() {
				return m_oName;
			}

			/** 레벨을 변경한다 */
			public void SetLV(int a_nLV) {
				m_nLV = a_nLV;
			}

			/** 체력을 변경한다 */
			public void SetHP(int a_nHP) {
				m_nHP = a_nHP;
			}

			/** 식별자를 변경한다 */
			public void SetID(string a_oID) {
				ID = a_oID;
			}

			/** 이름을 변경한다 */
			public void SetName(string a_oName) {
				m_oName = a_oName;
			}

			/*
			 * 맴버 메서드는 맴버 변수에 접근 할 수 있는 특징이 존재하기 때문에 특정 맴버 메서드가 맴버 변수를 필요로
			 * 할 경우 해당 정보를 입력으로 전달하지 않아도 된다는 것을 알 수 있다. (즉, 클래스는 변수와 메서드의 집합이기
			 * 때문에 해당 구문 동작한다는 것을 알 수 있다.)
			 */
			/** 정보를 출력한다 */
			public void ShowInfo() {
				Console.WriteLine("=====> {0} 정보 <=====", m_oName);
				Console.WriteLine("ID : {0}, LV : {1}, HP : {2}", ID, m_nLV, m_nHP);
			}
		}
#elif E08_CLASS_02
		/** 부모 클래스 */
		class CParent {
			private int m_nVal = 0;
			protected float m_fVal = 0.0f;

			/** 생성자 */
			public CParent() {
				// Do Something
			}

			/** 생성자 */
			public CParent(int a_nVal, float a_fVal) {
				m_nVal = a_nVal;
				m_fVal = a_fVal;
			}

			/** 정보를 출력한다 */
			public void ShowInfo() {
				Console.WriteLine("정수 : {0}", m_nVal);
				Console.WriteLine("실수 : {0}", m_fVal);
			}
		}

		/*
		 * 좋은 상속 구조란?
		 * - is a 의 관계가 성립이 되면 해당 상속 구조는 올바른 상속 구조라고 할 수 있다. (즉, 객체 지향 프로그래밍에서 특정 클래스는 상속하는데
		 * 별다른 제한이 없기 때문에 잘못된 상속 구조를 통해 프로그램을 제작 할 경우 이후 유지보수를 진행하는데 어려움을 겪을 수 있다는 것을 
		 * 알 수 있다.)
		 * 
		 * 따라서, 특정 클래스를 상속 할 때에는 반드시 is a 의 관계가 성립되는지 검토를 할 필요가 있으며 해당 관계를 성립하지 않는다고 한다면
		 * 해당 상속은 잘못된 구조 일 확률이 높기 때문에 다른 방향으로 접근 할 필요가 있다.
		 * 
		 * 또한, has s 의 관계도 예외적으로 상속 구조에 포함되기는 하지만 해당 구조는 확장성과 유연성이 떨어지는 단점이 존재하기 때문에 별로
		 * 추천하지 않는 구조이다. (즉, is a 의 관계만 잘 지켜주면 된다는 것을 의미한다.)
		 */
		/** 자식 클래스 */
		class CChild : CParent {
			public string m_oStr = "";

			/*
			 * 부모 클래스의 생성자는 반드시 자식 클래스의 생성자에서 호출해 줄 필요가 있다. 만약, 부모 클래스의 생성자를 호출하는 구문을 따로
			 * 작성하지 않았을 경우 컴파일러에 의해서 자동으로 부모 클래스의 기본 생성자를 호출하는 구문이 추가되는 특징이 존재한다.
			 * 
			 * 따라서, 자식 클래스의 생성자를 구현 할 때는 반드시 적절한 부모 클래스의 생성자를 호출하고 있는지 주의 할 필요가 있으며 생성자의
			 * 호출 순서는 반드시 부모 클래스 -> 자식 클래스 순으로 이루어져야한다.
			 */
			/** 생성자 */
			public CChild(int a_nVal, float a_fVal, string a_oStr) : base(a_nVal, a_fVal) {
				m_oStr = a_oStr;
			}

			/*
			 * 부모 클래스의 맴버와 자식 클래스의 맴버가 서로 중복 될 경우 자식 클래스의 맴버가 더 높은 우선 순위를 지닌다. 단, 컴파일러는 해당
			 * 상황의 의도된 것인지 확신 할 수 없기 때문에 해당 경우 컴파일 경고가 발생한다.
			 * 
			 * 따라서, 해당 경고를 제거하기 위해서는 new 키워드를 맴버 앞에 명시해줘야한다. (즉, new 키워드를 클래스 맴버에 명시함으로써 부모
			 * 클래스의 맴버와 중복이 발생한다 하더라도 해당 상황이 의도 된 것이라고 컴파일러에게 알리는 것이 가능하다.)
			 */
			/** 정보를 출력한다 */
			public new void ShowInfo() {
				/*
				 * 부모 클래스의 맴버에 접근하기 위해서는 base 키워드를 사용하면 된다. (즉, base 키워드는 부모 클래스를 지칭하는 키워드라는
				 * 것을 알 수 있다.)
				 */
				base.ShowInfo();
				Console.WriteLine("문자열 : {0}", m_oStr);

				/*
				 * 부모 클래스의 private 맴버는 자식 클래스라 하더라도 함부로 접근하는 것이 불가능하다. (즉, private 은 가장 높은 보호 수준이기
				 * 때문에 자식 클래스에도 접근이 불가능하다는 것을 알 수 있다.)
				 * 
				 * 단, 직접적인 접근이 불가능 할 뿐이지 부모 클래스의 private 맴버도 자식 클래스에게 상속이 되기 때문에 부모 클래스가 지니고 있는
				 * 접근자 메서드 (프로퍼티) 등을 활용하면 간접적으로 부모 클래스의 private 맴버를 제어하는 것이 가능하다.
				 */
				//m_nVal = 0;
			}
		}
#elif E08_CLASS_03
		/** 부모 클래스 */
		public class CParent {
			/*
			 * 가상 메서드란?
			 * - 부모 클래스의 메서드가 호출 될 때 부모 클래스 대신 자식 클래스에  존재하는 메서드를 호출 할 수 있도록 해주는 기능을 의미한다.
			 * (즉, 동일한 대상이라 하더라도 해당 대상 (객체) 을 가리키는 자료형에 따라 메서드 호출 결과가 달라지는 문제를 해결하는 것이 가능하다.)
			 * 
			 * 따라서, 자식 클래스에서 부모 클래스의 가상 메서드를 오버라이드 하면 자식 클래스의 메서드가 부모 클래스의 메서드 대신에 호출되는
			 * 결과를 만들어낼 수 있다.
			 * 
			 * 바인딩이란?
			 * - 특정 주체와 상호 작용에 대한 결과가 결정되는 것을 바인딩이라고 한다. (즉, 메서드를 호출했을때 어떤 메서드가 동작 할지
			 * 결정되는 것을 예로 들 수 있다.)
			 * 
			 * 정적 바인딩 vs 동적 바인딩
			 * - 정적 바인딩은 바인딩이 컴파일 타임에 결정되는 것을 의미하며, 동적 바인딩은 바인딩이 런타임 (실행 중) 에 결정되는
			 * 것을 의미한다.
			 * 
			 * 따라서, 가상 메서드를 사용한다는 것은 해당 메서드가 호출 되었을 때 어떤 클래스의 메서드가 호출 될 지가 런타임에 결정
			 * 된다는 것을 의미한다.
			 */
			/** 정보를 출력한다 */
			public virtual void ShowInfo() {
				Console.WriteLine("CParent.ShowInfo 호출");
			}
		}

		/** 자식 클래스 */
		public class CChild : CParent {
			/** 정보를 출력한다 */
			public override void ShowInfo() {
				Console.WriteLine("CChild.ShowInfo 호출");
			}
		}
#elif E08_CLASS_04
		/*
		 * 클래스 변수 및 메서드란?
		 * - 일반적인 맴버 변수 및 메서드는 생성 된 객체에 종속되는 반면 클래스 변수 및 메서드는 클래스 자체에 종속되는 
		 * 특징이 존재한다.
		 * 
		 * 따라서, 맴버 변수 및 메서드는 객체를 생성해야지만 가능하지만 클래스 변수 및 메서드는 객체를 생성하지 않고도 사용
		 * 하는 것이 가능하다.
		 * 
		 * C# 클래스 변수 및 메서드 선언 방법
		 * - static + 변수 선언
		 * - static + 메서드 구현
		 */
		/** 전역 데이터 */
		class CGlobalData {
			public static int m_nVal = 0;
			public static float m_fVal = 0.0f;

			/** 정보를 출력한다 */
			public void ShowInfo() {
				Console.WriteLine("정수 : {0}, 실수 : {1}", m_nVal, m_fVal);
			}

			/** 정수 값을 추가한다 */
			public static void AddIntVal(int a_nVal) {
				m_nVal += a_nVal;
			}

			/** 실수 값을 추가한다 */
			public static void AddRealVal(float a_fVal) {
				m_fVal += a_fVal;
			}
		}
#endif // E08_CLASS_01

		/** 초기화 */
		public static void Start(string[] args) {
#if E08_CLASS_01
			/*
			 * 클래스는 사용자 정의 자료형이기 때문에 특정 클래스를 사용해서 변수를 선언하는 것이 가능하다. 이때, 특정
			 * 클래스를 통해 선언 된 변수는 객체라고 지칭되기 때문에 클래스를 통한 변수의 선언은 변수를 선언한다는 
			 * 표현보다 객체를 생성한다는 표현을 사용하는 것이 일반적인 관례이다.
			 * 
			 * 즉, 클래스는 객체를 생성하기 위한 틀의 개념이라는 것을 알 수 있다. 또한, 클래스는 사물의 특징을 표현하지만
			 * 구체적인 정보는 빠져 있으며 해당 정보는 객체의 생성을 통해 설정하는 것이 가능하다.
			 * 
			 * 객체의 특정 맴버에 접근하기 위해서는 . (맴버 지정 연산자) 를 사용하면 된다. (즉, 객체는 변수와 메서드를
			 * 포함하고 있기 때문에 특정 객체 하위에 존재하는 특정 맴버에 접근하기 위해서는 반드시 맴버 지정 연산자를
			 * 사용해야한다.)
			 */
			CPCharacter oCharacterA = new CPCharacter();
			oCharacterA.SetID("1");
			oCharacterA.LV = 1;
			oCharacterA.HP = 20;
			oCharacterA.Name = "캐릭터 A";

			CPCharacter oCharacterB = new CPCharacter(20, 850);
			oCharacterB.SetID("2");

			CPCharacter oCharacterC = new CPCharacter(40, 1500, "캐릭터 C");
			oCharacterC.SetID("3");

			Console.WriteLine("=====> {0} 정보 <=====", oCharacterA.Name);
			Console.WriteLine("ID : {0}, LV : {1}, HP : {2}\n", oCharacterA.ID, oCharacterA.LV, oCharacterA.HP);

			oCharacterB.ShowInfo();

			Console.WriteLine();
			oCharacterC.ShowInfo();

#elif E08_CLASS_02
			var oParent = new CParent(10, 3.14f);
			var oChild = new CChild(20, 3.14f, "Hello, World!");

			/*
			 * protected 보호 수준은 클래스 내부와 자식 클래스에서만 접근 가능하기 때문에 해당 영역 이외에서는 접근이 불가능하다는 것을
			 * 알 수 있다.
			 */
			//oParent.m_fVal = 0.0f;

			Console.WriteLine("=====> 부모 정보 <=====");
			oParent.ShowInfo();

			Console.WriteLine("\n=====> 자식 정보 <=====");
			oChild.ShowInfo();
#elif E08_CLASS_03
			CParent oParentA = new CParent();
			CParent oParentB = new CChild();

			CChild oChild = oParentB as CChild;

			Console.WriteLine("=====> Parent A 호출 <=====");
			oParentA.ShowInfo();

			Console.WriteLine("\n=====> Parent B 호출 <=====");
			oParentB.ShowInfo();

			Console.WriteLine("\n=====> Child 호출 <=====");
			oChild.ShowInfo();
#elif E08_CLASS_04
			var oGlobalDataA = new CGlobalData();
			var oGlobalDataB = new CGlobalData();

			CGlobalData.AddIntVal(10);
			CGlobalData.AddRealVal(10.0f);

			/*
			 * 클래스 변수는 모든 객체가 공유하는 변수이기 때문에 해당 변수의 데이터가 변경되면 모든 객체가 영향을 받는 특징이
			 * 존재한다. (즉, 전역 변수의 개념과 유사하다.)
			 */
			Console.WriteLine("=====> 전역 데이터 <=====");
			Console.WriteLine("정수 : {0}, 실수 : {1}", CGlobalData.m_nVal, CGlobalData.m_fVal);

			Console.WriteLine("\n=====> 전역 데이터 A <=====");
			oGlobalDataA.ShowInfo();

			Console.WriteLine("\n=====> 전역 데이터 B <=====");
			oGlobalDataB.ShowInfo();
#endif // E08_CLASS_01
		}
	}
}
