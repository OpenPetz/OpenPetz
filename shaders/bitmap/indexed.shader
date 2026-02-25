shader_type canvas_item;

uniform vec2 size = vec2(5., 5.);
uniform vec2 pos = vec2(5., 5.);

uniform sampler2D tex : hint_default_white, filter_nearest, repeat_enable;
uniform sampler2D palette : hint_default_white, filter_nearest, repeat_enable;

void vertex() {
	VERTEX = VERTEX * size;
}

void fragment() {
	vec2 coord = fract((FRAGCOORD.xy - pos) / vec2(textureSize(tex, 0)));
	
	coord.y = 1. - coord.y;
	
	float index = texture(tex, coord).r;
	
	vec4 col = vec4(0.0);
	
	if (index != 253./255.)
		col = vec4(texture(palette, vec2(index, 0.0)).bgr, 1.);
	
	COLOR = col;
} 