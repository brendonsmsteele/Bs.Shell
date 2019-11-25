using Bs.Shell.Controllers;
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
        public string ControllerName { get { return typeof(T).Name; } }
        public abstract void Render();

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

    
    [Serializable]
    public class IncludeMainMenu : IncludeModel<MainMenuController.Model>
    {
        public string Title;
        public string Body;

        public override void Render()
        {
            Title = RenderText(Title, nameof(Title));
            Body = RenderText(Body, nameof(Body));
        }
    }

    [Serializable]
    public class IncludeBG : IncludeModel<BGController.Model>
    {
        public string PathToBG;

        public override void Render()
        {
            PathToBG = RenderText(PathToBG, nameof(PathToBG));
        }
    }

    /*

    [Serializable]
    public class IncludeGame : IncludeModel<GameController>
    {
        public bool IsTutorial;

        public override void Render()
        {
            IsTutorial = RenderBool(IsTutorial, nameof(IsTutorial));
        }
    }

    [Serializable]
    public class IncludeArtGallery : IncludeModel<ArtGalleryController>
    {
        public bool IsTutorial;

        public override void Render()
        {
            IsTutorial = RenderBool(IsTutorial, nameof(IsTutorial));
        }
    }

    [Serializable]
    public class IncludeResults : IncludeModel<ResultsController>
    {
        public bool IsTutorial;

        public override void Render()
        {
            IsTutorial = RenderBool(IsTutorial, nameof(IsTutorial));
        }
    }
    */
}
