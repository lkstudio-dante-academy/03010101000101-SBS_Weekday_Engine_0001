//#define P02_01
//#define P02_02
#define P02_03

//#define P02_STARTER
#define P02_EXPERTER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example._03020103000101_SBS_Weekday_Engine_0001.E01.Practice.Classes.Runtime.Practice_02 {
	/** Practice 2 */
	class CP01Practice_02 {
		/** 초기화 */
		public static void Start(string[] args) {
#if P02_STARTER
#if P02_01
			Console.Write("라인 수 입력 : ");
			int.TryParse(Console.ReadLine(), out int nNumLines);

			for(int i = 0; i < nNumLines; ++i) {
				for(int j = 0; j < nNumLines; ++j) {
					Console.Write("{0}", (j <= i) ? "*" : " ");
				}

				Console.WriteLine();
			}
#elif P02_02
			Console.Write("라인 수 입력 : ");
			int.TryParse(Console.ReadLine(), out int nNumLines);

			for(int i = 0; i < nNumLines; ++i) {
				for(int j = 0; j < nNumLines; ++j) {
					Console.Write("{0}", (j <= i) ? "*" : " ");
				}

				Console.WriteLine();
			}

			Console.WriteLine();

			for(int i = 0; i < nNumLines; ++i) {
				for(int j = nNumLines - 1; j >= 0; --j) {
					Console.Write("{0}", (j <= i) ? "*" : " ");
				}

				Console.WriteLine();
			}

			Console.WriteLine();

			for(int i = nNumLines - 1; i >= 0; --i) {
				for(int j = 0; j < nNumLines; ++j) {
					Console.Write("{0}", (j <= i) ? "*" : " ");
				}

				Console.WriteLine();
			}

			Console.WriteLine();

			for(int i = nNumLines - 1; i >= 0; --i) {
				for(int j = nNumLines - 1; j >= 0; --j) {
					Console.Write("{0}", (j <= i) ? "*" : " ");
				}

				Console.WriteLine();
			}
#elif P02_03
			Console.Write("라인 수 입력 : ");
			int.TryParse(Console.ReadLine(), out int nNumLines);

			int nWidth = (nNumLines * 2) + 1;
			int nCenter = nWidth / 2;

			for(int i = 0; i < nNumLines; ++i) {
				for(int j = 0; j < nWidth; ++j) {
					Console.Write("{0}", (j >= nCenter - i && j <= nCenter + i) ? "*" : " ");
				}

				Console.WriteLine();
			}
#endif // #if P02_01
#elif P02_EXPERTER
#if P02_01
			Console.Write("숫자 입력 : ");
			var oTokens = Console.ReadLine().Split();

			var oValList = new List<int>();
			
			oTokens.ToList().ForEach((a_oToken) => {
				// 정수로 변환이 가능 할 경우
				if(int.TryParse(a_oToken, out int nVal)) {
					oValList.Add(nVal);
				}
			});

			int nMaxVal = oValList.Max();

			for(int i = 0; i < nMaxVal; ++i) {
				for(int j = 0; j < oValList.Count; ++j) {
					Console.Write("{0} ", (oValList[j] > i) ? "*" : " ");
				}

				Console.WriteLine();
			}
#elif P02_02
			Console.Write("행렬 크기 입력 : ");
			var oTokens = Console.ReadLine().Split();

			int.TryParse(oTokens[0], out int nNumOriginRows);
			int.TryParse(oTokens[1], out int nNumOriginCols);

			int nIdxX = -1;
			int nIdxY = 0;

			int nVal = 1;
			int nDirection = 1;

			int nNumRows = nNumOriginRows;
			int nNumCols = nNumOriginCols;

			var oVals = new int[nNumOriginRows, nNumOriginCols];

			while(nVal <= oVals.Length) {
				for(int i = 0; i < nNumCols; ++i) {
					nIdxX += nDirection;
					oVals[nIdxY, nIdxX] = nVal++;
				}

				nNumCols -= 1;
				nNumRows -= 1;

				for(int i = 0; i < nNumRows; ++i) {
					nIdxY += nDirection;
					oVals[nIdxY, nIdxX] = nVal++;
				}

				nDirection = -nDirection;
			}

			for(int i = 0; i < oVals.GetLength(0); ++i) {
				for(int j = 0; j < oVals.GetLength(1); ++j) {
					Console.Write("{0,4}", oVals[i, j]);
				}

				Console.WriteLine();
			}
#elif P02_03
			Console.Write("행렬 크기 입력 : ");
			var oTokens = Console.ReadLine().Split();

			int.TryParse(oTokens[0], out int nNumRows);
			int.TryParse(oTokens[1], out int nNumCols);

			var oVals = MakeVals(nNumRows, nNumCols, out int[,] oAnswer);

			do {
				PrintVals(oVals);

				Console.Write("\n위치 입력 : ");
				oTokens = Console.ReadLine().Split();

				int.TryParse(oTokens[0], out int nRow);
				int.TryParse(oTokens[1], out int nCol);

				var oOffsets = new (int, int)[] {
					(1, 0), (-1, 0), (0, 1), (0, -1)
				};

				for(int i = 0; i < oOffsets.Length; ++i) {
					int nNextRow = nRow + oOffsets[i].Item1;
					int nNextCol = nCol + oOffsets[i].Item2;

					// 배열 범위를 벗어났을 경우
					if(nNextRow < 0 || nNextRow >= oVals.GetLength(0)) {
						continue;
					}

					// 배열 범위를 벗어났을 경우
					if(nNextCol < 0 || nNextCol >= oVals.GetLength(1)) {
						continue;
					}

					// 공백이 존재 할 경우
					if(oVals[nNextRow, nNextCol] == 0) {
						int nTemp = oVals[nRow, nCol];
						oVals[nRow, nCol] = oVals[nNextRow, nNextCol];
						oVals[nNextRow, nNextCol] = nTemp;

						break;
					}
				}

				Console.WriteLine();
			} while(!IsAnswer(oVals, oAnswer));

			PrintVals(oVals);
			Console.WriteLine("\n게임을 종료합니다.");
#endif // #if P02_01
#endif // #if P02_STARTER
		}

#if P02_STARTER
#if P02_01

#elif P02_02

#elif P02_03

#endif // #if P02_01
#elif P02_EXPERTER
#if P02_01

#elif P02_02

#elif P02_03
		/** 정답 여부를 검사한다 */
		private static bool IsAnswer(int[,] a_oVals, int[,] a_oAnswer) {
			for(int i = 0; i < a_oVals.GetLength(0); ++i) {
				for(int j = 0; j < a_oVals.GetLength(1); ++j) {
					// 값이 다를 경우
					if(a_oVals[i, j] != a_oAnswer[i, j]) {
						return false;
					}
				}
			}

			return true;
		}

		/** 값을 재배치한다 */
		private static void ShuffleVals(int[,] a_oVals) {
			var oRandom = new Random();

			for(int i = 0; i < a_oVals.GetLength(0); ++i) {
				for(int j = 0; j < a_oVals.GetLength(1); ++j) {
					int nRow = oRandom.Next(0, a_oVals.GetLength(0));
					int nCol = oRandom.Next(0, a_oVals.GetLength(1));

					int nTemp = a_oVals[i, j];
					a_oVals[i, j] = a_oVals[nRow, nCol];
					a_oVals[nRow, nCol] = nTemp;
				}
			}
		}

		/** 값을 출력한다 */
		private static void PrintVals(int[,] a_oVals) {
			for(int i = 0; i < a_oVals.GetLength(0); ++i) {
				for(int j = 0; j < a_oVals.GetLength(1); ++j) {
					Console.Write("{0,4}", (a_oVals[i, j] != 0) ? $"{a_oVals[i, j]}" : " ");
				}

				Console.WriteLine();
			}
		}

		/** 값을 생성한다 */
		private static int[,] MakeVals(int a_nNumRows, int a_nNumCols, out int[,] a_oOutAnswer) {
			int nVal = 1;
			var oVals = new int[a_nNumRows, a_nNumCols];

			a_oOutAnswer = new int[a_nNumRows, a_nNumCols];

			for(int i = 0; i < oVals.GetLength(0); ++i) {
				for(int j = 0; j < oVals.GetLength(1); ++j) {
					oVals[i, j] = nVal;
					a_oOutAnswer[i, j] = nVal;

					nVal = (nVal + 1) % oVals.Length;
				}
			}

			ShuffleVals(oVals);
			return oVals;
		}
#endif // #if P02_01
#endif // #if P02_STARTER
	}
}
