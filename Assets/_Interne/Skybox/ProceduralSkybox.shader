// Made with Amplify Shader Editor v1.9.0.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "DM/ProceduralSkybox"
{
	Properties
	{
		_Float3("Float 3", Float) = 5.7
		_Stars("Stars", 2D) = "white" {}
		_Starmask("Star mask", 2D) = "white" {}
		_PannerMaskSpeed("Panner Mask Speed", Vector) = (1,1,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" }
	LOD 0

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		AlphaToMask Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#define ASE_NEEDS_FRAG_WORLD_POSITION


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform sampler2D _Stars;
			uniform float4 _Stars_ST;
			uniform sampler2D _Starmask;
			uniform float2 _PannerMaskSpeed;
			uniform float _Float3;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.zw = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				float2 uv_Stars = i.ase_texcoord1.xy * _Stars_ST.xy + _Stars_ST.zw;
				float2 texCoord379 = i.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float cos389 = cos( ( _Time.y * 0.2 ) );
				float sin389 = sin( ( _Time.y * 0.2 ) );
				float2 rotator389 = mul( texCoord379 - float2( 0,0 ) , float2x2( cos389 , -sin389 , sin389 , cos389 )) + float2( 0,0 );
				float2 panner372 = ( _Time.x * _PannerMaskSpeed + rotator389);
				float4 color387 = IsGammaSpace() ? float4(0.03773582,0.03773582,0.03773582,1) : float4(0.002920729,0.002920729,0.002920729,1);
				float3 normalizeResult382 = normalize( WorldPosition );
				float dotResult381 = dot( normalizeResult382 , float3( 0,1,0 ) );
				float4 NewSky360 = ( ( tex2D( _Stars, uv_Stars ) * tex2D( _Starmask, panner372 ) ) + ( color387 * pow( ( 1.0 - abs( dotResult381 ) ) , _Float3 ) ) );
				
				
				finalColor = NewSky360;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	Fallback Off
}
/*ASEBEGIN
Version=19002
-5;2;973;1138;3285.295;1498.536;1.271126;False;False
Node;AmplifyShaderEditor.CommentaryNode;357;-3663.401,-1096.93;Inherit;False;2897.856;1327.987;Comment;23;358;360;371;372;374;380;381;383;384;385;387;388;386;389;367;382;377;379;376;390;391;392;393;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TimeNode;376;-3616.505,-531.5471;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WireNode;393;-3434.216,-557.0297;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;367;-3602.864,-373.5932;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WireNode;392;-3647.417,-575.2303;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;391;-3641.984,-727.9723;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;382;-3367.512,-372.3666;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;390;-3458.586,-819.8735;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;379;-3514.433,-947.1198;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DotProductOpNode;381;-3146.311,-374.6667;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,1,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;377;-3609.729,-681.5217;Inherit;False;Property;_PannerMaskSpeed;Panner Mask Speed;17;0;Create;True;0;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.AbsOpNode;383;-2903.733,-366.8175;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;389;-3264.109,-867.6808;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;380;-2848.164,-276.9026;Float;False;Property;_Float3;Float 3;8;0;Create;True;0;0;0;False;0;False;5.7;12.64;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;384;-2715.401,-353.6765;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;372;-3059.026,-707.6431;Inherit;False;3;0;FLOAT2;1,1;False;2;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;358;-2751.765,-943.9366;Inherit;True;Property;_Stars;Stars;15;0;Create;True;0;0;0;False;0;False;-1;71eb1cadc5880074bb3788081492b75e;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;385;-2547.719,-349.2245;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;387;-2609.517,-517.5342;Inherit;False;Constant;_Color0;Color 0;17;0;Create;True;0;0;0;False;0;False;0.03773582,0.03773582,0.03773582,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;371;-2750.841,-744.0001;Inherit;True;Property;_Starmask;Star mask;16;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;374;-2322.693,-878.6671;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;388;-2336.686,-431.8637;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;206;-5701.829,-2957.472;Inherit;False;4121.021;1625.509;Comment;15;198;57;175;76;55;63;233;234;305;309;311;313;314;316;356;Horizon And Sun Glow;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;386;-2171.179,-598.6646;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;308;-4285.255,-4273.055;Inherit;False;1802.634;338.6813;Comment;10;213;221;215;220;222;212;216;223;244;307;Scalar that makes the horizon glow brighter when the sun is low, scales it out when the sun is down and directly above;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;306;-5740.446,-4294.426;Inherit;False;1333.227;394.979;Scales the horizon glow depending on the direction of the sun.  If it's below the horizon it scales out faster;9;241;237;243;242;238;239;240;236;304;Horizon Daynight mask;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;360;-1998.338,-607.1229;Float;False;NewSky;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;316;-3287.849,-1965.217;Inherit;False;927.1527;431.4218;Comment;7;204;291;312;315;289;290;288;Tintint the sun with the horizon color for added COOL;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;311;-5678.415,-2514.585;Inherit;False;1484.109;291.775;;8;59;51;47;46;44;40;42;41;Base Horizon Glow;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;199;-3221.351,-3770.349;Inherit;False;2030.075;565.4365;Comment;11;78;79;56;91;92;81;85;86;84;197;235;Sky Color Base;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;356;-5675.264,-2120.884;Inherit;False;2341.093;657.4434;Comment;23;62;19;90;15;17;89;16;32;14;88;13;87;201;27;29;28;26;25;24;23;22;20;21;Horizon Glow added from the sun;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;205;-5723.964,-3809.773;Inherit;False;2352.849;723.3484;Comment;29;75;203;71;18;69;200;12;34;11;10;261;263;264;265;262;268;271;281;282;252;285;284;286;287;297;298;300;302;303;Sun Disk;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;288;-2536.218,-1812.415;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;239;-5036.471,-4219.873;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;69;-3899.976,-3615.931;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;236;-5631.638,-4244.426;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.PowerNode;290;-2765.923,-1867.303;Inherit;False;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;289;-2968.495,-1874.238;Inherit;False;Lerp White To;-1;;1;047d7c189c36a62438973bad9d37b1c2;0;2;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;262;-4684.928,-3752.538;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.01;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;218;-94.76869,-1010.058;Inherit;False;175;Sky;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-5615.264,-1920.364;Float;False;Property;_HorizonSunGlowSpreadMin;Horizon Sun Glow Spread Min;9;0;Create;True;0;0;0;False;0;False;5.075109;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;241;-5631.832,-4100.419;Inherit;False;235;NightTransScale;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;81;-2528.674,-3445.299;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,1,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;32;-4334.243,-1989.433;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;302;-4137.564,-3254.098;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;285;-5686.521,-3529.376;Float;False;Property;_SunDiskSizeAdjust;Sun Disk Size Adjust;4;0;Create;True;0;0;0;False;0;False;0;0.00299;0;0.01;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;252;-5681.096,-3760.804;Float;False;Property;_SunDiskSize;Sun Disk Size;3;0;Create;True;0;0;0;False;0;False;0;0;0;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;27;-4973.315,-1866.063;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;59;-4364.677,-2419.279;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;55;-2668.945,-2807.078;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SmoothstepOpNode;261;-4298.296,-3674.241;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-3908.138,-1996.674;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;315;-3235.453,-1822.265;Inherit;False;314;TotalHorizonMask;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;90;-4176.78,-1736.042;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;71;-3732.999,-3610.588;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;-3202.706,-2661.514;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;56;-2131.259,-3720.349;Float;False;Property;_SkyColor;Sky Color;0;0;Create;True;0;0;0;False;0;False;0.3764706,0.6039216,1,0;0.1200142,0.3730588,0.8207547,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;216;-3624.789,-4133.854;Float;False;Constant;_HideHorizonGlowScale;Hide Horizon Glow Scale;12;0;Create;True;0;0;0;False;0;False;4.14;4.14;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;76;-2315.188,-2715.739;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WorldPosInputsNode;300;-4392.955,-3274.261;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SaturateNode;75;-4847.784,-3320.666;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;86;-3136.089,-3332.472;Float;False;Property;_NightTransitionScale;Night Transition Scale;12;0;Create;True;0;0;0;False;0;False;1;7.84;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;204;-3193.807,-1652.056;Inherit;False;203;SunDisk;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TFHCRemapNode;223;-2969.924,-4180.874;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0.1;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMinOpNode;22;-5310.266,-1931.364;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;243;-5690.445,-4014.447;Float;False;Property;_NightTransitionHorizonDelay;Night Transition Horizon Delay;13;0;Create;True;0;0;0;False;0;False;0;-4.77;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;237;-5344.807,-4223.833;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OneMinusNode;268;-5143.144,-3760.765;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;291;-3244.371,-1731.165;Float;False;Property;_HorizonTintSunPower;Horizon Tint Sun Power;11;0;Create;True;0;0;0;False;0;False;0;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;305;-5653.031,-2831.91;Inherit;False;304;HorizonScaleDayNight;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;213;-3953.026,-4206.957;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,1,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;314;-2974.382,-2675.777;Float;False;TotalHorizonMask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LightColorNode;18;-3932.679,-3516.456;Inherit;False;0;3;COLOR;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.RegisterLocalVarNode;235;-2824.767,-3326.541;Float;False;NightTransScale;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;307;-2758.62,-4181.514;Float;False;HorizonGlowGlobalScale;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;62;-3742.367,-2002.85;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;198;-2961.547,-2867;Inherit;False;197;SkyColor;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;215;-3609.657,-4219.542;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;197;-1434.277,-3425.927;Float;False;SkyColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;298;-4082.118,-3625.423;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;13;-4717.129,-2000.698;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;312;-3222.439,-1918.478;Inherit;False;313;HorizonColor;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;263;-4468.696,-3622.676;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;303;-3905.697,-3264.179;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;44;-4965.309,-2411.521;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;287;-4967.44,-3479.542;Inherit;False;286;SunDiskSize;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;222;-3184.149,-4178.917;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;87;-4704.689,-1725.258;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;46;-4909.74,-2321.606;Float;False;Property;_HorizonSharpness;Horizon Sharpness;7;0;Create;True;0;0;0;False;0;False;5.7;12.64;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;85;-2347.579,-3392.521;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;42;-5429.089,-2417.07;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;23;-5295.266,-1815.364;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-4265.658,-1887.241;Float;False;Property;_HorizonGlowIntensity;Horizon Glow Intensity;6;0;Create;True;0;0;0;False;0;False;0.59;3.09;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-4948.617,-1708.764;Float;False;Constant;_Float2;Float 2;3;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;304;-4703.22,-4209.964;Float;False;HorizonScaleDayNight;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;91;-3171.351,-3518.678;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;10;-5684.645,-3387.37;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;361;-115.4119,-911.3593;Inherit;False;360;NewSky;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;297;-4360.23,-3475.083;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;265;-4633.829,-3342.748;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-4951.217,-1793.264;Float;False;Constant;_Float1;Float 1;3;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;175;-2005.705,-2685.873;Float;False;Sky;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;200;-5095.147,-3252.788;Float;False;InvVDotL;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;79;-2121.321,-3540.649;Float;False;Property;_NightColor;Night Color;2;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.09251461,0.09571571,0.1981127,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;281;-5410.73,-3382.239;Float;False;Property;_SunDiskSharpness;Sun Disk Sharpness;5;0;Create;True;0;0;0;False;0;False;0;0.0093;0;0.01;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;212;-4235.255,-4223.055;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;16;-4459.192,-1876.333;Float;False;Constant;_Float0;Float 0;0;0;Create;True;0;0;0;False;0;False;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;238;-5194.12,-4231.618;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,1,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;286;-4922.184,-3754.807;Float;False;SunDiskSize;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-5159.266,-1942.364;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;34;-5421.579,-3256.389;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;233;-5134.455,-2777.7;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;240;-4867.307,-4205.637;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;15;-4128.293,-2026.833;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-5625.264,-1831.364;Float;False;Property;_HorizonSunGlowSpreadMax;Horizon Sun Glow Spread Max;10;0;Create;True;0;0;0;False;0;False;0;2.52;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;242;-5342.652,-4092.604;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;271;-4928.328,-3568.031;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;57;-3251.708,-2842.976;Float;False;Property;_HorizonColor;Horizon Color;1;0;Create;True;0;0;0;False;0;False;0.9137255,0.8509804,0.7215686,0;1,0.9080161,0.4274507,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;47;-4776.977,-2398.38;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;203;-3583.393,-3615.91;Float;False;SunDisk;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;313;-2978.938,-2775.517;Float;False;HorizonColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-5147.266,-1813.364;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;234;-5355.499,-2829.648;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;201;-5024.261,-2064.346;Inherit;False;200;InvVDotL;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;88;-4463.604,-1724.59;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,-1,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;221;-3802.005,-4209.277;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;89;-4331.906,-1729.371;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;309;-5643.957,-2730.06;Inherit;False;307;HorizonGlowGlobalScale;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;51;-4609.295,-2393.928;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;41;-5623.786,-2448.381;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;11;-5684.211,-3243.494;Float;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.NegateNode;92;-2884.521,-3498.085;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ClampOpNode;14;-4496.793,-2001.033;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;264;-4467.641,-3752.204;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;84;-2160.83,-3341.162;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;284;-5316.775,-3757.038;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;282;-5132.056,-3577.596;Inherit;False;2;2;0;FLOAT;0.99;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;12;-5249.056,-3255.174;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;26;-4970.717,-1953.164;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;244;-3314.044,-4049.375;Float;False;Property;_HorizonMinAmountAlways;Horizon Min Amount Always;14;0;Create;True;0;0;0;False;0;False;0;0.238;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;220;-3351.759,-4190.11;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;78;-1678.121,-3441.448;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DotProductOpNode;40;-5152.887,-2418.37;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,1,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;39;132.271,-911.8556;Float;False;True;-1;2;ASEMaterialInspector;0;3;DM/ProceduralSkybox;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;RenderType=Opaque=RenderType;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;0;1;True;False;;False;0
WireConnection;393;0;376;2
WireConnection;392;0;393;0
WireConnection;391;0;392;0
WireConnection;382;0;367;0
WireConnection;390;0;391;0
WireConnection;381;0;382;0
WireConnection;383;0;381;0
WireConnection;389;0;379;0
WireConnection;389;2;390;0
WireConnection;384;0;383;0
WireConnection;372;0;389;0
WireConnection;372;2;377;0
WireConnection;372;1;376;1
WireConnection;385;0;384;0
WireConnection;385;1;380;0
WireConnection;371;1;372;0
WireConnection;374;0;358;0
WireConnection;374;1;371;0
WireConnection;388;0;387;0
WireConnection;388;1;385;0
WireConnection;386;0;374;0
WireConnection;386;1;388;0
WireConnection;360;0;386;0
WireConnection;288;0;290;0
WireConnection;288;1;204;0
WireConnection;239;0;238;0
WireConnection;239;1;242;0
WireConnection;69;0;298;0
WireConnection;290;0;289;0
WireConnection;290;1;291;0
WireConnection;289;1;312;0
WireConnection;289;2;315;0
WireConnection;262;0;286;0
WireConnection;262;1;271;0
WireConnection;81;0;92;0
WireConnection;32;0;14;0
WireConnection;302;0;300;2
WireConnection;27;0;25;0
WireConnection;59;0;51;0
WireConnection;55;0;198;0
WireConnection;55;1;313;0
WireConnection;55;2;314;0
WireConnection;261;0;297;0
WireConnection;261;1;264;0
WireConnection;261;2;263;0
WireConnection;19;0;15;0
WireConnection;19;1;17;0
WireConnection;19;2;90;0
WireConnection;90;0;89;0
WireConnection;71;0;69;0
WireConnection;71;1;18;2
WireConnection;71;2;18;1
WireConnection;63;0;233;0
WireConnection;63;1;59;0
WireConnection;63;2;62;0
WireConnection;76;0;55;0
WireConnection;76;1;288;0
WireConnection;75;0;200;0
WireConnection;223;0;222;0
WireConnection;223;3;244;0
WireConnection;22;0;20;0
WireConnection;22;1;21;0
WireConnection;237;0;236;0
WireConnection;268;0;284;0
WireConnection;213;0;212;0
WireConnection;314;0;63;0
WireConnection;235;0;86;0
WireConnection;307;0;223;0
WireConnection;62;0;19;0
WireConnection;215;0;221;0
WireConnection;197;0;78;0
WireConnection;298;0;261;0
WireConnection;298;1;303;0
WireConnection;13;0;201;0
WireConnection;13;1;26;0
WireConnection;13;2;27;0
WireConnection;13;3;28;0
WireConnection;13;4;29;0
WireConnection;263;0;287;0
WireConnection;263;1;262;0
WireConnection;303;0;302;0
WireConnection;44;0;40;0
WireConnection;222;0;220;0
WireConnection;85;0;81;0
WireConnection;85;1;235;0
WireConnection;42;0;41;0
WireConnection;23;0;20;0
WireConnection;23;1;21;0
WireConnection;304;0;240;0
WireConnection;297;0;265;0
WireConnection;265;0;75;0
WireConnection;265;1;75;0
WireConnection;175;0;76;0
WireConnection;200;0;12;0
WireConnection;238;0;237;0
WireConnection;286;0;268;0
WireConnection;24;0;22;0
WireConnection;24;1;22;0
WireConnection;34;0;10;0
WireConnection;233;0;234;0
WireConnection;233;1;309;0
WireConnection;240;0;239;0
WireConnection;15;0;32;0
WireConnection;15;1;16;0
WireConnection;242;0;241;0
WireConnection;242;1;243;0
WireConnection;271;0;282;0
WireConnection;47;0;44;0
WireConnection;203;0;71;0
WireConnection;313;0;57;0
WireConnection;25;0;23;0
WireConnection;25;1;23;0
WireConnection;234;0;305;0
WireConnection;88;0;87;0
WireConnection;221;0;213;0
WireConnection;89;0;88;0
WireConnection;51;0;47;0
WireConnection;51;1;46;0
WireConnection;92;0;91;0
WireConnection;14;0;13;0
WireConnection;264;0;287;0
WireConnection;264;1;262;0
WireConnection;84;0;85;0
WireConnection;284;0;252;0
WireConnection;284;1;285;0
WireConnection;282;1;281;0
WireConnection;12;0;34;0
WireConnection;12;1;11;0
WireConnection;26;0;24;0
WireConnection;220;0;215;0
WireConnection;220;1;216;0
WireConnection;78;0;56;0
WireConnection;78;1;79;0
WireConnection;78;2;84;0
WireConnection;40;0;42;0
WireConnection;39;0;361;0
ASEEND*/
//CHKSM=A766BC0B50E3C02F815A77D7559AB1BBB82B8518