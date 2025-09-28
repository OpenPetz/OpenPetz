shader_type canvas_item;

@LoadSubTextureShaderComponent

uniform vec2 ball_coords[2];
uniform float ball_diameters[2];
uniform float angle_to;

uniform vec2 atlas_position = vec2(0.0, 0.0);
uniform vec2 atlas_size = vec2(1.0, 1.0);

uniform float left_color = -1.;
uniform float right_color = -1.;

uniform float color_index = 0;
uniform float transparency = 0;

uniform sampler2D tex : hint_default_white, filter_nearest, repeat_enable;
uniform sampler2D palette: filter_nearest, repeat_enable;

varying vec2 v_coords;
varying vec2 v_uv;

varying float v_index;

vec2 rotate2d(float angle, vec2 coord){
    vec2 rotatable = vec2(1.0);
	rotatable.y = coord.y * cos(angle) + coord.x * sin(angle);
	rotatable.x = coord.x * cos(angle) - coord.y * sin(angle);

	return rotatable;
}

float random(float x) {
    return fract(sin(dot(vec2(x,x),
                         vec2(12.9898,78.233)))*
        43758.5453123) - 0.5;
}

void vertex () {
	int index = int(floor(VERTEX.x));
	v_index = floor(VERTEX.x);

	v_uv = vec2(VERTEX.x , VERTEX.y * 10.);

	v_coords = vec2(ball_diameters[index]/2., ball_diameters[index]/2.*VERTEX.y);

	VERTEX.x = ball_coords[index].x + -1.*VERTEX.y * sin(angle_to) * (ball_diameters[index] / 2.);
	VERTEX.y = ball_coords[index].y + VERTEX.y * cos(angle_to) * (ball_diameters[index] / 2.);
}

void fragment() {
	int index = int(v_index);

	vec2 coord = FRAGCOORD.xy - ball_coords[0];

	vec2 atlas_texture_unnormalized = vec2(textureSize(tex, 0));
	vec2 atlas_subtexture_unnormalized = vec2(atlas_size.x * atlas_texture_unnormalized.x, atlas_size.y * atlas_texture_unnormalized.y);

	vec2 texUV = fract(coord / atlas_subtexture_unnormalized);
	texUV.y = 1.0 - texUV.y;

	vec2 atlas_texUV = get_subtexture_uv(atlas_position, atlas_size, texUV);

	float tex_index = texture(tex, atlas_texUV).r;

	float mapped_color = color_map(tex_index, color_index, transparency);
	
	vec4 color = vec4(texture(palette, vec2(mapped_color, 0.0)).bgr, 1.0);

	vec3 outcol = vec3(0.);
	bool has_outline;

	if (v_coords.y < 0.){
		outcol = texture(palette, vec2(left_color / 255., 0.0)).bgr;
		has_outline = left_color != -1./255.;
	}
	else {
		outcol = texture(palette, vec2(right_color / 255., 0.0)).bgr;
		has_outline = right_color != -1./255.;
	}

	bool is_outline = (abs(v_coords.y) + 1.) < v_coords.x != true;

	color.rgb = mix(color.rgb, outcol.rgb, (is_outline && has_outline) ? 1. : 0.);

	COLOR = color;
}