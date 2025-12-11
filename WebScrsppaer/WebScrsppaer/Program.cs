using HtmlAgilityPack;

namespace WebScraper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // URL of the page to scrape
            string url = "https://handlaprivatkund.ica.se/stores/1004187/";

            Console.WriteLine($"Scraping data from: {url}");

            try
            {
                // Fetch the webpage content
                var html = await FetchHtmlAsync(url);

                // Parse and extract data from the HTML content
                ParseHtml(html);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Function to fetch HTML content from a URL
        static async Task<string> FetchHtmlAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        // Function to parse the HTML content and extract desired data
        static void ParseHtml(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            // Extracting all titles from the Hacker News website
            var nodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='image-container']");

            if (nodes != null)
            {
                Console.WriteLine("Image containers");
                foreach (var node in nodes)
                {
                    Console.WriteLine($"- {node.InnerText}");
                }
            }
            else
            {
                Console.WriteLine("No data found");
            }
        }
    }
}

