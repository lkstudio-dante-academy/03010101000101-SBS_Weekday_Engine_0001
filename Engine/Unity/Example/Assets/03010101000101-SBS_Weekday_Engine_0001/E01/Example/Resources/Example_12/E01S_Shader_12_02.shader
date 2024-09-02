Shader "Example_12/E01S_Shader_12_02" {
	Properties{
		_Color("Color", Color) = (1, 1, 1, 1)
		_MainTex("Main Texture", 2D) = "white" { }
	}
	SubShader{
		Tags {
			"Queue" = "Geometry+1"
			"RenderType" = "Opaque"
		}

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Standard

		float4 _Color;
		sampler2D _MainTex;

		/** 입력 */
		struct Input {
			float4 color;
			float2 uv_MainTex;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, inout SurfaceOutputStandard a_stOutput) {
			/*
			* tex2D 함수는 특정 텍스처로부터 정보를 가져오는 역할을 수행한다. (즉, 색상 정보를
			* 지니고 있는 텍스처 일 경우 색상 정보를 가져온다는 것을 알 수 있다.)
			*/
			float4 stColor = tex2D(_MainTex, a_stInput.uv_MainTex);

			a_stOutput.Alpha = stColor.a * _Color.a;
			a_stOutput.Albedo = stColor.rgb * _Color.rgb;
		}
		ENDCG
	}
}
