namespace backend.CORE.DTOs.Responses;

public class BaseResponse
{
    public string Message { get; set; }
    public string Exception { get; set; }
    public dynamic Data { get; set; }
}
