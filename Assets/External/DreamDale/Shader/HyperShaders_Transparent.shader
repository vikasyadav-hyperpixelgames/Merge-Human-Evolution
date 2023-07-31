Shader "HyperShaders/Transparent" {
	Properties {
		_MainColor ("Main Color", Vector) = (1,1,1,1)
		_ShadowColor ("Shadow Color", Vector) = (0.5,0.5,0.5,1)
		[Toggle(SF_MAIN_TEXTURE)] _SF_MAIN_TEXTURE ("Main texture", Float) = 0
		_MainTex ("Main Texture", 2D) = "white" {}
		[Toggle(SF_NORMAL_TEXTURE)] _SF_NORMAL_TEXTURE ("Normal texture", Float) = 0
		_NormalTex ("Normal texture", 2D) = "bump" {}
		[Toggle(SF_DIFFUSE)] _SF_DIFFUSE ("Diffuse", Float) = 0
		[Toggle(SF_DIFFUSE_RAMP)] _SF_DIFFUSE_RAMP ("Diffuse ramp", Float) = 0
		_MainColorRampTex ("Diffuse Ramp Map", 2D) = "white" {}
		[Toggle(SF_RIM_LIGHT)] _SF_RIM_LIGHT ("Rim Light", Float) = 0
		_RimColor ("Rim Color", Vector) = (1,1,1,1)
		_RimLightPower ("Rim Light Power", Float) = 1
		[Toggle(SF_SPECULAR)] _SF_SPECULAR ("Specular", Float) = 0
		_SpecTex ("Specular Texture", 2D) = "white" {}
		_SpecShininess ("Specular Shininess", Range(0, 1)) = 0.5
		_SpecIntensity ("Specular Intensity", Vector) = (1,1,1,1)
		[Toggle(SF_REFLECTION)] _SF_REFLECTION ("Reflection", Float) = 0
		_ReflectionTex ("Reflection Texture", 2D) = "white" {}
		_ReflectionCube ("Reflection Cube", Cube) = "" {}
		[Toggle(SF_EMISSION)] _SF_EMISSION ("Emission", Float) = 0
		_EmissionMap ("Emission Map", 2D) = "black" {}
		_EmissionIntensity ("Emission Intensity", Range(0, 1)) = 1
		[Toggle(SF_COLOR_LERP)] _SF_COLOR_LERP ("Color Lerp", Float) = 0
		_ColorToLerp ("Color To Lerp", Vector) = (1,1,1,1)
		_LerpValue ("Lerp Value", Range(0, 1)) = 0
		[Toggle(SF_SCROLL_UV)] _SF_SCROLL_UV ("Scroll UV", Float) = 0
		_ScrollUVSpeed ("Scroll UV Speed", Vector) = (0,0,0,0)
		[Toggle] _ZWriteOpt ("ZWrite", Float) = 0
		[Enum(UnityEngine.Rendering.CullMode)] _Culling ("Culling", Float) = 2
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendSource ("Blend Source", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _BlendTarget ("Blend Target", Float) = 10
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
	Fallback "Diffuse"
}