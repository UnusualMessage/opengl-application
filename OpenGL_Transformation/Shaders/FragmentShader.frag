#version 330

out vec4 FragColor;

uniform vec3 inputColor;

void main()
{
    FragColor = vec4(inputColor, 1.0f);
}