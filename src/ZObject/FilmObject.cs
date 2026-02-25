using Godot;
using System;
using System.IO;
using System.Collections.Generic;

using OpenPetz;

public partial class FilmObject : Node2D
{
	
	public FilmObject(string _name, string _sprite)
	{
		var zip = new OpenPetz.Parser.Zip("./Resource/Toyz/"+_name+".zip");
		Stream flh = zip.ReadFileStream(_sprite+".flh");
		Stream flm = zip.ReadFileStream(_sprite+".flm");
		var flmLen = zip.GetFileSize(_sprite+".flm");
		
		if (flh == null || flm == null || flmLen == 0)
			return;
		
		GD.Print(_name);
		
		var film = new OpenPetz.Parser.Film();
		film.ReadFileStream(flh, flm, flmLen);
		var img = film.GetImage();
		
		AddChild(img);
		//film.GetImage();
	}
}
