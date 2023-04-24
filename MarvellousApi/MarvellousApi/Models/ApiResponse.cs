namespace MarvellousApi.Models;

public class ApiResponse<T>
{
    public DataWrapper<T> Data { get; set; }
}