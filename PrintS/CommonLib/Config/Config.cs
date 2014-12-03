using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// .net System.Configuration
using System.Configuration;

using System.Xml;

namespace CommonLib.Config
{
    public class Config
    {
        /// <summary>
        /// 配置文件名
        /// </summary>
        static string configPath = "Conf.exe.config";

        static Configuration _config;
        /// <summary>
        /// 配置项集合
        /// </summary>
        static public Configuration config
        {
            get
            {
                if (_config == null)
                {
                    ExeConfigurationFileMap file = new ExeConfigurationFileMap();
                    file.ExeConfigFilename = configPath;
                    _config = ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);
                }
                return _config;
            }
        }

        /// <summary>
        /// 获取.config文件中的AppSettings键值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        static public string getConfig(string key)
        {
            return config.AppSettings.Settings[key].Value;
        }

        /// <summary>
        /// 设置.config文件中的APPSettings键值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        static public void setConfig(string key, object value)
        {
            string val = value.ToString();
            config.AppSettings.Settings[key].Value = val;
            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
