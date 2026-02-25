using Godot;
using System;

using System.IO;
using System.IO.Compression;

namespace OpenPetz.Parser
{

	public class Zip 
	{
		public ZipArchive Archive { get; private set; } = null;
		public Zip(string _path)
		{
			try {
				Archive = ZipFile.OpenRead(_path);
			} catch (Exception e) {
				GD.Print("Failed to open ZIP file: "+_path);
			}
		}
		
		public Stream ReadFileStream (string _path) //NOTE: it refers to the file *inside* the archive
		{
			try {
				ZipArchiveEntry entry = Archive.GetEntry(_path);
				return entry.Open();
			} catch (Exception e) {
				GD.Print("Failed to open the entry inside ZIP file: "+_path);
			}
			return null;
		}
		
		public long GetFileSize (string _path) //NOTE: it refers to the file *inside* the archive
		{
			ZipArchiveEntry entry = Archive.GetEntry(_path);
			if (entry == null)
				return 0;
			
			return entry.Length;
		}
	}
}