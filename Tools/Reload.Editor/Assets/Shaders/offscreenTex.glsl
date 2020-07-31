#type vertex
#version 330 core

layout (location = 0) in vec3 a_position;
layout (location = 1) in vec2 a_texCoord;

out vec2 fragTexCoord;

void main() {
   fragTexCoord = a_texCoord;
   gl_Position = vec4(a_position, 1.0f);
}

#type fragment
#version 330 core

uniform sampler2D tex;
in vec2 fragTexCoord;
out vec4 color;
void main() {
	color = texture(tex, fragTexCoord);
}