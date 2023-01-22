varying in vec4 vert_pos;

struct Light
{	
	float lightDistance;
	float lightIntensity;
	vec2 lightPosition;
	vec3 lightAmbient;
	vec3 lightColor;
};

uniform sampler2D texture;
uniform bool hasTexture;

const int LIGHTS_COUNT = 15;
uniform Light light[LIGHTS_COUNT];

vec4 CalcMultiLight(Light light)
{
	light.lightPosition = (gl_ModelViewProjectionMatrix * vec4(light.lightPosition, 0, 1)).xy;
	
	vec2 lightToFrag = light.lightPosition - vert_pos.xy;
	lightToFrag.y = lightToFrag.y / 1.7;

	float vecLength = clamp(length(lightToFrag) * light.lightDistance, 0, light.lightIntensity);

	vec4 pixel = texture2D(texture, gl_TexCoord[0].xy);

    	if(hasTexture == true)
	{
		return gl_Color * pixel * (clamp(vec4(light.lightAmbient, 1.0) + vec4(1-vecLength, 1-vecLength, 1-vecLength, 1), 0, 1)) * vec4(light.lightColor, 1);
	}
	else
	{
		return gl_Color;
	}
}

void main()
{	
	vec4 result;

	for (int i = 0; i < LIGHTS_COUNT; i++)
		result += CalcMultiLight(light[i]);

	gl_FragColor = result;
}