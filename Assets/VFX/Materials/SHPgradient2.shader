// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SHPerosion"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Main("Main", 2D) = "white" {}
		_ErosionDriver("ErosionDriver", Range( 0 , 1)) = 0.3562072
		_GradientLerp("GradientLerp", Range( 0 , 1)) = 0
		_Color3("Color 3", Color) = (1,0,0,0)
		_Color2Glow("Color2Glow", Float) = 0
		_Range("Range", Range( 0.0001 , 1)) = 1
		_Color2("Color2", Color) = (0.5582844,1,0,0)
		_Color0("Color 0", Color) = (0,0.7544947,1,0)
		_Color2Contrast("Color2Contrast", Range( 0 , 1)) = 0.9479553
		_Power("Power", Range( 0 , 1)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "AlphaTest+0" "IgnoreProjector" = "True" }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
		};

		uniform float4 _Color3;
		uniform float _Color2Glow;
		uniform float4 _Color0;
		uniform float4 _Color2;
		uniform float _GradientLerp;
		uniform float _Range;
		uniform float _Power;
		uniform sampler2D _Main;
		uniform float4 _Main_ST;
		uniform float _ErosionDriver;
		uniform float _Color2Contrast;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 appendResult27 = (float4(i.vertexColor.r , i.vertexColor.g , i.vertexColor.b , 0.0));
			float lerpResult2 = lerp( i.uv_texcoord.x , i.uv_texcoord.y , _GradientLerp);
			float clampResult23 = clamp( ( ( lerpResult2 - _Range ) / ( 0.0 - _Range ) ) , 0.0 , 1.0 );
			float4 lerpResult8 = lerp( _Color0 , _Color2 , pow( clampResult23 , _Power ));
			float2 uv_Main = i.uv_texcoord * _Main_ST.xy + _Main_ST.zw;
			float4 tex2DNode28 = tex2D( _Main, uv_Main );
			float temp_output_86_0 = ( 1.0 - _ErosionDriver );
			float clampResult104 = clamp( ( tex2DNode28.r - temp_output_86_0 ) , 0.0 , 1.0 );
			float temp_output_64_0 = pow( clampResult104 , 0.01 );
			float clampResult107 = clamp( ( ( tex2DNode28.r - temp_output_86_0 ) / ( 1.0 - temp_output_86_0 ) ) , 0.0 , 1.0 );
			float temp_output_83_0 = ( 1.0 - temp_output_86_0 );
			float clampResult84 = clamp( pow( ( ( ( temp_output_64_0 - clampResult107 ) - temp_output_83_0 ) / ( 1.0 - temp_output_83_0 ) ) , ( 1.0 - _Color2Contrast ) ) , 0.0 , 1.0 );
			float4 lerpResult70 = lerp( ( _Color3 * ( 1.0 + _Color2Glow ) ) , ( appendResult27 * lerpResult8 ) , clampResult84);
			o.Albedo = lerpResult70.xyz;
			o.Alpha = temp_output_64_0;
			clip( temp_output_64_0 - _Cutoff );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Lambert keepalpha fullforwardshadows exclude_path:deferred 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				half4 color : COLOR0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.color = v.color;
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.vertexColor = IN.color;
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16800
6.4;0.8;1524;836;-1124.505;-1446.724;1.3;True;False
Node;AmplifyShaderEditor.CommentaryNode;92;91.82701,1142.203;Float;False;3960.149;1249.513;Ramp and edge color;30;70;84;32;28;79;64;34;81;65;36;76;74;77;67;82;83;73;35;33;66;86;69;88;89;90;87;104;105;107;108;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;66;368.8694,1198.555;Float;False;Property;_ErosionDriver;ErosionDriver;2;0;Create;True;0;0;False;0;0.3562072;0.413;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;91;-659.3228,-777.8103;Float;False;2236.062;1292.503;Gradient;14;1;9;2;12;14;11;10;15;23;22;6;7;18;8;;1,1,1,1;0;0
Node;AmplifyShaderEditor.OneMinusNode;86;691.1588,1192.204;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;28;331.5793,1351.348;Float;True;Property;_Main;Main;1;0;Create;True;0;0;False;0;c612b8458a1cc7044bdbeb6de31733b6;c612b8458a1cc7044bdbeb6de31733b6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;34;513.608,1911.802;Float;False;Constant;_Float3;Float 3;7;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-609.3228,-181.0453;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;9;-526.8635,165.527;Float;True;Property;_GradientLerp;GradientLerp;3;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;65;924.4023,1325.512;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;36;794.6226,1780.895;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;33;775.4229,1541.769;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;67;1210.4,1457.373;Float;False;Constant;_RampPower;RampPower;6;0;Create;True;0;0;False;0;0.01;0.01;0.01;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;35;1399.707,1763.474;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;104;1199.494,1314.505;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;2;-172.1229,-42.74532;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;34.73653,-184.873;Float;False;Property;_Range;Range;6;0;Create;True;0;0;False;0;1;1;0.0001;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;85.93651,176.727;Float;False;Constant;_Float1;Float 1;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;107;1677.59,1775.203;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;64;1522.527,1380.396;Float;True;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;11;356.3367,181.5271;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;10;375.5367,-26.47301;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;105;1945.813,1617.491;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;73;1962.203,2223.612;Float;False;Constant;_Float5;Float 5;7;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;15;560.9628,47.9871;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;83;1990.058,2045.316;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;81;2375.284,2272.864;Float;False;Property;_Color2Contrast;Color2Contrast;9;0;Create;True;0;0;False;0;0.9479553;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;523.5921,400.0927;Float;False;Property;_Power;Power;10;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;76;2218.498,2132.605;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;23;913.8515,-29.27501;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;74;2289.505,1878.87;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;88;2304.499,1719.344;Float;False;Property;_Color2Glow;Color2Glow;5;0;Create;True;0;0;False;0;0;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;18;1225.135,-63.72574;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;90;2295.98,1583.233;Float;False;Constant;_Float0;Float 0;10;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;6;202.2349,-727.8103;Float;False;Property;_Color0;Color 0;8;0;Create;True;0;0;False;0;0,0.7544947,1,0;0,0.7544947,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;77;2422.044,1984.592;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;24;1859.171,-764.702;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;82;2730.857,2282.116;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;7;205.7012,-510.2575;Float;False;Property;_Color2;Color2;7;0;Create;True;0;0;False;0;0.5582844,1,0,0;0.5582844,1,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;69;2232.211,1367.944;Float;False;Property;_Color3;Color 3;4;0;Create;True;0;0;False;0;1,0,0,0;1,0.3833334,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;27;2161.523,-753.3588;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.LerpOp;8;1308.539,-482.0133;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;79;2890.179,1984.963;Float;True;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;89;2593.102,1660.489;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;87;2779.373,1545.336;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;84;3238.563,1797.231;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;2695.674,-393.0813;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ComputeScreenPosHlpNode;95;-499.5974,1296.064;Float;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;103;3628.193,-366.6093;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.UnityObjToClipPosHlpNode;98;-1202.795,1560.446;Float;False;1;0;FLOAT3;0,0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;101;-550.0265,1587.927;Float;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ComponentMaskNode;100;-853.5618,1698.195;Float;False;False;False;False;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComputeScreenPosHlpNode;99;-1006.515,1560.656;Float;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;32;141.8269,1730.277;Float;False;Constant;_Float2;Float 2;7;0;Create;True;0;0;False;0;0.6535491;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;70;3565.183,1464.522;Float;True;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.PosVertexDataNode;97;-1489.155,1528.628;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;93;-581.4514,1379.376;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;108;1917.712,1824.436;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;4650.488,-451.8689;Float;False;True;2;Float;ASEMaterialInspector;0;0;Lambert;vv;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;AlphaTest;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;86;0;66;0
WireConnection;65;0;28;1
WireConnection;65;1;86;0
WireConnection;36;0;34;0
WireConnection;36;1;86;0
WireConnection;33;0;28;1
WireConnection;33;1;86;0
WireConnection;35;0;33;0
WireConnection;35;1;36;0
WireConnection;104;0;65;0
WireConnection;2;0;1;1
WireConnection;2;1;1;2
WireConnection;2;2;9;0
WireConnection;107;0;35;0
WireConnection;64;0;104;0
WireConnection;64;1;67;0
WireConnection;11;0;12;0
WireConnection;11;1;14;0
WireConnection;10;0;2;0
WireConnection;10;1;14;0
WireConnection;105;0;64;0
WireConnection;105;1;107;0
WireConnection;15;0;10;0
WireConnection;15;1;11;0
WireConnection;83;0;86;0
WireConnection;76;0;73;0
WireConnection;76;1;83;0
WireConnection;23;0;15;0
WireConnection;74;0;105;0
WireConnection;74;1;83;0
WireConnection;18;0;23;0
WireConnection;18;1;22;0
WireConnection;77;0;74;0
WireConnection;77;1;76;0
WireConnection;82;0;81;0
WireConnection;27;0;24;1
WireConnection;27;1;24;2
WireConnection;27;2;24;3
WireConnection;8;0;6;0
WireConnection;8;1;7;0
WireConnection;8;2;18;0
WireConnection;79;0;77;0
WireConnection;79;1;82;0
WireConnection;89;0;90;0
WireConnection;89;1;88;0
WireConnection;87;0;69;0
WireConnection;87;1;89;0
WireConnection;84;0;79;0
WireConnection;25;0;27;0
WireConnection;25;1;8;0
WireConnection;103;0;24;4
WireConnection;103;1;64;0
WireConnection;98;0;97;0
WireConnection;101;0;99;0
WireConnection;101;1;100;0
WireConnection;100;0;99;0
WireConnection;99;0;98;0
WireConnection;70;0;87;0
WireConnection;70;1;25;0
WireConnection;70;2;84;0
WireConnection;108;0;64;0
WireConnection;108;1;107;0
WireConnection;0;0;70;0
WireConnection;0;9;64;0
WireConnection;0;10;64;0
ASEEND*/
//CHKSM=F91AB5D8EF0A9880726F5D002D45A3C7CB799436