Shader "Custom/Highlight"
{
	Properties
	{
		_RegularColor("Main Color", Color) = (1, 1, 1, .5) //Color when not intersecting
		_HighlightColor("Highlight Color", Color) = (1, 1, 1, .5) //Color when intersecting
		_HighlightThresholdMax("Highlight Threshold Max", Float) = 1 //Max difference for intersections
	}
		SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }

		GrabPass
	{
		"_BackgroundTexture"
	}

		Pass
	{
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		Cull Off

		CGPROGRAM
#pragma target 3.0
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		uniform sampler2D _CameraDepthTexture; //Depth Texture
	uniform float4 _RegularColor;
	uniform float4 _HighlightColor;
	uniform float _HighlightThresholdMax;

	struct v2f
	{
		float4 pos : SV_POSITION;
		float4 projPos : TEXCOORD1; //Screen position of pos
		float4 grabPos : TEXCOORD2;
	};

	v2f vert(appdata_base v)
	{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.grabPos = ComputeGrabScreenPos(o.pos);
		o.projPos = ComputeScreenPos(o.pos);

		return o;
	}

	sampler2D _BackgroundTexture;

	half4 frag(v2f i) : COLOR
	{
		float4 finalColor = _RegularColor;
		half4 bgcolor = tex2Dproj(_BackgroundTexture, i.grabPos);
		//Get the distance to the camera from the depth buffer for this point
		float sceneZ = LinearEyeDepth(tex2Dproj(_CameraDepthTexture,
			UNITY_PROJ_COORD(i.projPos)).r);

		//Actual distance to the camera
		float partZ = i.projPos.z;

		//If the two are similar, then there is an object intersecting with our object
		float diff = (abs(sceneZ - partZ)) /
			_HighlightThresholdMax;

		
		//finalColor = bgcolor;
		if (diff <= 1)
		{
			finalColor = _HighlightColor;
		}

		half4 c;
		c.r = finalColor.r;
		c.g = finalColor.g;
		c.b = finalColor.b;
		c.a = finalColor.a;

		return c;
	}

		ENDCG
	}
	}
		FallBack "VertexLit"
}