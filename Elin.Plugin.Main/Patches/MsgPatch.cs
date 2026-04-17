using Elin.Plugin.Main.Models.Impl;
using HarmonyLib;
using System;

namespace Elin.Plugin.Main.Patches
{
    [HarmonyPatch(typeof(Msg))]
    public class MsgPatch
    {
        #region function

        [HarmonyPatch(nameof(Msg.SetColor), new Type[0])]
        [HarmonyPostfix]
        public static void SetColorPostfix()
        {
            // [ELIN:Msg.SetColor]
            // -> currentColor = colors.Default
            MsgImpl.SetColorPostfix(Plugin.Instance.MessageRecorder!, Msg.colors.Default);
        }

        [HarmonyPatch(nameof(Msg.SetColor), new[] { typeof(Color) })]
        [HarmonyPostfix]
        public static void SetColorPostfix(Color color)
        {
            MsgImpl.SetColorPostfix(Plugin.Instance.MessageRecorder!, color);
        }

        [HarmonyPatch(nameof(Msg.SetColor), new[] { typeof(string) })]
        [HarmonyPostfix]
        public static void SetColor(string id)
        {
            MsgImpl.SetColorPostfix(Plugin.Instance.MessageRecorder!, id);
        }

        [HarmonyPatch(nameof(Msg.SayRaw), new[] { typeof(string) })]
        [HarmonyPostfix]
        public static void SayRawPostfix(string text)
        {
            MsgImpl.SayRawPostfix(Plugin.Instance.MessageRecorder!, text);
        }

        [HarmonyPatch(nameof(Msg.NewLine))]
        [HarmonyPostfix]
        public static void NewLinePostfix()
        {
            MsgImpl.NewLinePostfix(Plugin.Instance.MessageRecorder!);
        }

        #endregion
    }
}
