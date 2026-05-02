namespace Ztracena_Tlapka_Backend.Application.Common;

public class AppException<T>(int statusCode, T? data, string resCode, string message = "") : Exception(message)
{
    public new T? Data { get; } = data;
    public int StatusCode { get; } = statusCode;
    public string ResCode { get; } = resCode;
}
