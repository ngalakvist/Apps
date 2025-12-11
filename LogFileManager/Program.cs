// See https://aka.ms/new-console-template for more information
using System.Text;


if (args.Length < 2)
{
    Console.WriteLine("Usage: Hantera Logfiler <inputFilePath> <outputFilePath>");
    return;
}

string inputFilePath = args[0];
string outputFilePath = args[1];

try
{
    // Read the input file
    string[] lines = File.ReadAllLines(inputFilePath);

    // Prepare the output CSV content
    StringBuilder csvContent = new StringBuilder();
    foreach (string line in lines)
    {
        string[] values = line.Split(',');

        // Modify values if necessary
        // For example, you can apply some transformation or validation

        // Append the values to the CSV content
        csvContent.AppendLine(string.Join(',', values));
    }

    // Write the CSV content to the output file
    File.WriteAllText(outputFilePath, csvContent.ToString());

    Console.WriteLine("CSV file created successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
