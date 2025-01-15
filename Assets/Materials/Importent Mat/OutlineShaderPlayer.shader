
Shader "Custom/OutlineShader"
{
    Properties
    {
        _OutlineColor("Outline Color", Color) = (1,0,0,1)
        _OutlineWidth("Outline Width", Range(0.01, 0.1)) = 0.05
    }
        SubShader
    {
        Tags { "Queue" = "Overlay" "RenderType" = "Transparent" }

        // First Pass: Draw the object but mark it in the depth buffer
        Pass
        {
            Name "WriteDepth"
            ZWrite On
            ColorMask 0
        }

        // Second Pass: Draw the outline only when object is obscured
        Pass
        {
            Name "OutlinePass"
            Cull Front
            ZTest Greater
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
            };

            float _OutlineWidth;
            fixed4 _OutlineColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex + v.normal * _OutlineWidth);
                o.worldNormal = normalize(v.normal);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }

        FallBack "Diffuse"
}
