Shader "CustomSkybox/Vertical"
{
    Properties
    {
        _MainTex("Texture",2D) = "white"{}
        _Color("Color",Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent" 
            "Queue" = "Transparent"
            "RenderPipeline"="UniversalPipeline"
            
        }
        Blend SrcAlpha OneMinusSrcAlpha 
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float3 _Color;
            
            struct appdata
            {
                float4 vertex : POSITION;
                float3 texcoord : TEXCOORD0;
                float3 normal : NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID //追加
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 texcoord : TEXCOORD0;
                float3 normal : NORMAL;
                float3 viewDir : TEXCOORD2;
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
                o.normal = mul(unity_ObjectToWorld,v.normal);
                o.normal = normalize(o.normal);
                o.viewDir = normalize(float3(0,1,-6)-mul(unity_ObjectToWorld, v.vertex));
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float dist = dot(i.normal, i.viewDir);
                float alpha = pow(dist, 4);
                
                half4 col = tex2D(_MainTex, i.texcoord);
                col.rgb = _Color;
                col.a = alpha;
                return col;
            }
            ENDCG
        }
    }
}
