//#define T01_01
//#define T01_02
//#define T01_03
//#define T01_04
//#define T01_05
#define T01_06

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Classes.Training_01 {
	internal class CTraining_01 {
#if T01_01

#elif T01_02

#elif T01_03

#elif T01_04

#elif T01_05

#elif T01_06

#endif // #if T01_01

		/** 초기화 */
		public static void Start(string[] args) {
#if T01_01
			/*
			 * 연습 문제 1 - 1
			 * - 정수를 입력 받아 홀수/짝수 여부를 판단하기 (단, % 연산자 사용 불가)
			 * 
			 * Ex)
			 * 정수 입력 : 10
			 * 결과 : 짝수
			 */
			Console.Write("정수 입력 : ");
			int nVal = int.Parse(Console.ReadLine());

			Console.WriteLine("결과: {0}", (nVal & 0x01) != 0 ? "홀수" : "짝수"); 
#elif T01_02
			/*
			 * 연습 문제 1 - 2
			 * - 정수 2 개를 입력 받아 해당 데이터를 교환하기
			 * - 단, 단순히 출력 순서만 변경하는 것이 아니라 실제 변수에 있는 데이터가 서로 교환되어야한다
			 * 
			 * Ex)
			 * 정수 (2 개) 입력 : 10 20
			 * 
			 * =====> 교환 전 <=====
			 * 10, 20
			 * 
			 * =====> 교환 후 <=====
			 * 20, 10
			 */
			Console.Write("정수 (2 개) 입력 : ");
			var oTokens = Console.ReadLine().Split();

			int nVal01 = int.Parse(oTokens[0]);
			int nVal02 = int.Parse(oTokens[1]);

			Console.WriteLine("=====> 교환 전 <=====");
			Console.WriteLine("{0}, {1}", nVal01, nVal02);

			int nTemp = nVal01;
			nVal01 = nVal02;
			nVal02 = nTemp;

			Console.WriteLine("\n=====> 교환 후 <=====");
			Console.WriteLine("{0}, {1}", nVal01, nVal02);
#elif T01_03
			/*
			 * 연습 문제 1 - 3
			 * - 정수 2 개를 입력 받아 &&, || 연산자 결과 출력하기 (단, 해당 연산자를 직접적으로 사용 불가)
			 * 
			 * Ex)
			 * 정수 (2 개) 입력 : 0 10
			 * 
			 * =====> 결과 <=====
			 * 0 && 10 = False
			 * 0 || 10 = True
			 */
			Console.Write("정수 (2 개) 입력 : ");
			var oTokens = Console.ReadLine().Split();

			int nVal01 = int.Parse(oTokens[0]);
			int nVal02 = int.Parse(oTokens[1]);

			Console.WriteLine("=====> 결과 <=====");
			Console.WriteLine("{0} && {1} = {2}", nVal01, nVal02, (nVal01 * nVal02) != 0);
			Console.WriteLine("{0} || {1} = {2}", nVal01, nVal02, (nVal01 + nVal02) != 0);
#elif T01_04
			/*
			 * 연습 문제 1 - 4
			 * - 정수를 입력 받아 홀수/짝수를 구분해서 출력하기 (단, 0 을 입력하지 않으면 계속 정수를 입력
			 * 받는다)
			 * - 입력 받은 정수 중에 홀수는 왼쪽부터, 짝수는 오른쪽부터 차례대로 위치시킨다
			 * 
			 * Ex)
			 * 정수 입력 : 1
			 * 정수 입력 : 2
			 * 정수 입력 : 5
			 * 정수 입력 : 4
			 * 정수 입력 : 7
			 * 정수 입력 : 0
			 * 
			 * =====> 결과 <=====
			 * 1, 5, 7, 4, 2
			 */
			int nVal = 0;
			int nNumOddVals = 0;
			int nNumEvenVals = 0;

			var oVals = new int[5];

			do {
				Console.Write("정수 {0} 입력 : ", (nNumOddVals + nNumEvenVals) + 1);
				nVal = int.Parse(Console.ReadLine());

				// 올바른 값 일 경우
				if(nVal != 0) {
					// 배열이 가득 찼을 경우
					if(nNumOddVals + nNumEvenVals >= oVals.Length) {
						var oNewVals = new int[oVals.Length * 2];

						for(int i = 0; i < nNumOddVals; ++i) {
							oNewVals[i] = oVals[i];
						}

						for(int i = nNumEvenVals - 1; i >= 0 ; --i) {
							oNewVals[oNewVals.Length - (i + 1)] = oVals[oVals.Length - (i + 1)];
						}

						oVals = oNewVals;
					}

					// 홀수 일 경우
					if(nVal % 2 != 0) {
						oVals[nNumOddVals] = nVal; nNumOddVals += 1;
					} else {
						oVals[oVals.Length - (nNumEvenVals + 1)] = nVal; nNumEvenVals += 1;
					}
				}
			} while(nVal != 0);

			Console.WriteLine("\n=====> 결과 <=====");

			for(int i = 0; i < nNumOddVals; ++i) {
				Console.Write("{0}, ", oVals[i]);
			}

			for(int i = nNumEvenVals - 1; i >= 0; --i) {
				Console.Write("{0}, ", oVals[oVals.Length - (i + 1)]);
			}
#elif T01_05
			/*
			 * 연습 문제 1 - 5
			 * - 야구 게임 제작하기
			 * - 프로그램이 시작하면 0 ~ 9 사이에 존재하는 숫자 4 개를 생성한다 (단, 각 숫자는 중복되지 않는다)
			 * - 4 개의 숫자를 입력 받아 Ball 또는 Strike 여부를 검사한다
			 * - 입력한 숫자가 처음 생성한 숫자 중에 존재 할 경우 Ball
			 * - 순서까지 일치 할 경우 Strike
			 * - 4 Strike 가 되면 게임 종료
			 * 
			 * Ex)
			 * 생성 한 숫자 : 1, 5, 3, 2
			 * 
			 * 숫자 (4 개) 입력 : 1, 3, 5, 0
			 * 결과 : 1 Strike, 2 Ball
			 * 
			 * 숫자 (4 개) 입력 : 1, 5, 3, 0
			 * 결과 : 3 Strike, 0 Ball
			 * 
			 * 숫자 (4 개) 입력 : 1, 5, 3, 2
			 * 결과 : 4 Strike
			 * 
			 * 게임을 종료합니다
			 */
			int nNumVals = 0;

			var oRandom = new Random((int)DateTime.Now.Ticks);
			var oAnswerVals = new int[4];

			while(nNumVals < oAnswerVals.Length) {
				int i = 0;
				int nVal = oRandom.Next(0, 9);

				for(i = 0; i < nNumVals && oAnswerVals[i] != nVal; ++i) {
					continue;
				}

				// 값이 없을 경우
				if(i >= nNumVals) {
					oAnswerVals[nNumVals++] = nVal;
				}
			}

			Console.Write("생성 한 숫자 : ");

			for(int i = 0; i < oAnswerVals.Length; ++i) {
				Console.Write("{0} ", oAnswerVals[i]);
			}

			Console.WriteLine();

			int nBallCount = 0;
			int nStrikeCount = 0;

			do {
				Console.Write("\n숫자 (4 개) 입력 : ");
				var oTokens = Console.ReadLine().Split();

				nBallCount = 0;
				nStrikeCount = 0;

				for(int i = 0; i < oTokens.Length; ++i) {
					int j = 0;
					int nVal = int.Parse(oTokens[i]);

					for(j = 0; j < oAnswerVals.Length && oAnswerVals[j] != nVal; ++j) {
						continue;
					}

					nBallCount += (i != j && j < oAnswerVals.Length) ? 1 : 0;
					nStrikeCount += (i == j && j < oAnswerVals.Length) ? 1 : 0;
				}

				Console.WriteLine("결과 : {0} Strike, {1} Ball", nStrikeCount, nBallCount);
			} while(nStrikeCount < oAnswerVals.Length);
#elif T01_06
			/*
			 * 연습 문제 1 - 6
			 * - 행, 열 개수를 입력 받은 후 달팽이 형태로 2 차원 배열 초기화히기
			 * 
			 * Ex)
			 * 행, 열 개수 입력 : 5 4
			 * 
			 * =====> 배열 요소 <=====
			 *  1  2  3  4
			 * 14 15 16  5
			 * 13 20 17  6
			 * 12 19 18  7
			 * 11 10  9  8
			 */
			Console.Write("행, 열 개수 입력 : ");
			var oTokens = Console.ReadLine().Split();

			int nNumRows = int.Parse(oTokens[0]);
			int nNumCols = int.Parse(oTokens[1]);

			int i = 0;
			int j = -1;

			int nVal = 0;
			int nDirection = 1;

			var oVals = new int[nNumRows, nNumCols];

			while(nVal < oVals.Length) {
				for(int k = 0; k < nNumCols; ++k) {
					j += nDirection;
					oVals[i, j] = nVal++;
				}

				nNumRows -= 1;

				for(int k = 0; k < nNumRows; ++k) {
					i += nDirection;
					oVals[i, j] = nVal++;
				}

				nNumCols -= 1;
				nDirection = -nDirection;
			}

			Console.WriteLine("\n=====> 결과 <=====");

			for(i = 0; i < oVals.GetLength(0); ++i) {
				for(j = 0; j < oVals.GetLength(1); ++j) {
					Console.Write("{0,4}", oVals[i, j]);
				}

				Console.WriteLine();
			}
#endif // #if T01_01
		}
	}
}
