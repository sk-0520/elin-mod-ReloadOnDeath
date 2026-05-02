using Elin.Plugin.Main.Models.Impl;
using HarmonyLib;

namespace Elin.Plugin.Main.Patches
{
    [HarmonyPatch(typeof(Scene))]
    public class ScenePatch
    {
        #region function

        [HarmonyPatch(nameof(Scene.Init), new[] { typeof(Scene.Mode) })]
        [HarmonyPriority(Priority.Last)]
        [HarmonyPostfix]
        public static void InitPostfix(Scene.Mode newMode)
        {
            ScenePatchImpl.InitPostfix(newMode);
        }


        #endregion
    }
}
