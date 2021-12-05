#version 410

in vec2 vpos;
in vec3 vcol;
in vec2 uv;

uniform sampler2D texture0;

out vec4 Frag_Color;

void main(void) {
    vec4 color = vec4(0.0);

    if (distance(vec2(0.0), vpos) < 0.5)
        color = vec4(vcol.r, vcol.g, vcol.b, 1.0) * texture(texture0, uv);

    Frag_Color = color;
}