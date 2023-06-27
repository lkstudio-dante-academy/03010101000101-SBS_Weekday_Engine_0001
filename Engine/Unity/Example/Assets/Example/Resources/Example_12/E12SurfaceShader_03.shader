Shader "Example_12/E12SurfaceShader_03" {
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
		#pragma surface SSMain Custom

		float4 _Color;
		sampler2D _MainTex;

		/** 입력 */
		struct Input {
			float4 color;
			float2 uv_MainTex;
			float3 worldNormal;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, inout SurfaceOutput a_stOutput) {
			float4 stColor = tex2D(_MainTex, a_stInput.uv_MainTex);

			a_stOutput.Alpha = stColor.a * _Color.a;
			a_stOutput.Albedo = stColor.rgb * _Color.rgb;
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutput a_stOutput,
			float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation) {
			float fDot = saturate(dot(a_stOutput.Normal, a_stLightDirection));

			float3 stFinalColor = a_stOutput.Albedo * fDot;
			stFinalColor += a_stOutput.Albedo * 0.05;

			return float4(stFinalColor, a_stOutput.Alpha);
		}
		ENDCG
	}
}
