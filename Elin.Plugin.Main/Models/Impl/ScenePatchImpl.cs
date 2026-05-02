using Elin.Plugin.Main.PluginHelpers;

namespace Elin.Plugin.Main.Models.Impl
{
    public static class ScenePatchImpl
    {
        #region Scene

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

                            if (Plugin.Instance.MessageRecorder.RecordMode == RecordMode.Recoded)
                            {
                                EClass.core.WaitForEndOfFrame(() =>
                                {
                                    using (ModHelper.Message.UseColor(Color.magenta))
                                    {
                                        // 信仰中の文言と同じ行に出力されることを防ぐため改行しておく
                                        // 信仰無しは、わからん、試してない
                                        Msg.NewLine();
                                        ModHelper.Message.OutputLineWithoutContext(ModHelper.Lang.Formatter.FormatPreviousLog(range: ModHelper.Lang.General.PreviousLogRangeStart));
                                    }

                                    Msg.SetColor();
                                    Plugin.Instance.MessageRecorder.Play();

                                    using (ModHelper.Message.UseColor(Color.magenta))
                                    {
                                        ModHelper.Message.OutputLineWithoutContext(ModHelper.Lang.Formatter.FormatPreviousLog(range: ModHelper.Lang.General.PreviousLogRangeEnd));
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
