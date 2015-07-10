﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.ConnectTheDots.Common;

namespace WorkerHost
{
    using System.Configuration;

    public class AMQPConfig
    {
        public string AMQPSAddress;
        public string EventHubName;
        public string EventHubMessageSubject;
        public string EventHubDeviceId;
        public string EventHubDeviceDisplayName;
    };

    public class Loader
    {
        public static IList<XMLApiConfigItem> GetAPIConfigItems()
        {
            var result = new List<XMLApiConfigItem>();

            XMLApiListConfigSection config = ConfigurationManager.GetSection("XMLApiListConfig") as XMLApiListConfigSection;

            if (config != null)
            {
                result.AddRange(config.Instances.Cast<XMLApiConfigItem>());
            }

            return result;
        }

        internal static AMQPConfig GetAMQPConfig(string configSection, ILogger logger)
        {
            AMQPConfig configData = null;
            try
            {
                AMQPServiceConfigSection section =
                    ConfigurationManager.GetSection(configSection) as AMQPServiceConfigSection;

                if (section != null)
                {
                    configData = new AMQPConfig
                    {
                        AMQPSAddress = section.AMQPSAddress,
                        EventHubName = section.EventHubName,
                        EventHubMessageSubject = section.EventHubMessageSubject,
                        EventHubDeviceId = section.EventHubDeviceId,
                        EventHubDeviceDisplayName = section.EventHubDeviceDisplayName
                    };
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
            }

            return configData;
        }
    }

    internal class AMQPServiceConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("AMQPSAddress", DefaultValue = "AMQPSAddress", IsRequired = true)]
        public string AMQPSAddress
        {
            get
            {
                return (string)this["AMQPSAddress"];
            }
            set
            {
                this["AMQPSAddress"] = value;
            }
        }

        [ConfigurationProperty("EventHubName", DefaultValue = "EventHubName", IsRequired = true)]
        public string EventHubName
        {
            get
            {
                return (string)this["EventHubName"];
            }
            set
            {
                this["EventHubName"] = value;
            }
        }

        [ConfigurationProperty("EventHubMessageSubject", DefaultValue = "EventHubMessageSubject", IsRequired = true)]
        public string EventHubMessageSubject
        {
            get
            {
                return (string)this["EventHubMessageSubject"];
            }
            set
            {
                this["EventHubMessageSubject"] = value;
            }
        }

        [ConfigurationProperty("EventHubDeviceId", DefaultValue = "EventHubDeviceId", IsRequired = true)]
        public string EventHubDeviceId
        {
            get
            {
                return (string)this["EventHubDeviceId"];
            }
            set
            {
                this["EventHubDeviceId"] = value;
            }
        }

        [ConfigurationProperty("EventHubDeviceDisplayName", DefaultValue = "EventHubDeviceDisplayName", IsRequired = true)]
        public string EventHubDeviceDisplayName
        {
            get
            {
                return (string)this["EventHubDeviceDisplayName"];
            }
            set
            {
                this["EventHubDeviceDisplayName"] = value;
            }
        }
    }

    public class XMLApiListConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public APIListConfigInstanceCollection Instances
        {
            get { return (APIListConfigInstanceCollection)this[""]; }
            set { this[""] = value; }
        }
    }

    public class APIListConfigInstanceCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new XMLApiConfigItem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((XMLApiConfigItem)element).APIAddress;
        }
    }

    public class XMLApiConfigItem : ConfigurationElement
    {
        [ConfigurationProperty("APIAddress", IsKey = true, IsRequired = true)]
        public string APIAddress
        {
            get
            {
                return (string)base["APIAddress"];
            }
        }

        [ConfigurationProperty("DefinitionAddress", IsRequired = true)]
        public string DefinitionAddress
        {
            get
            {
                return (string)base["DefinitionAddress"];
            }
        }

        [ConfigurationProperty("DataXMLRootNodeName", IsRequired = true)]
        public string DataXMLRootNodeName
        {
            get
            {
                return (string)base["DataXMLRootNodeName"];
            }
        }

        [ConfigurationProperty("DefinitionXMLRootNodeName", IsRequired = true)]
        public string DefinitionXMLRootNodeName
        {
            get
            {
                return (string)base["DefinitionXMLRootNodeName"];
            }
        }

        [ConfigurationProperty("IntervalSecs", IsRequired = true)]
        public int IntervalSecs
        {
            get
            {
                return (int)base["IntervalSecs"];
            }
        }
    }
}