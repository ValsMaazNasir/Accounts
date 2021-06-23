using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace LiquadCargoManagment.DataAccessLayer
{
    public static class Utility
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static bool AppLogEntry(string sLog)
        {
            DateTime dt;
            dt = DateTime.Now;
            string eventDate = DateTime.Now.ToString("MM-dd-yyyy");
            string LogPath = ConfigurationManager.AppSettings["LogPath"].ToString() + "Log\\";
            try
            {
                if (!System.IO.Directory.Exists(LogPath))
                {
                    System.IO.Directory.CreateDirectory(LogPath);
                }

                File.AppendAllText(LogPath + eventDate + ".log", System.Environment.NewLine);
                File.AppendAllText(LogPath + eventDate + ".log", DateTime.Now.ToString());
                File.AppendAllText(LogPath + eventDate + ".log", System.Environment.NewLine);
                File.AppendAllText(LogPath + eventDate + ".log", sLog);
                File.AppendAllText(LogPath + eventDate + ".log", System.Environment.NewLine);
                //   StreamWriter sw = new StreamWriter(LogPath + "\\Log-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Year + ".log", true);
                // sw.WriteLine(" Log Entry: " + dt + Environment.NewLine + sLog + Environment.NewLine); sw.Flush(); sw.Close();
            }
            catch (Exception CurrentException)
            {
                //   throw;
            }
            return true;
        }
    }
}