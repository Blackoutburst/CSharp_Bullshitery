#version 410

layout(location = 0) in vec2 position;
layout(location = 1) in vec3 color;
layout(location = 2) in vec2 textCoord;

out vec2 vpos;
out vec3 vcol;
out vec2 uv;

void main(void) {
    vpos = position;
    vcol = color;
    uv = textCoord;
    gl_Position = vec4(position, 0.0, 1.0);
}