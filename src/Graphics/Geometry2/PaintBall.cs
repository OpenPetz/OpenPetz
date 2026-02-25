using Godot;
using System;
using System.Collections.Generic;

/* A PaintBall object cannot be "directly" rendered.
 * Instead, it contains information needed to render a paintball. It needs to be passed into a PaintBallGroup object in order to be rendered.
 */

public struct PaintBallParams {
	public int Fuzz {get; set;} = 0;
	public int Diameter {get; set;} = 1;
	public int ColorIndex {get; set;} = 0;
	public int OutlineType {get; set;} = 0;
	public int OutlineColor {get; set;} = 0;
	public int TextureIndex {get; set;} = -1;
	public Vector3 Direction {get; set;} = new Vector3(0f, 0f, 0f);
	public PaintBallParams(){}
}
 
public partial class PaintBall
{
	public PaintBallParams Info {get; private set;} = new PaintBallParams();
	
	public PaintBall()
	{
		
	}
	
	public PaintBall(PaintBallParams _params)
	{
		_params.Direction = _params.Direction.Normalized();
		Info = _params;
	}
}