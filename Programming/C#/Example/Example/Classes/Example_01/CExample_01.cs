using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 프로그래밍 언어란?
 * - 컴퓨터에게 명령을 내리기 위한 명령문을 작성 할 수 있는 언어를 의미한다. (즉, 
 * 프로그래밍 언어를 활용하면 컴퓨터가 특정 동작을 수행하도록 명령을 내릴 수 있는
 * 프로그램을 제작하는 것이 가능하다.)
 * 
 * 단, 컴퓨터는 0 과 1 로 이루어진 기계어 (네이티브 코드) 만을 이해 할 수 있기 때문에
 * C# 과 같은 고급 언어로 작성 된 명령어를 컴퓨터가 바로 처리하는 것은 불가능하다.
 * 
 * 따라서, 기계어 이외의 언어로 작성 된 명령어를 기계어로 변환 시켜 줄 필요가 있으며,
 * 해당 작업은 컴파일러에 의해서 수행된다. (즉, 컴파일러는 사용자 (프로그래머) 와 컴퓨터
 * 사이에 존재하는 통역사와 같은 역할을 수행한다.)
 */
namespace Example.Classes.Example_01 {
	class CExample_01 {
		/** 초기화 */
		public static void Start(string[] args) {
			/*
			 * Console 클래스는 콘솔 프로그램을 대상으로 특정 데이터를 출력하거나
			 * 입력 받는 역할을 수행한다. (즉, Console 클래스를 활용하면 원하는
			 * 문장을 화면 상에 출력하는 것이 가능하다.)
			 * 
			 * Console.WriteLine 메서드는 주어진 문장을 화면 상에 출력하는 역할을
			 * 수행하는 메서드이다.
			 * 
			 * 클래스란?
			 * - 특정 속성 (변수) 과 기능 (메서드) 를 하나로 그룹화시키는 기능을 의미한다.
			 * (즉, 클래스를 활용하면 연관 된 기능을 하나로 묶어서 관리하는 것이 가능하다.)
			 */
			Console.WriteLine("Hello, World!");

			/*
			 * Console.ReadLine 메서드는 콘솔 프로그램으로부터 특정 문장을 입력 받는
			 * 역할을 수행한다. (즉, 프로그램이 구동 중에 사용자로부터 특정 입력이 
			 * 필요 할 경우 해당 메서드를 활용하면 된다.)
			 * 
			 * \n 문자는 이스케이프 코드 중 하나로써 개행 역할을 수행하는 문자이다.
			 * 즉, 한줄로 문장을 작성했다하더라도 해당 문자를 활용하면 여러 줄에 걸쳐서
			 * 문장을 출력하는 것이 가능하다.
			 */
			Console.Write("\n문장 1 입력 : ");
			string oStr01 = Console.ReadLine();

			Console.Write("문장 2 입력 : ");
			string oStr02 = Console.ReadLine();

			Console.WriteLine("\n입력 된 문장 1 : {0}", oStr01);
			Console.WriteLine("입력 된 문장 2 : {0}", oStr02);
		}
	}
}
