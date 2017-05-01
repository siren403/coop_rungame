Shader "3Dynamite/Vertex-Color Particle Emissive"
{
	Properties
	{
		_Emission ("Emission", Float) = 1.0
	}
	SubShader
	{
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
	Blend SrcAlpha OneMinusSrcAlpha
	Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
		LOD 100

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
				float4 color : COLOR;
			};

			struct v2f
			{
				UNITY_FOG_COORDS(1)
				float4 color : COLOR;
				float4 vertex : SV_POSITION;
			};


			float _Emission;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.color = v.color;
				o.vertex = UnityObjectToClipPos(v.vertex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = i.color;
				fixed4 o = float4(col.rgb * _Emission, col.a);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return o;
			}
			ENDCG
		}
	}
}