// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "shieldShader"
{
    Properties 
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Color ("Color (RGBA)", Color) = (1, 1, 1, 1) // add _Color property
        [HDR]
		_RimColor("Rim Color", Color) = (1,1,1,1)
		_RimAmount("Rim Amount", Range(0, 1)) = 0.716
		_RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
        
        _Power("Power", Int) = 2
    }

    SubShader 
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull front 
        LOD 100

        Pass 
        {
            CGPROGRAM

            #pragma vertex vert alpha
            #pragma fragment frag alpha

            #include "UnityCG.cginc"

            struct appdata_t 
            {
                float4 vertex   : POSITION;
                float2 texcoord : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f 
            {
                float4 vertex  : SV_POSITION;
                float3 worldNormal : NORMAL;
                half2 texcoord : TEXCOORD0;
                float3 viewDir : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;

            v2f vert (appdata_t v)
            {
                v2f o;

                o.vertex     = UnityObjectToClipPos(v.vertex);
                v.texcoord.x = 1 - v.texcoord.x;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = WorldSpaceViewDir(v.vertex);
                o.texcoord   = TRANSFORM_TEX(v.texcoord, _MainTex);

                return o;
            }

            float4 _RimColor;
			float _RimAmount;
			float _RimThreshold;
            int _Power;

            fixed4 frag (v2f i) : SV_Target
            {
                float3 normal = normalize(i.worldNormal);
				float3 viewDir = normalize(i.viewDir);
                fixed4 col = tex2D(_MainTex, i.texcoord) * _Color; // multiply by _Color
                // float4 rimDot = 1 - dot(viewDir, normal);
				// float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
				// rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
				// float4 rim = rimIntensity * _RimColor;
                float rim = 1 - saturate ( dot(viewDir, normal) );
     
                float3 rimLight = pow(rim, _Power) * _Color;
                //i.Emission = _RimColor.rgb * pow (rim, _RimPower);
                return float4(col + rimLight, 1.0f);
            }

            ENDCG
        }
    }
}