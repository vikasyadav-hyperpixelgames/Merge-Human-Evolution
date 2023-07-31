Shader "Custom/Clipping/SphereRimClipping" {
	Properties {
		_Pos ("Position", Vector) = (0,0,0,0)
		_Radius ("_Radius", Range(0, 20)) = 0
		_Soft ("_Soft", Range(0, 3)) = 0
		_Color ("Color", Vector) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_RimColor ("Rim Color", Vector) = (1,1,1,1)
		_RimPower ("Rim Power", Range(1, 6)) = 3
		_Color2 ("Color2", Vector) = (1,1,1,1)
		_MainTex2 ("Base2 (RGB)", 2D) = "white" {}
		_EmissionColor2 ("Emission color2", Vector) = (1,1,1,1)
		_WhiteOutValue ("White out value", Range(0, 1)) = 0
		_WhiteOutColor ("White out color", Vector) = (1,1,1,1)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Standard"
}