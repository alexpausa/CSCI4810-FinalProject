// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Toon Shader"
{

	// SWTICH TO POINT LIGHT FOR BETTER SPECULARITY AND MAYBE TRY TO ADD OUTLINE IN HERE
	// ALSO TRY TO MAKE ANOTHER SHADER JUST USING THE NORMAL.Z TO MAKE A DIFFERENT TOON SHADER
	// LIKE IN PROJECT4.
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Main Texture", 2D) = "white" {}
		
		_AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)

    }
    SubShader
		{
			Pass
			{
				// Renders Forwart and only takes in directional lighting from the scene.
				Tags
				{
					"LightMode" = "ForwardAdd"
					//"PassFlags" = "OnlyDirectional"
				}

				CGPROGRAM

				#pragma vertex toonVertexShader
				#pragma fragment toonFragmentShader

				// Compile multiple versions of this shader depending on lighting settings.
				#pragma multi_compile_fwdbase

				#include "UnityCG.cginc"
				// Files to help with lighting and shadows within Unity. I am not too sure on the use
				// of these but was recommended from some HLSL tutorials for Unity.
				#include "Lighting.cginc"
				#include "AutoLight.cginc"

				struct appdata
				{
					//float# represents a vector with a specific # of variables
					float4 vertex : POSITION;		
					float4 uv : TEXCOORD0;
					float3 normal : NORMAL;
				};

				// Represents data from a vertex shader to the frame buffer.
				struct v2f
				{
					float4 pos : SV_POSITION;
					float3 worldNormal : NORMAL;
					float2 uv : TEXCOORD0;
					float3 viewDir : TEXCOORD1;
					float dist : DISTANCE;

					//DELETE THIS AND SEE WHAT IT DOES!!!!
					// Macro found in Autolight.cginc. Declares a vector4
					// into the TEXCOORD2 semantic with varying precision 
					// depending on platform target.
					SHADOW_COORDS(2)
				};

				// Figure out what these do too
				sampler2D _MainTex;
				float4 _MainTex_ST;

				v2f toonVertexShader(appdata v)
				{
					v2f vertexShader;
					vertexShader.pos = UnityObjectToClipPos(v.vertex);
					vertexShader.worldNormal = UnityObjectToWorldNormal(v.normal);
					vertexShader.viewDir = WorldSpaceViewDir(v.vertex);
					vertexShader.uv = TRANSFORM_TEX(v.uv, _MainTex);

					float3 vertexPos = UnityObjectToClipPos(v.vertex).xyz;
					vertexShader.dist = distance(vertexPos, _WorldSpaceLightPos0);

					// Defined in Autolight.cginc. Assigns the above shadow coordinate
					// by transforming the vertex from world space to shadow-map space.
					TRANSFER_SHADOW(vertexShader)
				
					return vertexShader;
				}

				float4 _Color;
				float4 _AmbientColor;

				float4 toonFragmentShader (v2f i) : SV_Target
				{
					float3 normal = normalize(i.worldNormal);
					float3 viewDir = normalize(i.viewDir);

					// Lighting is calculated using Phong converted to make a 
					// nice toon look

					// Calculate illumination from directional light.
					// _WorldSpaceLightPos0 is a vector pointing the OPPOSITE
					// direction of the main directional light.
					float NdotL = dot(_WorldSpaceLightPos0, normal);
					float lightToObject = i.dist;

					float lightIntensity;

					float func1 = (lightToObject - 26.0) / -65.0;
					float func2 = (lightToObject - 35.0) / -70.0;
					float func3 = (lightToObject - 45.0) / -41.0;
					float func4 = (lightToObject - 60.0) / -40.0;

					if ( NdotL > func4 - 1) {
						lightIntensity = 1.0;
					}
					else if ( NdotL > func3 - 1 ) {
						lightIntensity = 0.7;
					}
					else if ( NdotL > func2 - 1 ) {
						lightIntensity = 0.5;
					}
					else if ( NdotL > func1 - 1 ) {
						lightIntensity = 0.1;
					}
					else {
						lightIntensity = 0.0;
					}

					// Multiply by the main directional light's intensity and color.
					float4 light = lightIntensity * _LightColor0;

					float4 sample = tex2D(_MainTex, i.uv);
	
					return (light + _AmbientColor) * _Color * sample;
				}

			ENDCG
			}

			// Shadow casting support. - maybe add your own custom shadow maker or just turn them off lol
			UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
		}
}
