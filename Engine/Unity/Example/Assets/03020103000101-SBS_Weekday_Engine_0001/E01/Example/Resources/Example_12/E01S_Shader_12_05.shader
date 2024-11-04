Shader "Example_12/E01S_Shader_12_05" {
	Properties{
		_Color("Color", Color) = (1, 1, 1, 1)
		_FresnelColor("Fresnel Color", Color) = (1, 1, 1, 1)

		_NormalTex("Normal Texture", 2D) = "bump" { }
	}
	SubShader{
		Tags {
			/*
			* Unity 는 내부적으로 물체를 화면 상에 그릴 때 투명한 물체와 불투명한 물체를 처리하기
			* 위한 과정이 구분된다. (즉, 불투명한 물체라 하더라도 투명한 물체를 처리하기 위한
			* Transparent 속성을 지정해주면 내부적으로 물체를 그려내는 과정에서 부하가 발생한다는
			* 것을 알 수 있다.)
			*/
			"Queue" = "Transparent+1"
			"RenderType" = "Transparent"
		}

		CGPROGRAM
		/*
		* alpha:fade 속성을 지정하면 반투명한 물체를 화면 상에 출력하는 것이 가능하다. (즉,
		* 해당 속성은 알파 값에 따라 물체의 투명도 여부를 설정하는 속성이라는 것을 알 수 있다.)
		*/
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
