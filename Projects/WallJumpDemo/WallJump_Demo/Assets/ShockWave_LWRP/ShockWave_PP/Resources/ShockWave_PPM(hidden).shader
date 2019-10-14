// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/ShockWave_PPM"
{

	HLSLINCLUDE
        
			// This include uses a relative path, and may need to be updated if shaders are moved or
			// a different version of Unity's PostFX stack is being used (eg. if your version of the
			// project imports the PostFX stack from Package Manager).
			//#include "../../../../../../PostProcessing/Shaders/StdLib.hlsl"
			//#include "Packages/com.unity.postprocessing/Shaders/StdLib.hlsl"
			#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

			TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
			float4 _MainTex_TexelSize;

			
			float ScreenRatio; //Width/Height
			uniform sampler2D _CameraOpaqueTexture;

			float2 SW_Center00;
			float1 SW_Radius00;
			float1 SW_Amplitude00;
			float1 SW_WaveSize00;
			float4 SW_Color00;
			float1 SW_Sat00;

			float2 SW_Center01;
			float1 SW_Radius01;
			float1 SW_Amplitude01;
			float1 SW_WaveSize01;
			float4 SW_Color01;
			float1 SW_Sat01;

			float2 SW_Center02;
			float1 SW_Radius02;
			float1 SW_Amplitude02;
			float1 SW_WaveSize02;
			float4 SW_Color02;
			float1 SW_Sat02;

			float2 SW_Center03;
			float1 SW_Radius03;
			float1 SW_Amplitude03;
			float1 SW_WaveSize03;
			float4 SW_Color03;
			float1 SW_Sat03;

			float2 SW_Center04;
			float1 SW_Radius04;
			float1 SW_Amplitude04;
			float1 SW_WaveSize04;
			float4 SW_Color04;
			float1 SW_Sat04;

			float2 SW_Center05;
			float1 SW_Radius05;
			float1 SW_Amplitude05;
			float1 SW_WaveSize05;
			float4 SW_Color05;
			float1 SW_Sat05;

			float2 SW_Center06;
			float1 SW_Radius06;
			float1 SW_Amplitude06;
			float1 SW_WaveSize06;
			float4 SW_Color06;
			float1 SW_Sat06;

			float2 SW_Center07;
			float1 SW_Radius07;
			float1 SW_Amplitude07;
			float1 SW_WaveSize07;
			float4 SW_Color07;
			float1 SW_Sat07;

			float2 SW_Center08;
			float1 SW_Radius08;
			float1 SW_Amplitude08;
			float1 SW_WaveSize08;
			float4 SW_Color08;
			float1 SW_Sat08;

			float2 SW_Center09;
			float1 SW_Radius09;
			float1 SW_Amplitude09;
			float1 SW_WaveSize09;
			float4 SW_Color09;
			float1 SW_Sat09;
			
			float4 Frag(VaryingsDefault i) : SV_Target
			{

				ScreenRatio = _ScreenParams.y / _ScreenParams.x;

				float2 diff = float2(0, 0);
				float dist = 0;
				float2 uv_displaced = float2(i.texcoord.x, i.texcoord.y);
				float2 tempDisplaced = float2(i.texcoord.x, i.texcoord.y);
				float angle = 0;
				float cossin = 0;

				float4 color = float4(1, 1, 1, 1);
				float4 thisColor = float4(1, 1, 1, 1);

				float2 disDiff = float2(1, 1);
				float displacement = 0;
				float3 intensity = float3(1, 1, 1);

				//00
				tempDisplaced = float2(i.texcoord.x, i.texcoord.y);
				thisColor = float4(1, 1, 1, 1);
				diff = float2(i.texcoord.x - SW_Center00.x, (i.texcoord.y - SW_Center00.y) * ScreenRatio);
				dist = sqrt(diff.x*diff.x + diff.y*diff.y);
				if (dist > SW_Radius00)
				{
					if (dist < SW_Radius00 + SW_WaveSize00)
					{
						angle = (dist - SW_Radius00) * 2 * 3.141592654 / SW_WaveSize00;
						cossin = (1 - cos(angle))*0.5;
						float2 displ = float2(cossin * diff.x*SW_Amplitude00 / dist, cossin * diff.y*SW_Amplitude00 / dist);
						uv_displaced -= displ;
						tempDisplaced -= displ;
					}
				}
				disDiff = float2(tempDisplaced.x - i.texcoord.x, tempDisplaced.y - i.texcoord.y);
				displacement = 1 - sqrt(disDiff.x * disDiff.x + disDiff.y * disDiff.y);
				thisColor = lerp(SW_Color00, float4(1, 1, 1, 1), displacement);
				intensity = dot(thisColor, float3(0.299, 0.587, 0.114));
				thisColor.xyz = lerp(intensity.xyz, thisColor.xyz, SW_Sat00 * 10);
				color *= thisColor;
				//

				//01
				tempDisplaced = float2(i.texcoord.x, i.texcoord.y);
				thisColor = float4(1, 1, 1, 1);
				diff = float2(i.texcoord.x - SW_Center01.x, (i.texcoord.y - SW_Center01.y) * ScreenRatio);
				dist = sqrt(diff.x*diff.x + diff.y*diff.y);
				if (dist > SW_Radius01)
				{
					if (dist < SW_Radius01 + SW_WaveSize01)
					{
						angle = (dist - SW_Radius01) * 2 * 3.141592654 / SW_WaveSize01;
						cossin = (1 - cos(angle))*0.5;
						float2 displ = float2(cossin * diff.x*SW_Amplitude01 / dist, cossin * diff.y*SW_Amplitude01 / dist);
						uv_displaced -= displ;
						tempDisplaced -= displ;
					}
				}
				disDiff = float2(tempDisplaced.x - i.texcoord.x, tempDisplaced.y - i.texcoord.y);
				displacement = 1 - sqrt(disDiff.x * disDiff.x + disDiff.y * disDiff.y);
				thisColor = lerp(SW_Color01, float4(1, 1, 1, 1), displacement);
				intensity = dot(thisColor, float3(0.299, 0.587, 0.114));
				thisColor.xyz = lerp(intensity.xyz, thisColor.xyz, SW_Sat01 * 10);
				color *= thisColor;
				//

				//02
				tempDisplaced = float2(i.texcoord.x, i.texcoord.y);
				thisColor = float4(1, 1, 1, 1);
				diff = float2(i.texcoord.x - SW_Center02.x, (i.texcoord.y - SW_Center02.y) * ScreenRatio);
				dist = sqrt(diff.x*diff.x + diff.y*diff.y);
				if (dist > SW_Radius02)
				{
					if (dist < SW_Radius02 + SW_WaveSize02)
					{
						angle = (dist - SW_Radius02) * 2 * 3.141592654 / SW_WaveSize02;
						cossin = (1 - cos(angle))*0.5;
						float2 displ = float2(cossin * diff.x*SW_Amplitude02 / dist, cossin * diff.y*SW_Amplitude02 / dist);
						uv_displaced -= displ;
						tempDisplaced -= displ;
					}
				}
				disDiff = float2(tempDisplaced.x - i.texcoord.x, tempDisplaced.y - i.texcoord.y);
				displacement = 1 - sqrt(disDiff.x * disDiff.x + disDiff.y * disDiff.y);
				thisColor = lerp(SW_Color02, float4(1, 1, 1, 1), displacement);
				intensity = dot(thisColor, float3(0.299, 0.587, 0.114));
				thisColor.xyz = lerp(intensity.xyz, thisColor.xyz, SW_Sat02 * 10);
				color *= thisColor;
				//

				//03
				tempDisplaced = float2(i.texcoord.x, i.texcoord.y);
				thisColor = float4(1, 1, 1, 1);
				diff = float2(i.texcoord.x - SW_Center03.x, (i.texcoord.y - SW_Center03.y) * ScreenRatio);
				dist = sqrt(diff.x*diff.x + diff.y*diff.y);
				if (dist > SW_Radius03)
				{
					if (dist < SW_Radius03 + SW_WaveSize03)
					{
						angle = (dist - SW_Radius03) * 2 * 3.141592654 / SW_WaveSize03;
						cossin = (1 - cos(angle))*0.5;
						float2 displ = float2(cossin * diff.x*SW_Amplitude03 / dist, cossin * diff.y*SW_Amplitude03 / dist);
						uv_displaced -= displ;
						tempDisplaced -= displ;
					}
				}
				disDiff = float2(tempDisplaced.x - i.texcoord.x, tempDisplaced.y - i.texcoord.y);
				displacement = 1 - sqrt(disDiff.x * disDiff.x + disDiff.y * disDiff.y);
				thisColor = lerp(SW_Color03, float4(1, 1, 1, 1), displacement);
				intensity = dot(thisColor, float3(0.299, 0.587, 0.114));
				thisColor.xyz = lerp(intensity.xyz, thisColor.xyz, SW_Sat03 * 10);
				color *= thisColor;
				//

				//04
				tempDisplaced = float2(i.texcoord.x, i.texcoord.y);
				thisColor = float4(1, 1, 1, 1);
				diff = float2(i.texcoord.x - SW_Center04.x, (i.texcoord.y - SW_Center04.y) * ScreenRatio);
				dist = sqrt(diff.x*diff.x + diff.y*diff.y);
				if (dist > SW_Radius04)
				{
					if (dist < SW_Radius04 + SW_WaveSize04)
					{
						angle = (dist - SW_Radius04) * 2 * 3.141592654 / SW_WaveSize04;
						cossin = (1 - cos(angle))*0.5;
						float2 displ = float2(cossin * diff.x*SW_Amplitude04 / dist, cossin * diff.y*SW_Amplitude04 / dist);
						uv_displaced -= displ;
						tempDisplaced -= displ;
					}
				}
				disDiff = float2(tempDisplaced.x - i.texcoord.x, tempDisplaced.y - i.texcoord.y);
				displacement = 1 - sqrt(disDiff.x * disDiff.x + disDiff.y * disDiff.y);
				thisColor = lerp(SW_Color04, float4(1, 1, 1, 1), displacement);
				intensity = dot(thisColor, float3(0.299, 0.587, 0.114));
				thisColor.xyz = lerp(intensity.xyz, thisColor.xyz, SW_Sat04 * 10);
				color *= thisColor;
				//

				//05
				tempDisplaced = float2(i.texcoord.x, i.texcoord.y);
				thisColor = float4(1, 1, 1, 1);
				diff = float2(i.texcoord.x - SW_Center05.x, (i.texcoord.y - SW_Center05.y) * ScreenRatio);
				dist = sqrt(diff.x*diff.x + diff.y*diff.y);
				if (dist > SW_Radius05)
				{
					if (dist < SW_Radius05 + SW_WaveSize05)
					{
						angle = (dist - SW_Radius05) * 2 * 3.141592654 / SW_WaveSize05;
						cossin = (1 - cos(angle))*0.5;
						float2 displ = float2(cossin * diff.x*SW_Amplitude05 / dist, cossin * diff.y*SW_Amplitude05 / dist);
						uv_displaced -= displ;
						tempDisplaced -= displ;
					}
				}
				disDiff = float2(tempDisplaced.x - i.texcoord.x, tempDisplaced.y - i.texcoord.y);
				displacement = 1 - sqrt(disDiff.x * disDiff.x + disDiff.y * disDiff.y);
				thisColor = lerp(SW_Color05, float4(1, 1, 1, 1), displacement);
				intensity = dot(thisColor, float3(0.299, 0.587, 0.114));
				thisColor.xyz = lerp(intensity.xyz, thisColor.xyz, SW_Sat05 * 10);
				color *= thisColor;
				//

				//06
				tempDisplaced = float2(i.texcoord.x, i.texcoord.y);
				thisColor = float4(1, 1, 1, 1);
				diff = float2(i.texcoord.x - SW_Center06.x, (i.texcoord.y - SW_Center06.y) * ScreenRatio);
				dist = sqrt(diff.x*diff.x + diff.y*diff.y);
				if (dist > SW_Radius06)
				{
					if (dist < SW_Radius06 + SW_WaveSize06)
					{
						angle = (dist - SW_Radius06) * 2 * 3.141592654 / SW_WaveSize06;
						cossin = (1 - cos(angle))*0.5;
						float2 displ = float2(cossin * diff.x*SW_Amplitude06 / dist, cossin * diff.y*SW_Amplitude06 / dist);
						uv_displaced -= displ;
						tempDisplaced -= displ;
					}
				}
				disDiff = float2(tempDisplaced.x - i.texcoord.x, tempDisplaced.y - i.texcoord.y);
				displacement = 1 - sqrt(disDiff.x * disDiff.x + disDiff.y * disDiff.y);
				thisColor = lerp(SW_Color06, float4(1, 1, 1, 1), displacement);
				intensity = dot(thisColor, float3(0.299, 0.587, 0.114));
				thisColor.xyz = lerp(intensity.xyz, thisColor.xyz, SW_Sat06 * 10);
				color *= thisColor;
				//

				//07
				tempDisplaced = float2(i.texcoord.x, i.texcoord.y);
				thisColor = float4(1, 1, 1, 1);
				diff = float2(i.texcoord.x - SW_Center07.x, (i.texcoord.y - SW_Center07.y) * ScreenRatio);
				dist = sqrt(diff.x*diff.x + diff.y*diff.y);
				if (dist > SW_Radius07)
				{
					if (dist < SW_Radius07 + SW_WaveSize07)
					{
						angle = (dist - SW_Radius07) * 2 * 3.141592654 / SW_WaveSize07;
						cossin = (1 - cos(angle))*0.5;
						float2 displ = float2(cossin * diff.x*SW_Amplitude07 / dist, cossin * diff.y*SW_Amplitude07 / dist);
						uv_displaced -= displ;
						tempDisplaced -= displ;
					}
				}
				disDiff = float2(tempDisplaced.x - i.texcoord.x, tempDisplaced.y - i.texcoord.y);
				displacement = 1 - sqrt(disDiff.x * disDiff.x + disDiff.y * disDiff.y);
				thisColor = lerp(SW_Color07, float4(1, 1, 1, 1), displacement);
				intensity = dot(thisColor, float3(0.299, 0.587, 0.114));
				thisColor.xyz = lerp(intensity.xyz, thisColor.xyz, SW_Sat07 * 10);
				color *= thisColor;
				//

				//08
				tempDisplaced = float2(i.texcoord.x, i.texcoord.y);
				thisColor = float4(1, 1, 1, 1);
				diff = float2(i.texcoord.x - SW_Center08.x, (i.texcoord.y - SW_Center08.y) * ScreenRatio);
				dist = sqrt(diff.x*diff.x + diff.y*diff.y);
				if (dist > SW_Radius08)
				{
					if (dist < SW_Radius08 + SW_WaveSize08)
					{
						angle = (dist - SW_Radius08) * 2 * 3.141592654 / SW_WaveSize08;
						cossin = (1 - cos(angle))*0.5;
						float2 displ = float2(cossin * diff.x*SW_Amplitude08 / dist, cossin * diff.y*SW_Amplitude08 / dist);
						uv_displaced -= displ;
						tempDisplaced -= displ;
					}
				}
				disDiff = float2(tempDisplaced.x - i.texcoord.x, tempDisplaced.y - i.texcoord.y);
				displacement = 1 - sqrt(disDiff.x * disDiff.x + disDiff.y * disDiff.y);
				thisColor = lerp(SW_Color08, float4(1, 1, 1, 1), displacement);
				intensity = dot(thisColor, float3(0.299, 0.587, 0.114));
				thisColor.xyz = lerp(intensity.xyz, thisColor.xyz, SW_Sat08 * 10);
				color *= thisColor;
				//

				//09
				tempDisplaced = float2(i.texcoord.x, i.texcoord.y);
				thisColor = float4(1, 1, 1, 1);
				diff = float2(i.texcoord.x - SW_Center09.x, (i.texcoord.y - SW_Center09.y) * ScreenRatio);
				dist = sqrt(diff.x*diff.x + diff.y*diff.y);
				if (dist > SW_Radius09)
				{
					if (dist < SW_Radius09 + SW_WaveSize09)
					{
						angle = (dist - SW_Radius09) * 2 * 3.141592654 / SW_WaveSize09;
						cossin = (1 - cos(angle))*0.5;
						float2 displ = float2(cossin * diff.x*SW_Amplitude09 / dist, cossin * diff.y*SW_Amplitude09 / dist);
						uv_displaced -= displ;
						tempDisplaced -= displ;
					}
				}
				disDiff = float2(tempDisplaced.x - i.texcoord.x, tempDisplaced.y - i.texcoord.y);
				displacement = 1 - sqrt(disDiff.x * disDiff.x + disDiff.y * disDiff.y);
				thisColor = lerp(SW_Color09, float4(1, 1, 1, 1), displacement);
				intensity = dot(thisColor, float3(0.299, 0.587, 0.114));
				thisColor.xyz = lerp(intensity.xyz, thisColor.xyz, SW_Sat09 * 10);
				color *= thisColor;
				//


				float4 orgCol = tex2D(_CameraOpaqueTexture, uv_displaced); //Get the orginal rendered color
				orgCol *= color;
				return orgCol;

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








