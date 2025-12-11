using System.Net.Http.Json;
using System.Text.Json.Serialization;

// --- 1. Define Request and Response Structures for OpenAI Chat Completions ---

// Represents a single message in the conversation history
public record ChatMessage(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("content")] string Content
);

// The main payload sent to the /v1/chat/completions endpoint
public record ChatRequest(
    [property: JsonPropertyName("model")] string Model,
    [property: JsonPropertyName("messages")] ChatMessage[] Messages
);

// The object representing a successful generated response choice
public record ChatChoice(
    [property: JsonPropertyName("message")] ChatMessage Message
);

// The overall response received from the OpenAI API
public record ChatResponse(
    [property: JsonPropertyName("choices")] ChatChoice[] Choices
);

public class ChatClientOpenAI
{
    // The fixed, static, and reusable HttpClient for efficiency
    private static readonly HttpClient client = new HttpClient
    {
        // Use the official OpenAI base address
        BaseAddress = new Uri("https://api.openai.com/"),
    };

    // IMPORTANT: Replace this with your actual OpenAI API Key
    private const string ApiKey = "sk-efghijkl5678mnopabcd1234efghijkl5678mnop";

    public static async Task Main(string[] args)
    {
        // Set the Authorization header globally for the static client
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");

        Console.WriteLine("Welcome to the C# OpenAI Chat App Demo.");

        string prompt = "Explain why using a static HttpClient is crucial in C# web applications.";
        string response = await SendChatRequest(prompt);

        Console.WriteLine("\n--- Response from AI ---");
        Console.WriteLine(response);
    }

    /// <summary>
    /// Sends a prompt to the OpenAI Chat Completions API.
    /// </summary>
    public static async Task<string> SendChatRequest(string userPrompt)
    {
        // 2. Construct the API payload using the required structure (messages array)
        var payload = new ChatRequest(
            Model: "gpt-3.5-turbo", // Standard, fast chat model
            Messages: new ChatMessage[]
            {
                new ChatMessage(
                    Role: "user",
                    Content: userPrompt
                )
            }
        );

        try
        {
            // 3. Send the POST request to the correct OpenAI endpoint
            var response = await client.PostAsJsonAsync("v1/chat/completions", payload);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ChatResponse>();

                // Extract the content from the first choice
                return apiResponse?.Choices?[0]?.Message?.Content
                    ?? "Error: Could not parse text from API response.";
            }
            else
            {
                // Handle API error codes (e.g., 401 Unauthorized, 429 Rate Limit)
                string errorContent = await response.Content.ReadAsStringAsync();
                return $"API Error: {response.StatusCode}\nDetails: {errorContent}";
            }
        }
        catch (HttpRequestException ex)
        {
            // Handle network/connection errors
            return $"Network or HTTP Request Error: {ex.Message}";
        }
        catch (Exception ex)
        {
            // Catch all other unexpected errors
            return $"An unexpected error occurred: {ex.Message}";
        }
    }
}