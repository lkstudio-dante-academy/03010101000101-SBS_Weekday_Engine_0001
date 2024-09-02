//#define P01_01
//#define P01_02
//#define P01_03
#define P01_04

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example._03010101000101_SBS_Weekday_Engine_0001.E01.Practice.Classes.Runtime.Practice_01 {
	/** Practice 1 */
	class CP01Practice_01 {
#if P01_01

#elif P01_02
		/** 정답을 설정한다 */
		private static void SetupAnswers(int[] a_oAnswer, int a_nMin, int a_nMax) {
			var oRandom = new Random();
			int nNumVals = 0;

			while(nNumVals < a_oAnswer.Length) {
				int j = 0;
				int nVal = oRandom.Next(a_nMin, a_nMax + 1);

				for(j = 0; j < nNumVals && nVal != a_oAnswer[j]; ++j) {
					continue;
				}

				// 값이 없을 경우
				if(j >= nNumVals) {
					a_oAnswer[nNumVals++] = nVal;
				}
			}
		}
#elif P01_03
		/** 선택 */
		private enum ESel {
			NONE = -1,
			ROCK,
			SCISSORS,
			PAPER,
			MAX_VAL
		}

		/** 결과 */
		private enum EResult {
			NONE = -1,
			WIN,
			DRAW,
			LOSE,
			MAX_VAL
		}

		/** 선택 문자열을 반환한다 */
		private static string GetSelStr(ESel a_eSel) {
			var oStrs = new string[] {
				"바위", "가위", "보"
			};

			return oStrs[(int)a_eSel];
		}

		/** 결과 문자열을 반환한다 */
		private static string GetResultStr(EResult a_eResult) {
			var oStrs = new string[] {
				"이겼습니다.", "비겼습니다.", "졌습니다."
			};

			return oStrs[(int)a_eResult];
		}

		/** 결과를 반환한다 */
		private static EResult GetResult(ESel a_eUser, ESel a_eComputer) {
			var oResults = new EResult[,] {
				{ EResult.DRAW, EResult.WIN, EResult.LOSE },
				{ EResult.LOSE, EResult.DRAW, EResult.WIN },
				{ EResult.WIN, EResult.LOSE, EResult.DRAW }
			};


			return oResults[(int)a_eUser, (int)a_eComputer];
		}
#elif P01_04
		/** 정답 여부를 검사한다 */
		private static bool IsAnswer(char[] a_oAnswer, char[] a_oInputWord) {
			for(int i = 0; i < a_oAnswer.Length && i < a_oInputWord.Length; ++i) {
				// 문자가 다를 경우
				if(a_oAnswer[i] != a_oInputWord[i]) {
					return false;
				}
			}

			return a_oAnswer.Length == a_oInputWord.Length;
		}

		/** 단어를 출력한다 */
		private static void PrintWord(char[] a_oWord) {
			for(int i = 0; i < a_oWord.Length; ++i) {
				Console.Write("{0} ", a_oWord[i]);
			}

			Console.WriteLine();
		}
#endif // #if P01_01

		/** 초기화 */
		public static void Start(string[] args) {
#if P01_01
			/*
			 * 업/다운 게임 제작
			 * - 게임 시작 시 1 ~ 99 사이에 정답 랜덤하게 생성
			 * - 숫자를 입력 받아서 정답과 일치 할 경우 게임 종료
			 * - 정답이 아닐 경우는 입력 한 숫자와 비교해서 힌트 출력
			 * 
			 * Ex)
			 * 정답 : 65
			 * 
			 * 숫자 입력 : 33
			 * 정답은 33 보다 큽니다.
			 * 
			 * 숫자 입력 : 89
			 * 정답은 89 보다 작습니다.
			 * 
			 * 숫자 입력 : 65
			 * 정답입니다.
			 */
			var oRandom = new Random();
			int nAnswer = oRandom.Next(1, 100);

			Console.WriteLine("정답 : {0}\n", nAnswer);

			while(true) {
				/*
				 * int.TryParse 메서드는 숫자로 이루어진 문자열을 정수로 변환하는
				 * 역할을 수행한다. 단, 문자열 중에 숫자가 아닌 문자가 존재 할 경우
				 * 숫자로 변환하는 것이 불가능하기 때문에 해당 메서드는 반환 값으로
				 * 변환이 성공했는지 여부를 나타내는 bool 데이터를 반환한다.
				 * 
				 * 즉, 반환 값이 참이면 변환에 성공한 것이고 거짓이라면 숫자가 아닌
				 * 문자가 문자열에 포함되어있다는 것을 의미한다.
				 * 
				 * 따라서, 문자열을 안전하게 정수로 변환하기 위해서는 int.Parse 보다
				 * int.TryParse 메서드를 사용하는 것을 추천한다.
				 */
				Console.Write("숫자 입력 : ");
				int.TryParse(Console.ReadLine(), out int nVal);

				// 정답 일 경우
				if(nVal == nAnswer) {
					Console.WriteLine("정답입니다.");
					break;
				} else {
					Console.WriteLine("정답은 {0} 보다 {1}",
						nVal, (nVal < nAnswer) ? "큽니다." : "작습니다.");
				}

				Console.WriteLine();
			}
#elif P01_02
			/*
			 * 야구 게임 제작
			 * - 게임 시작 시 1 ~ 9 범위에 있는 숫자 중 중복되지 않는 수 4 개 생성
			 * - 사용자로부터 4 개의 숫자를 입력 받은 후 Strike 또는 Ball 여부 판정
			 * - 사용자가 입력한 숫자가 정답에 들어있고 위치도 같다면 Strike
			 * - 정답에 들어있지만 위치가 다르다면 Ball
			 * - 4 Strike 를 달성하면 게임 종료
			 * 
			 * Ex)
			 * 정답 : 4, 2, 5, 9
			 * 
			 * 숫자 (4 개) 입력 : 4 2 9 6
			 * 결과 : 2 Strike, 1 Ball
			 * 
			 * 숫자 (4 개) 입력 : 4 2 5 9
			 * 결과 : 4 Strike, 0 Ball
			 * 
			 * 게임을 종료합니다.
			 */
			var oAnswer = new int[4];
			SetupAnswers(oAnswer, 1, 9);

			int nNumBalls = 0;
			int nNumStrikes = 0;

			Console.Write("정답 : ");

			for(int i = 0; i < oAnswer.Length; ++i) {
				Console.Write("{0}, ", oAnswer[i]);
			}

			Console.WriteLine();

			do {
				Console.Write("숫자 (4 개) 입력 : ");

				nNumBalls = 0;
				nNumStrikes = 0;

				var oVals = new int[oAnswer.Length];
				var oTokens = Console.ReadLine().Split();

				for(int i = 0; i < oVals.Length && i < oTokens.Length; ++i) {
					int.TryParse(oTokens[i], out oVals[i]);
				}

				for(int i = 0; i < oVals.Length; ++i) {
					int j = 0;

					for(j = 0; j < oAnswer.Length && oVals[i] != oAnswer[j]; ++j) {
						continue;
					}

					nNumBalls += (j < oAnswer.Length && i != j) ? 1 : 0;
					nNumStrikes += (j < oAnswer.Length && i == j) ? 1 : 0;
				}

				Console.WriteLine("결과 : {0} Strike, {1} Ball\n", nNumStrikes, nNumBalls);
			} while(nNumStrikes < oAnswer.Length);

			Console.WriteLine("게임을 종료합니다.");
#elif P01_03
			/*
			 * 바위 가위 보 게임 제작
			 * - 게임 시작 시 컴퓨터는 바위 (1) 가위 (2) 보 (3) 중에 랜덤하게 하나 선택
			 * - 사용자로부터 입력을 받은 후 가위 바위 보 비교 결과 출력
			 * - 사용자가 컴퓨터에게 이겼거나 비겼으면 다시 게임 반복
			 * - 사용자가 졌으면 그 동안 전적 출력 후 게임 종료
			 * 
			 * Ex)
			 * 바위 (1), 가위 (2), 보 (3) 선택 : 2
			 * 결과 : 비겼습니다. (나 - 가위, 컴퓨터 - 가위)
			 * 
			 * 바위 (1), 가위 (2), 보 (3) 선택 : 3
			 * 결과 : 졌습니다. (나 - 보, 컴퓨터 - 가위)
			 * 
			 * 전적 : 0 승 1 무
			 * 게임을 종료합니다.
			 */
			var oRandom = new Random();
			var eResult = EResult.NONE;

			int nWinCount = 0;
			int nDrawCount = 0;

			do {
				Console.Write("바위 (1), 가위 (2), 보 (3) 선택 : ");
				int.TryParse(Console.ReadLine(), out int nUser);

				var eUser = (ESel)(nUser - 1);
				var eComputer = (ESel)oRandom.Next((int)ESel.ROCK, (int)ESel.MAX_VAL);

				eResult = GetResult(eUser, eComputer);

				nWinCount += (eResult == EResult.WIN) ? 1 : 0;
				nDrawCount += (eResult == EResult.DRAW) ? 1 : 0;

				Console.WriteLine("결과 : {0} (나 - {1}, 컴퓨터 - {2})\n",
					GetResultStr(eResult), GetSelStr(eUser), GetSelStr(eComputer));
			} while(eResult != EResult.LOSE);

			Console.WriteLine("전적 : {0} 승, {1} 무", nWinCount, nDrawCount);
			Console.WriteLine("게임을 종료합니다.");
#elif P01_04
			/*
			 * 단어 맞추기 게임 제작
			 * - 게임 시작 시 미리 생성 되어있는 단어 중 하나를 램덤하게 선택
			 * - 한 문자를 입력 받은 후 해당 문자가 단어에 포함 되어있을 경우 해당 문자 활성화
			 * - 단어를 모두 완성하면 게임 종료
			 * - 단, 대/소문자는 구분하지 않는다.
			 * 
			 * Ex)
			 * 정답 : Apple
			 * 
			 * _ _ _ _ _
			 * 문자 입력 : a
			 * 
			 * A _ _ _ _
			 * 문자 입력 : l
			 * 
			 * A _ _ l _
			 * 문자 입력 : b
			 * 
			 * 이하 생략...
			 * 
			 * A p p l e
			 * 게임을 종료합니다.
			 */
			var oWords = new string[] {
				"Apple", "Google", "Samsung", "Microsoft"
			};
			
			var oRandom = new Random();
			int nIdx = oRandom.Next(0, oWords.Length);

			var oAnswer = oWords[nIdx].ToArray();
			Console.WriteLine("정답 : {0}\n", oWords[nIdx]);

			var oInputWord = new char[oAnswer.Length];

			for(int i = 0; i < oInputWord.Length; ++i) {
				oInputWord[i] = '_';
			}

			do {
				PrintWord(oInputWord);

				Console.Write("문자 입력 : ");
				char.TryParse(Console.ReadLine(), out char chLetter);

				for(int i = 0; i < oAnswer.Length; ++i) {
					// 문자가 존재 할 경우
					if(char.ToUpper(oAnswer[i]) == char.ToUpper(chLetter)) {
						oInputWord[i] = oAnswer[i];
					}
				}

				Console.WriteLine();
			} while(!IsAnswer(oAnswer, oInputWord));

			PrintWord(oInputWord);
			Console.WriteLine("게임을 종료합니다.");
#endif // #if P01_01
		}
	}
}
