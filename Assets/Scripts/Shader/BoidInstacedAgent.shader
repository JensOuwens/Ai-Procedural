Shader "Custom/BoidInstacedAgent"
{
    SubShader
    {
        tags
        {
            "RenderPipeline" = "UniversalRenderPipeline" "RenderType"="Opaque"
        }
        
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct BoidAgent
            {
                float3 position;
                float2 velocity;
                float3 target;
                float maxSpeed;
            };
            
            StructuredBuffer<BoidAgent> agents;

            struct Attributes
            {
                float3 positionOS : POSITION;
                uint instanceID : SV_InstanceID;
            };
            
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
            };
            
            Varyings vert(Attributes v)
            {
                BoidAgent agent = agents[v.instanceID];
                
                float3 positionOS = v.positionOS;
                positionOS.x += agent.position.x;
                positionOS.y += agent.position.y;
                
                Varyings O;
                O.positionCS = TransformObjectToHClip(positionOS);
                return O;
            }
            
            half4 frag() : SV_Target {
                return half4(1,1,1,1);
            }
            ENDHLSL
        }
    }
}
