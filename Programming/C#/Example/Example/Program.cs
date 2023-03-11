using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * C# 학습 주제
 * - Example_01 (C# 기초)
 * - Example_02 (자료형, 변수/상수)
 * - Example_03 (데이터 표현 방법)
 * - Example_04 (연산자)
 * - Example_05 (조건문)
 * - Example_06 (반복문)
 * - Example_07 (컬렉션)
 * - Example_08 (메서드)
 * - Example_09 (클래스)
 * - Example_10 (제네릭)
 * - Example_11 (델리게이트)
 */
namespace Example {
	class Program {
		/*
		 * 메인 메서드란?
		 * - C# 으로 제작 된 프로그램이 실행 될 때 가장 먼저 호출 (실행) 되는 메서드를
		 * 의미한다. (즉, 메인 메서드가 실행 되었다는 것은 프로그램이 구동 되었다는
		 * 것을 의미한다.)
		 * 
		 * 또한, 메인 메서드가 종료되면 프로그램도 종료되는 특징이 존재한다. 따라서,
		 * C# 으로 프로그램을 제작 할 때는 반드시 메인 메서드를 구현해야한다.
		 * 
		 * 만약, 메인 메서드를 구현하지 않았을 경우 작성 된 프로그램에서 어떤 부분을
		 * 먼저 실행해야하는지 구분 하는 것이 불가능하기 때문에 프로그램 제작 자체가
		 * 안된다.
		 * 
		 * 주석이란?
		 * - 컴퓨터가 아닌 사용자 (프로그래머) 를 위한 기능으로 메모를 작성 할 수 있는
		 * 기능을 의미한다. (즉, 주석을 활용하면 특정 명령어를 이해하는데 필요한 개념들을
		 * 정리하는 것이 가능하다.)
		 * 
		 * 메서드란?
		 * - 정해진 특정 역할을 수행하는 기능을 의미한다. (즉, 메서드를 호출하면 해당 메서드에
		 * 존재하는 명령어들이 실행된다는 것을 의미한다.)
		 */
		static void Main(string[] args) {
			/*
			 * Visual Studio 주석 관련 단축키
			 * - Ctrl + K, C (주석 처리)
			 * - Ctrl + K, U (주석 해제)
			 */
			//Classes.Example_01.CExample_01.Start(args);
			//Classes.Example_02.CExample_02.Start(args);
			//Classes.Example_03.CExample_03.Start(args);
			//Classes.Example_04.CExample_04.Start(args);
			//Classes.Example_05.CExample_05.Start(args);
			//Classes.Example_06.CExample_06.Start(args);
			//Classes.Example_07.CExample_07.Start(args);
			//Classes.Example_08.CExample_08.Start(args);
			//Classes.Example_09.CExample_09.Start(args);
			//Classes.Example_10.CExample_10.Start(args);
			Classes.Example_11.CExample_11.Start(args);

			Console.ReadKey();
		}
	}
}
