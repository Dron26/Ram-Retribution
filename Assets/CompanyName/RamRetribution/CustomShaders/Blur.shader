Shader "Custom/Blur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
        }

        CGPROGRAM
        #pragma surface surf Lambert

        struct Input
        {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2Dlod(_MainTex, float4(IN.uv_MainTex, 0.0, 0.0));
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
}