// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Prime[31]/vertex Colorizer No Alpha" {
	Properties {
	
	}
	SubShader 
	{
		Lighting Off
		pass
		{
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct a2v
			{
			float4 vertex : POSITION;
			float4 color  : COLOR;
			};
			
			struct v2f
			{
				float4 pos : SV_POSITION;
				float4 color : COLOR0;
			};
			
			v2f vert( a2v input)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(input.vertex);
				o.color = input.color;
				return o;
			}
			
			half4 frag(v2f i):COLOR
			{
			return i.color;
			}
			ENDCG

		}
	}
	
}
