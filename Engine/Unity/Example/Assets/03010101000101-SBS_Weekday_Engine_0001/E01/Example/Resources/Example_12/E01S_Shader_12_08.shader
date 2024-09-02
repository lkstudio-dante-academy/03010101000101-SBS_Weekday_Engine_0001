Shader "Example_12/E01S_Shader_12_08" {
	Properties{
		_Cutout("Cutout", Range(0.0, 1.0)) = 0

		_Color("Color", Color) = (1, 1, 1, 1)
		_CutoutColor("Cutout Color", Color) = (1, 1, 1, 1)
		_FresnelColor("Fresnel Color", Color) = (1, 1, 1, 1)
		_SpecularColor("Specular Color", Color) = (1, 1, 1, 1)

		_MainTex("Main Texture", 2D) = "white" { }
		_NoiseTex("Noise Texture", 2D) = "white" { }
		_NormalTex("Normal Texture", 2D) = "bump" { }
		_SpecularTex("Specular Texture", 2D) = "white" { }
	}
	SubShader{
		Tags {
			"Queue" = "Transparent+1"
			"RenderType" = "Transparent"
		}

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Custom alpha:fade

		float _Cutout;

		float4 _Color;
		float4 _CutoutColor;
		float4 _FresnelColor;
		float4 _SpecularColor;

		sampler2D _MainTex;
		sampler2D _NoiseTex;
		sampler2D _NormalTex;
		sampler2D _SpecularTex;

		/** 입력 */
		struct Input {
			float4 color;

			float2 uv_MainTex;
			float2 uv_NoiseTex;
			float2 uv_NormalTex;
			float2 uv_SpecularTex;
		};

		/** 출력 */
		struct SurfaceOutputCustom {
			float Gloss;
			float Alpha;
			float Specular;

			float3 Albedo;
			float3 Normal;
			float3 Emission;

			float4 m_stNoise;
			float4 m_stSpecular;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, inout SurfaceOutputCustom a_stOutput) {
			float4 stColor = tex2D(_MainTex, a_stInput.uv_MainTex);
			float3 stNormal = UnpackNormal(tex2D(_NormalTex, a_stInput.uv_NormalTex));

			a_stOutput.Alpha = stColor.a;
			a_stOutput.Albedo = stColor.rgb;
			a_stOutput.Normal = float3(stNormal.r, -stNormal.g, stNormal.b);

			a_stOutput.m_stNoise = tex2D(_NoiseTex, a_stInput.uv_NoiseTex);
			a_stOutput.m_stSpecular = tex2D(_SpecularTex, a_stInput.uv_SpecularTex);
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutputCustom a_stOutput, float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation) {
			float3 stHalf = normalize(a_stLightDirection + a_stViewDirection);

			float fDot = saturate(dot(a_stOutput.Normal, a_stLightDirection));
			float fRim = 1.0 - saturate(dot(a_stOutput.Normal, a_stViewDirection));
			float fSpecular = saturate(dot(a_stOutput.Normal, stHalf));

			float fAlpha = 1.0;
			float fOutline = 0.0;

			// 투명 처리가 필요 할 경우
			if(min(0.99999, a_stOutput.m_stNoise.r * 5.0) < _Cutout) {
				fAlpha = 0.0;
			}

			// 외곽선 처리가 필요 할 경우
			if(min(0.99999, a_stOutput.m_stNoise.r * 5.0) < _Cutout * 1.5) {
				fOutline = 1.0;
			}

			float3 stFinalColor = a_stOutput.Albedo * fDot;
			stFinalColor += _CutoutColor.rgb * fOutline;
			stFinalColor += _FresnelColor.rgb * pow(fRim, 10.0);
			stFinalColor += _SpecularColor.rgb * pow(fSpecular, 30.0) * a_stOutput.m_stSpecular.r;

			return float4(stFinalColor, fAlpha) * _Color;
		}
		ENDCG
	}
}
