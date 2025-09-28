shader_type canvas_item;

@LoadSubTextureShaderComponent

@LoadCircleShaderComponent

uniform vec2 center;
uniform float diameter;
uniform float outline_width;

uniform float outline_color;

uniform vec2 atlas_position = vec2(0.0, 0.0);
uniform vec2 atlas_size = vec2(1.0, 1.0);

uniform float color_index = 0;
uniform float transparency = 0;

uniform sampler2D tex : hint_default_white, filter_nearest, repeat_enable;
uniform sampler2D palette: filter_nearest, repeat_enable;

uniform float fuzz = 0.0;

float random (vec2 st) {
    return fract(sin(dot(st.xy,
                         vec2(12.9898,78.233)))*
        43758.5453) - 0.5;
}

void vertex() {
	VERTEX *= (diameter / 2.0) + fuzz + 1.;
	//VERTEX.x += fuzz;
}

void fragment() {
	vec2 coord = FRAGCOORD.xy - center;

	vec2 atlas_texture_unnormalized = vec2(textureSize(tex, 0));
	vec2 atlas_subtexture_unnormalized = vec2(atlas_size.x * atlas_texture_unnormalized.x, atlas_size.y * atlas_texture_unnormalized.y);

	vec2 texUV = fract(coord / atlas_subtexture_unnormalized);
	texUV.y = 1.0 - texUV.y;

	vec2 atlas_texUV = get_subtexture_uv(atlas_position, atlas_size, texUV);

	float radius = diameter / 2.0;
	
	coord.x += random(vec2(coord.y + fuzz)) * fuzz;
	
	float tex_index = texture(tex, atlas_texUV).r;
	
	vec4 outline = vec4(circle(coord, radius));
	
	outline *= vec4(texture(palette, vec2(outline_color / 255., 0.0)).bgr, 1.0);
	
	vec4 ball = vec4(0.0);
	
	if (outline_width == -2.0)
	{
		coord.x += 1.0;
		ball = vec4(circle(coord, radius));
	} else if (outline_width == -1.0)
	{
		ball = vec4(circle(coord, radius));
	} else if (outline_width == 0.0)
	{
		coord.x -= 1.0;
		ball = vec4(circle(coord, radius));
	} else if (outline_width == 1.0)
	{
		float err = fract(radius);
		coord.x = abs(coord.x - err) + 1.0;
		ball = vec4(circle(coord, radius));
	} else {
		ball = vec4(circle(coord, radius - outline_width + 1.0));
	}
	
	float is_ball = ball.a;
	
	float mapped_color = color_map(tex_index, color_index, transparency);
	
	ball *= vec4(texture(palette, vec2(mapped_color, 0.0)).bgr, 1.0);
	
	vec4 color = mix(outline, ball, is_ball);
	
	COLOR = color;
}
