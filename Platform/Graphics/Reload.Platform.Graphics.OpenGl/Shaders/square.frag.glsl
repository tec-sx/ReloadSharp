// Fragment

#version 330

layout(location = 0) out vec4 color;

in vec3 v_position;
in vec2 v_texCoord;

uniform sampler2D u_texture;

void main()
{
   color = texture(u_texture, v_texCoord);
}