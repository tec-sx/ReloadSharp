// Vertex
#version 330

layout (location = 0) in vec3 position;
layout (location = 1) in vec3 color;

uniform mat4 u_viewProjection;

out vec3 v_position;
out vec3 v_color;

void main() {
    v_position = position;
    v_color = color;
    gl_Position = u_viewProjection * vec4(position, 1.0);
}