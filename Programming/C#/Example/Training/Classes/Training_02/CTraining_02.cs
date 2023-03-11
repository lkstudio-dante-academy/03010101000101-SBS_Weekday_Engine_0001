#define T02_01

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Classes.Training_02 {
	internal class CTraining_02 {
#if T02_01
		/** 회원 */
		public class CMember {
			private string m_oName = "";
			private string m_oPhoneNumber = "";

			public string Name { get { return m_oName; } set { m_oName = value; } }
			public string PhoneNumber { get { return m_oPhoneNumber; } set { m_oPhoneNumber = value; } }

			/** 생성자 */
			public CMember(string a_oName, string a_oPhoneNumber) {
				m_oName = a_oName;
				m_oPhoneNumber = a_oPhoneNumber;
			}

			/** 정보를 출력한다 */
			public void ShowInfo() {
				Console.WriteLine("이름 : {0}", m_oName);
				Console.WriteLine("전화번호 : {0}", m_oPhoneNumber);
			}
		}

		/** 회원 관리자 */
		public class CMemberManager {
			private List<CMember> m_oMemberList = new List<CMember>();

			/*
			 * 프로퍼티는 Getter 와 Setter 모두를 구현해도 되고 둘 중에 필요한 접근자만 구현해도 된다. (즉, Getter 만
			 * 필요 할 경우 Setter 는 구현하지 않아도 된다는 것을 의미한다.)
			 * 
			 * 또한, Getter 프로퍼티만 구현 할 경우에는 => 연산자를 사용해서 축약해서 명시하는 것이 가능하다.
			 */
			//public int NumMembers { get { return m_oMemberList.Count; } }
			public int NumMembers => m_oMemberList.Count;

			/** 회원을 추가한다 */
			public void AddMember(string a_oName, string a_oPhoneNumber) {
				var oMember = new CMember(a_oName, a_oPhoneNumber);
				m_oMemberList.Add(oMember);
			}

			/** 회원을 제거한다 */
			public void RemoveMember(string a_oName) {
				/*
				 * this 키워드는 해당 메서드를 호출한 객체의 참조 값을 지니고 있는 키워드를 의미한다. (즉, 해당 키워드가
				 * 존재함으로써 특정 메서드가 호출 되었을 때 해당 메서드를 호출 한 객체에 따라 서로 다른 맴버 변수에
				 * 접근하는 것이 가능하다는 것을 의미한다.)
				 */
				int nIdx = this.FindMember(a_oName);

				// 회원이 존재 할 경우
				if(nIdx >= 0) {
					m_oMemberList.RemoveAt(nIdx);
				}
			}

			/** 회원을 탐색한다 */
			public int FindMember(string a_oName) {
				for(int i = 0; i < m_oMemberList.Count; ++i) {
					// 회원이 존재 할 경우
					if(m_oMemberList[i].Name.Equals(a_oName)) {
						return i;
					}
				}

				return -1;
			}

			/** 회원 정보를 출력한다 */
			public void ShowMemberInfo(int a_nIdx) {
				// 인덱스가 유효 할 경우
				if(a_nIdx >= 0 && a_nIdx < m_oMemberList.Count) {
					m_oMemberList[a_nIdx].ShowInfo();
				}
			}

			/** 회원 정보를 출력한다 */
			public void ShowMemberInfo(string a_oName) {
				this.ShowMemberInfo(this.FindMember(a_oName));
			}
		}

		/** 회원 관리자 어플리케이션 */
		public class CMemberManagerApp {
			private CMemberManager m_oMemberManager = new CMemberManager();

			/** 어플리케이션을 실행한다 */
			public void Run() {
				int nSelMenu = 0;

				const int ADD_MEBER = 1;
				const int SEARCH_MEMBER = 2;
				const int REMOVE_MEMBER = 3;
				const int PRINT_ALL_MEMBERS = 4;
				const int EXIT = 5;

				do {
					this.ShowMenu();

					Console.Write("\n메뉴 선택 : ");
					int.TryParse(Console.ReadLine(), out nSelMenu);

					switch(nSelMenu) {
						case ADD_MEBER: this.AddMember(); break;
						case REMOVE_MEMBER: this.RemoveMember(); break;
						case SEARCH_MEMBER: this.SearchMember(); break;
						case PRINT_ALL_MEMBERS: this.PrintAllMembers(); break;
					}

					Console.WriteLine();
				} while(nSelMenu != EXIT);
			}

			/** 메뉴를 출력한다 */
			private void ShowMenu() {
				Console.WriteLine("=====> 메뉴 <=====");
				Console.WriteLine("1. 회원 추가");
				Console.WriteLine("2. 회원 검색");
				Console.WriteLine("3. 회원 삭제");
				Console.WriteLine("4. 모든 회원 출력");
				Console.WriteLine("5. 종료");
			}

			/** 회원을 추가한다 */
			private void AddMember() {
				Console.WriteLine("=====> 회원 정보 입력 <=====");

				Console.Write("이름 : ");
				string oName = Console.ReadLine();

				Console.Write("전화번호 : ");
				string oPhoneNumber = Console.ReadLine();

				m_oMemberManager.AddMember(oName, oPhoneNumber);
			}

			/** 회원을 검색한다 */
			private void SearchMember() {
				Console.Write("회원 이름 입력 : ");
				string oName = Console.ReadLine();

				int nIdx = m_oMemberManager.FindMember(oName);

				// 회원이 존재 할 경우
				if(nIdx >= 0) {
					Console.WriteLine("\n=====> 회원 정보 <=====");
					m_oMemberManager.ShowMemberInfo(oName);
				} else {
					Console.WriteLine("\n{0} 회원이 존재하지 않습니다.", oName);
				}
			}

			/** 회원을 삭제한다 */
			private void RemoveMember() {
				Console.Write("회원 이름 입력 : ");
				string oName = Console.ReadLine();

				int nIdx = m_oMemberManager.FindMember(oName);

				// 회원이 존재 할 경우
				if(nIdx >= 0) {
					m_oMemberManager.RemoveMember(oName);
					Console.WriteLine("\n{0} 회원을 삭제했습니다.", oName);
				} else {
					Console.WriteLine("\n{0} 회원이 존재하지 않습니다.", oName);
				}
			}

			/** 모든 회원을 출력한다 */
			private void PrintAllMembers() {
				Console.WriteLine("=====> 모든 회원 정보 <=====");

				for(int i = 0; i < m_oMemberManager.NumMembers; ++i) {
					m_oMemberManager.ShowMemberInfo(i);
					Console.WriteLine();
				}
			}
		}
#endif // #if T02_01

		/** 초기화 */
		public static void Start(string[] args) {
#if T02_01
			/*
			 * 연습 문제 2 - 1
			 * - 회원 관리 프로그램 제작하기
			 * 
			 * Ex)
			 * =====> 메뉴 <=====
			 * 1. 회원 추가
			 * 2. 회원 검색
			 * 3. 회원 삭제
			 * 4. 모든 회원 출력
			 * 5. 종료
			 * 
			 * 메뉴 선택 : 1
			 * =====> 회원 정보 입력 <=====
			 * 이름 : 회원 A
			 * 전화번호 : 1234
			 * 
			 * 메뉴 선택 : 2
			 * 회원 이름 입력 : 회원 B
			 * 
			 * Case 1.
			 * 회원 B 는 존재하지 않는 회원 입니다.
			 * 
			 * Case 2.
			 * =====> 회원 정보 <=====
			 * 이름 : 회원 B
			 * 전화번호 : 1234
			 * 
			 * 메뉴 선택 : 3
			 * 회원 이름 입력 : 회원 B
			 * 
			 * Case 1.
			 * 회원 B 는 존재하지 않는 회원 입니다.
			 * 
			 * Case 2.
			 * 회원 B 를 삭제했습니다
			 * 
			 * 메뉴 선택 : 4
			 * =====> 모든 회원 출력 <=====
			 * 이름 : 회원 A
			 * 전화번호 : 1234
			 * 
			 * 이름 : 회원 B
			 * 전화번호 : 1234
			 * 
			 * ... 이하 생략
			 */
			var oMemberManagerApp = new CMemberManagerApp();
			oMemberManagerApp.Run();
#endif // #if T02_01
		}
	}
}
