﻿using System.Text.Json;

namespace CTI.Metron.ImagePreProcessor
{
    public class ConfigurationManager
    {
        public string SourceDirectory { get; private set; } = "";
        public string DestinationDirectory { get; private set; } = "";
        public int ScaleWidth { get; private set; } 
        public int ScaleHeight { get; private set; }
        public int Threshold { get; private set; }
        public ConfigurationManager(string configFilePath = "AppSettings.json")
        {
            LoadConfiguration(configFilePath);
        }

        private void LoadConfiguration(string filePath)
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                var config = JsonSerializer.Deserialize<Config>(jsonString);

                if (config != null)
                {
                    SourceDirectory = config.SourceDirectory;
                    DestinationDirectory = config.DestinationDirectory;
                    ScaleHeight = config.ImageScale.Height;
                    ScaleWidth = config.ImageScale.Width;
                    Threshold = config.Threshold;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading configuration: {ex.Message}");
                // Consider exiting the application or providing default values
            }
        }

        private class Config
        {
            public string SourceDirectory { get; set; } = "";
            public string DestinationDirectory { get; set; } = "";
            public Scale ImageScale { get; set; } = new Scale();
            public int Threshold { get; set; }             
        }
        private class Scale
        {
            public int Width { get; set; } = 1;
            public int Height { get; set; } = 1;
        }
    }
}
