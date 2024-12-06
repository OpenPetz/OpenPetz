shader_type canvas_item;

uniform sampler2D tex : hint_default_white, filter_nearest, repeat_enable;
uniform vec2 max_uvs;
varying float max_y;
uniform vec2 center;
uniform float outline1_enabled = 1.0;
uniform float outline2_enabled = 1.0;
uniform float color_index;
uniform float transparent_color_index;
uniform vec3 r_outline_color;
uniform vec3 l_outline_color;
uniform float fuzz = 0.0;
uniform vec2 vec_to_upright;
uniform sampler2D palette: filter_nearest;

vec4 get_shifted_color(float palette_index) {
	float modded_color = color_index - mod(color_index, 10);
	float amount_to_shift_index = mod(palette_index * 255.0, 10);
	float new_palette_index = modded_color + amount_to_shift_index;
	vec4 new_color = texture(palette, vec2(new_palette_index / 255.0, 0.0));
	return new_color;
}

void vertex() {
	 if(VERTEX.x > 0.0) {
               max_y = max_uvs.x;
       } else {
               max_y = max_uvs.y;
       }

}

float random(float x) {
    return fract(sin(dot(vec2(x,x),
                         vec2(12.9898,78.233)))*
        43758.5453123) - 0.5;
}

float bucket(float y) {
	return trunc(y / 2.0);
}

void fragment() {
	vec2 mod_uv = UV;
	
	mod_uv -= random(bucket(FRAGCOORD.y - center.y)) * vec_to_upright * fuzz;
	float min_y = max_y / 4.0;
	float end_y = max_y - min_y;
	float in_line = 1.0 - step(mod_uv.y, min_y);
	in_line = in_line * step(mod_uv.y, end_y);
	float outline = step(mod_uv.y, min_y + 1.0) * outline1_enabled;
	vec4 outline_r = vec4(r_outline_color * vec3(outline), 1.0) * in_line;
	float outline2 = (1.0 - step(mod_uv.y, end_y - 1.0)) * outline2_enabled;
	vec4 outline_l = vec4(l_outline_color * outline2, 1.0) * in_line;
	vec2 uv = ((FRAGCOORD.xy - center) / vec2(textureSize(tex, 0)));
	outline = outline + outline2;
	float inside = (1.0 - outline) * in_line;
	vec4 texcolor = texture(tex, uv);
	texcolor = get_shifted_color(texcolor.r);
	COLOR = outline_r + outline_l + (inside * texcolor);
}