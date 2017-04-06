// Shader created with Shader Forge v1.35 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.35;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:32825,y:32699,varname:node_4013,prsc:2|diff-398-RGB,spec-9850-OUT,gloss-9850-OUT,normal-8070-RGB,emission-7781-OUT,alpha-1971-OUT,refract-2528-OUT;n:type:ShaderForge.SFN_Vector1,id:4806,x:32381,y:32651,varname:node_4806,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:4783,x:32426,y:32883,varname:node_4783,prsc:2,v1:1;n:type:ShaderForge.SFN_Tex2d,id:8070,x:32143,y:32897,ptovrint:False,ptlb:Refraction,ptin:_Refraction,varname:node_8070,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:bbab0a6f7bae9cf42bf057d8ee2755f6,ntxv:3,isnm:True;n:type:ShaderForge.SFN_ComponentMask,id:5941,x:32327,y:32947,varname:node_5941,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-8070-RGB;n:type:ShaderForge.SFN_Multiply,id:2528,x:32545,y:33015,varname:node_2528,prsc:2|A-5941-OUT,B-2986-OUT;n:type:ShaderForge.SFN_Slider,id:7160,x:32065,y:33124,ptovrint:False,ptlb:node_7160,ptin:_node_7160,varname:node_7160,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.7863248,max:1;n:type:ShaderForge.SFN_Multiply,id:2986,x:32402,y:33155,varname:node_2986,prsc:2|A-7160-OUT,B-5972-OUT;n:type:ShaderForge.SFN_Vector1,id:5972,x:32155,y:33239,varname:node_5972,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:3620,x:32250,y:32677,varname:node_3620,prsc:2|A-3365-RGB,B-4806-OUT;n:type:ShaderForge.SFN_Slider,id:1658,x:32042,y:32792,ptovrint:False,ptlb:node_7160_copy,ptin:_node_7160_copy,varname:_node_7160_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.6666667,max:3;n:type:ShaderForge.SFN_Vector1,id:9850,x:32500,y:32750,varname:node_9850,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:1971,x:32500,y:32883,varname:node_1971,prsc:2|A-3620-OUT,B-4783-OUT;n:type:ShaderForge.SFN_Tex2d,id:3365,x:32339,y:32491,ptovrint:False,ptlb:node_3365,ptin:_node_3365,varname:node_3365,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d12288d3fa93be244801fd352e64fe62,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector1,id:863,x:32500,y:32689,varname:node_863,prsc:2,v1:1;n:type:ShaderForge.SFN_Tex2d,id:398,x:32588,y:32510,ptovrint:False,ptlb:node_398,ptin:_node_398,varname:node_398,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:7781,x:32638,y:32760,varname:node_7781,prsc:2|A-1971-OUT,B-1658-OUT;proporder:8070-7160-1658-3365-398;pass:END;sub:END;*/

Shader "Shader Forge/Ice" {
    Properties {
        _Refraction ("Refraction", 2D) = "bump" {}
        _node_7160 ("node_7160", Range(0, 1)) = 0.7863248
        _node_7160_copy ("node_7160_copy", Range(0, 3)) = 0.6666667
        _node_3365 ("node_3365", 2D) = "white" {}
        _node_398 ("node_398", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _GrabTexture;
            uniform sampler2D _Refraction; uniform float4 _Refraction_ST;
            uniform float _node_7160;
            uniform float _node_7160_copy;
            uniform sampler2D _node_3365; uniform float4 _node_3365_ST;
            uniform sampler2D _node_398; uniform float4 _node_398_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
                UNITY_FOG_COORDS(6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Refraction_var = UnpackNormal(tex2D(_Refraction,TRANSFORM_TEX(i.uv0, _Refraction)));
                float3 normalLocal = _Refraction_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float node_2528 = (_Refraction_var.rgb.g*(_node_7160*0.2));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + float2(node_2528,node_2528);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float node_9850 = 0.5;
                float gloss = node_9850;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float3 specularColor = float3(node_9850,node_9850,node_9850);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _node_398_var = tex2D(_node_398,TRANSFORM_TEX(i.uv0, _node_398));
                float3 diffuseColor = _node_398_var.rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 _node_3365_var = tex2D(_node_3365,TRANSFORM_TEX(i.uv0, _node_3365));
                float3 node_1971 = ((_node_3365_var.rgb*1.0)*1.0);
                float3 emissive = (node_1971*_node_7160_copy);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,node_1971),1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _GrabTexture;
            uniform sampler2D _Refraction; uniform float4 _Refraction_ST;
            uniform float _node_7160;
            uniform float _node_7160_copy;
            uniform sampler2D _node_3365; uniform float4 _node_3365_ST;
            uniform sampler2D _node_398; uniform float4 _node_398_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
                LIGHTING_COORDS(6,7)
                UNITY_FOG_COORDS(8)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Refraction_var = UnpackNormal(tex2D(_Refraction,TRANSFORM_TEX(i.uv0, _Refraction)));
                float3 normalLocal = _Refraction_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float node_2528 = (_Refraction_var.rgb.g*(_node_7160*0.2));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + float2(node_2528,node_2528);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float node_9850 = 0.5;
                float gloss = node_9850;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float3 specularColor = float3(node_9850,node_9850,node_9850);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _node_398_var = tex2D(_node_398,TRANSFORM_TEX(i.uv0, _node_398));
                float3 diffuseColor = _node_398_var.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                float4 _node_3365_var = tex2D(_node_3365,TRANSFORM_TEX(i.uv0, _node_3365));
                float3 node_1971 = ((_node_3365_var.rgb*1.0)*1.0);
                fixed4 finalRGBA = fixed4(finalColor * node_1971,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
