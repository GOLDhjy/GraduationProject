using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;
public class LastInitSystem : Feature
{
    public LastInitSystem(Contexts contexts) : base("Last Init")
    {
        Add(new AddStateMSystem(contexts));
    }
}

