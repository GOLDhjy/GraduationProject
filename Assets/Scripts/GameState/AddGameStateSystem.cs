using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;

public class AddGameStateSystem : Feature
{

    public AddGameStateSystem( Contexts contexts) : base("Game State")
    {
        Add(new GameStateSystem(contexts));
    }
}