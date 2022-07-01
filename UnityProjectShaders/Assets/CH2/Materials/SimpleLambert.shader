Shader "UFMA/CH5/SimpleLambert"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white"
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf SimpleLambert
        #pragma target 3.0

        sampler2D _MainTex;

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

        // Cria o modo de ilumina��o SimpleLambert
        half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten)
        {
            //Calcula o dot entre a dire��o da luz e a normal da superficie
            half NDotL = dot(s.Normal, lightDir);

            half4 color;

            color.rgb = s.Albedo * _LightColor0.rgb * (NDotL * atten);
            color.a = s.Alpha;

            return color;
        }


        ENDCG
    }
    FallBack "Diffuse"
}
