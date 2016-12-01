//This is my first shader.
Shader "LavrChar"
{
	Properties 
	{
		_Base ("First diffuse map", 2D) = "white" {}
		_Shininess ("Specular focus", Range(1,100)) = 1
		_Blik("Spec power", Range (0,5)) = 0
		Merge("Mix diffuse 1 and diffuse 2", Range(0,1)) = 0
		_NormalMap ("Normal", 2D) = "bump" {}
		_Mask ("CubeMask", 2D)= "gray"{}
		Mask_gr ("Decolor mask (On >>->)", Range(0,1)) = 0
		_Cube ("CubeMap", CUBE) = "" {}
		_RefVal("Reflection Value", Range (0,1)) = 0
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		Cull off
		
		CGPROGRAM
		#pragma surface surf BlinnPhong2 fullforwardshadows
		#pragma target 3.0

		struct MySurfaceOutput
      {
		float3 Albedo;
		float3 Normal;
		float3 Gloss;
		float3 Specular;
		float3 Emission;
		float Alpha;
		float Multi;
		float Custom;
      };
      
		fixed4 LightingBlinnPhong2_PrePass (MySurfaceOutput s, half4 light)
	{
		fixed spec = light.a * s.Gloss * s.Specular * s.Multi;
	
		fixed4 c;
		c.rgb = (s.Albedo * light.rgb + light.rgb * _SpecColor.rgb * spec);
		c.a = s.Alpha + spec * _SpecColor.a;
		return c;
	}
      
      float4 LightingBlinnPhong2 (MySurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
      { 
      	
		float3 h = normalize (lightDir + viewDir);
		float3 n = normalize (s.Normal);
		float3 v = normalize (viewDir);
		float3 l = normalize (lightDir);
		
		float3 r    = reflect ( -v, n );
		float4 diff = max(0,dot(n,l));
		float nh = max(0,dot(l,r));
		float3 spec = pow(nh,s.Gloss) * s.Specular * s.Multi;
		
		float4 te = 0;
		te.rgb =s.Albedo * diff * _LightColor0.rgb + spec * _LightColor0.rgb;
		te.a = s.Alpha + spec * _SpecColor.a;
		//te.a = 0.0;
		te *= atten * 2;
		return te;
      }
      
		struct Input 
		{
			float2 uv_Base;
			float2 uv_Base2;
			float2 uv_NormalMap;
			float2 uv_Mask;
			float3 worldRefl;
			INTERNAL_DATA
			
		};
		
		sampler2D _Base;
		sampler2D _NormalMap;
		sampler2D _Mask;
		samplerCUBE _Cube;
		float _RefVal;
		float Merge;
		float Mask_gr;
		float _Shininess;
		float _Blik;
		
		void surf (Input IN, inout MySurfaceOutput o)
		{
			float4 tex = tex2D (_Base, IN.uv_Base);// Первый дифуз тут
			float4 NM = tex2D (_NormalMap, IN.uv_Base);
			float3 NormalMap = UnpackNormal (NM);//карта нормалей помноженная на усилитель
			float4 Mask = tex2D (_Mask, IN.uv_Base);//Тут маска кубомапы, можно оставить цветной, или обецветить далее
			if(Mask_gr==1)
			{
				Mask.rgb = (Mask.r + Mask.g + Mask.b)/3;
			}
			
			o.Albedo = tex.rgb;
			o.Normal = NormalMap;
			o.Emission = texCUBE (_Cube, WorldReflectionVector (IN, o.Normal)).rgb * Mask.rgb * _RefVal;
			o.Specular = tex2D (_Base, IN.uv_Base).a;
			o.Gloss = _Shininess;
			o.Multi = _Blik;
		}
		
		
		ENDCG
	} 
	FallBack "Diffuse"
}
