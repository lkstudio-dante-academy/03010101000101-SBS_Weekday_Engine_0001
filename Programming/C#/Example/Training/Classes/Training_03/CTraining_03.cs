#define T03_01

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Classes.Training_03 {
	internal class CTraining_03 {
#if T03_01
		/** 도형 */
		class CShape {
			public string Color { get; private set; } = "";
			public virtual string Shape => string.Empty;

			/** 생성자 */
			public CShape(string a_oColor) {
				this.Color = a_oColor;
			}

			/** 그린다 */
			public virtual void Draw() {
				// Do Something
			}
		}

		/** 삼각형 */
		class CTriangle : CShape {
			public override string Shape => "삼각형";

			/** 생성자 */
			public CTriangle(string a_oColor) : base(a_oColor) {
				// Do Something
			}

			/** 그린다 */
			public override void Draw() {
				Console.WriteLine("{0} 삼각형을 그렸습니다.", this.Color);
			}
		}

		/** 사각형 */
		class CSquare : CShape {
			public override string Shape => "사각형";

			/** 생성자 */
			public CSquare(string a_oColor) : base(a_oColor) {
				// Do Something
			}

			/** 그린다 */
			public override void Draw() {
				Console.WriteLine("{0} 사각형을 그렸습니다.", this.Color);
			}
		}

		/** 캔버스 */
		class CCanvas {
			private List<CShape> m_oShapeList = new List<CShape>();

			/** 도형을 추가한다 */
			public void AddShape(CShape a_oShape) {
				m_oShapeList.Add(a_oShape);
			}

			/** 도형을 그린다 */
			public void DrawShapes() {
				for(int i = 0; i < m_oShapeList.Count; ++i) {
					m_oShapeList[i].Draw();
				}
			}
		}

		/** 그림판 어플리케이션 */
		class CPaintApp {
			private CCanvas m_oCanvas = new CCanvas();

			private const int ADD_TRIANGLE = 1;
			private const int ADD_SQUARE = 2;
			private const int DRAW_ALL_SHAPES = 3;
			private const int EXIT = 4;

			/** 실행한다 */
			public void Run() {
				int nSelMenu = 0;

				do {
					PrintMenu();

					Console.Write("\n메뉴 선택 : ");
					int.TryParse(Console.ReadLine(), out nSelMenu);

					switch(nSelMenu) {
						case DRAW_ALL_SHAPES: {
							Console.WriteLine("=====> 모든 도형 그리기 <=====");
							m_oCanvas.DrawShapes();

							break;
						}
						default: {
							var oShape = CreateShape(nSelMenu);

							// 도형이 존재 할 경우
							if(oShape != null) {
								m_oCanvas.AddShape(oShape);
								Console.WriteLine("{0} {1} 을(를) 추가했습니다.", oShape.Color, oShape.Shape);
							}

							break;
						}
					}

					Console.WriteLine();
				} while(nSelMenu != EXIT);
			}

			/** 메뉴를 출력한다 */
			private void PrintMenu() {
				Console.WriteLine("=====> 메뉴 <=====");
				Console.WriteLine("1. 삼각형 추가");
				Console.WriteLine("2. 사각형 추가");
				Console.WriteLine("3. 모든 도형 그리기");
				Console.WriteLine("4. 종료");
			}

			/** 도형을 생성한다 */
			private CShape CreateShape(int a_nSelMenu) {
				var oColors = new string[] {
					"빨간색", "초록색", "파란색"
				};

				var oRandom = new Random();
				string oColor = oColors[oRandom.Next(0, oColors.Length)];

				switch(a_nSelMenu) {
					case ADD_TRIANGLE: return new CTriangle(oColor);
					case ADD_SQUARE: return new CSquare(oColor);
				}

				return null;
			}
		}
#endif // #if T03_01

		/** 초기화 */
		public static void Start(string[] args) {
#if T03_01
			/*
			 * 연습 문제 3 - 1
			 * - 콘솔 그림판 제작하기
			 * - 도형의 색상은 도형을 생성 할 때 랜덤하게 결정 (빨강, 초록, 파랑)
			 * 
			 * Ex)
			 * =====> 메뉴 <=====
			 * 1. 삼각형 추가
			 * 2. 사각형 추가
			 * 3. 모든 도형 그리기
			 * 4. 종료
			 * 
			 * 메뉴 선택 : 1
			 * 빨간색 삼각형을 추가헀습니다
			 * 
			 * 메뉴 선택 : 2
			 * 초록색 사각형을 추가했습니다
			 * 
			 * 메뉴 선택 : 3
			 * =====> 모든 도형 그리기 <=====
			 * 빨간색 삼각형을 그렸습니다
			 * 초록색 사각형을 그렸습니다
			 */
			var oPaintApp = new CPaintApp();
			oPaintApp.Run();
#endif // #if T03_01
		}
	}
}
