Shader "Unlit/WorldText"
{
 Properties
 {
     _MainTex ("Texture", 2D) = "white" {}
 }
 SubShader
 {
     Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
     LOD 100

     ZWrite Off
     Blend SrcAlpha OneMinusSrcAlpha

     Pass
     {
         CGPROGRAM
         #pragma vertex vert
         #pragma fragment frag
         
         #include "UnityCG.cginc"

         struct appdata
         {
             float4 vertex : POSITION;
             float2 uv : TEXCOORD0;
             fixed4 color: COLOR;
         };

         struct v2f
         {
             float2 uv : TEXCOORD0;
             float4 vertex : SV_POSITION;
             fixed4 color: COLOR;
         };

         sampler2D _MainTex;
         float4 _MainTex_ST;
         
         v2f vert (appdata v)
         {
             v2f o;
             o.vertex = UnityObjectToClipPos(v.vertex);
             o.uv = TRANSFORM_TEX(v.uv, _MainTex);
             o.color = v.color;
             return o;
         }
         
         fixed4 frag (v2f i) : SV_Target
         {
             fixed4 tex = tex2D(_MainTex, i.uv);
             fixed4 col = i.color;
             col.a = i.color.a*tex.a;
             return col;
         }
         ENDCG
     }
 }
}
