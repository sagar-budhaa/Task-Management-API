namespace Task_Management_API.Service.Result;

public class ResultService<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public int StatusCode { get; set; }


    public static ResultService<T> Ok(T data, int statusCode = 200) =>
        new()
        {
            Data = data,
            Success = true,
            StatusCode = statusCode
        };
    
    public static ResultService<T> Created(T data, int statusCode = 201) =>
        new()
        {
            Data = data,
            Success = true,
            StatusCode = statusCode
        };

    public static ResultService<T> NotFound(string errorMessage = "Resource not found") =>
        new()
        {
            Success = false,
            StatusCode = 404,
            ErrorMessage = errorMessage
        };

    public static ResultService<T> BadRequest(string errorMessage = "Bad request") =>
        new()
        {
            Success = false,
            StatusCode = 400,
            ErrorMessage = errorMessage
        };

}