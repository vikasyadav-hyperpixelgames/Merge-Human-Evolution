Shader "Hidden/Fog Of War Effect" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Position ("Position", Vector) = (0,0,0,0)
		_Radius ("Radius", Float) = 0.1
		_Aspect ("Aspect", Float) = 1
		_Projection ("_Projection", Float) = 1
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