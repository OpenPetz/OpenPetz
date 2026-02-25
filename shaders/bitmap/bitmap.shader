shader_type canvas_item;

uniform vec2 size = vec2(5., 5.);

uniform sampler2D tex : hint_default_white, filter_nearest, repeat_enable;

void vertex() {
	VERTEX = VERTEX * size;
}

void fragment() {
	vec2 coord = fract(FRAGCOORD.xy / vec2(textureSize(tex, 0)));
	
	coord.y = 1. - coord.y;
	
	vec4 col = texture(tex, coord).rgba;
	
	COLOR = col;
} 