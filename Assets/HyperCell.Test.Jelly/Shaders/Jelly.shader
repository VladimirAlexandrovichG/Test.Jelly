Shader "Custom/Jelly" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Color ("Main Color", Color) = (1,1,1,1)
    }
    SubShader {
        Pass {
            Tags { "RenderType"="Opaque" }
       
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag 
            #include "UnityCG.cginc"
 
            sampler2D _MainTex;
            fixed4 _Color;
            
            struct v2f {
                float4 pos : SV_POSITION;
                half2 uv : TEXCOORD0;
            };
 
            v2f vert(appdata_base v) {
                v2f o;
                v.vertex.x += sign(v.vertex.x) * sin(_Time.w)/50;
                v.vertex.y += sign(v.vertex.y) * cos(_Time.w)/50;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }
 
 
            fixed4 frag(v2f i) : COLOR {
                fixed4 col = _Color;
                return col;
            }
 
            ENDCG
        }
    }
    FallBack "Diffuse"
}