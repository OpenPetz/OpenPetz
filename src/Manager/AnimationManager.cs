using Godot;
using System.Collections.Generic;
using OpenPetz;

public static class AnimationManager {
	
	private static BallzModel CatBhd { get; set; } = null;
	
	//Methods
	
	private static void LoadCatBhd()
	{
		
	}
	
  public static BallzModel FetchCatBhd()
    {
        if (CatBhd == null)
        {
			List<string> bdtFiles = new List<string>();
			for (int i = 0; i <= 492; i++)
			{
				bdtFiles.Add($"./ptzfiles/cat/CAT{i}.bdt");
			}

			CatBhd = new BallzModel("./ptzfiles/cat/CAT.bhd", bdtFiles);

			if (CatBhd == null)
			{
				GD.Print("Failed to load CAT.bhd");
			}
		}
		
		return CatBhd;
    }

}
