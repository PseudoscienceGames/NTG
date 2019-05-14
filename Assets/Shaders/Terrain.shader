Shader "Custom/Terrain"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2DArray) = "white" {}
        _MainTex2 ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM

        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.5

        UNITY_DECLARE_TEX2DARRAY(_MainTex);
		sampler2D _MainTex2;

        struct Input
        {
            float2 uv_MainTex   : TEXCOORD0;
			float2 uv2_MainTex2 : TEXCOORD1;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float3 uv = float3(IN.uv_MainTex.xy, IN.uv2_MainTex2.x);
            fixed4 c = UNITY_SAMPLE_TEX2DARRAY(_MainTex, uv);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
