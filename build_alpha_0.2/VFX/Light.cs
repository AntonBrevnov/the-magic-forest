using SFML.Graphics;
using SFML.System;

namespace build_alpha_0._2.VFX
{
    public class Light
    {
        public Shader LightShader { get; set; }
        public Vector2f LightPosition { get; set; }
        public float LightIntensity { get; set; }
        public float LightDistance { get; set; }

        public Vector3f LightColor { get; set; }
        public Vector3f AmbientColor { get; set; }

        public void EnableLight()
        {
            LightShader.SetParameter("hasTexture", 1);
            LightShader.SetParameter("lightDistance", LightDistance);
            LightShader.SetParameter("lightIntesivity", LightIntensity);
            LightShader.SetParameter("lightPos", LightPosition.X, LightPosition.Y);
            LightShader.SetParameter("lightAmbient", AmbientColor.X, AmbientColor.Y, AmbientColor.Z);
            LightShader.SetParameter("lightColor", LightColor.X, LightColor.Y, LightColor.Z);
        }
    }
}
