﻿Shader "Custom/UnlitTexAlpha" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	}
	SubShader {
		Tags { "Queue"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Unlit noambient alphatest:_Cutoff

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		
		half4 LightingUnlit (SurfaceOutput s, half3 lightDir, half atten) {
              half4 c;
              c.rgb = s.Albedo;
              c.a = s.Alpha;
              return c;
          }


		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};
		
		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG		
	} 
	FallBack "Diffuse"
}
