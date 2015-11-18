// Original shader by Davit Naskidashvili
// This shader is an adaptation of Naskidashvili's Subsurface Scattering Shader on the Unity Asset Store
// This shader is for educational purposes only in order to better understand Subsurface Scattering.
// Original Code belongs to Davit Naskidashvili

Shader "Custom/SSS_BumpedSpecular" {
	Properties {
		_Color ("Diffuse Color", Color) = (1,1,1,1)
		_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_Shininess ("Shininess", Range (0.03, 1)) = 0.078125
		_MainTex ("Diffuse Map", 2D) = "white" {}		
		_BumpSize("Bump Size", float) = 1
		_BumpMap ("Normal Map", 2D) = "bump" {}
		
		_TransDistortion ("Trans. Distortion",Range(0,0.5)) = 0.1
		_TransPower("Trans. Power",Range(1.0,16.0)) = 1.0
		_TransScale("Trans. Scale", Float) = 1.0
		
		_TransMap ("Translucency Map",2D) = "white" {}
		_TransColor ("Translucency Color", color) = (1, 1, 1, 1)
		_TransBackfaceIntensity("Backface Intensity", Float) = 0.15

		_TransDirectianalLightStrength("Directional Light Strength", Float) = 0.2
		_TransOtherLightsStrength("Spot/Point Light Strengths", Float) = 0.5
		_Emission("Emission", Float) = 0
		
		[MaterialToggle] _Rim_On("Rim On", Range(0, 1)) = 1
		_Rim_Color("Rim Color", color) = (1, 1, 1, 1)
		_Rim_Pow("Rim Power", Range(0.5, 8.0)) = 2.0
		
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" "SSSType"="Legacy/PixelLit"}
		LOD 400

		CGPROGRAM
		#pragma surface surf TransBlinnPhong noambient nodynlightmap
		#pragma target 3.0

		fixed4 _Color;
		sampler2D _MainTex;

		half _TransDistortion;
		half _TransPower;
		half _TransScale;

		half _TransDirectianalLightStrength;
		half _TransOtherLightsStrength;
		half _Emission;

		//for specular
		half _Shininess;

		//for bumped
		half _BumpSize;
		sampler2D _BumpMap;

		//for rim light
		fixed4 _Rim_Color;
		fixed _Rim_Pow;
		int _Rim_On;

		//for advanced translucency
		sampler2D _TransMap;
		fixed4 _TransColor;
		half _TransBackfaceIntensity;

	/////////////////////////////////////////////////////////////////// structs

		struct Input
		{
			float2 uv_MainTex;
			
			//for rim light
			float3 viewDir;

			//for bump
			float2 uv_BumpMap;
		};

		struct appdata 
		{
			float4 vertex : POSITION;
        	float4 tangent : TANGENT;
        	float3 normal : NORMAL;
        	float2 texcoord : TEXCOORD0;
		};

		struct TransSurfaceOutput
		{
			fixed3 Albedo;
			fixed3 Normal;
			fixed3 Emission;
			half Specular;
			fixed Gloss;
			fixed Alpha;
			fixed3 TransCol;
		};

	/////////////////////////////////////////////////////////////////// lighting
		inline fixed4 LightingTransBlinnPhong (TransSurfaceOutput s, fixed3 lightDir, half3 viewDir, fixed atten)
		{	
			half atten2 = (atten * 2);

			fixed3 diffCol;
			fixed3 specCol;
			float spec;	
		
			half NL = dot (s.Normal, lightDir);

			half3 h = normalize (lightDir + viewDir);
		
			float nh = max (0, dot (s.Normal, h));
			spec = pow (nh, s.Specular*128.0) * s.Gloss;
		
			diffCol = (s.Albedo * _LightColor0.rgb * NL) * atten2;
			specCol = (_LightColor0.rgb * _SpecColor.rgb * spec) * atten2;

			half3 transLight = lightDir + s.Normal * _TransDistortion;
			float VinvL = saturate(dot(viewDir, -transLight));
		
			float transDot = pow(VinvL,_TransPower);
			//for advanced translucency
			transDot *= _TransScale;

			half3 lightAtten = _LightColor0.rgb * atten2;
				//lightAtten *= _TransDirectianalLightStrength;
				//lightAtten *= _TransOtherLightsStrength;
				#ifdef UNITY_PASS_FORWARDBASE
					lightAtten *= _TransDirectianalLightStrength;
				#else
					lightAtten *= _TransOtherLightsStrength;
				#endif

			half3 transComponent = (transDot + _Color.rgb);

			//for advanced translucency
				half3 subSurfaceComponent = s.TransCol * _TransScale;	
				transComponent = lerp(transComponent, subSurfaceComponent, transDot);		
				transComponent += (1 - NL) * s.TransCol * _LightColor0.rgb * _TransBackfaceIntensity;

			diffCol = s.Albedo * (_LightColor0.rgb * atten2 * NL + lightAtten * transComponent);

		
			fixed4 c;
			c.rgb = diffCol + specCol * 2;
			c.a = s.Alpha + _LightColor0.a * _SpecColor.a * spec * atten;
			return c;
		}

		void surf (Input IN, inout TransSurfaceOutput o)
		{
			half4 tex = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = tex.rgb * _Color.rgb;
			o.Alpha = tex.a * _Color.a;
			
			//for advanced translucency
			o.TransCol = tex2D(_TransMap,IN.uv_MainTex).rgb * _TransColor.rgb;
			
			//for specular
			o.Gloss = tex.a;
			o.Specular = _Shininess;

			//bumped
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			o.Normal.x *= _BumpSize;
			o.Normal.y *= _BumpSize;
			o.Normal = normalize(o.Normal);

			o.Emission += o.Albedo * _Emission * o.Alpha;

			//for rim light
			if (_Rim_On == 1) {
			half rim = 1.0 - saturate(dot (Unity_SafeNormalize(IN.viewDir), o.Normal));
		        o.Emission += _Rim_Color.rgb * pow (rim, _Rim_Pow);
		    }
		}

		ENDCG
	} 
	FallBack "Legacy Shaders/Diffuse"
}
