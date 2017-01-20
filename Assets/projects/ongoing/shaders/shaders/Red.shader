// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyShader"
{
    Properties
    {
        // The Properties of your shaders
        // - textures
        // - colors
        // - parameters.
        // ...
        
        _MyTexture ("My texture", 2D) = "white" {}
        _MyNormalMap ("My normal map", 2D) = "bump" {} // Gray
        
        _MyInt ("My integer", Int) = 2
        _MyFloat ("My float", Float) = 1.5
        _MyRange ("My range", range(0.0,1.0)) = 0.5
        
                              // I'm copying this from the
                              // Unity tutorial, which has
                              // all the British English 
                              // flavoured words. However,
                              // I'm using their American
                              // English spellings. But turns
                              // out the `Color` right there
                              // this engrained in the shader
                              
                              // language. So the tutorial shader:
                              // ("My colour", Color).
        _MyColor ("My color", Color) = (1, 0, 0, 1) // (R, G, B, A)
        _MyVector ("My Vector4", Vector) = (0, 0, 0, 0) // (x, y, z, w)
    }
    
    SubShader
    {
        // The code of your shaders.
        // - surface shader
        //    OR
        // - vertex and frament shader
        //    OR
        // - fixed function shader
        
        Tags
        {
            "Queue" = "Geometry"
            "RenderType" = "Opaque"
        }
        
        Pass
        {
            CGPROGRAM
            // Cg / HLSL code of the shader
            // ...
            
            #pragma vertex vert
            #pragma fragment frag

            struct vertInput
            {
                float4 pos : POSITION;
            };

            struct vertOutput
            {
                float4 pos : SV_POSITION;
            };

            sampler2D _MyTexture;
            sampler2D _MyNormalMap;
            
            int _MyInt;
            float _MyFloat;
            float _MyRange;
            half4 _MyColor;
            float4 _MyVector;
        
            vertOutput vert(vertInput input)
            {
                vertOutput o;
                o.pos = UnityObjectToClipPos(input.pos);
                return o;
            }

            half4 frag(vertOutput output) : COLOR
            {
                return _MyColor;//half4(1.0, 0.0, 0.0, 1.0);
            }
            ENDCG
        }
    }
}
