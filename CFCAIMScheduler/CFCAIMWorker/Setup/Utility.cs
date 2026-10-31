using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using CFCAIMCore.Entity;

namespace CFCAIMWorker.Setup
{
	public class Utility
	{
		private static String _settingXML = Application.StartupPath + @"\" + "CFCAIMWorkerSetting.xml";

		public static String SettingXML
		{
			get
			{
				return _settingXML;
			}
		}

		//public static Boolean SaveXmlDatabaseDetailData(XmlWriter writer, ref SettingInfo setting)
		//{
		//	try
		//	{
		//		Boolean isData = false;

		//		writer.WriteStartElement("Setting");
		//		writer.WriteElementString("SettingID", setting.SettingID.ToString());
		//		writer.WriteElementString("IsDebug", setting.IsDebug.ToString());
		//		writer.WriteElementString("IsTest", setting.IsTestUrl.ToString());
		//		writer.WriteElementString("APIUrl", setting.APIUrl);
		//		writer.WriteElementString("APIUrlTest", setting.APIUrlTest);
		//		writer.WriteElementString("AIMAPIKey", setting.AIMAPIKey);
		//		writer.WriteElementString("AIMInstrument", setting.AIMInstrumentKey);
		//		writer.WriteElementString("FossCSVPath", setting.FossCSVPath);
		//		writer.WriteElementString("FossNIRPath", setting.FossNIRPath);
		//		writer.WriteElementString("FossArchivePath", setting.FossArchivePath);
		//		writer.WriteElementString("FossErrorPath", setting.FossErrorPath);
		//		writer.WriteEndElement();
		//		isData = true;


		//		return isData;
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK);
		//		return false;
		//	}
		//}

		//public static void SaveXmlDatabaseData(ref SettingInfo setting, String cfcAIMWorkerFile)
		//{
		//	try
		//	{
		//		XmlWriterSettings settings = new XmlWriterSettings();
		//		Boolean isData = false;
		//		settings.Indent = true;

		//		using (XmlWriter writer = XmlWriter.Create(cfcAIMWorkerFile, settings))
		//		{
		//			writer.WriteStartDocument();
		//			writer.WriteComment("CFCAIMWorker");
		//			writer.WriteStartElement("Worker");
		//			isData = SaveXmlDatabaseDetailData(writer, ref setting);
		//			writer.WriteEndElement();
		//			writer.WriteEndDocument();
		//			writer.Flush();
		//		}

		//		if (isData == false && File.Exists(cfcAIMWorkerFile))
		//		{
		//			File.Delete(cfcAIMWorkerFile);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK);
		//	}
		//}

		//public static void LoadConnection(ref SettingInfo setting)
		//{
		//	try
		//	{
		//		XmlDocument xmlDoc = new XmlDocument();
		//		XmlNodeList xmlNode;
		//		Boolean isDebug = false;
		//		Boolean IsTest = false;

		//		setting = new SettingInfo();

		//		if (File.Exists(SettingXML))
		//		{
		//			using (FileStream fs = new FileStream(SettingXML, FileMode.Open, FileAccess.Read))
		//			{
		//				if (fs.Length > 0)
		//				{
		//					xmlDoc.Load(fs);
		//					xmlNode = xmlDoc.GetElementsByTagName("Setting");
		//					for (Int32 i = 0; i <= xmlNode.Count - 1; i++)
		//					{

		//						isDebug = false;

		//						if (xmlNode[i].ChildNodes.Item(1).InnerText.Trim().ToUpper() == "TRUE")
		//						{
		//							isDebug = true;
		//						}

		//						IsTest = false;

		//						if (xmlNode[i].ChildNodes.Item(2).InnerText.Trim().ToUpper() == "TRUE")
		//						{
		//							IsTest = true;
		//						}

		//						setting.SettingID = Int32.Parse(xmlNode[i].ChildNodes.Item(0).InnerText.Trim());
		//						setting.IsDebug = isDebug;
		//						setting.IsTestUrl = IsTest;
		//						setting.APIUrl = xmlNode[i].ChildNodes.Item(3).InnerText.Trim();
		//						setting.APIUrlTest = xmlNode[i].ChildNodes.Item(4).InnerText.Trim();
		//						setting.AIMAPIKey = xmlNode[i].ChildNodes.Item(5).InnerText.Trim();
		//						setting.AIMInstrumentKey = xmlNode[i].ChildNodes.Item(6).InnerText.Trim();
		//						setting.FossCSVPath = xmlNode[i].ChildNodes.Item(7).InnerText.Trim();
		//						setting.FossNIRPath = xmlNode[i].ChildNodes.Item(8).InnerText.Trim();
		//						setting.FossArchivePath = xmlNode[i].ChildNodes.Item(9).InnerText.Trim();
		//						setting.FossErrorPath = xmlNode[i].ChildNodes.Item(10).InnerText.Trim();
		//					}
		//				}
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK);
		//	}
		//}
	}
}
