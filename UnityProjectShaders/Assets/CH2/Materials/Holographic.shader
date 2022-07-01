Shader "UFMA/CH3/Holographic"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _DotProduct("Rim effect", Range(-1, 1)) = 0.25
    }
    SubShader
    {
        Tags 
        { 
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent" 
        }

        //backface culling
        Cull front

        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert alpha:fade
        #pragma target 3.0

        float _DotProduct;

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
            float3 viewDir;
        };

        fixed4 _Color;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            float4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

            o.Albedo = c.rgb;

            //quanto mais alinhado com a vista, menor será o valor de border (mais transparente)
            float border = 1 - abs(dot(IN.viewDir, IN.worldNormal));

            float alpha = (border * (1 - _DotProduct) + _DotProduct);
            o.Alpha = c.a * alpha;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
