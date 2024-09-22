Shader "Example_12/E01S_Shader_12_03" {
	Properties{
		_Color("Color", Color) = (1, 1, 1, 1)
		_FresnelColor("Fresnel Color", Color) = (1, 1, 1, 1)
		_SpecularColor("Specular Color", Color) = (1, 1, 1, 1)

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
		float4 _FresnelColor;
		float4 _SpecularColor;

		sampler2D _MainTex;

		/** 입력 */
		struct Input {
			float4 color;
			float3 worldNormal;

			float2 uv_MainTex;
		};

		/*
		* SurfaceOutputCustom 구조체를 활용하면 광원 연산을 커스텀하게 처리 할 때 필요한 정보를
		* 추가적으로 전달하는 것이 가능하다. (즉, Unity 가 필요로하는 기본적인 정보 이외에도
		* 부가적인 정보를 추가적으로 전달 할 수 있다는 것을 알 수 있다.)
		*/
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
			float4 stColor = tex2D(_MainTex, a_stInput.uv_MainTex);

			a_stOutput.Alpha = stColor.a;
			a_stOutput.Albedo = stColor.rgb;
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
			stFinalColor += _SpecularColor.rgb * pow(fSpecular, 30.0);

			return float4(stFinalColor, a_stOutput.Alpha) * _Color;
		}
		ENDCG
	}
}
