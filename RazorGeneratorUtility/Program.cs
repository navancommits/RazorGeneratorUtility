using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RazorGeneratorUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            var SlnPath = "C:\\test\\toyota-main-site-9.2\\Toyota.MainSite\\Toyota.MainSite.sln";
            var Content = File.ReadAllText(SlnPath);
            Regex projReg = new Regex(
                "Project\\(\"\\{[\\w-]*\\}\"\\) = \"([\\w _]*.*)\", \"(.*\\.(cs|vcx|vb)proj)\""
                , RegexOptions.Compiled);
            var matches = projReg.Matches(Content).Cast<Match>();
            var Projects = matches.Select(x => x.Groups[2].Value).ToList();
            for (int i = 0; i < Projects.Count; ++i)
            {
                if (!Path.IsPathRooted(Projects[i]))
                    Projects[i] = Path.Combine(Path.GetDirectoryName(SlnPath),
                        Projects[i]);
                Projects[i] = Path.GetFullPath(Projects[i]);
                ReadFile(Projects[i]);
                
            }
            Console.ReadLine();
        }

        private static string GetSubString(string stringVal, string string1,string string2)
        {

            int pFrom = stringVal.IndexOf(string1) + string1.Length;
            int pTo = stringVal.LastIndexOf(string2);

            return stringVal.Substring(pFrom, pTo - pFrom);
        }

        private static void ReadFile(string filePath)
        {
            string cshtmlPath = string.Empty;
            string newline = string.Empty;
            int lastIndexofSlash;
            int indexofCshtml;
            int fileStringLength;
            bool fileChanged;
            string concatLines=string.Empty;

            using (var input = File.OpenText(filePath))
            using (var output = new StreamWriter("temp.csproj"))
            {
                string currline;
                fileChanged = false;
                while (null != (currline = input.ReadLine()))
                {
                    // optionally modify line.
                    if (currline.Contains("Content") && currline.Contains("Include=")  && currline.Contains(".cshtml"))
                    {
                        cshtmlPath = GetSubString(currline, "\"", "\"");
                        
                        lastIndexofSlash= cshtmlPath.LastIndexOf(@"\");
                        indexofCshtml = cshtmlPath.IndexOf(@".");
                        fileStringLength = indexofCshtml - lastIndexofSlash;

                        newline = "\r\t<None Include = \"" + cshtmlPath + "\">\r\t\t<Generator>RazorGenerator</Generator>\r\t\t<LastGenOutput>" + cshtmlPath.Substring(lastIndexofSlash + 1, fileStringLength) + "generated.cs</LastGenOutput>\r\t</None>\r";

                        fileChanged = true;
                        concatLines += newline;
                        
                    }
                    else
                    {
                        concatLines += currline + "\r";
                    }
                }
               
            }
            if (fileChanged)
            {

                File.WriteAllText(@"temp1.csproj", concatLines);
                    
                File.Replace("temp1.csproj", filePath, null);
                File.Delete("temp1.csproj");
            }


        }
    }
}
