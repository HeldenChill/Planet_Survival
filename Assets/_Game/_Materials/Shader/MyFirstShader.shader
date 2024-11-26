Shader "Introduction/MyFirstShader"
{
    Properties
    {
        _Color("Test Color", color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert //Runs on every vert
            #pragma fragment frag //Runs on every single pixel

            #include "UnityCG.cginc"

            struct appdata //Object Data or Mesh
            {
                float4 vertex : POSITION;
            };

            struct v2f //Vertex to frag, passes data drom vert shader to fragment shader
            {
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); //Model View Projection
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = _Color;
                return col;
            }
            ENDCG
        }
    }
}
