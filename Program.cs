using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OpenAI;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Que souhaitez-vous faire ?");
        Console.WriteLine("1. Corriger des fautes d'orthographe en Anglais");
        Console.WriteLine("2. Traduire un texte");
        Console.WriteLine("3. Créer un projet React JS et l'ouvrir dans VSCode");

        int choix = int.Parse(Console.ReadLine());

        switch (choix)
        {
            case 1:
                await CorrectGrammar();
                break;
            case 2:
                await TranslateText();
                break;
            case 3:
                CreateAndOpenReactProject();
                break;
            default:
                Console.WriteLine("Choix invalide.");
                break;
        }
    }

    static async Task CorrectGrammar()
    {
        string apiKey = "f639bfb9e778e791cae62cc2b19032808a9f9b17 "; // Remplacez par votre clé d'API NLP Cloud

        var httpClient = new HttpClient();

        string endpoint = "https://api.nlpcloud.io/v1/gpu/finetuned-llama-2-70b/gs-correction";

        Console.Write("Entrez le texte à corriger : ");
        string textToCorrect = Console.ReadLine();

        var requestData = new
        {
            text = textToCorrect
        };

        var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        var response = await httpClient.PostAsync(endpoint, content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Texte corrigé : ");
            Console.WriteLine(responseContent);
        }
        else
        {
            Console.WriteLine($"Erreur : {response.StatusCode}");
        }
    }

    static async Task TranslateText()
    {
        string apiKey = "f639bfb9e778e791cae62cc2b19032808a9f9b17 "; // Remplacez par votre clé d'API NLP Cloud

        var httpClient = new HttpClient();

        string endpoint = "https://api.nlpcloud.io/v1/nllb-200-3-3b/translation";

        Console.Write("Entrez le texte à traduire : ");
        string textToTranslate = Console.ReadLine();

        Console.Write("Entrez la langue cible (par exemple, 'fr' pour le français) : ");
        string targetLanguage = Console.ReadLine();

        var requestData = new
        {
            text = textToTranslate,
            source_language = "fra_Latn", // Langue source en français
            target_language = targetLanguage
        };

        var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        var response = await httpClient.PostAsync(endpoint, content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Texte traduit : ");
            Console.WriteLine(responseContent);
        }
        else
        {
            Console.WriteLine($"Erreur : {response.StatusCode}");
        }
    }

    static void CreateAndOpenReactProject()
    {
        Console.WriteLine("Entrez le nom du projet React :");
        string projectName = Console.ReadLine();

        ExecuteCommand($"npx create-react-app {projectName}");
        ExecuteCommand($"cd {projectName} && code . -r");
        ExecuteCommand($"npm install");
    }

   

    static void ExecuteCommand(string command)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();
        process.StandardInput.WriteLine(command);
        process.WaitForExit();
        process.Close();
    }
}
