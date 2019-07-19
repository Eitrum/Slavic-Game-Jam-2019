// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SHPgradient"
{
	Properties
	{
		_GradientLerp("GradientLerp", Range( 0 , 1)) = 0
		_Range("Range", Range( 0.0001 , 1)) = 1
		_Color2("Color2", Color) = (0.5582844,1,0,0)
		_Color0("Color 0", Color) = (0,0.7544947,1,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform float4 _Color0;
		uniform float4 _Color2;
		uniform float _GradientLerp;
		uniform float _Range;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float lerpResult2 = lerp( i.uv_texcoord.x , i.uv_texcoord.y , _GradientLerp);
			float temp_output_15_0 = ( ( lerpResult2 - _Range ) / ( 0.0 - _Range ) );
			float clampResult23 = clamp( temp_output_15_0 , 0.0 , 1.0 );
			float4 lerpResult8 = lerp( _Color0 , _Color2 , clampResult23);
			float3 ase_worldPos = i.worldPos;
			float lerpResult31 = lerp( 1.0 , frac( ( ( ase_worldPos.x + ase_worldPos.y + ase_worldPos.z ) * 1000.0 ) ) , 0.0);
			o.Albedo = ( lerpResult8 * lerpResult31 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16800
58.4;55.2;1408;782;-772.4036;1029.005;1.680402;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-425.3304,-46.7806;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;9;-342.8711,299.7917;Float;True;Property;_GradientLerp;GradientLerp;0;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;218.7289,-50.60832;Float;False;Property;_Range;Range;1;0;Create;True;0;0;False;0;1;0.49;0.0001;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;24;1151.377,-764.6918;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;2;11.86952,91.51939;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;269.9289,310.9917;Float;False;Constant;_Float1;Float 1;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;11;540.3291,315.7918;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;10;559.529,107.7917;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;1421.449,-648.9785;Float;False;Constant;_Float0;Float 0;5;0;Create;True;0;0;False;0;1000;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;27;1414.449,-807.9785;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;15;744.9551,182.2518;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;1611.561,-775.4936;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;1783.773,-943.8947;Float;False;Constant;_Float2;Float 2;5;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;29;1782.482,-763.4499;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;1700.926,-645.9973;Float;False;Constant;_RandValue;RandValue;5;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;23;1261.912,-53.2873;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;6;386.2273,-593.5456;Float;False;Property;_Color0;Color 0;3;0;Create;True;0;0;False;0;0,0.7544947,1,0;0,0.7544947,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;7;389.6935,-375.9928;Float;False;Property;_Color2;Color2;2;0;Create;True;0;0;False;0;0.5582844,1,0,0;0.5582844,1,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;31;2102.487,-886.1259;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;8;1492.532,-347.7486;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;2361.345,-585.0078;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;18;1131.178,261.6295;Float;True;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;707.5844,534.3574;Float;False;Property;_Power;Power;4;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2842.435,-546.0026;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;SHPgradient;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;1;1
WireConnection;2;1;1;2
WireConnection;2;2;9;0
WireConnection;11;0;12;0
WireConnection;11;1;14;0
WireConnection;10;0;2;0
WireConnection;10;1;14;0
WireConnection;27;0;24;1
WireConnection;27;1;24;2
WireConnection;27;2;24;3
WireConnection;15;0;10;0
WireConnection;15;1;11;0
WireConnection;25;0;27;0
WireConnection;25;1;28;0
WireConnection;29;0;25;0
WireConnection;23;0;15;0
WireConnection;31;0;32;0
WireConnection;31;1;29;0
WireConnection;31;2;33;0
WireConnection;8;0;6;0
WireConnection;8;1;7;0
WireConnection;8;2;23;0
WireConnection;30;0;8;0
WireConnection;30;1;31;0
WireConnection;18;0;15;0
WireConnection;18;1;22;0
WireConnection;0;0;30;0
ASEEND*/
//CHKSM=96BE796E02D7FFBA360B957EB22CCCE9B104649B