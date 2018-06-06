using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Config
{
    class Program
    {
        static void Main(string[] args)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + @"App.config";
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            ClientSettingsSection sec = (ClientSettingsSection)config.GetSection("userSettings/Section1");
            Console.WriteLine(sec.Settings.Get("Name").Value.ValueXml.InnerText);
            Console.WriteLine(sec.Settings.Get("Age").Value.ValueXml.InnerText);
            sec = (ClientSettingsSection)config.GetSection("userSettings/Section2");
            Console.WriteLine(sec.Settings.Get("Name").Value.ValueXml.InnerText);
            Console.WriteLine(sec.Settings.Get("Age").Value.ValueXml.InnerText);
            Console.ReadKey();
        }
    }
}
