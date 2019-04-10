using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;

public class PlayerSystem : Feature
{
    public PlayerSystem(Contexts  contexts) : base("Player Systems")
    {
        Add(new AddPlayerSystem(contexts));
        Add(new PlayerMoveSystem(contexts));
        Add(new PlayerAudioSystem(contexts));
    }
}
