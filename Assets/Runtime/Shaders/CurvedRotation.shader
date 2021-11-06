﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/CurvedRotation"
{ 
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_BrightnessMultiplier("Brightness Multiplier", float) = 1.37
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_Outline("Outline width", Range(.002, 0.03)) = .002
		_RotationSpeed("Rotation Speed", float) = 3.14
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" "DisableBatching" = "True"}
		LOD 100

		UsePass "Unlit/CurvedOutline/OUTLINE"
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work 
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 color : TEXCOORD1;
				UNITY_FOG_COORDS(2)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _CurveStrength;
			float _BrightnessMultiplier;


			v2f vert(appdata v)
			{
				v2f o;

				float _Horizon = 100.0f;
				float _FadeDist = 50.0f;

				float4 rotVert = v.vertex;
				rotVert.y = v.vertex.y * cos(_Time.y * 3.14f) - v.vertex.x * sin(_Time.y * 3.14f);
				rotVert.x = v.vertex.y * sin(_Time.y * 3.14f) + v.vertex.x * cos(_Time.y * 3.14f);

				o.vertex = UnityObjectToClipPos(rotVert);

				float dist = UNITY_Z_0_FAR_FROM_CLIPSPACE(o.vertex.z);

				o.vertex.y -= _CurveStrength * dist * dist * _ProjectionParams.x;

				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				o.color = v.color * _BrightnessMultiplier;

				UNITY_TRANSFER_FOG(o, o.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv) * i.color;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}

			ENDCG
		}
	}
}
