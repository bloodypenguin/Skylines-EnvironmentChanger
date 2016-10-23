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
            throw new Exception("Unsupported metadata!");
        }

        public static void SetMetadataTheme(MetaData metadata, string theme)
        {
            var mapMetadata = metadata as MapMetaData;
            if (mapMetadata != null)
            {
                mapMetadata.mapThemeRef = theme;
                return;
            }
            var saveGameMetadata = metadata as SaveGameMetaData;
            if (saveGameMetadata != null)
            {
                saveGameMetadata.mapThemeRef = theme;
                return;
            }
            var mapThemeMetadata = metadata as MapThemeMetaData;
            if (mapThemeMetadata != null)
            {
                mapThemeMetadata.mapThemeRef = theme;
                return;
            }
            throw new Exception("Unsupported metadata!");
        }
    }
}