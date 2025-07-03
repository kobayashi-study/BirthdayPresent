Shader "Custom/MosaicEffect_Stable"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlockSize ("Block Size (pixels)", Range(2,100)) = 20
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _BlockSize;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 blockUV = floor(i.uv / (_BlockSize * _MainTex_TexelSize.xy)) * (_BlockSize * _MainTex_TexelSize.xy);
                return tex2D(_MainTex, blockUV);
            }
            ENDCG
        }
    }
}