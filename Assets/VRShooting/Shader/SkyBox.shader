Shader "CustomSkybox/Vertical"
{
    Properties
    {
        _UpColor("UpColor",Color)=(1,1,1,1)
        _DownColor("DownColor",Color)=(0,0,0,1)
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Background"
            "Queue"="Background"
            "PreviewType"="SkyBox"
        }

        Pass
        {
            ZWrite Off
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed3 _UpColor;
            fixed3 _DownColor;
            
            struct appdata
            {
                float4 vertex : POSITION;
                float3 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID //追加
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 texcoord : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO//追加
            };

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v); //追加
                UNITY_INITIALIZE_OUTPUT(v2f, o); //追加
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); //追加
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(lerp( _DownColor,_UpColor, i.texcoord.y * 1 + 1), 1.0);
            }
            ENDCG
        }
    }
}
