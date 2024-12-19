using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;
using Kaitai;

public class CatScpController 
{
	/*Scp scp = Scp.FromFile("\"C:\\Users\\petz_\\Documents\\OpenPetz\\pet\\data\\cat.scp\"");

	Random random = new Random();

	public void ProcessScp(Pet pet)
	{
		if (pet.actionStack.Count > 0) { 

			Scp.Action curAction = pet.actionStack.Pop();

			//pick a random script
			Scp.Script script = curAction.Scripts[random.Next(0,curAction.Scripts.Count-1)];

			List<Scp.Verbs> commands = new List<Scp.Verbs>(script.Command);

			while (commands.Count > 0)
			{
				Scp.Verbs curCommand = commands.First();
				commands.RemoveAt(0);
				switch (curCommand)
				{
					case Scp.Verbs.Lookatsprite1:
						// do some stuff
						break;
					//add more cases when we actually have things to do
				}
			}

			pet.lastScpAction = curAction;
			pet.currentScpState = curAction.EndState;

			//update pet pos
			//emit singal        
		}   
		
	} */
}


