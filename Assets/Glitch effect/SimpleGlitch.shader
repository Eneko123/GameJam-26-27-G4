Shader "Custom/SimpleGlitch"
{
    Properties
    {
        _Intensity ("Intensity", Range(0, 1)) = 0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        ZWrite Off
        ZTest Always
        Cull Off

        Pass
        {
            Name "SimpleGlitch"

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            float _Intensity;

            // Número pseudoaleatorio entre 0 y 1
            float Hash(float2 p)
            {
                return frac(sin(dot(p, float2(127.1, 311.7))) * 43758.5453);
            }

            half4 Frag(Varyings input) : SV_Target
            {
                float2 uv = input.texcoord;

                // El patrón de glitch cambia unas 12 veces por segundo
                float tick = floor(_Time.y * 12.0);

                // 1) Algunas franjas horizontales se desplazan de lado
                float row = floor(uv.y * 25.0);
                float isGlitchRow = step(1.0 - _Intensity * 0.4, Hash(float2(row, tick)));
                float shift = (Hash(float2(row + 7.0, tick)) - 0.5) * 0.2 * _Intensity;
                uv.x += shift * isGlitchRow;

                // 2) Separación de canales de color (rojo a un lado, azul al otro)
                float split = 0.015 * _Intensity * (0.5 + isGlitchRow);
                half r = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv + float2(split, 0)).r;
                half g = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv).g;
                half b = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv - float2(split, 0)).b;

                return half4(r, g, b, 1);
            }
            ENDHLSL
        }
    }
}
