using Elin.Plugin.Generated;

namespace Elin.Plugin.Main.Models.Settings
{
    [GeneratePluginConfig]
    public partial class Setting
    {
        #region property

        [RangePluginConfig(0, 300)]
        public virtual int RecordCount { get; set; } = 100;

        #endregion
    }
}
