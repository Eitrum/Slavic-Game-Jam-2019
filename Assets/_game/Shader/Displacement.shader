// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Game/Displacement"
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Refraction("Refraction", float) = 1
		_Displacement("Displacement", float) = 0.1
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

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};


				struct v2f
				{
					float4 grabPos : TEXCOORD0;
					float4 pos : SV_POSITION;
					half3 worldNormal : TEXCOORD1;
					float3 worldViewDir : TEXCOORD2;
					float2 uv : TEXCOORD4;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				float _Displacement;

				v2f vert(appdata v, float3 normal : NORMAL) {
					v2f o;

					o.uv = TRANSFORM_TEX(v.uv, _MainTex);

					o.uv.x += cos(_Time * 9);
					o.uv.y += cos(_Time * 12);
					half offset = tex2Dlod(_MainTex, float4(o.uv, 0, 0)).r * _Displacement;


					o.pos = UnityObjectToClipPos(v.vertex + (v.vertex * offset));
					o.grabPos = ComputeGrabScreenPos(o.pos);
					o.worldNormal = UnityObjectToWorldNormal(normal);
					float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
					o.worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));

					return o;
				}

				sampler2D _BackgroundTexture;
				float _Refraction;

				half4 frag(v2f i) : SV_Target
				{
					half4 offset = tex2Dlod(_MainTex, float4(i.uv , 0, 0)) * _Displacement;

					float fresnel = (1.1 - dot(i.worldNormal,i.worldViewDir));
					i.grabPos.xyz +=( i.worldViewDir + offset.xyz) / fresnel * _Refraction ;
					half4 bgcolor = tex2Dproj(_BackgroundTexture, i.grabPos);
					return bgcolor;
				}
				ENDCG
			}
		}
}
