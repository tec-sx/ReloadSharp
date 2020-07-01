#type vertex
#version 330 core

layout (location = 0) in vec3 aPosition;
out vec2 fragCoord;

uniform mat4 u_Transform;
uniform mat4 u_viewProjection;

void main()
{
	fragCoord = aPosition.xy;
	gl_Position = u_viewProjection * u_Transform * vec4(aPosition, 1.0);
}

#type fragment
#version 330

in vec2 fragCoord;
out vec4 fragColor;

uniform vec3 color;

const float PI = 3.1415926535897932384626433832795;
const vec2 CANVAS_SIZE = vec2(2);

vec2 convert_to_cell_coords(vec2 coord, vec2 grid);

float lineWidth = 0.01;
vec2 grid = vec2(4);
vec2 cellSize = CANVAS_SIZE / grid;

void main()
{
	vec2 cellCoord = convert_to_cell_coords(fragCoord, cellSize);
	vec2 cutoff = convert_to_cell_coords(vec2(1.0 - lineWidth), cellSize);

	vec2 alpha = step(cutoff, cellCoord);
	if (max(alpha.x, alpha.y) == 0.0)
		discard;

	fragColor = vec4(color, 1.0);
}

vec2 convert_to_cell_coords(vec2 coord, vec2 cellSize)
{
	return cos(((2 * PI) / cellSize) * coord);
}