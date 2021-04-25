using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time_Blocking_App.Models.ClickUp
{
    public static class ClickUpCredentials
    {
        // private static readonly string CredentialFilePath = "ms-appx:///ClickUp.txt";
        private static readonly string CredentialFilePath = Windows.ApplicationModel.Package.Current.InstalledPath + "/ClickUp.txt";

        public static string GetClientID()
        {

            string[] lines = ClickUpCredentials.GetFileContents();
            return lines[0];
        }

        public static string GetClientSecret()
        {

            string[] lines = ClickUpCredentials.GetFileContents();
            return lines[1];
        }

        public static string GetRedirectUri()
        {
            string[] lines = ClickUpCredentials.GetFileContents();
            return lines[2];
        }

        private static string[] GetFileContents()
        {
            if (!File.Exists(ClickUpCredentials.CredentialFilePath))
            {
                throw new Exception("API credentials file is missing.");
            }

            string allText = File.ReadAllText(ClickUpCredentials.CredentialFilePath);

            string[] lines = allText.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Replace("\n", "");
                lines[i] = lines[i].Replace("\r", "");
            }

            if (lines.Length < 3)
            {
                throw new Exception("API credentials file is incorrectly formatted.");
            }

            return lines;
        }
    }
}
