//CANNOT BE USED AS A SHADER ON ITS OWN

vec2 get_subtexture_uv(vec2 _dest, vec2 _size, vec2 _uv) 
{

	vec2 uv = vec2(_uv.x * _size.x, _uv.y * _size.y);
    
	vec2 result = _dest + uv;
	
	return result;
}

float color_map(float _index, float _color, float _transparency)
{
	float index255 = _index * 255.;
	if (_transparency == 0.0)
	{
		return _index;
	} else {
		float color_index = mod(index255, 10.);
		float color_abs = _color - mod(_color, 10.);
		
		return (color_abs + color_index) / 255.;
	}
}