shader_type canvas_item;

@LoadSubTextureShaderComponent

@LoadCircleShaderComponent

uniform vec2 global_center;
uniform vec2 center[128];
uniform int color[128];
uniform float diameter[128];

uniform vec2 atlas_position[128];
uniform vec2 atlas_size[128];

//

uniform sampler2D tex : hint_default_white, filter_nearest, repeat_enable;
uniform sampler2D palette: filter_nearest, repeat_enable;

varying float v_instance;

float random (vec2 st) {
    return fract(sin(dot(st.xy,
                         vec2(12.9898,78.233)))*
        43758.5453) - 0.5;
}

void vertex() {
	VERTEX *= (diameter[INSTANCE_ID] / 2.0) + 1.;
	VERTEX.xy += center[INSTANCE_ID];
	
	v_instance = float(INSTANCE_ID);
}

void fragment() {
	int instnc = int(v_instance);

	vec2 coord = FRAGCOORD.xy - global_center - center[instnc];
	
	//temp fuzz
	coord.x += floor(random(vec2(coord.y + 2.)) * 2.);
	
	//
	
	vec2 atlas_texture_unnormalized = vec2(textureSize(tex, 0));
	vec2 atlas_subtexture_unnormalized = vec2(atlas_size[instnc].x * atlas_texture_unnormalized.x, atlas_size[instnc].y * atlas_texture_unnormalized.y);

	vec2 texUV = fract(coord / atlas_subtexture_unnormalized);
	texUV.y = 1.0 - texUV.y;

	vec2 atlas_texUV = get_subtexture_uv(atlas_position[instnc], atlas_size[instnc], texUV);
	
	float tex_index = texture(tex, atlas_texUV).r;
	//

	float radius = diameter[instnc] / 2.0;
	
	//color[instnc]
	int icolor = color[instnc];  //outline col and col
	
	//
	int outline_icolor = icolor / 256; // >> 8
	int ball_icolor = icolor - outline_icolor * 256; //i mean, what else? when mod(int, int) doesnt work?
	//process again
	outline_icolor = outline_icolor - (icolor / 65535) * 65535;
	
	float outline_color = float(outline_icolor) / 255.;
	float ball_color = float(ball_icolor); 
	
	float mapped_color = color_map(tex_index, ball_color, 1.0);
	
	vec2 outline = vec2(outline_color, 1.) * vec2(circle(coord, radius));
	vec2 ball = vec2(mapped_color, 1.) * vec2(circle(coord, radius - 1.));
	
	vec2 col = mix(outline, ball, ball.y);
	
	vec4 sampled_color = vec4(texture(palette, vec2(col.x, 0.0)).bgr, col.y);
	
	COLOR = sampled_color;
}
