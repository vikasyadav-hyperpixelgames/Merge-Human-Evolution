Shader "Custom/Rainbow" {
	Properties {
		_Saturation ("Saturation", Range(0, 1)) = 0.8
		_Luminosity ("Luminosity", Range(0, 1)) = 0.5
		_Spread ("Spread", Range(0.5, 10)) = 3.8
		_Speed ("Speed", Range(-10, 10)) = 2.4
		_TimeOffset ("TimeOffset", Range(0, 6.2831855)) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Diffuse"
}