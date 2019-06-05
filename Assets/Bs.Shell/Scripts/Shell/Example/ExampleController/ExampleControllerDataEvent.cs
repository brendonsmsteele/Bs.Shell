﻿using Bs.Shell.EditorVariables;
using UnityEngine;

namespace Bs.Shell.Example
{
    [CreateAssetMenu(fileName = nameof(ExampleControllerDataEvent), menuName = "Bs.Shell/Controllers/" + nameof(ExampleControllerDataEvent))]
    public class ExampleControllerDataEvent : ControllerDataEvent<ExampleControllerData>, IControllerDataEvent { }
}