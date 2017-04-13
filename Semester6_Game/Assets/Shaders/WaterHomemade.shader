Shader "Water/WaterHomemade" {
	Properties{
		_Color("Tint Color", Color) = (1,1,1,0.5)
		_ReflectColor("Reflection Color", Color) = (1,1,1,0.5)
		_ReflCube("Reflection Cubemap", Cube) = "" {}
	_BumpMap("Normalmap", 2D) = "bump" {}
	_BumpMap2("Normalmap 2", 2D) = "bump" {}
	_Noise("Noise Map", 2D) = "black" {}
	_Cycle("Wave Cycles", Float) = 1.0
		_WaveDirection("Wave Direction (X)(Y)", Vector) = (0,0,0,0)
		_WaveSpeed("Wave Speed", Range(0.0,0.5)) = 0.1
	_EdgeBlend("Edge Blending", Range(0.0,3.0)) = 0.1
		_Transparency("Transparency", Range(1.0,0.0)) = 1.0
		_DistortionAmount("Amount of Distorton",Range(0.0,100.0)) = 50.0
		_AlphaMap("Alpha Map", 2D) = "white" {}
	}

		Category{
		Tags{ "Queue" = "Transparent" "RenderType" = "Opaque" }
		LOD 100

		SubShader{
		Blend SrcAlpha OneMinusSrcAlpha
		// Always drawn reflective pass

		//Grab Pass, get texture behind the water
		GrabPass{
		Name "BASE"
		Tags{ "LightMode" = "Always" }
	}
		//Refraction Pass

		//Normal/Reflection Pass
		Pass{
		Name "BASE"
		Tags{ "LightMode" = "Always" }
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_fog

#include "UnityCG.cginc"

		struct v2f {
		float4 pos		 : SV_POSITION;
		float2	uv		 : TEXCOORD0;
		float2	uv2		 : TEXCOORD1;
		float3	I		 : TEXCOORD2;
		float3	TtoW0 	 : TEXCOORD3;
		float3	TtoW1	 : TEXCOORD4;
		float3	TtoW2	 : TEXCOORD5;
		float2  uv3	     : TEXCOORD6;
		float4 screenPos : TEXCOORD7;
		float4 uvgrab	 : TEXCOORD8;
		float2 uv4		 : TEXCOORD9;
		float2 uv5       : TEXCOORD10;
		UNITY_FOG_COORDS(10)
	};


	uniform float4 _BumpMap_ST, _BumpMap2_ST, _Noise_ST;
	uniform float4 _AlphaMap_ST;
	uniform float2 _WaveDirection;

	v2f vert(appdata_tan v)
	{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.texcoord, _BumpMap);
		o.uv2 = TRANSFORM_TEX(v.texcoord,_BumpMap2);
		o.uv3 = TRANSFORM_TEX(v.texcoord, _Noise); //Load the noise map
		o.uv5 = TRANSFORM_TEX(v.texcoord, _AlphaMap);
		o.screenPos = ComputeScreenPos(o.pos);
		o.I = -WorldSpaceViewDir(v.vertex);
		o.uvgrab.xy = (float2(o.pos.x, o.pos.y * -1) + o.pos.w) * 0.5;
		o.uvgrab.zw = o.pos.zw;

		float3 worldNormal = UnityObjectToWorldNormal(v.normal);
		float3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);

		//Get the world binormal/bitangent of the vertex
		float3 worldBinormal = cross(worldNormal, worldTangent) * v.tangent.w;
		o.TtoW0 = float3(worldTangent.x, worldBinormal.x, worldNormal.x);
		o.TtoW1 = float3(worldTangent.y, worldBinormal.y, worldNormal.y);
		o.TtoW2 = float3(worldTangent.z, worldBinormal.z, worldNormal.z);

		UNITY_TRANSFER_FOG(o,o.pos);
		return o;
	}

	uniform sampler2D _BumpMap2;
	uniform sampler2D _MainTex;
	uniform sampler2D _Noise;
	uniform sampler2D _AlphaMap;
	uniform samplerCUBE _ReflCube;
	uniform float _Cycle;
	uniform fixed4 _ReflectColor;
	uniform fixed4 _Color;
	uniform float _WaveSpeed;
	uniform sampler2D _BumpMap;
	uniform sampler2D _CameraDepthTexture; //Depth Texture
	uniform float _EdgeBlend;
	uniform float _Transparency;
	sampler2D _GrabTexture;
	float4 _GrabTexture_TexelSize;
	float _DistortionAmount;

	fixed4 frag(v2f i) : SV_Target
	{

		float3 noise = tex2D(_Noise, i.uv3);
		float phase = _Time[1] / _Cycle + noise.r * 0.5f;
		float f = frac(phase);
		float4 alpha = tex2D(_AlphaMap, i.uv5);
		// Sample and expand the normal map texture	
		fixed3 normal = UnpackNormal(tex2D(_BumpMap, i.uv2 + normalize(_WaveDirection) * _WaveSpeed * frac(phase + 0.5)));
			fixed3 normal2 = UnpackNormal(tex2D(_BumpMap2, i.uv + normalize(_WaveDirection) * _WaveSpeed * f));
		half2 bump1 = UnpackNormal(tex2D(_BumpMap, i.uv2 + normalize(_WaveDirection) * _WaveSpeed * frac(phase + 0.5))).rg;
		half2 bump2 = UnpackNormal(tex2D(_BumpMap2, i.uv + normalize(_WaveDirection) * _WaveSpeed * f)).rg;
		if (f > 0.5f)
			f = 2.0f * (1.0f - f);
		else
			f = 2.0f * f;
		// transform normal to the world space
		half3 wn;
		wn.x = dot(i.TtoW0, lerp(normal,normal2,f));
		wn.y = dot(i.TtoW1, lerp(normal,normal2,f));
		wn.z = dot(i.TtoW2, lerp(normal,normal2,f));

		// calculate reflection vector in the world space
		half3 r = reflect(i.I, wn);
		fixed4 c = UNITY_LIGHTMODEL_AMBIENT;
		fixed4 reflcolor = texCUBE(_ReflCube, r) * _ReflectColor;
		//Edge Blending//
		half depth = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos));
		depth = LinearEyeDepth(depth);
		float edgeBlendFactors = saturate(_EdgeBlend* (depth - i.screenPos.w));
		//Refraction
		float2 offset = lerp(normal,normal2,f) * _DistortionAmount * _GrabTexture_TexelSize.xy;



		c.rgb = reflcolor;
		c.rgb *= _Color;
		c.a = alpha * edgeBlendFactors;
		c.a *= _Transparency;
		UNITY_APPLY_FOG(i.fogCoord, c);
		return c;
	}
		ENDCG
	}
	}

	}

}