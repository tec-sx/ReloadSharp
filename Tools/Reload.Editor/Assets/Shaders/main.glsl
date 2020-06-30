#type vertex
#version 330

layout (location = 0) in vec3 position;
layout (location = 1) in vec2 texCoord;

uniform mat4 u_viewProjection;
uniform mat4 u_Transform;

out vec3 v_position;
out vec2 v_texCoord;

void main() {
    v_position = position;
    v_texCoord = texCoord;

    gl_Position = u_viewProjection * u_Transform * vec4(position, 1.0);
}

#type fragment
#version 330

layout(location = 0) out vec4 color;

in vec3 v_position;
in vec2 v_texCoord;

uniform sampler2D u_texture;

void main()
{
   color = texture(u_texture, v_texCoord);
}