using CFCAIMCore.Entity;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace CFCAIMWorker
{
	public static class Library
	{
		#region Globals
		private static Boolean _isDebugOn = false;
		private static String _fullLogFile = String.Empty;
		#endregion

		#region Parameter
		public static Boolean IsDebugOn
		{
			get
			{
				return _isDebugOn;
			}

			set
			{
				_isDebugOn = value;
			}
		}
		public static String FullLogFile
		{
			get
			{
				return _fullLogFile;
			}

			set
			{
				_fullLogFile = value;
			}
		}
		#endregion


		public static String ReadFile(String fullFilePath)
		{
			String result = String.Empty;
			String line = String.Empty;

			try
			{
				if (File.Exists(fullFilePath))
				{
					using (StreamReader file = new StreamReader(fullFilePath))
					{
						while ((line = file.ReadLine()) != null)
						{
							result += line + Environment.NewLine;
						}
						file.Close();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK);
			}

			return result;
		}




		public static void SaveXmlSettingData(ref SettingInfo setting, String cfcSettingFile)
		{
			try
			{
				XmlWriterSettings settings = new XmlWriterSettings();

				settings.Indent = true;

				if (File.Exists(cfcSettingFile))
				{
					File.Delete(cfcSettingFile);
				}

				using (XmlWriter writer = XmlWriter.Create(cfcSettingFile, settings))
				{
					writer.WriteStartDocument();
					writer.WriteStartElement("CFCSetting");
					SaveXmlSettingDetailData(writer, ref setting);
					writer.WriteEndElement();
					writer.WriteEndDocument();
					writer.Flush();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK);
			}
		}

		//4/4/23
		public static void SaveXmlConcept5Settings(ref SettingsConcept5Info settings, String concept5SettingsFile)
		{
			try
			{
				XmlWriterSettings setting = new XmlWriterSettings();
				setting.Indent = true;

				if (File.Exists(concept5SettingsFile))
				{
					File.Delete(concept5SettingsFile);
				}

				using (XmlWriter writer = XmlWriter.Create(concept5SettingsFile, setting))
				{
					writer.WriteStartDocument();
					writer.WriteStartElement("Concept5Settings");
					SaveXmlConcept5SettingsDetailData(writer, ref settings);
					writer.WriteEndElement();
					writer.WriteEndDocument();
					writer.Flush();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK);
			}
		}

		//4/4/23
		public static void LoadConcept5Settings(ref SettingsConcept5Info settings, String concept5SettingsFile)
		{
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				XmlNodeList xmlNode;


				String FormulationPath = "";
				String DatabaseConnection = "";
			//	Int32 SyncType = 0;
				Int32 count = 0;

				settings = new SettingsConcept5Info();
				settings.SettingsConcept5ID = -1;
				settings.FormulationPath = FormulationPath;
				settings.DatabaseConnection = DatabaseConnection;
				//// ??? settings.SyncType = SyncType;


				if (File.Exists(concept5SettingsFile))
				{
					using (FileStream fs = new FileStream(concept5SettingsFile, FileMode.Open, FileAccess.Read))
					{
						if (fs.Length > 0)
						{
							xmlDoc.Load(fs);
							xmlNode = xmlDoc.GetElementsByTagName("Concept5Settings");

							if (xmlNode.Count > 0)
							{
								settings = new SettingsConcept5Info();
								//// ??? SettingsConcept5Info.SettingsConcept5ID = Int32.Parse(xmlNode[count].ChildNodes.Item(0).InnerText.Trim());

								settings.FormulationPath = xmlNode[count].ChildNodes.Item(3).InnerText.Trim();
								settings.DatabaseConnection = xmlNode[count].ChildNodes.Item(4).InnerText.Trim();
								//// ??? settings.SyncType = xmlNode[count].ChildNodes.Item(5).InnerText.Trim();

							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK);
			}
		}

		//4/4/23
		public static void SaveXmlConcept5SettingsDetailData(XmlWriter writer, ref SettingsConcept5Info settings)
		{
			try
			{
				writer.WriteStartElement("SettingsConcept5");
				writer.WriteElementString("SettingsConcept5ID", settings.SettingsConcept5ID.ToString());
				writer.WriteElementString("FormulationPath", settings.FormulationPath.ToString());
				writer.WriteElementString("DatabaseConnection", settings.DatabaseConnection.ToString());

				writer.WriteEndElement();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK);
			}
		}

		public static void SaveXmlSettingDetailData(XmlWriter writer, ref SettingInfo setting)
		{
			try
			{
				writer.WriteStartElement("Setting");
				writer.WriteElementString("SettingID", setting.SettingID.ToString());
				writer.WriteElementString("IsDebug", setting.IsDebug.ToString());
				writer.WriteElementString("IsTestUrl", setting.IsTestUrl.ToString());
				writer.WriteElementString("APIUrl", setting.APIUrl);
				writer.WriteElementString("APIUrlTest", setting.APIUrlTest);
				writer.WriteElementString("AIMAPIKey", setting.AIMAPIKey);
				writer.WriteElementString("AIMInstrumentKey", setting.AIMInstrumentKey);
				writer.WriteElementString("FossCSVPath", setting.FossCSVPath);
				writer.WriteElementString("FossNIRPath", setting.FossNIRPath);
				writer.WriteElementString("FossArchivePath", setting.FossArchivePath);
				writer.WriteElementString("FossErrorPath", setting.FossErrorPath);
				writer.WriteElementString("IsPNETestUrl", setting.IsPNETestUrl.ToString());
				writer.WriteElementString("PNEAPIUrl", setting.PNEAPIUrl);
				writer.WriteElementString("PNEAPITestUrl", setting.PNEAPITestUrl);
				writer.WriteEndElement();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK);
			}
		}

		public static void LoadSetting(ref SettingInfo setting, String cfcSettingFile)
		{
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				XmlNodeList xmlNode;

				Boolean IsDebug = false;
				Boolean IsTestUrl = false;
				Int32 count = 0;

				setting = new SettingInfo();
				setting.SettingID = -1;
				setting.IsDebug = IsDebug;
				setting.IsTestUrl = IsTestUrl;
				setting.APIUrl = String.Empty;
				setting.APIUrlTest = String.Empty;
				setting.AIMAPIKey = String.Empty;
				setting.AIMInstrumentKey = String.Empty;
				setting.FossCSVPath = String.Empty;
				setting.FossNIRPath = String.Empty;
				setting.FossArchivePath = String.Empty;
				setting.FossErrorPath = String.Empty;

				if (File.Exists(cfcSettingFile))
				{
					using (FileStream fs = new FileStream(cfcSettingFile, FileMode.Open, FileAccess.Read))
					{
						if (fs.Length > 0)
						{
							xmlDoc.Load(fs);
							xmlNode = xmlDoc.GetElementsByTagName("Setting");

							if (xmlNode.Count > 0)
							{
								IsDebug = false;
								if (xmlNode[count].ChildNodes.Item(1).InnerText.Trim().ToUpper() == "TRUE")
								{
									IsDebug = true;
								}

								IsTestUrl = false;
								if (xmlNode[count].ChildNodes.Item(2).InnerText.Trim().ToUpper() == "TRUE")
								{
									IsTestUrl = true;
								}

								setting = new SettingInfo();
								setting.SettingID = Int32.Parse(xmlNode[count].ChildNodes.Item(0).InnerText.Trim());
								setting.IsDebug = IsDebug;
								setting.IsTestUrl = IsTestUrl;
								setting.APIUrl = xmlNode[count].ChildNodes.Item(3).InnerText.Trim();
								setting.APIUrlTest = xmlNode[count].ChildNodes.Item(4).InnerText.Trim();
								setting.AIMAPIKey = xmlNode[count].ChildNodes.Item(5).InnerText.Trim();
								setting.AIMInstrumentKey = xmlNode[count].ChildNodes.Item(6).InnerText.Trim();
								setting.FossCSVPath = xmlNode[count].ChildNodes.Item(7).InnerText.Trim();
								setting.FossNIRPath = xmlNode[count].ChildNodes.Item(8).InnerText.Trim();
								setting.FossArchivePath = xmlNode[count].ChildNodes.Item(9).InnerText.Trim();
								setting.FossErrorPath = xmlNode[count].ChildNodes.Item(10).InnerText.Trim();

								if (xmlNode[count].ChildNodes.Count > 11)
								{
									setting.IsPNETestUrl = false;
									if (xmlNode[count].ChildNodes.Item(11).InnerText.Trim().ToUpper() == "TRUE")
									{
										setting.IsPNETestUrl = true;
									}
									setting.PNEAPIUrl = xmlNode[count].ChildNodes.Item(12).InnerText.Trim();
									setting.PNEAPITestUrl = xmlNode[count].ChildNodes.Item(13).InnerText.Trim();
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK);
			}
		}

		public static void WriteErrorLog(Exception ex)
		{
			WriteErrorLog(ex, EventLogEntryType.Information);
		}

		public static void WriteErrorLog(Exception ex, EventLogEntryType eventLog)
		{
			EventLog appLog = new EventLog();
			StreamWriter sw = null;
			String message = String.Empty;

			try
			{
				appLog.Source = Assembly.GetExecutingAssembly().GetName().Name;
				message = ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim();
				appLog.WriteEntry(message);
			}
			catch
			{
				sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
				sw.WriteLine(DateTime.Now.ToString() + ": " + message);
				sw.Flush();
				sw.Close();
			}
		}

		public static void WriteErrorLog(String message)
		{
			WriteErrorLog(message, EventLogEntryType.Information);
		}

		public static void WriteErrorLog(String message, EventLogEntryType eventLog)
		{
			EventLog appLog = new EventLog();
			StreamWriter sw = null;

			try
			{

				if (IsDebugOn == true)
				{
					appLog.Source = Assembly.GetExecutingAssembly().GetName().Name;
					appLog.WriteEntry(message, eventLog);
				}
			}
			catch
			{
				sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
				sw.WriteLine(DateTime.Now.ToString() + ": " + message);
				sw.Flush();
				sw.Close();
			}
		}

		public static void RemoveLogFile()
		{
			if (File.Exists(Library.FullLogFile))
			{
				File.Delete(Library.FullLogFile);
			}
		}

		public static void RemoveOldLogFile()
		{
			String fileName = "CFCAIMWorker.Log";
			String[] file = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*" + fileName);
			Int32 startPostion = 0;
			Int32 fullDate = 0;
			Int32 fileDate = 0;


			foreach (String item in file)
			{
				startPostion = item.IndexOf(fileName);
				Int32.TryParse(item.Substring(startPostion - 8, 8), out fullDate);

				if (fullDate > 0)
				{
					Int32.TryParse(DateTime.Now.AddDays(-7).ToString("yyyyMMdd"), out fileDate);

					if (fullDate < fileDate)
					{
						File.Delete(item);
					}
				}
			}
		}

		public static void WriteErrorLogFile(String message)
		{
			WriteErrorLogFile(message, "Info");
		}

		public static void WriteErrorLogFile(String message, String status)
		{
			EventLog appLog = new EventLog();
			StreamWriter sw = null;

			if (IsDebugOn == true)
			{
				sw = new StreamWriter(FullLogFile, true);
				sw.WriteLine(DateTime.Now.ToString() + ": " + status + ": " + message);
				sw.Flush();
				sw.Close();
			}
		}
	}
}
