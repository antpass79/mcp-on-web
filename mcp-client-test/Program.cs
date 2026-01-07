using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;

Console.WriteLine("MCP Test Client (Stdio)");
Console.WriteLine("========================");

// Create stdio client transport for our MCP server
var stdioClientTransport = new StdioClientTransport(new StdioClientTransportOptions
{
    Name = "TextProcessorMcp",
    Command = "dotnet",
    Arguments = ["run", "--project", @"C:\WORK\Esaote\mcp-on-web\mcp-server\TextProcessorMcp\TextProcessorMcp.csproj"]
});

try
{
    // Connect to the MCP server
    Console.WriteLine("Connecting to MCP server via stdio...");
    var client = await McpClient.CreateAsync(stdioClientTransport);
    Console.WriteLine("Connected successfully!");

    // List available tools
    Console.WriteLine("\nAvailable tools:");
    var tools = await client.ListToolsAsync();
    foreach (var tool in tools)
    {
        Console.WriteLine($"- {tool.Name}: {tool.Description}");
    }

    // Test the tools with sample data
    Console.WriteLine("\nTesting tools with sample text: 'Hello World'");
    var testText = "Hello World";

    // Test ReverseText tool
    Console.WriteLine("\n1. Testing reverse_text tool:");
    var reverseResult = await client.CallToolAsync("reverse_text", 
        new Dictionary<string, object?> { ["text"] = testText });
    Console.WriteLine($"   Input: '{testText}'");
    Console.WriteLine($"   Result: '{ExtractTextContent(reverseResult)}'");

    // Test CountCharacters tool
    Console.WriteLine("\n2. Testing count_characters tool:");
    var charCountResult = await client.CallToolAsync("count_characters", 
        new Dictionary<string, object?> { ["text"] = testText });
    Console.WriteLine($"   Input: '{testText}'");
    Console.WriteLine($"   Character count: {ExtractTextContent(charCountResult)}");

    // Test CountWords tool
    Console.WriteLine("\n3. Testing count_words tool:");
    var wordCountResult = await client.CallToolAsync("count_words", 
        new Dictionary<string, object?> { ["text"] = testText });
    Console.WriteLine($"   Input: '{testText}'");
    Console.WriteLine($"   Word count: {ExtractTextContent(wordCountResult)}");

    // Interactive testing
    Console.WriteLine("\n=== Interactive Testing ===");
    while (true)
    {
        Console.WriteLine("\nEnter text to process (or 'quit' to exit):");
        var input = Console.ReadLine();
        
        if (string.IsNullOrEmpty(input) || input.ToLower() == "quit")
            break;

        // Test all three tools with user input
        Console.WriteLine($"\nProcessing: '{input}'");
        
        try
        {
            var userReverseResult = await client.CallToolAsync("reverse_text", 
                new Dictionary<string, object?> { ["text"] = input });
            Console.WriteLine($"  Reversed: {ExtractTextContent(userReverseResult)}");
            
            var userCharCountResult = await client.CallToolAsync("count_characters", 
                new Dictionary<string, object?> { ["text"] = input });
            Console.WriteLine($"  Characters: {ExtractTextContent(userCharCountResult)}");
            
            var userWordCountResult = await client.CallToolAsync("count_words", 
                new Dictionary<string, object?> { ["text"] = input });
            Console.WriteLine($"  Words: {ExtractTextContent(userWordCountResult)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  Error processing: {ex.Message}");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();

// Helper method to extract text content from tool results
static string ExtractTextContent(CallToolResult result)
{
    var textContent = result.Content.OfType<TextContentBlock>().FirstOrDefault();
    return textContent?.Text ?? "No text result";
}
