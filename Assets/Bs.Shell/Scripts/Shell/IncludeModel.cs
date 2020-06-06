using System;
using UnityEngine;

namespace Bs.Shell.Navigation
{
    [Serializable]
    public abstract class IncludeModel<T>
        where T : Model
    {
        public bool Include;
        public T Model;
        public string ControllerName { get { return typeof(T).FullName; } }

        public string RenderText(string text, string label)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label, GUILayout.Width(100));
            text = GUILayout.TextField(text);
            GUILayout.EndHorizontal();
            return text;
        }

        public bool RenderBool(bool toggle, string label)
        {
            return GUILayout.Toggle(toggle, label);
        }
    }
}
