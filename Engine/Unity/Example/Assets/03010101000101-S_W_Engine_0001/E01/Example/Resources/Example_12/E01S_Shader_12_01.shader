/*
* 쉐이더는 일반적인 에셋과 달리 프로그램이 실행 중에 동적으로 특정 쉐이더를 로드하고 싶다면
* 해당 쉐이더 에셋의 경로가 아닌 쉐이더 코드 상에서 명시한 경로를 명시해줘야한다.
*/
Shader "Example_12/E01S_Shader_12_01" {
	/*
	* Properties 영역은 쉐이더 동작하기 위해서 필요한 다양한 옵션을 명시하는 영역을 의미한다.
	* (즉, 해당 영역에 명시 된 옵션은 Unity 에디터 상에 손쉽게 설정하는 것이 가능하다.)
	*/
	Properties{
		_Color("Color", Color) = (1, 1, 1, 1)
	}

	/*
	* SubShader 영역은 쉐이더 명령문을 작성하는 영역을 의미한다. (즉, 해당 영역 안에 쉐이더
	* 언어를 통해서 명령문을 작성하면 해당 명령문은 GPU 상에서 구동되는 프로그램이 된다는 것을
	* 알 수 있다.)
	*/
	SubShader{
		/*
		* Tags 영역은 쉐이더 동작하기 위해서 필요한 부가적인 옵션을 명시하는 영역을 의미한다.
		* (즉, 해당 영역에 설정되는 옵션에 따라 쉐이더가 처리되는 순서 등을 제어하는 것이 가능하다.)
		*/
		Tags{
			"Queue" = "Geometry+1"
			"RenderType" = "Opaque"
		}

		/*
		* 작성한 쉐이더 언어에 따라 해당 명령문이 시작되는 위치와 종료되는 위치를 명시 할 필요가
		* 있으며 해당 영역은 ~PROGRAM 부터 END~ 영역까지를 의미한다.
		*
		* Ex)
		* CGPROGRAM
		*		// CG 명령문
		* ENDCG
		*
		* HLSLPROGRAM
		*		// HLSL 명령문
		* ENDHLSL
		*/
		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Standard

		float4 _Color;

		/*
		* 정점 쉐이더 동작 할 때 입력으로 전달 되는 점 데이터의 구조를 의미한다. (즉, 입력 
		* 데이터는 필요에 따라 위치 정보 이외에 부가적인 정보를 추가하는 것이 가능하다.)
		*/
		/** 입력 */
		struct Input {
			float4 color;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, inout SurfaceOutputStandard a_stOutput) {
			a_stOutput.Alpha = _Color.a;
			a_stOutput.Albedo = _Color.rgb;
		}
		ENDCG
	}
}
