// Vertex
#version 330

layout (location = 0) in vec3 position;

out vec3 v_position;
out vec3 v_color;

void main() {
    v_position = position;
    gl_Position = vec4(position, 1.0);
}