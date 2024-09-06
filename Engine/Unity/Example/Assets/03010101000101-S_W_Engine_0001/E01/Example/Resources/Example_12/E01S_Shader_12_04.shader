Shader "Example_12/E01S_Shader_12_04" {
	Properties{
		_Color("Color", Color) = (1, 1, 1, 1)
		_FresnelColor("Fresnel Color", Color) = (1, 1, 1, 1)
		_SpecularColor("Specular Color", Color) = (1, 1, 1, 1)

		_MainTex("Main Texture", 2D) = "white" { }
		_NormalTex("Normal Texture", 2D) = "bump" { }
		_SpecularTex("Specular Texture", 2D) = "white" { }
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
		float4 _FresnelColor;
		float4 _SpecularColor;

		sampler2D _MainTex;
		sampler2D _NormalTex;
		sampler2D _SpecularTex;

		/** 입력 */
		struct Input {
			float4 color;

			float2 uv_MainTex;
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

			float4 m_stSpecular;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, inout SurfaceOutputCustom a_stOutput) {
			float4 stColor = tex2D(_MainTex, a_stInput.uv_MainTex);

			/*
			* UnpackNormal 함수는 탄젠트 (오브젝트) 공간에 존재하는 법선을 월드 공간으로 변환 
			* 시키는 역할을 수행한다. (즉, 노말 맵을 제작하는 프로그램은 탄젠트 공간 상에 
			* 존재하는 노말 정보를 기반으로 노말 텍스처를 제작하기 때문에 해당 데이터를 바로 
			* 광원 연산에 활용하는 것은 불가능하다는 것을 알 수 있다.)
			*/
			float3 stNormal = UnpackNormal(tex2D(_NormalTex, a_stInput.uv_NormalTex));

			a_stOutput.Alpha = stColor.a;
			a_stOutput.Albedo = stColor.rgb;
			a_stOutput.Normal = float3(stNormal.r, -stNormal.g, stNormal.b);

			a_stOutput.m_stSpecular = tex2D(_SpecularTex, a_stInput.uv_SpecularTex);
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutputCustom a_stOutput, 
			float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation) {
			float3 stHalf = normalize(a_stLightDirection + a_stViewDirection);

			float fDot = saturate(dot(a_stOutput.Normal, a_stLightDirection));
			float fRim = 1.0 - saturate(dot(a_stOutput.Normal, a_stViewDirection));
			float fSpecular = saturate(dot(a_stOutput.Normal, stHalf));

			float3 stFinalColor = a_stOutput.Albedo * fDot;
			stFinalColor += _FresnelColor.rgb * pow(fRim, 10.0);
			stFinalColor += _SpecularColor.rgb * pow(fSpecular, 30.0) * a_stOutput.m_stSpecular.r;

			return float4(stFinalColor, a_stOutput.Alpha) * _Color;
		}
		ENDCG
	}
}
