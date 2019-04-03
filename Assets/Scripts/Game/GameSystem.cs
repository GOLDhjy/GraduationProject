using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class GameSystem : Feature
{
    public GameSystem(Contexts contexts) : base("Game System")
    {
        Add(new CameraSystem(contexts));
    }
}
