Shader "Custom/TerrainShader"
{
        Properties {
         _Color ("Color", Color) = (1,1,1,1)
         _MainTex ("Albedo (RGB)", 2D) = "white" {}
         _BumpMap ("Normal Map", 2D) = "bump" {}
         _Glossiness ("Smoothness", Range(0,1)) = 0.5
         _Metallic ("Metallic", Range(0,1)) = 0.0
         _Scale ("Scale", Float) = 2.0
     }
     SubShader {
         Tags { "RenderType"="Opaque" }
         LOD 200
         
         CGPROGRAM
         
         #pragma surface surf BlinnPhong fullforwardshadows vertex:vert
 
         
         #pragma target 3.0
 
         sampler2D _MainTex;
         sampler2D _BumpMap;
 
         struct Input {
             float3 coords;
             float3 normal;
         };
 
         half _Glossiness;
         half _Metallic;
         half _Scale;
         fixed4 _Color;
 
         void vert (inout appdata_full v, out Input o) {
             UNITY_INITIALIZE_OUTPUT (Input, o);
             o.coords = mul (unity_ObjectToWorld, v.vertex) * _Scale;
             o.normal = mul ((float3x3)unity_ObjectToWorld, v.normal);
         }
 
         void surf (Input IN, inout SurfaceOutput o) {
             float3 blend = abs (IN.normal);
             blend /= dot (blend, 1.0);
 
             fixed4 bx = tex2D (_BumpMap, IN.coords.zy);
             fixed4 bz = tex2D (_BumpMap, IN.coords.xy);
             fixed4 by = tex2D (_BumpMap, IN.coords.xz);
             fixed4 b = bx * blend.x + bz * blend.z + by * blend.y;
             o.Normal = UnpackNormal (b);
             // Albedo comes from a texture tinted by color
             fixed4 cx = tex2D (_MainTex, IN.coords.zy);
             fixed4 cz = tex2D (_MainTex, IN.coords.xy);
             fixed4 cy = tex2D (_MainTex, IN.coords.xz);
             fixed4 c = cx * blend.x + cz * blend.z + cy * blend.y * _Color;
             o.Albedo = c.rgb;
             o.Alpha = c.a;
         }
         ENDCG
     }
     FallBack "Diffuse"
 }