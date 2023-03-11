//#define E05_IF_ELSE
#define E05_SWITCH_CASE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 조건문이란?
 * - 프로그램의 흐름을 특정 조건에 따라 분기 시킬 수 있는 기능을 의미한다. (즉, 조건문을
 * 활용하면 특정 조건에 맞춰 다양한 결과를 만들어내는 프로그램을 제작하는 것이 가능하다.)
 * 
 * C# 조건문 종류
 * - if ~ else
 * - switch ~ case
 * 
 * Ex)
 * if(조건 1) {
 *		// 조건 1 을 만족했을 때 실행 할 명령어
 * } else if(조건 2) {
 *		// 조건 2 를 만족했을 때 실행 할 명령어
 * } else {
 *		// 조건 1 과 2 를 모두 만족하지 않았을 경우 실행 할 명령어
 * }
 * 
 * switch(비교 값) {
 * case 조건 1: {
 *		// 조건 1 을 만족했을 때 실행 할 명령어
 * } break;
 * case 조건 2: {
 *		// 조건 2 를 만족했을 때 실행 할 명령어
 * } break;
 * default: {
 *		// 조건 1 과 2 를 모두 만족하지 않았을 경우 실행 할 명령어
 * }
 * }
 */
namespace Example.Classes.Example_05 {
	class CExample_05 {
		/** 초기화 */
		public static void Start(string[] args) {
#if E05_IF_ELSE
			Console.Write("점수 입력 : ");
			int nScore = int.Parse(Console.ReadLine());

			if(nScore < 60) {
				Console.WriteLine("F 학점입니다.");
			} else {
				if(nScore >= 90) {
					Console.WriteLine("A 학점입니다.");
				} else if(nScore >= 80) {
					Console.WriteLine("B 학점입니다.");
				} else if(nScore >= 70) {
					Console.WriteLine("C 학점입니다.");
				} else {
					Console.WriteLine("D 학점입니다.");
				}
			}
#elif E05_SWITCH_CASE
			Console.Write("점수 입력 : ");
			int nScore = int.Parse(Console.ReadLine());

			/*
			 * break 키워드는 switch ~ case 또는 반복문 내부에서 사용하는 것이 가능하며,
			 * 해당 키워드의 역할은 프로그램의 흐름을 해당 키워드를 감싸하고 있는 제어문
			 * 밖으로 이동시키는 것이다. (즉, 프로그램의 흐름을 중단 시키고 싶을 경우
			 * 해당 키워드를 사용하는 것이 가능하다.)
			 * 
			 * 또한, C# switch ~ case 는 break 키워드를 반드시 명시하도록 제한하는
			 * 특징이 존재한다. (즉, C 와 같은 switch ~ case 의 특징을 이용한 테크닉은
			 * C# 에서 사용하는 것이 제한적이라는 것을 의미한다.)
			 */
			switch(nScore / 10) {
				case 9: case 10: Console.WriteLine("A 학점입니다."); break;
				case 8: Console.WriteLine("B 학점입니다."); break;
				case 7: Console.WriteLine("C 학점입니다."); break;
				case 6: Console.WriteLine("D 학점입니다."); break;
				default: Console.WriteLine("F 학점입니다."); break;
			}
#endif // #if E05_IF_ELSE
		}
	}
}
