Shader "Hidden/PixelateShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Width("Width", float) = 192
		_Height("Height", float) = 108
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

			float _Width;
			float _Height;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            { 
				float2 pixelScaling = float2(_Width, _Height);
				i.uv = round(i.uv * pixelScaling) / pixelScaling;
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
