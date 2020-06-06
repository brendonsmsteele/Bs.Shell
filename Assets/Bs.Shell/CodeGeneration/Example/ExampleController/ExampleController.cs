using System;

namespace Bs.Shell.Controllers
{
    public class ExampleController : SceneController<ExampleController.Model>
    {
        [Serializable]
        public class Model : Shell.Model
        {
            public Model()
            {
            }
        }

        public override void Refresh()
        {
        }

        /// <summary>
        /// Increase the delay on dispose to give time to animate out.
        /// </summary>
        /// <returns></returns>
        public override ManualYieldInstruction Dispose()
        {
            return GetTimedYield(1f);
            //  return base.Dispose();
        }

        protected override void OnEnable()
        {
        }

        protected override void OnDisable()
        {
        }
    }
}
