using System;
using UnityEngine;

namespace Bs.Shell
{
    public class ControllerData : ScriptableObject
    {
        public event EventHandler RequestBind;

        public void Bind()
        {
            RequestBind?.Invoke(this, EventArgs.Empty);
        }
    }
}