Shader "Example_12/E12SurfaceShader_05" {
	Properties{
		_Color("Color", Color) = (1, 1, 1, 1)
		_FresnelColor("Fresnel Color", Color) = (1, 1, 1, 1)

		_NormalTex("Normal Texture", 2D) = "bump" { }
	}
	SubShader{
		Tags {
			"Queue" = "Transparent+1"
			"RenderType" = "Transparent"
		}

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Custom alpha:fade

		float4 _Color;
		float4 _FresnelColor;

		sampler2D _NormalTex;

		/** 입력 */
		struct Input {
			float4 color;
			float3 worldPos;

			float2 uv_NormalTex;
		};

		/** 출력 */
		struct SurfaceOutputCustom {
			float Gloss;
			float Alpha;
			float Specular;

			float3 Albedo;
			float3 Normal;
			float3 Emission;

			float3 m_stWorldPos;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, inout SurfaceOutputCustom a_stOutput) {
			float3 stNormal = UnpackNormal(tex2D(_NormalTex, a_stInput.uv_NormalTex));

			a_stOutput.Normal = float3(stNormal.r, -stNormal.g, stNormal.b);
			a_stOutput.m_stWorldPos = a_stInput.worldPos;
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutputCustom a_stOutput, float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation) {
			float fRim01 = pow(1.0 - saturate(dot(a_stOutput.Normal, a_stViewDirection)), 3.0);
			float fRim02 = saturate(pow(frac(a_stOutput.m_stWorldPos.y * 0.01 - _Time.y), 5.0)) * 0.25;

			float fAlpha = saturate(sin(_Time.y * 10000.0));
			return float4(_FresnelColor.rgb, (fRim01 + fRim02) * fAlpha) * _Color;
		}
		ENDCG
	}
}
