﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Project.CodeGeneratorV2.EnumExt;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using Encoding = System.Text.Encoding;

namespace Project.CodeGeneratorV2.Utils
{
    public class TemplateHelper
    {
        public static string ReadTemplate(string folder, string fileName, string encoding = "GBK")
        {
            fileName = Path.Combine(folder, fileName);
            return FileHelper.Read(fileName, Encoding.GetEncoding(encoding));
        }

        public static string ReadTemplate(string fileName, string encoding = "GBK")
        {
            return ReadTemplate("Template", fileName + ".cshtml", encoding);
        }

        public static string Parse(TemplateKey templateKey, string template, object model, DynamicViewBag viewbag)
        {
            var result = Engine.Razor.RunCompile(template, templateKey.ToString("G"), null, model, (DynamicViewBag)viewbag);
            return result;
        }
        /// <summary>
        /// 删除提示信息：
        /// RazorEngine: We can't cleanup temp files if you use RazorEngine on the default Appdomain.
        /// </summary>
        public static void Init()
        {
            //参考地址
            //https://github.com/Antaris/RazorEngine/issues/244
            //
            var config = new TemplateServiceConfiguration();
            config.DisableTempFileLocking = true; // loads the files in-memory (gives the templates full-trust permissions)
            config.CachingProvider = new DefaultCachingProvider(t => { }); //disables the warnings
            // Use the config
            Engine.Razor = RazorEngineService.Create(config); // new API
            Razor.SetTemplateService(new TemplateService(config)); // legacy API
        }

        public static string Clean(string generator, Regex re)
        {
            return re.Replace(generator, "");
        }
    }
}