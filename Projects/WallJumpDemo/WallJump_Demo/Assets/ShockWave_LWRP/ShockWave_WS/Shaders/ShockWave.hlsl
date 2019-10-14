/*
Texture2D tex,
SamplerState texSS, 
float2 uv,
float2 screenSize, 
float iterations, 
float kernel ,
out float4 color
*/

//void GaussianBlur_float(Texture2D tex,SamplerState texSS, float4 uv,float2 screenSize, float iterations, float kernel ,out float4 color)


/*
Texture2D tex, 
SamplerState texSS, 
float4 uv, 
float2 center,
float radius, 
float wavesize, 
float amplitude,
out float4 color
*/

void ShockWave_float(
	Texture2D tex, 
	SamplerState texSS, 
	float4 spuv, 
	float4 uv, 
	float radius, 
	float wavesize, 
	float amplitude, 
	out float4 color,
	out float displacement
)
//void ShockWave_float(Texture2D tex, SamplerState texSS, float4 uv, out float4 color)
{
	//color = tex.Sample(texSS, float2(uv.x, uv.y));

	//ScreenRatio = _ScreenParams.y / _ScreenParams.x;

	color = tex.Sample(texSS, spuv.xy);


	float2 diff = float2(uv.x - 0.5, uv.y - 0.5);
	float dist = sqrt(diff.x * diff.x + diff.y * diff.y);
	//float2 uv_displaced = float2(uv.x, uv.y);
	float2 uv_displaced = spuv.xy;

	//displacement = float2(0,0);

	if (dist > radius)
	{
		if (dist < radius + wavesize)
		{
			float angle = (dist - radius) * 2 * 3.141592654 / wavesize;
			float cossin = (1 - cos(angle))*0.5;
			uv_displaced.x -= cossin * diff.x*amplitude / dist;
			uv_displaced.y -= cossin * diff.y*amplitude / dist;
		}
	}

	float2 disDiff = float2(uv_displaced.x - spuv.x, uv_displaced.y - spuv.y);
	displacement = 1- sqrt(disDiff.x * disDiff.x + disDiff.y * disDiff.y);

	


	//if (uv.y > 0.25 && uv.y < 0.75)
	//{
	//	if (uv.x > 0.25 && uv.x < 0.75)
	//	{
	//		color += float4(0, 1, 0, 0.25);
	//	}
	//}

	//if (dist < 0.1)
	//{
	//	color += float4(0, 1, 0, 0.25);
	//}


	//if (uv.y < 0.5)
	//{
	//	if (uv.x > 0.5)
	//	{
	//		color += float4(1, 0, 0, 0.5);
	//	}
	//	
	//}
	
	color = tex.Sample(texSS, uv_displaced);
}
