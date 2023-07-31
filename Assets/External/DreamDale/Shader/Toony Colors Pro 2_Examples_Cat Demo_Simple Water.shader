Shader "Toony Colors Pro 2/Examples/Cat Demo/Simple Water" {
	Properties {
		[TCP2HelpBox(Warning,Make sure that the Camera renders the depth texture for this material to work properly.    You can use the script __TCP2_CameraDepth__ for this.)] [TCP2HeaderHelp(BASE, Base Properties)] _HColor ("Highlight Color", Vector) = (0.6,0.6,0.6,1)
		_SColor ("Shadow Color", Vector) = (0.3,0.3,0.3,1)
		_MainTex ("Main Texture (RGB)", 2D) = "white" {}
		[TCP2Separator] _RampThreshold ("Ramp Threshold", Range(0, 1)) = 0.5
		_RampSmooth ("Ramp Smoothing", Range(0.001, 1)) = 0.1
		[TCP2Separator] [TCP2HeaderHelp(WATER)] _Color ("Water Color", Vector) = (0.5,0.5,0.5,1)
		[Header(Vertex Waves Animation)] _WaveSpeed ("Speed", Float) = 2
		_WaveHeight ("Height", Float) = 0.1
		_WaveFrequency ("Frequency", Range(0, 10)) = 1
		[Header(UV Waves Animation)] _UVWaveSpeed ("Speed", Float) = 1
		_UVWaveAmplitude ("Amplitude", Range(0.001, 0.5)) = 0.05
		_UVWaveFrequency ("Frequency", Range(0, 10)) = 1
		[TCP2Separator] [HideInInspector] __dummy__ ("unused", Float) = 0
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
	//CustomEditor "TCP2_MaterialInspector_SG"
}