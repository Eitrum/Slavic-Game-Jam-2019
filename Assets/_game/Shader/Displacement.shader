// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Game/Displacement"
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
	_Refraction("Refraction", float) = 1
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		GrabPass{
			"_BackgroundTexture"
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f
			{
				float4 grabPos : TEXCOORD0;
				float4 pos : SV_POSITION;
				half3 worldNormal : TEXCOORD1;
				float3 worldViewDir : TEXCOORD2;
			};

			v2f vert(appdata_base v, float3 normal : NORMAL) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.grabPos = ComputeGrabScreenPos(o.pos);
				o.worldNormal = UnityObjectToWorldNormal(normal);
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
				return o;
			}

			sampler2D _MainTex;
			sampler2D _BackgroundTexture;
			float _Refraction;

			half4 frag(v2f i) : SV_Target
			{
				float fresnel = (1.1 - dot(i.worldNormal,i.worldViewDir));
				i.grabPos.xyz += i.worldViewDir / fresnel * _Refraction;
				half4 bgcolor = tex2Dproj(_BackgroundTexture, i.grabPos);
				return bgcolor;
			}
			ENDCG
		}
	}
}
