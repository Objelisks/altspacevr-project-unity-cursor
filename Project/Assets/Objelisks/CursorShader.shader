Shader "Custom/CursorShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		// Draw the object as transparent, so that it doesn't do any fancy depth culling tricks.
		Tags {"Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Opaque"}
		LOD 200

		// Always draw object, ignoring existing depth values
		ZTest Always

		CGPROGRAM

		#pragma surface surf Flat

		sampler2D _MainTex;

		// Ignore all lighting, passthrough color
		half4 LightingFlat (SurfaceOutput s, half3 lightDir, half atten) {
			half4 c;
			c.rgb = s.Albedo;
			c.a = s.Alpha;
			return c;
		}

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = 1.0;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
