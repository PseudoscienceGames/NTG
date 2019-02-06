// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Toon/Basic Outline" {
	Properties {
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (.0002, 0.3)) = .005
	}
	
	CGINCLUDE
	#include "UnityCG.cginc"
	
	struct appdata {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

	struct v2f {
		float4 pos : POSITION;
		fixed4 color : COLOR;
	};
	
	uniform float _Outline;
	uniform float4 _OutlineColor;
	
	v2f vert(appdata v) {
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);

		float3 norm   = normalize(mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal));
		float2 offset = TransformViewToProjection(norm.xy);
		o.pos.xy += offset * o.pos.z * _Outline;

		o.color = _OutlineColor;
		return o;
	}
	ENDCG

	SubShader {
				Tags { "Queue"="Transparent"}
		Pass
		{
			Name "Base"
			Cull Back
			Blend Zero One

			SetTexture[_OutlineColor]
			{
				ConstantColor(0,0,0,0)
				Combine constant
			}
		}

		Pass {
			Name "OUTLINE"
					Tags { "LightMode" = "Always" }
			Cull Front
			ZWrite On
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			fixed4 frag(v2f i) : SV_Target
			{
				UNITY_APPLY_FOG(i.fogCoord, i.color);
				return i.color;
			}
			ENDCG
		}
	}
	
	Fallback "Toon/Basic"
}