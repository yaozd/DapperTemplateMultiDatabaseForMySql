using System;
using System.Collections.Generic;
using Project.CodeGeneratorV2.DBSchema;
using Project.CodeGeneratorV2.EnumExt;
using Project.CodeGeneratorV2.Utils;
using RazorEngine.Templating;

namespace Project.CodeGeneratorV2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TemplateHelper.Init();
            //
            var dbSchema = DBSchemaFactory.GetDBSchema();
            var tableList = dbSchema.GetTablesList();
            //生成Model================================================
            var templateFileName = "ModelAuto";
            var savePath = @"D:\._1\MYSQL\Template.Model";
            var modelNamespaceVal = "XS.Insure.ShortUrl.Model";
            GenerateTemplate_Model(templateFileName, tableList, dbSchema, modelNamespaceVal, savePath);
            //return;
            //生成Dao================================================
            templateFileName = "DaoAuto";
            savePath = @"D:\._1\MYSQL\Template.Dao";
            var daoNamespaceVal = "XS.Insure.ShortUrl.Dao";
            var dbNameVal = "Test1";//EnumDbName中数据库的名称
            GenerateTemplate_Dao(templateFileName, tableList, dbSchema, daoNamespaceVal, modelNamespaceVal, dbNameVal, savePath);
            //生成ViewModel================================================
            templateFileName = "ViewModelAuto";
            savePath = @"D:\._1\MYSQL\Template.ViewModel";
            modelNamespaceVal = "XS.Insure.ShortUrl.Model";
            GenerateTemplate_ViewModel(templateFileName, tableList, dbSchema, modelNamespaceVal, savePath);
            //return;
            //生成ViewModel================================================
            templateFileName = "ViewHelperAuto";
            savePath = @"D:\._1\MYSQL\Template.ViewHelper";
            modelNamespaceVal = "XS.Insure.ShortUrl.Model";
            GenerateTemplate_ViewHelper(templateFileName, tableList, dbSchema, modelNamespaceVal, savePath);
            //return;
            return;
            //生成Service================================================
            //templateFileName = "ModelAuto";
            //savePath = @"D:\._1\Service";
            //_namespaceVal = "Project.CodeGeneratorV2.Model";
            //GenerateTemplate_Model(templateFileName, tableList, dbSchema, _namespaceVal, savePath);
            //Pause();
        }

        private static void GenerateTemplate_Model(string templateFileName, List<string> tableList, IDBSchema dbSchema, string modelNamespaceVal, string savePath)
        {
            DisplayTemplateName(templateFileName);
            var templateText = TemplateHelper.ReadTemplate(templateFileName);
            foreach (var tableName in tableList)
            {
                var table = dbSchema.GetTableMetadata(tableName);
                if (table.PKs.Count == 0) throw new Exception(string.Format("表{0}:没有设置主键！", tableName));
                Display(tableName, table);
                dynamic viewbag = new DynamicViewBag();
                viewbag.classnameVal = tableName;
                viewbag.namespaceVal = modelNamespaceVal;
                var outputText = TemplateHelper.Parse(TemplateKey.Model, templateText, table, viewbag);
                outputText = TemplateHelper.Clean(outputText, RegexPub.H1());
                FileHelper.Save(string.Format(@"{0}\{1}.cs", savePath, viewbag.classnameVal), outputText);
            }
        }
        private static void GenerateTemplate_Dao(string templateFileName, List<string> tableList, IDBSchema dbSchema, string daoNamespaceVal, string modelNamespaceVal, string dbNameVal, string savePath)
        {
            DisplayTemplateName(templateFileName);
            var templateText = TemplateHelper.ReadTemplate(templateFileName);
            foreach (var tableName in tableList)
            {
                var table = dbSchema.GetTableMetadata(tableName);
                if (table.PKs.Count == 0) throw new Exception(string.Format("表{0}:没有设置主键！", tableName));
                Display(tableName, table);
                dynamic viewbag = new DynamicViewBag();
                viewbag.classnameVal = tableName + "Dao";
                viewbag.namespaceVal = daoNamespaceVal;
                viewbag.modelNamespaceVal = modelNamespaceVal;
                viewbag.dbNameVal = dbNameVal;
                var outputText = TemplateHelper.Parse(TemplateKey.Dao, templateText, table, viewbag);
                outputText = TemplateHelper.Clean(outputText, RegexPub.H1());
                outputText = TemplateHelper.Clean(outputText, RegexPub.H2());
                FileHelper.Save(string.Format(@"{0}\{1}.cs", savePath, viewbag.classnameVal), outputText);
            }
        }
        private static void GenerateTemplate_ViewModel(string templateFileName, List<string> tableList, IDBSchema dbSchema, string modelNamespaceVal, string savePath)
        {
            DisplayTemplateName(templateFileName);
            var templateText = TemplateHelper.ReadTemplate(templateFileName);
            foreach (var tableName in tableList)
            {
                var table = dbSchema.GetTableMetadata(tableName);
                if (table.PKs.Count == 0) throw new Exception(string.Format("表{0}:没有设置主键！", tableName));
                Display(tableName, table);
                dynamic viewbag = new DynamicViewBag();
                viewbag.classnameVal = tableName;
                viewbag.namespaceVal = modelNamespaceVal;
                var outputText = TemplateHelper.Parse(TemplateKey.ViewModel, templateText, table, viewbag);
                outputText = TemplateHelper.Clean(outputText, RegexPub.H1());
                FileHelper.Save(string.Format(@"{0}\{1}ViewModel.cs", savePath, viewbag.classnameVal), outputText);
            }
        }
        private static void GenerateTemplate_ViewHelper(string templateFileName, List<string> tableList, IDBSchema dbSchema, string modelNamespaceVal, string savePath)
        {
            DisplayTemplateName(templateFileName);
            var templateText = TemplateHelper.ReadTemplate(templateFileName);
            foreach (var tableName in tableList)
            {
                var table = dbSchema.GetTableMetadata(tableName);
                if (table.PKs.Count == 0) throw new Exception(string.Format("表{0}:没有设置主键！", tableName));
                Display(tableName, table);
                dynamic viewbag = new DynamicViewBag();
                viewbag.classnameVal = tableName;
                viewbag.namespaceVal = modelNamespaceVal;
                var outputText = TemplateHelper.Parse(TemplateKey.ViewHelper, templateText, table, viewbag);
                outputText = TemplateHelper.Clean(outputText, RegexPub.H1());
                FileHelper.Save(string.Format(@"{0}\{1}ViewHelper.cs", savePath, viewbag.classnameVal), outputText);
            }
        }
        private static void Pause()
        {
            Console.ReadKey();
        }

        private static void DisplayTemplateName(string name)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Display Template Name:{0}", name);
            Console.ForegroundColor = ConsoleColor.White;

        }

        private static void Display(string tableName, Table table)
        {
            Console.WriteLine("tableName:{0},table.ColumnNames:{1}", tableName, table.ColumnNames);
        }
    }
}