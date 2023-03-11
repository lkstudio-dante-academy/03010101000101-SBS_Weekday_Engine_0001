using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 연산자란?
 * - 주어진 역할을 수행하는 특별한 의미를 지니는 기호 (심볼) 을 의미한다. (즉, 연산자를 
 * 활용하면 데이터를 프로그램에 목적에 맞게 제어 및 처리하는 것이 가능하다.)
 * 
 * C# 연산자 종류
 * - 산술 연산자 (+, -, *, /, %)
 * - 관계 연산자 (<, >, <=, >=, ==, !=)
 * - 할당 연산자 (=, +=, -=, *=, ...)
 * - 논리 연산자 (&&, ||, !)
 * - 증감 연산자 (++, --)
 * - 부호 연산자 (+, -)
 * - 비트 연산자 (&, |, ^, ~, <<, >>)
 * - 조건 연산자 (?:)
 * - 기타 연산자 (sizeof, 형 변환 연산자, 우선 순위 연산자)
 */
namespace Example.Classes.Example_04 {
	class CExample_04 {
		/** 초기화 */
		public static void Start(string[] args) {
			Console.Write("정수 (2 개) 입력 : ");
			var oTokens = Console.ReadLine().Split();

			int nLhs = int.Parse(oTokens[0]);
			int nRhs = int.Parse(oTokens[1]);

			Console.WriteLine("=====> 산술 연산자 <=====");
			Console.WriteLine("{0} + {1} = {2}", nLhs, nRhs, nLhs + nRhs);
			Console.WriteLine("{0} - {1} = {2}", nLhs, nRhs, nLhs - nRhs);
			Console.WriteLine("{0} * {1} = {2}", nLhs, nRhs, nLhs * nRhs);
			Console.WriteLine("{0} / {1} = {2}", nLhs, nRhs, nLhs / (float)nRhs);
			Console.WriteLine("{0} % {1} = {2}", nLhs, nRhs, nLhs % nRhs);

			Console.WriteLine("\n=====> 관계 연산자 <=====");
			Console.WriteLine("{0} < {1} = {2}", nLhs, nRhs, nLhs < nRhs);
			Console.WriteLine("{0} > {1} = {2}", nLhs, nRhs, nLhs > nRhs);
			Console.WriteLine("{0} <= {1} = {2}", nLhs, nRhs, nLhs <= nRhs);
			Console.WriteLine("{0} >= {1} = {2}", nLhs, nRhs, nLhs >= nRhs);
			Console.WriteLine("{0} == {1} = {2}", nLhs, nRhs, nLhs == nRhs);
			Console.WriteLine("{0} != {1} = {2}", nLhs, nRhs, nLhs != nRhs);

			Console.WriteLine("\n=====> 논리 연산자 <=====");
			Console.WriteLine("{0} && {1} = {2}", nLhs, nRhs, nLhs != 0 && nRhs != 0);
			Console.WriteLine("{0} || {1} = {2}", nLhs, nRhs, nLhs != 0 || nRhs != 0);
			Console.WriteLine("!{0} = {1}", nLhs, !(nLhs != 0));

			Console.WriteLine("\n=====> 증감 연산자 <=====");
			Console.WriteLine("{0}, {1}", ++nLhs, --nRhs);
			Console.WriteLine("{0}, {1}", nLhs++, nRhs--);

			Console.WriteLine("\n=====> 증감 연산자 후 <=====");
			Console.WriteLine("{0}, {1}", nLhs, nRhs);

			Console.WriteLine("\n=====> 조건 연산자 <=====");
			Console.WriteLine("{0} < {1} = {2}", nLhs, nRhs, (nLhs < nRhs) ? nLhs : nRhs);

			Console.WriteLine("\n=====> 비트 연산자 <=====");
			Console.WriteLine("{0} & {1} = {2}", Convert.ToString(nLhs, 2), Convert.ToString(nRhs, 2), Convert.ToString(nLhs & nRhs, 2));
			Console.WriteLine("{0} | {1} = {2}", Convert.ToString(nLhs, 2), Convert.ToString(nRhs, 2), Convert.ToString(nLhs | nRhs, 2));
			Console.WriteLine("{0} ^ {1} = {2}", Convert.ToString(nLhs, 2), Convert.ToString(nRhs, 2), Convert.ToString(nLhs ^ nRhs, 2));
			Console.WriteLine("~{0} = {1}", Convert.ToString(nLhs, 2), Convert.ToString(~nLhs, 2));
			Console.WriteLine("{0} << 1 = {1}", Convert.ToString(nLhs, 2), Convert.ToString(nLhs << 1, 2));
			Console.WriteLine("{0} >> 1 = {1}", Convert.ToString(nLhs, 2), Convert.ToString(nLhs >> 1, 2));
		}
	}
}
