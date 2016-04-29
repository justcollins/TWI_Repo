Shader "FX/Force Field" 
{
	Properties 
	{
	   _Color ("Color1", Color) = (1,1,1,1)
	   _Color2 ("Color2", Color) = (1,1,1,1)
	   _Rate ("Oscillation Rate", Range (5, 200)) = 50
	   _Scale ("Scale", Range (0.02, 0.5)) = 0.25
	   _Distortion ("Distortion", Range (0.1, 20)) = 1
	}
 
	SubShader 
	{
	   ZWrite Off //affects depth. Meant to be off for transparenices
	   Tags { "Queue" = "Transparent" }
	   Blend One One //an additive, blends the colors together&make transparent
//	   ZWrite On
	   Cull Off //so we see it inside the object
   
	   Pass 
	   {	//accessing CGPROGRAM libraries
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_fog_exp2
			#include "UnityCG.cginc"
 			
 			//connects values from the Inspector
			float4 _Color;
			float4 _Color2;
			float _Rate;
			float _Scale;
			float _Distortion;
 			
 			
			struct v2f //v2f now stores all the variables in one block
			{
			   float4 pos : SV_POSITION;
			   float3 uv : TEXCOORD0;
			   float3 vert : TEXCOORD1;
			};
 
			v2f vert (appdata_base v)//instead of half4, it's using v2f
			{
				float s = 1 / _Scale; //s and t is the same as u and v
				float t = _Time[0]*_Rate*_Scale;
				v2f o;
				//o's values in terms of position,verts,uvs	 //mul multiplies a matrix with a matrix
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);//
   				o.vert = sin((v.vertex.xyz + t) * s);  
				o.uv = sin((v.texcoord.xyz + t) * s) * _Distortion;
   
				return o;//return all the values just assigned
			}
 
			half4 frag (v2f i) : COLOR
			{
				float3 vert = i.vert;
				float3 uv = i.uv;
				float mix = 1 + sin((vert.x - uv.x) + (vert.y - uv.y) + (vert.z - uv.z));
				float mix2 = 1 + sin((vert.x + uv.x) - (vert.y + uv.y) - (vert.z + uv.z)) / _Distortion;
   
				return half4( (_Color * mix * 0.3) + (_Color2 * mix2 * 0.3));
			}

			ENDCG
		}
	}
	Fallback "Transparent/Diffuse"
}