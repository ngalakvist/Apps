using System.Text.Json;

// --- 1. Data Structures and Predictable Result Pattern ---

/// <summary>
/// Defines the expected structure of a User object received from the API.
/// </summary>
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

/// <summary>
/// A generic, consistent wrapper for all asynchronous function results.
/// This enforces predictable output and moves error handling out of the caller's logic.
/// </summary>
/// <typeparam name="T">The type of data being returned upon success.</typeparam>
public class Result<T>
{
    public T? Data { get; }
    public bool IsSuccess { get; }
    public string ErrorMessage { get; } = string.Empty;
    public int? StatusCode { get; }

    // Private constructor for success
    private Result(T data)
    {
        Data = data;
        IsSuccess = true;
    }

    // Private constructor for failure
    private Result(string errorMessage, int? statusCode = null)
    {
        ErrorMessage = errorMessage;
        IsSuccess = false;
        StatusCode = statusCode;
    }

    // Factory methods for clarity
    public static Result<T> Success(T data) => new Result<T>(data);
    public static Result<T> Failure(string errorMessage, int? statusCode = null) => new Result<T>(errorMessage, statusCode);
}

// --- 2. Dedicated Service Class (Separation of Concerns) ---

/// <summary>
/// Handles all business logic and network communication related to Users.
/// </summary>
public class UserService
{
    // Best Practice: HttpClient should be reused across calls for performance.
    // In a real application, inject this via Dependency Injection.
    private readonly HttpClient _httpClient = new HttpClient();
    private const string BaseUrl = "https://jsonplaceholder.typicode.com";

    public UserService()
    {
        // Set up a default timeout to prevent infinite hangs (Best Practice: Security/Cleanup)
        _httpClient.Timeout = TimeSpan.FromSeconds(10);
    }

    /// <summary>
    /// Fetches a user by ID using robust async/await and error handling.
    /// </summary>
    /// <param name="userId">The ID of the user to fetch.</param>
    /// <returns>A Task resolving to a Result containing either the User object or an error message.</returns>
    public async Task<Result<User>> GetUserByIdAsync(int userId)
    {
        var url = $"{BaseUrl}/users/{userId}";

        // 2. ERROR HANDLING: Catch low-level network errors (disconnects, timeouts)
        try
        {
            // 1. ASYNCHRONICITY: Using await to make the IO operation non-blocking
            var response = await _httpClient.GetAsync(url);

            // 2. ERROR HANDLING: Explicitly check for non-2xx status codes (e.g., 404, 500)
            if (!response.IsSuccessStatusCode)
            {
                var errorText = $"API returned status code {response.StatusCode}.";

                // You could try to read an error body here for more detail
                // var errorBody = await response.Content.ReadAsStringAsync();

                return Result<User>.Failure(errorText, (int)response.StatusCode);
            }

            // SUCCESS PATH: Deserialize the content
            var contentStream = await response.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<User>(contentStream,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // 4. PREDICTABLE OUTPUT: Ensure the deserialized object is not null
            if (user == null)
            {
                return Result<User>.Failure("Failed to deserialize user data.", (int)response.StatusCode);
            }

            return Result<User>.Success(user);

        }
        catch (HttpRequestException ex)
        {
            // Handles network-specific issues (DNS errors, connection timeouts, etc.)
            return Result<User>.Failure($"Network error during request: {ex.Message}");
        }
        catch (JsonException ex)
        {
            // Handles issues where the received JSON is malformed
            return Result<User>.Failure($"Data parsing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Catch-all for any unexpected errors
            return Result<User>.Failure($"An unexpected error occurred: {ex.Message}");
        }
    }
}

// --- 3. Program Entry Point (Caller Logic) ---

public class Program
{
    public static async Task Main()
    {
        Console.WriteLine("Initializing Network Demo...");
        var service = new UserService();

        // Scenario 1: Successful Fetch (User ID 1)
        await RunTest(service, 1);

        // Scenario 2: API Not Found Error (User ID 999 - will return 404)
        await RunTest(service, 999);

        // Scenario 3: Test for a low-level error (using a non-existent domain to simulate DNS fail)
        // NOTE: This test is commented out because it depends on modifying the BaseUrl to force a failure.
        // If uncommented, you'd change BaseUrl to "http://nonexistent-domain-xyz.com" and expect a network failure.
        // await RunTest(service, 1, "Simulated Network Failure Test");

        Console.WriteLine("\nDemo Complete.");
    }

    private static async Task RunTest(UserService service, int userId, string? customTitle = null)
    {
        var title = customTitle ?? $"Fetching User ID {userId}";
        Console.WriteLine($"\n--- {title} ---");

        // Call the robust, asynchronous service method
        var result = await service.GetUserByIdAsync(userId);

        // Consume the predictable Result<T> object
        if (result.IsSuccess)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"SUCCESS: User Found!");
            Console.WriteLine($"- ID: {result.Data!.Id}");
            Console.WriteLine($"- Name: {result.Data.Name}");
            Console.WriteLine($"- Email: {result.Data.Email}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"FAILURE: Could not retrieve user.");
            Console.WriteLine($"- Status: {result.StatusCode?.ToString() ?? "N/A"}");
            Console.WriteLine($"- Reason: {result.ErrorMessage}");
        }
        Console.ResetColor();
    }
}