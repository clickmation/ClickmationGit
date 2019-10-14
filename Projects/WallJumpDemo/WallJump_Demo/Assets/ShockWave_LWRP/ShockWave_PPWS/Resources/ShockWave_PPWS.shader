/*
GaussianBlur_PPM.shader
uses properties and the global textures to generate a blurred effect on the screen.

*/


Shader "Custom/ShockWave_PPWS"
{
	Properties
	{

		_MainTex ("Texture", 2D) = "white" {}

		radius("radius",Float) = 0.1
		wavesize("wavesize",Float) = 0.1
		amplitude("amplitude",Float) = 0.1

		//tint("tint", Color) = (1,1,1,1)
		//saturation("saturation",Float) = 1
	}
	SubShader
	{
		Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }
		Pass
		{
            //Blend One One
			Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            ZTest Always
        
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
                float4 color : COLOR;

				//float2 guv : TEXCOORD1;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
                float alpha : TEXCOORD1;
                float4 projPos : TEXCOORD2;

				//float2 guv : TEXCOORD3;
			};

			sampler2D _MainTex;
            sampler2D_float _CameraDepthTexture;
			float4 _MainTex_ST;
            //float _Magnitude;
			//int _Iterations;

			float radius;
			float wavesize;
			float amplitude;

			//float4 tint;
			//float saturation;

			//TEXTURE2D_SAMPLER2D(_GlobalBlurTex, sampler_GlobalBlurTex);

			//sampler2D _GlobalBlurTex;
			//float4 _GlobalBlurTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
                
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.alpha = v.color.a;
				o.projPos = ComputeScreenPos(o.vertex);
				COMPUTE_EYEDEPTH(o.projPos.z);

				//o.guv = TRANSFORM_TEX(v.guv, _GlobalBlurTex);
                
				return o;
			}
			
			float4 frag (v2f i) : SV_Target
			{
                float sceneEyeDepth = DECODE_EYEDEPTH(tex2D(_CameraDepthTexture, i.projPos.xy / i.projPos.w));
                float zCull = sceneEyeDepth > i.projPos.z;
				
				//float4 this = tex2D(_MainTex, i.uv) * zCull;
				//float4 this = tex2D(_MainTex, i.uv);
				float4 this = float4(1,1,1,0);


				float2 diff = float2(i.uv.x - 0.5, i.uv.y - 0.5);
				float dist = sqrt(diff.x * diff.x + diff.y * diff.y);
				//float2 uv_displaced = float2(0,0);
				float4 uv_displaced = float4(0, 0,0,1);

				if (dist > radius)
				{
					if (dist < radius + wavesize)
					{
						float angle = (dist - radius) * 2 * 3.141592654 / wavesize;
						float cossin = (1 - cos(angle))*0.5;
						uv_displaced.x -= cossin * diff.x*amplitude / dist;
						uv_displaced.y -= cossin * diff.y*amplitude / dist;

						this.w = (1 - sqrt(pow(uv_displaced.x, 2) + pow(uv_displaced.y, 2)))/2;
						//this.w = disAmout/2;

						//tint = lerp(tint, float4(1, 1, 1, 1), this.w);
						//float3 intensity = dot(tint, float3(0.299, 0.587, 0.114));
						//tint.xyz = lerp(intensity.xyz, tint.xyz, saturation * 10);

						//this *= tint;
					}
				}

				return this * uv_displaced * zCull;
				

			}
			ENDCG
		}
	}
}

