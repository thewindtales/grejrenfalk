Shader "Custom/ControlTransparent" 
{
	Properties 
	{
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NormalMap ("Normal Map",2D) = "white" {}
		_Glossmap ("GlossMap", 2D) = "white"{}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_MetalMap ("MetalMap",2D) = "white" {}
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Coef("Transparent from Diffuse", Range(0,1)) = 0.0
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard alpha:fade fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _NormalMap;
		sampler2D _Glossmap;
		sampler2D _MetalMap;

		struct Input 
		{
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		half _Coef;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 gloss = tex2D (_Glossmap, IN.uv_MainTex);
			fixed4 metal = tex2D (_MetalMap, IN.uv_MainTex);
			fixed4 NM = tex2D (_NormalMap, IN.uv_MainTex);

			half4 grdif = (c.r + c.g + c.b)/3;
			grdif = pow(grdif, _Coef*10);
			half4 emis = clamp(NM.b, _Coef-0.1, _Coef+0.1);

			o.Albedo = c.rgb * grdif.rgb;
			o.Normal = UnpackNormal (NM);
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic * metal;
			o.Smoothness = _Glossiness * gloss;
			o.Emission =  emis;
			o.Alpha =grdif.r;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
