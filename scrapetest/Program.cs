using HtmlAgilityPack;

namespace ScrapeTest
{
    internal static class Program
    {
        private static void Main()
        {
            var hits = 0;
            List<string> notAvailable = new();
            var currentDirectory = Environment.CurrentDirectory;
            var threadList = new Dictionary<int, Thread>();

            Console.Title = "Steam lowid checker | by fema3832 | Current hits: 0";
            Console.WriteLine("How many threads do you want to use (5 for average performance, 100 for best):");
            int threadNum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("");

            for (int i = 0; i < threadNum; i++)
            {
                threadList[i] = new Thread(checkID);
                threadList[i].Start();
            }


            void checkID()
            {
                while (true)
                {
                    var randomID = scrapetest.createRandom.random_string(3);
                    if (!notAvailable.Contains(randomID))
                    {

                        try
                        {
                            var web = new HtmlWeb();
                            var web1 = new HtmlWeb();
                            var web2 = new HtmlWeb();
                            var doc = web.Load($"https://steamcommunity.com/id/{randomID}");
                            var doc1 = web1.Load($"https://steamcommunity.com/id/{randomID}");
                            var doc2 = web2.Load($"https://steamcommunity.com/id/{randomID}");
                            Thread.Sleep(4000);

                            Console.WriteLine($"Checking id/{randomID} ...");
                            if (doc.DocumentNode.SelectSingleNode("//*[@id=\"responsive_page_template_content\"]/div").HasClass("error_ctn"))
                            {
                                if (doc1.DocumentNode.SelectSingleNode("//*[@id=\"message\"]/h3").InnerText == "The specified profile could not be found.")
                                {
                                    if (doc2.DocumentNode.SelectSingleNode("//*[@id=\"message\"]/h1").InnerText == "Sorry!")
                                    {
                                        using (var writer = new StreamWriter($"{currentDirectory}/available.txt", true))
                                        {
                                            writer.WriteLine($"Available id: {randomID}");
                                        }

                                        Console.WriteLine($"Available id: {randomID}");
                                        hits++;

                                        Console.Title = $"Steam lowid checker | by fema3832 | Current hits: {hits}";
                                    }
                                }
                            }
                            notAvailable.Add(randomID);
                        }

                        catch (Exception ex)
                        {
                            using (var writer = new StreamWriter($"{currentDirectory}/errors.txt", true))
                            {
                                writer.WriteLine($"[ERROR]\n{ex}");
                            }
                        } finally { checkID(); }
                    }
                }
            }
        }
    }
}