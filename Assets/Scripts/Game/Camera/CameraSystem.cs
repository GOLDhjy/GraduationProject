using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;

public class CameraSystem : Feature
{
    public CameraSystem(Contexts contexts) : base("Camera System")
    {
        Add(new MainCameraSystem(contexts));
    }
}
