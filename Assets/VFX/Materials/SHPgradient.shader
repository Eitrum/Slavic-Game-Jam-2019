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
		_Power("Power", Range( 0 , 1)) = 1
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
		};

		uniform float4 _Color0;
		uniform float4 _Color2;
		uniform float _GradientLerp;
		uniform float _Range;
		uniform float _Power;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float lerpResult2 = lerp( i.uv_texcoord.x , i.uv_texcoord.y , _GradientLerp);
			float clampResult23 = clamp( ( ( lerpResult2 - _Range ) / ( 0.0 - _Range ) ) , 0.0 , 1.0 );
			float4 lerpResult8 = lerp( _Color0 , _Color2 , pow( clampResult23 , _Power ));
			o.Albedo = lerpResult8.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16800
58.4;55.2;1408;782;72.17249;629.9455;1.930208;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-425.3304,-46.7806;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;9;-342.8711,299.7917;Float;True;Property;_GradientLerp;GradientLerp;0;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;2;11.86952,91.51939;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;269.9289,310.9917;Float;False;Constant;_Float1;Float 1;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;218.7289,-50.60832;Float;False;Property;_Range;Range;1;0;Create;True;0;0;False;0;1;0.49;0.0001;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;11;540.3291,315.7918;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;10;559.529,107.7917;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;15;744.9551,182.2518;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;707.5844,534.3574;Float;False;Property;_Power;Power;4;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;23;1097.844,104.9897;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;6;386.2273,-593.5456;Float;False;Property;_Color0;Color 0;3;0;Create;True;0;0;False;0;0,0.7544947,1,0;0,0.7544947,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;7;389.6935,-375.9928;Float;False;Property;_Color2;Color2;2;0;Create;True;0;0;False;0;0.5582844,1,0,0;0.5582844,1,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;18;1409.128,70.53896;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;8;1492.532,-347.7486;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2842.435,-546.0026;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;SHPgradient;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;1;1
WireConnection;2;1;1;2
WireConnection;2;2;9;0
WireConnection;11;0;12;0
WireConnection;11;1;14;0
WireConnection;10;0;2;0
WireConnection;10;1;14;0
WireConnection;15;0;10;0
WireConnection;15;1;11;0
WireConnection;23;0;15;0
WireConnection;18;0;23;0
WireConnection;18;1;22;0
WireConnection;8;0;6;0
WireConnection;8;1;7;0
WireConnection;8;2;18;0
WireConnection;0;0;8;0
ASEEND*/
//CHKSM=E6B06A674D0516C036D49DAC396F4560B35D3353