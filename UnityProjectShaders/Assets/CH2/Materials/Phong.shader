Shader "UFMA/CH5/Phong"
{
    Properties
    {
        _MainColor ("Diffuse Tint", Color) = (1, 1, 1, 1)
        _MainTex ("Base RGB", 2D) = "white" {}
        _SpecularColor ("Specular Color", Color) = (1,1,1,1)
        _SpecPower ("Specular Power", Range(0, 30)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Phong
        #pragma target 3.0

        float4 _SpecularColor;
        sampler2D _MainTex;
        float4 _MainColor;
        float _SpecPower;

        struct Input
        {
            float2 uv_MainTex;
        };

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _MainColor;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }

        fixed4 LightingPhong(SurfaceOutput s, fixed3 lightDir, half3 viewDir, fixed atten)
        {
            float NDotL = dot(s.Normal, lightDir);

            //Calcula o vetor de reflexão da luz
            float3 reflectionVector = normalize(2.0 * s.Normal * NDotL - lightDir);

            //Calcula o componente specular
            float spec = pow(max(0, dot(reflectionVector, viewDir)), _SpecPower);

            float3 finalSpec = _SpecularColor.rgb * spec;
            
            //Calcula a cor final
            fixed4 c;
            c.rgb = (s.Albedo * _LightColor0.rgb * max(0, NDotL) * atten) + (_LightColor0.rgb * finalSpec * atten);
            c.a = s.Alpha;
            return c;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
