using Elin.Plugin.Main.PluginHelpers;
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
            switch (newMode)
            {
                case Scene.Mode.Title:
                    Plugin.Instance.MessageRecorder?.StopRecording();
                    break;

                case Scene.Mode.None:
                    break;

                case Scene.Mode.StartGame:
                    {
                        if (Plugin.Instance.MessageRecorder is not null)
                        {
                            ModHelper.WriteDev($"{nameof(Plugin.Instance.MessageRecorder.RecordMode)}: {Plugin.Instance.MessageRecorder.RecordMode}");

                            if (Plugin.Instance.MessageRecorder.RecordMode == Models.RecordMode.Recoded)
                            {
                                EClass.core.WaitForEndOfFrame(() =>
                                {
                                    var currentColor = Msg.currentColor;
                                    try
                                    {
                                        Msg.SetColor(Color.magenta);
                                        Msg.SayRaw(ModHelper.Lang.Formatter.FormatPreviousLog(range: ModHelper.Lang.General.PreviousLogRangeStart));
                                        Msg.NewLine();

                                        Msg.SetColor(currentColor);
                                        Plugin.Instance.MessageRecorder.Play();

                                        Msg.SetColor(Color.magenta);
                                        Msg.SayRaw(ModHelper.Lang.Formatter.FormatPreviousLog(range: ModHelper.Lang.General.PreviousLogRangeEnd));
                                        Msg.NewLine();
                                    }
                                    finally
                                    {
                                        Msg.SetColor(currentColor);
                                    }

                                    Plugin.Instance.MessageRecorder.Clear();
                                    Plugin.Instance.MessageRecorder.StartRecording();
                                });
                            }
                            else
                            {
                                // どういう経路でここに来たのか不明なため一旦停止, 後続でクリアと開始を行ってるので不要だとは思うけど
                                Plugin.Instance.MessageRecorder.StopRecording();

                                Plugin.Instance.MessageRecorder.Clear();
                                Plugin.Instance.MessageRecorder.StartRecording();
                            }
                        }
                    }
                    break;

                case Scene.Mode.Zone:
                    break;
            }
        }


        #endregion
    }
}
