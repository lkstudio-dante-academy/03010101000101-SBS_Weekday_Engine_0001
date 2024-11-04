Shader "Example_12/E01S_Shader_12_06" {
	Properties{
		_Color("Color", Color) = (1, 1, 1, 1)
		_AmbientTex("Ambient Texture", Cube) = "white" { }
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
		samplerCUBE _AmbientTex;

		/** 입력 */
		struct Input {
			float4 color;
			float3 worldRefl;
			float3 worldNormal;

			INTERNAL_DATA
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
			float4 stColor = texCUBE(_AmbientTex, WorldReflectionVector(a_stInput, a_stInput.worldNormal));

			a_stOutput.Alpha = stColor.a;
			a_stOutput.Albedo = stColor.rgb;
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutputCustom a_stOutput, float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation) {
			return float4(a_stOutput.Albedo, a_stOutput.Alpha) * _Color;
		}
		ENDCG
	}
}
