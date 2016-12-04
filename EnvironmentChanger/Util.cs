using System;

namespace EnvironmentChanger
{
    public static class Util
    {
        public static string GetMetadataEnvironment(MetaData metadata)
        {
            var mapMetadata = metadata as MapMetaData;
            if (mapMetadata != null)
            {
                return mapMetadata.environment;
            }
            var saveGameMetadata = metadata as SaveGameMetaData;
            if (saveGameMetadata != null)
            {
                return saveGameMetadata.environment;
            }
            var mapThemeMetadata = metadata as MapThemeMetaData;
            if (mapThemeMetadata != null)
            {
                return mapThemeMetadata.environment;
            }
            var scenarioMetadata = metadata as ScenarioMetaData;
            if (scenarioMetadata != null)
            {
                return scenarioMetadata.environment;
            }
            throw new Exception("Unsupported metadata!");
        }

        public static string GetMetadataTheme(MetaData metadata)
        {
            var mapMetadata = metadata as MapMetaData;
            if (mapMetadata != null)
            {
                return mapMetadata.mapThemeRef;
            }
            var saveGameMetadata = metadata as SaveGameMetaData;
            if (saveGameMetadata != null)
            {
                return saveGameMetadata.mapThemeRef;
            }
            var mapThemeMetadata = metadata as MapThemeMetaData;
            if (mapThemeMetadata != null)
            {
                return mapThemeMetadata.mapThemeRef;
            }
            var scenarioMetadata = metadata as ScenarioMetaData;
            if (scenarioMetadata != null)
            {
                return scenarioMetadata.mapThemeRef;
            }
            throw new Exception("Unsupported metadata!");
        }
    }
}