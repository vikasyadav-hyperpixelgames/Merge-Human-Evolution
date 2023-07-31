Shader "Custom/Postprocessing_Blur" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_BlurSize ("Blur Size", Range(0, 0.5)) = 0
		[KeywordEnum(Low, Medium, High)] _Samples ("Sample amount", Float) = 0
		[Toggle(GAUSS)] _Gauss ("Gaussian Blur", Float) = 0
		[PowerSlider(3)] _StandardDeviation ("Standard Deviation (Gauss only)", Range(0, 0.3)) = 0.02
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}