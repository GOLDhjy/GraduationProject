using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas.CodeGeneration.Attributes;

[GameConfig,Unique,ComponentName("GameConfig")]
public interface GameConfig
{
    int Big { get; set; }
}
