//#define E06_WHILE
#define E06_FOR
#define E06_DO_WHILE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 반복문이란?
 * - 특정 조건에 따라 프로그램의 특정 부분을 반복해서 실행 할 수 있는 기능을 의미한다. (즉, 반복문을 활용
 * 하면 프로그램의 흐름을 특정 조건에 맞춰서 반복 시키는 것이 가능하다.)
 * 
 * 따라서, 반복문을 사용 할 때는 반복의 조건이 깨질 수 있도록 구문을 작성 할 필요가 있다. 만약, 반복을 깨기
 * 위한 조건을 만족 하지 못 할 경우 프로그램은 특정 부분을 무한히 실행하는 무한 상태가 된다.
 * (즉, 반복문을 작성 할 때는 일반적으로 해당 반복문이 종료 되도록 구문을 작성하는 것이 중요하다.)
 * 
 * C# 반복문 종류
 * - while
 * - for
 * - do ~ while
 * 
 * Ex)
 * while(조건절) {
 *      // 조건절이 참 일 경우 실행 할 명령어
 * }
 * 
 * for(초기절; 조건절; 반복절) {
 *      // 조건절이 참 일 경우 실행 할 명령어
 * }
 * 
 * do {
 *      // 조건절이 참 일 경우 실행 할 명령어
 * } while(조건절);
 * 
 * 사전 판단 반복문 vs 사후 판단 반복문
 * - 사전 판단 반복문은 반복 할 명령어들을 실행하기 전에 조건을 먼저 검사하기 때문에 처음부터 해당 조건이
 * 거짓이라면 해당 반복문의 명령어들은 한번도 실행되지 않는 특징이 존재한다. 반면, 사후 판단 반복문은 반복 조건을
 * 반복 할 명령어들이 모두 실행 된 후에 검사를 하기 때문에 처음부터 조건이 거짓이라 하더라도 반드시 한번 이상은
 * 실행 된다는 것을 알 수 있다.
 */
namespace Example.Classes.Example_06 {
	class CExample_06 {
		/** 초기화 */
		public static void Start(string[] args) {
#if E06_WHILE
			Console.Write("정수 입력 (양수) : ");

			int i = 0;
			int nTimes = int.Parse(Console.ReadLine());

			while(i < nTimes) {
				if((i + 1) % 2 == 0) {
					i += 1;

					/*
					 * continue 반복문 내부에서 사용가능한 키워드이며, 해당 키워드는 프로그램의 현재 흐름을
					 * 생략하고 다음 흐름으로 이동시키는 특징이 존재한다.
					 * 
					 * 따라서, while 반복문에 해당 키워드를 사용 할 경우 무한 루프에 빠지지 않도록 주의가
					 * 필요하다. (즉, while 반복문은 반복 할 명령어들 중 반복 조건을 거짓으로 만드는 구문이
					 * 포함되어있기 때문이다.)
					 */
					continue;
				}

				Console.Write("{0}, ", i + 1);
				i += 1;
			}

			Console.WriteLine("\n\n반복문이 종료되었습니다.");
#elif E06_FOR
			Console.Write("정수 입력 (양수) : ");
			int nTimes = int.Parse(Console.ReadLine());

			for(int i = 0; i < nTimes; ++i) {
				/*
				 * for 반복문은 while 반복문과 달리 반복 구문 내에서 continue 키워드를 사용해도 반복 조건을
				 * 깨기 위한 반복절이 항상 실행되기 때문에 while 반복문에 비해 좀 더 사용자 (프로그래머) 의
				 * 실수를 줄여주는 특징이 존재한다.
				 */
				if((i + 1) % 2 == 0) {
					continue;
				}

				Console.Write("{0}, ", i + 1);
			}

			Console.WriteLine("\n\n=====> 구구단 <=====");

			for(int i = 2; i < 10; ++i) {
				Console.WriteLine("// {0} 단", i);

				for(int j = 1; j < 10; ++j) {
					Console.WriteLine("{0} * {1} = {2}", i, j, i * j);
				}

				Console.WriteLine();
			}

			Console.WriteLine("반복문이 종료되었습니다.");
#elif E06_DO_WHILE
			do {
				Console.WriteLine("do ~ while 반복문 실행!");
			} while(false);
#endif // #if E06_WHILE
		}
	}
}
