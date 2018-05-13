﻿Shader "Custom/NewSurfaceShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		
		_DissolveFactor("DissolveFactor",Range(0.0,1.0)) = 0
		_DissolveTexture("Noise Texture",2D)="white"{}
		
		_BurnDistance("Burn Distance",Range(0.0,2.0))=0.0
		_BurnColor("Burn Color",Color)=(1,0,0,1)

	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue" = "Transparent"}
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha:blend

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		float _DissolveFactor;
		sampler2D _DissolveTexture; 
		float _BurnDistance;
		fixed4 _BurnColor;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 noise = tex2D(_DissolveTexture,IN.uv_MainTex)*_Color;

			float alpha = step(_DissolveFactor,noise);

			float value = smoothstep(noise-_BurnDistance,noise,_DissolveFactor);

			fixed3 color =lerp(c.rgb,_BurnColor.rgb,value);


			o.Albedo = color;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = alpha;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
