Shader "Example_12/E01S_Shader_12_09" {
	Properties{
		_Refraction("Refraction", Range(-0.5, 0.5)) = 0

		_Color("Color", Color) = (1, 1, 1, 1)
		_NoiseTex("Noise Texture", 2D) = "white" { }
	}
	SubShader{
		Tags {
			"Queue" = "Transparent+1"
			"RenderType" = "Transparent"
		}

		zwrite off
		GrabPass { }

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Custom alpha:fade

		float _Refraction;
		float4 _Color;

		sampler2D _NoiseTex;
		sampler2D _GrabTexture;

		/** 입력 */
		struct Input {
			float4 color;
			float4 screenPos;
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
			float3 stScreenUV = a_stInput.screenPos.xyz / a_stInput.screenPos.w;
			float4 stNoise = tex2D(_NoiseTex, stScreenUV.xy);
			float2 stOffset = stNoise.xy * _Refraction;

			a_stOutput.Alpha = 1.0;
			a_stOutput.Albedo = tex2D(_GrabTexture, (stScreenUV.xy + stOffset) % 1.0);
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutputCustom a_stOutput, float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation) {
			return float4(a_stOutput.Albedo, a_stOutput.Alpha) * _Color;
		}
		ENDCG
	}
}
