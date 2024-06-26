Shader "Custom/DissolveSurfaces" {
 Properties {
 _Color ("Color", Color) = (1,1,1,1)
 _MainTex ("Albedo (RGB)", 2D) = "white" {}
 
 //Dissolve properties
 _DissolveTexture("Dissolve Texutre", 2D) = "white" {} 
 _Amount("Amount", Range(0,1)) = 0
 }
 
 SubShader {
 Tags { "RenderType"="Opaque" }
 LOD 200
 Cull Off //Fast way to turn your material double-sided
 
 CGPROGRAM
 #pragma surface surf Standard fullforwardshadows
 
 #pragma target 3.0
 
 sampler2D _MainTex;
 
 struct Input {
 float2 uv_MainTex;
 };
 
 fixed4 _Color;
 
 //Dissolve properties
 sampler2D _DissolveTexture;
 float _Amount;
 
 void surf (Input IN, inout SurfaceOutputStandard o) {
 
 //Dissolve function
 half dissolve_value = tex2D(_DissolveTexture, IN.uv_MainTex).r;
 clip(dissolve_value - _Amount);
 
 //Basic shader function
 fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color; 
 
 o.Albedo = c.rgb;
 o.Alpha = c.a;
 }
 ENDCG
 }
 FallBack "Diffuse"
}