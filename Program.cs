using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;

namespace BlogReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<string> pages = new List<string>();
            List<string> articles = new List<string>();
            for (int i = 1; i < 37; i++) //todo: get page count via args
            {
                Console.WriteLine("Attempting to fetch page "+i);
                pages.Add(getHTMLFromURL(@"https://greento100.com/page/" + i + @"/")); //todo: get page URL via args
            }

            foreach (var page in pages)
            {
                string tmp = page;
                while (tmp.IndexOf(@"<article id=") > 0)
                {
                    int beginning = tmp.IndexOf("<article id=");
                    int end = tmp.IndexOf(@"</article>");
                    string article = tmp.Substring(beginning, end - beginning + 10);
                    articles.Add(article);
                    tmp = tmp.Substring(end + 10);
                }
            }
            
            articles.Reverse();
            Console.WriteLine("got " + articles.Count + " articles");

            int j = 5;
            for (int i = 0; j < articles.Count;i=j)
            {
                j = i + 10;
                Thread.Sleep(1100);
                string FileName = @"C:\temp\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_articles.html";
                Console.WriteLine("writing" +(j-i)+" to " + FileName);
                if(j +i > articles.Count)
                    writeListToFile(FileName, articles.GetRange(i, articles.Count -i));
                else
                    writeListToFile(FileName, articles.GetRange(i, j-i));    
            }

            
            
            
        }

        private static string getHTMLFromURL(string urlAddress)
        {

            string data = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }

            return data;
        }

        private static void writeListToFile(string fileName,List<string> data)
        {
            System.IO.StreamWriter file = new StreamWriter(fileName);
            file.Write(@"<html><body>");
            foreach (var line in data)
            {
                file.Write(line);
                file.Write(@"<hr />");
            }
            file.Write(@"</body></html>");
            file.Close();
        }
    }
}