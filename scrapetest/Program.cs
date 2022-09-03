using HtmlAgilityPack;
using System;

namespace ScrapeTest
{
    static class Program
    {
        static void Main()
        {
            int hits = 0;
            List<string> notAvaliable = new();
            var currentDirectory = Environment.CurrentDirectory;

            Thread thread1 = new Thread(() => checkID());
            Thread thread2 = new Thread(() => checkID());
            Thread thread3 = new Thread(() => checkID());
            Thread thread4 = new Thread(() => checkID());
            Thread thread5 = new Thread(() => checkID());
            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread5.Start();

            Console.Title = "Steam lowid checker | by fema3832 | Current hits: 0";

            void checkID() {
                while (true) {
                    string randomID = scrapetest.createRandom.random_string(3);
                    if (!notAvaliable.Contains(randomID))
                    {
                        Thread.Sleep(100);
                        HtmlWeb web = new HtmlWeb();
                        HtmlDocument doc = web.Load($"https://steamcommunity.com/id/{randomID}");

                        try
                        {
                            if (doc.DocumentNode.SelectSingleNode("//*[@id=\"responsive_page_template_content\"]/div").GetAttributeValue("class", "default") == "error_ctn")
                            {
                                using (StreamWriter writer = new StreamWriter($"{currentDirectory}/avaiable.txt", true)){ writer.WriteLine($"{randomID} is available!"); }
                                Console.WriteLine($"{randomID} is available!");
                                hits++;

                                Console.Title = $"Steam lowid checker | by fema3832 | Current hits: {hits.ToString()}\n";
                            }
                            else
                            {
                                Console.WriteLine($"{randomID} is not available!");
                                notAvaliable.Add(randomID);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            checkID();
                        }
                    }
                }
            }
        }
    }
}