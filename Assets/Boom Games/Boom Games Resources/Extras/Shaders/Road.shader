Shader "Boom Shaders/Road"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Angle ("Angle", Range(0, 360))= 0.0
        _ThresholdShadow ("Shadow Threshold", Range(0.0,1.0))= 0.3
        _ThresholdLight ("Light Threshold", Range(0.0,1.0))= 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Ramp fullforwardshadows noambient
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
        sampler2D _Ramp;
        sampler2D _MainTex;
        fixed4 _MainTex_ST;
        fixed4 _Color;
        float _Angle;
        float _ThresholdShadow;
        float _ThresholdLight;
        
        half4 LightingRamp (SurfaceOutput s, half3 lightDir, half atten) {
            half NdotL = dot (s.Normal, lightDir);
            half diff = NdotL * .5 + .5;
            diff = smoothstep(0, 1.0-_ThresholdLight, diff) * _LightColor0;
            atten = smoothstep(0., 1-_ThresholdShadow, atten) * .5 + .5;
            half4 c;
            c.rgb = s.Albedo * diff * atten;
            c.a = s.Alpha;
            return c;
        }
        
        struct Input
        {
            float3 worldPos;
        };
        
        float2 rotateUV(float2 uv, float rotation) {
            uv.xy -= 0.5; // shift the center of the coordinates to (0,0)
            float s, c;
            sincos(rotation, s, c); // compute the sin and cosine
            float2x2 rotation_matrix = float2x2(c, -s, s, c);
            uv.xy = mul(uv.xy, rotation_matrix);
            uv.xy += 0.5; // shift the center of the coordinates back to (0.5,0.5)
            return uv;
        }

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        
        void surf (Input IN, inout SurfaceOutput o)
        {
            float2 uv = rotateUV(IN.worldPos.xz, _Angle * UNITY_PI / 180.);
            float2 st = (uv + _MainTex_ST.zw) * _MainTex_ST.xy;
            // Albedo comes from a texture tinted by color
            half4 c = tex2D (_MainTex, st) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = _Color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
