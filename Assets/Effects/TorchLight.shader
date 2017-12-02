Shader "Hidden/TorchLight"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;

			#define SPHERES 2

			fixed4 _PlayerPos;
			fixed4 _CamSize;
			
			fixed _BrightRadius[SPHERES]; // radius including the border
			fixed _Border[SPHERES];
			fixed4 _Skew[SPHERES];
			fixed4 _Offset[SPHERES];
			fixed _Strength[SPHERES];

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = fixed4(0, 0, 0, 0);
				fixed highest = 0;
				for(int j = 0; j < SPHERES; j++){
					fixed strength = distance(i.uv.xy * _CamSize.xy * _Skew[j], _Offset[j] + _PlayerPos * _Skew[j]);
					strength = (1 - clamp((strength - (_BrightRadius[j] - _Border[j])) / _Border[j], 0, 1)) * _Strength[j];
					col += fixed4(strength, strength, strength, 1);
					//return col * tex2D(_MainTex, i.uv);
					//break;
					highest = max(strength, highest);
				}
				return min(col, fixed4(highest, highest, highest, 0)) * tex2D(_MainTex, i.uv);
			}
			ENDCG
		}
	}
}
