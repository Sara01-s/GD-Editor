Shader "Custom/Unlit/sh_sprite" {

    Properties {
        _MainTex ("Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1, 1, 1, 1)
    }

    SubShader {
        
        Tags {
            "RenderType"="Opaque"
        }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex VertShader
            #pragma fragment FragShader
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct VertexIn {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct VertexOut {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			fixed4 _Color;

            VertexOut VertShader(VertexIn vertIn) {
                VertexOut output;
                
                output.pos = UnityObjectToClipPos(vertIn.pos);
                output.uv = TRANSFORM_TEX(vertIn.uv, _MainTex);

                return output;
            }

            fixed4 FragShader(VertexOut fragIn) : SV_Target {

                fixed4 fragColor = tex2D(_MainTex, fragIn.uv);

                return fragColor * _Color;
            }
            ENDCG
        }
    }
}
