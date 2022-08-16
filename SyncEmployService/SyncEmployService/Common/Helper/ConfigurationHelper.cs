using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncEmployService.Common.Helper
{
    /// <summary>
    /// appsettings配置读取辅助类
    /// </summary>
    public class ConfigurationHelper
    {
        #region 字段定义

        private static readonly string CONFIGFILELOCK = "CONFIGFILELOCK";
        private static IConfigurationRoot configurationRoot = null;

        #endregion

        #region 单例实现

        private static ConfigurationHelper _instance = null;

        private ConfigurationHelper()
        {
            configurationRoot = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();
        }

        public static ConfigurationHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (CONFIGFILELOCK)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConfigurationHelper();
                        }                        
                    }
                }
                return _instance;
            }
        }

        #endregion

        public string GetConfiguration(params string[] sections)
        {
            try
            {

                if (sections.Any())
                {
                    return configurationRoot[string.Join(":", sections)];
                }
            }
            catch (Exception) { }

            return "";
        }
    }
}
