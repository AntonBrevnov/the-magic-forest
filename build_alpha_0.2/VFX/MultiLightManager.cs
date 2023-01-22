using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace build_alpha_0._2.VFX
{
    public class MultiLightManager
    {
        private const int LIGHTS_COUNT = 15;

        private List<Light> lights;
        private Light multipleLight;

        private Clock dayCycleClock;
        private bool isDay;

        public MultiLightManager(string vertexShader, string fragmentShader)
        {
            multipleLight = new Light();
            multipleLight.LightShader = new Shader(vertexShader, null, fragmentShader);

            lights = new List<Light>();

            dayCycleClock = new Clock();
            isDay = true;
        }

        public void AddLight(Light light)
        {
            if (lights.Count < LIGHTS_COUNT)
            {
                foreach (var l in lights)
                    if (l == light) return;

                lights.Add(light);
            }
        }
        public void DeleteLight(Light light)
        {
            lights.Remove(light);
        }
        public void ClearLightList()
        {
            lights.Clear();
        }

        public void SetDay()
        {
            lights[0].LightIntensity = 0.0f;
            multipleLight.LightShader.SetParameter($"light[0].lightIntensity", lights[0].LightIntensity);
            isDay = true;
            dayCycleClock.Restart();
        }
        public void SetNigth()
        {
            lights[0].LightIntensity = 0.9f;
            multipleLight.LightShader.SetParameter($"light[0].lightIntensity", lights[0].LightIntensity);
            isDay = false;
            dayCycleClock.Restart();
        }

        public void OnUpdate()
        {
            if (lights.Count > 0)
            {
                if (isDay)
                { 
                    if (dayCycleClock.ElapsedTime.AsSeconds() >= 60)
                    {
                        lights[0].LightIntensity = 0.1f;
                        multipleLight.LightShader.SetParameter($"light[0].lightIntensity", lights[0].LightIntensity);
                    }
                    if (dayCycleClock.ElapsedTime.AsSeconds() >= 65)
                    {
                        lights[0].LightIntensity = 0.3f;
                        multipleLight.LightShader.SetParameter($"light[0].lightIntensity", lights[0].LightIntensity);
                    }
                    if (dayCycleClock.ElapsedTime.AsSeconds() >= 70)
                    {
                        lights[0].LightIntensity = 0.5f;
                        multipleLight.LightShader.SetParameter($"light[0].lightIntensity", lights[0].LightIntensity);
                    }
                    if (dayCycleClock.ElapsedTime.AsSeconds() >= 75)
                    {
                        lights[0].LightIntensity = 0.7f;
                        multipleLight.LightShader.SetParameter($"light[0].lightIntensity", lights[0].LightIntensity);
                    }
                    if (dayCycleClock.ElapsedTime.AsSeconds() >= 80)
                    {
                        lights[0].LightIntensity = 0.9f;
                        multipleLight.LightShader.SetParameter($"light[0].lightIntensity", lights[0].LightIntensity);
                        isDay = false;
                        dayCycleClock.Restart();
                    }
                }
                else
                {
                    if (dayCycleClock.ElapsedTime.AsSeconds() >= 60)
                    {
                        lights[0].LightIntensity = 0.7f;
                        multipleLight.LightShader.SetParameter($"light[0].lightIntensity", lights[0].LightIntensity);
                    }
                    if (dayCycleClock.ElapsedTime.AsSeconds() >= 65)
                    {
                        lights[0].LightIntensity = 0.5f;
                        multipleLight.LightShader.SetParameter($"light[0].lightIntensity", lights[0].LightIntensity);
                    }
                    if (dayCycleClock.ElapsedTime.AsSeconds() >= 70)
                    {
                        lights[0].LightIntensity = 0.3f;
                        multipleLight.LightShader.SetParameter($"light[0].lightIntensity", lights[0].LightIntensity);
                    }
                    if (dayCycleClock.ElapsedTime.AsSeconds() >= 75)
                    {
                        lights[0].LightIntensity = 0.1f;
                        multipleLight.LightShader.SetParameter($"light[0].lightIntensity", lights[0].LightIntensity);
                    }
                    if (dayCycleClock.ElapsedTime.AsSeconds() >= 80)
                    {
                        lights[0].LightIntensity = 0.0f;
                        multipleLight.LightShader.SetParameter($"light[0].lightIntensity", lights[0].LightIntensity);
                        isDay = true;
                        dayCycleClock.Restart();
                    }
                }
            }

            for (int i = 0; i < lights.Count; i++)
            {
                multipleLight.LightShader.SetParameter("hasTexture", 1);
                multipleLight.LightShader.SetParameter($"light[{i}].lightDistance", lights[i].LightDistance);
                multipleLight.LightShader.SetParameter($"light[{i}].lightIntensity", lights[i].LightIntensity);
                multipleLight.LightShader.SetParameter($"light[{i}].lightPosition", lights[i].LightPosition.X, lights[i].LightPosition.Y);
                multipleLight.LightShader.SetParameter($"light[{i}].lightAmbient", lights[i].AmbientColor.X, lights[i].AmbientColor.Y, lights[i].AmbientColor.Z);
                multipleLight.LightShader.SetParameter($"light[{i}].lightColor", lights[i].LightColor.X, lights[i].LightColor.Y, lights[i].LightColor.Z);
            }
        }

        public Light GetMultipleLight()
        {
            return multipleLight;
        }
        public bool GetDayCycleState() 
        {
            return isDay;
        }
    }
}
