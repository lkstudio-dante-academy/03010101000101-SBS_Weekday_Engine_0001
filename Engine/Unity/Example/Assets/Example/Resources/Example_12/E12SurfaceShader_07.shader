Shader "Example_12/E12SurfaceShader_07" {
	Properties{
		_Color("Color", Color) = (1, 1, 1, 1)
	}
	SubShader{
		Tags {
			"Queue" = "Geometry+1"
			"RenderType" = "Opaque"
		}

		cull front

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Custom vertex:VSMain

		float4 _color;

		/** 입력 */
		struct Input {
			float4 color;
		};

		/** 정점 쉐이더 */
		void VSMain(inout appdata_full a_stInput) {
			a_stInput.vertex.xyz += a_stInput.normal.xyz * 0.05;
		}

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, inout SurfaceOutput a_stOutput) {
			a_stOutput.Alpha = 1.0;
			a_stOutput.Albedo = float3(0.0, 0.0, 0.0);
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutput a_stOutput, float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation) {
			return float4(a_stOutput.Albedo, a_stOutput.Alpha);
		}
		ENDCG

		cull back

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Custom

		float4 _Color;

		/** 입력 */
		struct Input {
			float4 color;
			float3 worldNormal;
		};

		/** 출력 */
		struct SurfaceOutputCustom {
			float Gloss;
			float Alpha;
			float Specular;

			float3 Albedo;
			float3 Normal;
			float3 Emission;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, inout SurfaceOutputCustom a_stOutput) {
			a_stOutput.Alpha = 1.0;
			a_stOutput.Albedo = float3(1.0, 1.0, 1.0);
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutputCustom a_stOutput, float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation) {
			float fDot = saturate(dot(a_stOutput.Normal, a_stLightDirection));
			fDot = ceil(fDot * 2.0) / 2.0;

			return float4(a_stOutput.Albedo * fDot, a_stOutput.Alpha) * _Color;
		}
		ENDCG
	}
}
