// http://www.alanzucconi.com/2015/07/01/vertex-and-fragment-shaders-in-unity3d/
// To render non-static distortions, need to add time to a shader.
// - theoretically possible by adding _Time property updated with game time.
// - Unity does this automatically. Built in variable:
//   - _Time
// - This property a packed array of length four which contains t/20, t, t*2, and t*3
// - Where t is the actual time.
// - If need something to oscillate over time, can also use:
//   - _SinTime
// - Contains sin(t/8), sin(t/4), sin(t/2), and sin(t).

// Now creating a toony 2D water shader.
// Displace previously grabbed textures, and use the current time in the calculation of the
// displacement.

// Three textures used are:
//  _GrabTexture: the previously grabbed texture.
//  _NoiseTex: texture filled with random noise which is used to increase the random look of the water.
// _CausticTex: texture which is a caustic reflection, used to give a more realistic feel to the water.

Shader "Custom/WaterShader"
{
    Properties
    {
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
        _NoiseTex ("Noise texture", 2D) = "bump" {}
        _CausticTex ("Caustic Texture", 2D) = "bump" {}

        _waterMagnitude ("Water Magnitude", Range(0,1)) = 1
        _waterPeriod("Water Period", float) = 1
        _offset ("Offset", float) = 1
        
        _waterColor ("Water Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Geometry"
            "IgnoreProjector" = "True"
            "RenderType" = "Opaque"
        }

        ZWrite On
        Lighting Off
        Cull Off
        Fog { Mode Off }
        Blend One Zero

        GrabPass { "_GrabTexture" }
        
        Pass
        {
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _GrabTexture;

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            sampler2D _CausticTex;
            
            float _offset;
            float _waterMagnitude;
            float _waterPeriod;
            
            half4 _waterColor;

            struct vin_vct
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f_vct
            {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;

                float4 uvgrab : TEXCOORD1;
            };

            float2 sinusoid (float2 x, float2 m, float2 M, float2 p)
            {
                float2 e = M - m;
                float2 c = 3.1415 * 2.0 / p;
                return e / 2.0 * (1.0 + sin(x * c)) + m;
            }
            
            // Vertex function
            v2f_vct vert( vin_vct v )
            {
                v2f_vct o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                o.texcoord = v.texcoord;
                
                o.uvgrab = ComputeGrabScreenPos(o.vertex);

                return o;
            }

            // Fragment function
            fixed4 frag(v2f_vct i) : COLOR
            {
                fixed4 noise = tex2D(_NoiseTex, i.texcoord);
                fixed4 mainColor = tex2D(_MainTex, i.texcoord);

                float time = _Time[1];

                float2 waterDisplacement = sinusoid
                                            (
                                                float2(time, time) + (noise.xy) * _offset,
                                                float2(-_waterMagnitude, -_waterMagnitude),
                                                float2(+_waterMagnitude, +_waterMagnitude),
                                                float2(_waterPeriod, _waterPeriod)
                                            );
                
                i.uvgrab.xy += waterDisplacement;

                fixed4 col = tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(i.uvgrab));
                fixed4 causticColor = tex2D(_CausticTex, i.texcoord.xy * 0.25 + waterDisplacement * 5);

                return col * mainColor * _waterColor * causticColor;
            }
            ENDCG
        }
    }
}
