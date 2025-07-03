Shader "Custom/SpriteBlurFixed"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _BlurAmount("Blur Amount", Range(1, 20)) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
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
            float4 _MainTex_ST;
            float _BlurAmount;

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float blurStep = 1.0 / _BlurAmount;

                fixed4 color = fixed4(0,0,0,0);
                color += tex2D(_MainTex, uv + float2(-blurStep, -blurStep));
                color += tex2D(_MainTex, uv + float2(-blurStep, 0));
                color += tex2D(_MainTex, uv + float2(-blurStep, blurStep));
                color += tex2D(_MainTex, uv + float2(0, -blurStep));
                color += tex2D(_MainTex, uv);
                color += tex2D(_MainTex, uv + float2(0, blurStep));
                color += tex2D(_MainTex, uv + float2(blurStep, -blurStep));
                color += tex2D(_MainTex, uv + float2(blurStep, 0));
                color += tex2D(_MainTex, uv + float2(blurStep, blurStep));

                return color / 9.0;
            }
            ENDCG
        }
    }
}