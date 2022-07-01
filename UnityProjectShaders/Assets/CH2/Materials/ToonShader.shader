Shader "UFMA/CH5/SimpleLambert"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white"
        _RampTex ("Ramp Texture", 2D) = "white"
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Toon
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _RampTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
        }

        // Cria o modo de iluminação Toon
        half4 LightingToon (SurfaceOutput s, half3 lightDir, half atten)
        {
            //Calcula o dot entre a direção da luz e a normal da superficie
            half NDotL = dot(s.Normal, lightDir);

            //Mapeia o valor do Dot encontrado dentro do mapa de textura
            NDotL = tex2D(_RampTex, fixed2(NDotL, 0.5));

            half4 color;

            color.rgb = s.Albedo * _LightColor0.rgb * (NDotL * atten);
            color.a = s.Alpha;

            return color;
        }


        ENDCG
    }
    FallBack "Diffuse"
}
