using System;
using System.IO;
using Scriban;

class Program
{
    static void Main()
    {
        string templateDirectory = "./templates"; // Directory containing your Scriban templates
        string outputDirectory = "output";

        // Get all Scriban template files in the template directory
        string[] templatePaths = Directory.GetFiles(templateDirectory, "*.sb");

        foreach (string templatePath in templatePaths)
        {
            // Read Scriban template from file
            string templateContent = File.ReadAllText(templatePath);

            // Process the template
            var template = Template.Parse(templateContent);
            var result = template.Render();

            // Create the output directory if it doesn't exist
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            // Write the result to an HTML file
            string outputFileName = Path.GetFileNameWithoutExtension(templatePath) + ".html";
            string outputPath = Path.Combine(outputDirectory, outputFileName);
            File.WriteAllText(outputPath, result);

            Console.WriteLine($"Static page generated at: {outputPath}");
        }
    }
}
