using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class FunctionBuildingScenes {

    public enum TypeFunction {
        Linear,
        Exponential,
        Parabola,
        Sine
    }

    public TypeFunction ConfigSelection;

}
