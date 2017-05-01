Shader "3Dynamite/Vertex-Color" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
}
SubShader {
	Tags { "RenderType"="Opaque" }
	Blend SrcAlpha OneMinusSrcAlpha Cull off
	LOD 200
	
CGPROGRAM
#pragma surface surf Lambert

float4 _Color;

struct Input {
	float4 color : COLOR;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = _Color;
	o.Albedo = c.rgb * IN.color;
	o.Alpha = c.a;
}
ENDCG
} 
FallBack "3Dynamite/Mobile/Vertex Color Mobile Diffuse"
}
