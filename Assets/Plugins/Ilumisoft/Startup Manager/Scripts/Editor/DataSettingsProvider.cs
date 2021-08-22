namespace Ilumisoft.Plugins.StartupManager.Editor
{
    using UnityEditor;

    class DataSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider Get()
        {
            var settingsObj = DataProvider.GetData();
            var provider = AssetSettingsProvider.CreateProviderFromObject("Project/Startup Manager", settingsObj);

            provider.keywords = SettingsProvider.GetSearchKeywordsFromSerializedObject(new SerializedObject(settingsObj));
            return provider;
        }
    }
}