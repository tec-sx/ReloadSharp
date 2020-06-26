// Vertex
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