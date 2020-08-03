// Fragment

#version 330

layout(location = 0) out vec4 color;

in vec3 v_position;
in vec2 v_texCoord;

uniform sampler2D u_Texture;

void main()
{
   color = texture(u_Texture, v_texCoord);
}