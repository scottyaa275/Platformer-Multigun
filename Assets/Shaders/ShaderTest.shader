// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:Standard,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.05,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32783,y:32714,varname:node_3138,prsc:2|emission-825-OUT,alpha-1121-OUT,tess-9210-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32531,y:32510,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:0;n:type:ShaderForge.SFN_Time,id:5327,x:32086,y:32610,varname:node_5327,prsc:2;n:type:ShaderForge.SFN_Append,id:825,x:32531,y:32712,varname:node_825,prsc:2|A-7241-R,B-7241-G,C-7241-B,D-6512-OUT;n:type:ShaderForge.SFN_Sin,id:4290,x:32325,y:32751,varname:node_4290,prsc:2|IN-5269-OUT;n:type:ShaderForge.SFN_Abs,id:6512,x:32325,y:32610,varname:node_6512,prsc:2|IN-4290-OUT;n:type:ShaderForge.SFN_ComponentMask,id:1121,x:32514,y:32851,varname:node_1121,prsc:2,cc1:3,cc2:-1,cc3:-1,cc4:-1|IN-825-OUT;n:type:ShaderForge.SFN_Multiply,id:5269,x:32135,y:32751,varname:node_5269,prsc:2|A-5327-T,B-1930-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1930,x:32121,y:32911,ptovrint:False,ptlb:Amplifier,ptin:_Amplifier,varname:node_1930,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_NormalVector,id:1745,x:32121,y:33029,prsc:2,pt:False;n:type:ShaderForge.SFN_Time,id:1548,x:32297,y:32911,varname:node_1548,prsc:2;n:type:ShaderForge.SFN_Add,id:4660,x:32320,y:33059,varname:node_4660,prsc:2|A-1548-T,B-1745-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9210,x:32541,y:33160,ptovrint:False,ptlb:Smoothness,ptin:_Smoothness,varname:node_9210,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:7241-1930-9210;pass:END;sub:END;*/

Shader "Shader Forge/ShaderTest" {
    Properties {
        _Color ("Color", Color) = (1,1,1,0)
        _Amplifier ("Amplifier", Float ) = 1
        _Smoothness ("Smoothness", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "Tessellation.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 5.0
            uniform float4 _Color;
            uniform float _Amplifier;
            uniform float _Smoothness;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            #ifdef UNITY_CAN_COMPILE_TESSELLATION
                struct TessVertex {
                    float4 vertex : INTERNALTESSPOS;
                    float3 normal : NORMAL;
                    float4 tangent : TANGENT;
                };
                struct OutputPatchConstant {
                    float edge[3]         : SV_TessFactor;
                    float inside          : SV_InsideTessFactor;
                    float3 vTangent[4]    : TANGENT;
                    float2 vUV[4]         : TEXCOORD;
                    float3 vTanUCorner[4] : TANUCORNER;
                    float3 vTanVCorner[4] : TANVCORNER;
                    float4 vCWts          : TANWEIGHTS;
                };
                TessVertex tessvert (VertexInput v) {
                    TessVertex o;
                    o.vertex = v.vertex;
                    o.normal = v.normal;
                    o.tangent = v.tangent;
                    return o;
                }
                float Tessellation(TessVertex v){
                    return _Smoothness;
                }
                float4 Tessellation(TessVertex v, TessVertex v1, TessVertex v2){
                    float tv = Tessellation(v);
                    float tv1 = Tessellation(v1);
                    float tv2 = Tessellation(v2);
                    return float4( tv1+tv2, tv2+tv, tv+tv1, tv+tv1+tv2 ) / float4(2,2,2,3);
                }
                OutputPatchConstant hullconst (InputPatch<TessVertex,3> v) {
                    OutputPatchConstant o = (OutputPatchConstant)0;
                    float4 ts = Tessellation( v[0], v[1], v[2] );
                    o.edge[0] = ts.x;
                    o.edge[1] = ts.y;
                    o.edge[2] = ts.z;
                    o.inside = ts.w;
                    return o;
                }
                [domain("tri")]
                [partitioning("fractional_odd")]
                [outputtopology("triangle_cw")]
                [patchconstantfunc("hullconst")]
                [outputcontrolpoints(3)]
                TessVertex hull (InputPatch<TessVertex,3> v, uint id : SV_OutputControlPointID) {
                    return v[id];
                }
                [domain("tri")]
                VertexOutput domain (OutputPatchConstant tessFactors, const OutputPatch<TessVertex,3> vi, float3 bary : SV_DomainLocation) {
                    VertexInput v = (VertexInput)0;
                    v.vertex = vi[0].vertex*bary.x + vi[1].vertex*bary.y + vi[2].vertex*bary.z;
                    v.normal = vi[0].normal*bary.x + vi[1].normal*bary.y + vi[2].normal*bary.z;
                    v.tangent = vi[0].tangent*bary.x + vi[1].tangent*bary.y + vi[2].tangent*bary.z;
                    VertexOutput o = vert(v);
                    return o;
                }
            #endif
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_5327 = _Time;
                float4 node_825 = float4(_Color.r,_Color.g,_Color.b,abs(sin((node_5327.g*_Amplifier))));
                float3 emissive = node_825.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,node_825.a);
            }
            ENDCG
        }
    }
    FallBack "Standard"
    CustomEditor "ShaderForgeMaterialInspector"
}
