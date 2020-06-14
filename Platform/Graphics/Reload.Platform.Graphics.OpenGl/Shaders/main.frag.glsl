// Fragment

#version 330

layout(location = 0) out vec3 color;

in vec3 v_position;
in vec3 v_color;

void main()
{
   color = vec3(v_position * 0.5 + 0.5);
   color = v_color;
}