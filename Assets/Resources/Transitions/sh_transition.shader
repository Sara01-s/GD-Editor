Shader "Custom/sh_transition" {

    Properties {
        _MainTex ("Texture", 2D) = "white" {}
		_Progess ("Progress", Float) = 0.0

		[Space(20)]
		_ShapeSize ("Shape Size", Range(0.0, 1.0)) = 0.0
    }

    SubShader {
        Cull Off 
        ZWrite Off 
        ZTest Always

        Pass {
            CGPROGRAM
            #pragma vertex VertShader
            #pragma fragment FragShader

            #include "UnityCG.cginc"

            struct VertexIn {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct VertexOut {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            VertexOut VertShader (VertexIn vertIn) {
                VertexOut vertOut;

                vertOut.pos = UnityObjectToClipPos(vertIn.pos);
                vertOut.uv = vertIn.uv;

                return vertOut;
            }

            sampler2D _MainTex;
			float _Progess;
			float _ShapeSize;

            fixed4 FragShader (VertexOut i) : SV_Target {

				float xFrac = frac(i.uv.x / _ShapeSize);
				float yFrac = frac(i.uv.y / _ShapeSize);

				float xDst = abs(xFrac - 0.5);
				float yDst = abs(yFrac - 0.5);

				if ((xDst + yDst) + (i.uv.x + -i.uv.y) < _Progess * 2.0) {
					discard;
				}

                fixed4 col = tex2D(_MainTex, i.uv);
				
                return col;
            }
            ENDCG
        }
    }
}
