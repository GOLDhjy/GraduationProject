using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InputSystem : Feature
{
    public InputSystem(Contexts contexts) : base("Input System")
    {
        Add(new EmitInputSystem(contexts));
    }
}

