﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Djin.Core.ModuleManagement;

namespace Djin.Core.ConfigManagement
{
    class XmlConfigHandler : IConfigHandler
    {
        private string configPath;

        private const string ModuleXPath = "/Djin/Modules/Module";

        private const string ModuleInfoAssemblyPathXPath = "./ModuleInfo/AssemblyPath";
        private const string ModuleInfoModuleNameXPath = "./ModuleInfo/ModuleName";
        private const string ModuleInfoNameSpaceXPath = "./ModuleInfo/NameSpace";
        private const string ModuleInfoClassNameXPath = "./ModuleInfo/ClassName";
        private const string ModuleInfoParametersParameter = "./ModuleInfo/Parameters/Parameter";
        private const string ModuleInfoParametersParameterName = "./Name";
        private const string ModuleInfoParametersParameterValue = "./Value";

        private const string ModuleConfigLoopOnExit = "./ModuleConfig/LoopOnExit";

        public XmlConfigHandler(string configPath)
        {
            this.configPath = configPath;

            if (File.Exists(configPath) == false)
            {
                CreateNewConfig();
            }
        }

        public void CreateNewConfig()
        {
            using (var f = new StreamWriter(File.Create(configPath)))
            {
                f.WriteLine("<?xml version='1.0'?>");
                f.WriteLine("<Djin>");
                f.WriteLine("</Djin>");
            }
        }

        public List<ModuleDescription> GetModuleListFromConfig()
        {
            var modules = new List<ModuleDescription>();
            try
            {
                var nodes = GetModuleXmlNodeListFromConfig();

                foreach (XmlNode node in nodes)
                {
                    var moduleDescription = CreateModuleDescriptionFromXmlNode(node);
                    modules.Add(moduleDescription);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR XmlConfigHandler.GetModuleListFromConfig: " + e.Message);
            }
            return modules;
        }

        private List<XmlNode> GetModuleXmlNodeListFromConfig()
        {
            var nodes = new List<XmlNode>();
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(configPath);
                var moduleNodes= xDoc.SelectNodes(ModuleXPath);
                foreach (XmlNode moduleNode in moduleNodes)
                {
                    nodes.Add(moduleNode);
                }
            }
            catch (XmlException e)
            {
                throw e;
            }

            return nodes;
        }

        private ModuleDescription CreateModuleDescriptionFromXmlNode(XmlNode module)
        {

            var moduleDescription = new ModuleDescription();
            try
            {
                moduleDescription.AssemblyPath = GetModuleAssemblyPath(module);
                moduleDescription.ModuleName = GetModuleName(module);
                moduleDescription.Namespace = GetModuleNameSpace(module);
                moduleDescription.ClassName = GetModuleClassName(module);

                moduleDescription.Parameters = GetModuleParameters(module);

                moduleDescription.Loop = GetModuleLoopOnExit(module);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR XmlConfigHandler.CreateModuleDescriptionFromXmlNode"
                    + e.Message);
                throw e;
            }

            return moduleDescription;
        }

        private string GetModuleAssemblyPath(XmlNode module)
        {
            return module.SelectSingleNode(ModuleInfoAssemblyPathXPath).InnerText;
        }

        private string GetModuleName(XmlNode module)
        {
            return module.SelectSingleNode(ModuleInfoModuleNameXPath).InnerText;
        }

        private string GetModuleNameSpace(XmlNode module)
        {
            return module.SelectSingleNode(ModuleInfoNameSpaceXPath).InnerText;
        }

        private string GetModuleClassName(XmlNode module)
        {
            return module.SelectSingleNode(ModuleInfoClassNameXPath).InnerText;
        }

        private Dictionary<string, object> GetModuleParameters(XmlNode module)
        {
            var parameters = new Dictionary<string, object>();
            var parameterList = module.SelectNodes(ModuleInfoParametersParameter);
            foreach (XmlNode parameter in parameterList)
            {
                var name = parameter.SelectSingleNode(ModuleInfoParametersParameterName).InnerText;
                var value = parameter.SelectSingleNode(ModuleInfoParametersParameterValue).InnerText;
                if (name != null && value != null)
                {
                    parameters.Add(name, value);
                }
            }
            return parameters;
        }

        private bool GetModuleLoopOnExit(XmlNode module)
        {
            var loopOnExit = module.SelectSingleNode(ModuleConfigLoopOnExit).InnerText;
            return loopOnExit.ToUpper().CompareTo("TRUE") == 0 ? true : false;
        }

        public void AddModuleToConfig(ModuleManagement.ModuleDescription md)
        {
            throw new NotImplementedException();
        }

        public void RemoveModuleFromConfig(ModuleManagement.ModuleDescription md)
        {
            throw new NotImplementedException();
        }
    }
}
