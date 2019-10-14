// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/ShockWave_PPWS"
{

	HLSLINCLUDE
        
			// This include uses a relative path, and may need to be updated if shaders are moved or
			// a different version of Unity's PostFX stack is being used (eg. if your version of the
			// project imports the PostFX stack from Package Manager).
			//#include "../../../../../../PostProcessing/Shaders/StdLib.hlsl"
			//#include "Packages/com.unity.postprocessing/Shaders/StdLib.hlsl"
			#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

			TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
			TEXTURE2D_SAMPLER2D(_GlobalSWWSTex, sampler_GlobalSWWSTex);
			float4 _MainTex_TexelSize;

			//int _Iterations;
			//float _Lightness;
			//float4 tint;
			//float saturation;
			//float4 _Tint;

			
			float3 Frag(VaryingsDefault i) : SV_Target
			{
				float4 SW_displacement = SAMPLE_TEXTURE2D(_GlobalSWWSTex, sampler_GlobalSWWSTex, i.texcoord);
				float4 MainDisplaced = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + SW_displacement.xy);
				//float4 MainDisplaced = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
				
				return MainDisplaced.xyz;

			}

	ENDHLSL

	SubShader
	{
		Cull Off
		ZWrite Off
		ZTest Always

		Pass
		{
			HLSLPROGRAM

				#pragma vertex VertDefault
				#pragma fragment Frag

			ENDHLSL
			
		}

	}
}








