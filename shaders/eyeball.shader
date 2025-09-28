shader_type canvas_item;

@LoadSubTextureShaderComponent

@LoadCircleShaderComponent

uniform vec2 center;
uniform float diameter;
uniform vec3 rotation;

uniform sampler2D palette: filter_nearest, repeat_enable;

vec3 rotate3d(vec3 angle, vec3 coord){
    vec3 v1 = vec3(0.0);
    vec3 v2 = vec3(0.0);
    vec3 v3 = vec3(0.0);

    //Y
	v1.z = coord.z * cos(angle.y) - coord.x * sin(angle.y);
	v1.x = coord.x * cos(angle.y) + coord.z * sin(angle.y);

	//Z
	v2.y = coord.y * cos(angle.z) - v1.x * sin(angle.z);
	v2.x = v1.x * cos(angle.z) + coord.y * sin(angle.z);

	//X
	v3.z = v1.z * cos(angle.x) - v2.y * sin(angle.x);
	v3.y = v2.y * cos(angle.x) + v1.z * sin(angle.x);

	vec3 rotatable = vec3(v2.x, v3.y, v3.z);

	return rotatable;
}

void vertex() {
	VERTEX *= (diameter / 2.0);
}

void fragment() {
	vec2 coord = FRAGCOORD.xy - center;
	
	float radius = diameter / 2.0;
	
	vec3 iris_coord = floor(rotate3d(rotation, vec3(0., 0., radius)));
	
	vec4 outline = vec4(circle(coord, radius));
	
	outline *= vec4(texture(palette, vec2(39. / 255., 0.0)).bgr, 1.0);
	
	vec4 ball = vec4(circle(coord, radius - 1.));
	
	float is_ball = ball.a * (coord.y + radius / 2. < 0. ? 0. : 1.);
	
	ball *= vec4(texture(palette, vec2(200. / 255., 0.0)).bgr, 1.0);
	
	vec4 color = mix(outline, ball, is_ball);
	
	vec4 iris = vec4(circle(coord + iris_coord.xy, radius / 2.));
	
	iris.rgb *= texture(palette, vec2(39. / 255., 0.0)).bgr;
	
	iris.a *= outline.a;
	
	color = mix(color, iris, iris.a);
	
	COLOR = color;
}
