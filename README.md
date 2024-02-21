# Small .NET app to develop and render Scriban templates

This is a small .NET console app that uses Scriban to render templates. It makes developing Scriban templates much easier and faster.

## Usage
### Clone the repository (easy way)
- `git clone`
- Run the app using `dotnet run`
- Open the generated HTML page in the `output` directory

### Create a new .NET Console App (manual way)
Follow the steps [below](##How-to-setup) to create a new .NET Console App and add the Scriban NuGet package to it.


## Repository structure

- `templates` directory: Contains all the Scriban templates
- `output` directory: Contains the generated HTML pages
- `Program.cs`: The main file that contains the code to render the templates
- `index.sb`: A sample Scriban template
- `sample.sb`: A more complex sample Scriban template with loops, conditionals and data.

```
.
├── Program.cs
├── README.md
├── bin
│   └── Debug
├── obj
├── output
│   ├── index.html
│   └── sample.html
├── scriban-dev.csproj
└── templates
    ├── index.sb
    └── sample.sb
```

## How to setup

1. Ensure you can run dotnet commands in your terminal
- test by running `dotnet --version` in your terminal. If you get a version number, you are good to go. If not, you can install the .NET SDK from [here](https://dotnet.microsoft.com/download)

2. create a new .NET Console App 

`dotnet new console -n ScribanTemplate`

`cd ScribanTemplate`

3. Add the Scriban NuGet package

`dotnet add package Scriban`

4. Replace the content of Program.cs with the following code:

```csharp
using System;
using System.IO;
using Scriban;

class Program
{
    static void Main()
    {
        string templateDirectory = "./templates"; // Directory containing Scriban templates
        string outputDirectory = "output"; // Directory where the generated HTML pages will be saved

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
```

5. Create a new file `index.sb` in the `templates folder` of your project and add the following markup:

```handlebars

<!DOCTYPE html>
<html>
<head>
    <title>Hello, Scriban!</title>
</head>
<body>
    {{  name = "World" }}
    <h1>Hello, {{ name }}!</h1>
    <p>This is a simple HTML page generated using Scriban.</p>
</body>
</html>

```

6. Run the app using the following command:

`dotnet run`

You should see the following output:

```bash
Static page generated at: output/index.html
```

7. Open `output/index.html` file to see the generated HTML page.

8. To avoid having to run `dotnet run` every time you make a change to the template, you can use the `dotnet watch` command:

`dotnet watch run`

- Learn more about dotnet watch [here](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-watch)

9. So that dotnet watch can detect changes to any template file, you need to add the following to your .csproj file:

```xml

<ItemGroup>
  <Watch Include="**/*.sb" Exclude="bin\**;obj\**;**\*.xlf;packages\**" />
</ItemGroup>

```
10. **Optional**: To be able to also reload the generated HTML page in the browser, you can use the `live-server` package or if you want to use IIS you just need to create a site pointing to the output folder (I find live-server easier :) ). 

- Install `live-server` using the following command:
- Make sure you have Node.js installed.

`npm install -g live-server`

11. Run the following command to start the live server:

`live-server output --port=8080 --watch`
- `output` is the directory where the generated HTML page is saved  
- `--port=8080` is the port number that the server will listen on. You can change this to any port number you want.
- `--watch` tells the server to watch for changes in the `output` directory

Now, every time you make a change to any of the scriban templates, the HTML page will be automatically reloaded in the browser.

`live-server` documentation can be found [here](http://tapiov.net/live-server/)

## Scriban Documentation

For more information about Scriban, check the [official documentation](https://github.com/scriban/scriban/blob/master/doc/language.md).
