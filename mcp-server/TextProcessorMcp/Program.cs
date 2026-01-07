using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole(consoleLogOptions =>
{
    // Configure all logs to go to stderr
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();
await builder.Build().RunAsync();

/// <summary>
/// Text processing tools for the MCP server
/// </summary>
[McpServerToolType]
public static class TextProcessorTools
{
    /// <summary>
    /// Reverses the input text
    /// </summary>
    /// <param name="text">The text to reverse</param>
    /// <returns>The reversed text</returns>
    [McpServerTool, Description("Reverses the input text")]
    public static string ReverseText([Description("The text to reverse")] string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;
            
        char[] charArray = text.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    /// <summary>
    /// Counts the number of characters in the text
    /// </summary>
    /// <param name="text">The text to count characters in</param>
    /// <returns>The number of characters</returns>
    [McpServerTool, Description("Counts the number of characters in the text")]
    public static int CountCharacters([Description("The text to count characters in")] string text)
    {
        return text?.Length ?? 0;
    }

    /// <summary>
    /// Counts the number of words in the text
    /// </summary>
    /// <param name="text">The text to count words in</param>
    /// <returns>The number of words</returns>
    [McpServerTool, Description("Counts the number of words in the text")]
    public static int CountWords([Description("The text to count words in")] string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return 0;

        return text.Split(new char[] { ' ', '\t', '\n', '\r' }, 
            StringSplitOptions.RemoveEmptyEntries).Length;
    }
}
