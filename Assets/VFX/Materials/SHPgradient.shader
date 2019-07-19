// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "New Amplify Shader"
{
	Properties
	{
		_GradientLerp("GradientLerp", Range( 0 , 1)) = 0
		_Range("Range", Range( 0.0001 , 1)) = 1
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

		uniform float _GradientLerp;
		uniform float _Range;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color6 = IsGammaSpace() ? float4(0,0.7544947,1,0) : float4(0,0.5295509,1,0);
			float4 color7 = IsGammaSpace() ? float4(0.5582844,1,0,0) : float4(0.2720086,1,0,0);
			float lerpResult2 = lerp( i.uv_texcoord.x , i.uv_texcoord.y , _GradientLerp);
			float4 lerpResult8 = lerp( color6 , color7 , ( ( lerpResult2 - _Range ) / ( 0.0 - _Range ) ));
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
122.4;55.2;1408;782;786.6118;752.7051;2.1489;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-425.3304,-46.7806;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;9;-342.8711,299.7917;Float;True;Property;_GradientLerp;GradientLerp;0;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;2;11.86952,91.51939;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;269.9289,310.9917;Float;False;Constant;_Float1;Float 1;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;218.7289,-50.60832;Float;False;Property;_Range;Range;1;0;Create;True;0;0;False;0;1;0;0.0001;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;11;540.3291,315.7918;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;10;559.529,107.7917;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;15;788.329,205.3917;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;6;386.2273,-593.5456;Float;False;Constant;_Color0;Color 0;1;0;Create;True;0;0;False;0;0,0.7544947,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;7;389.6935,-375.9928;Float;False;Constant;_Color2;Color2;1;0;Create;True;0;0;False;0;0.5582844,1,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;8;1062.988,-294.0014;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1428.661,-295.8978;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;New Amplify Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;1;1
WireConnection;2;1;1;2
WireConnection;2;2;9;0
WireConnection;11;0;12;0
WireConnection;11;1;14;0
WireConnection;10;0;2;0
WireConnection;10;1;14;0
WireConnection;15;0;10;0
WireConnection;15;1;11;0
WireConnection;8;0;6;0
WireConnection;8;1;7;0
WireConnection;8;2;15;0
WireConnection;0;0;8;0
ASEEND*/
//CHKSM=20B3ADC0257C49658CF2D85224F8024F1EE90336