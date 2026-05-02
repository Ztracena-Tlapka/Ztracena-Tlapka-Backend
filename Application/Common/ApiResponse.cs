namespace Ztracena_Tlapka_Backend.Application.Common;

public record ApiResponse<T>(
    string ResCode,
    T? Data,
    string ResType
);

public static class ApiResponse
{
    public static ApiResponse<T> Success<T>(T data, string resCode = ResCodes.Success) =>
        new(resCode, data, "success");

    public static ApiResponse<object> Success(string resCode = ResCodes.Success) =>
        new(resCode, new { }, "success");

    public static ApiResponse<T> Error<T>(T data, string resCode = ResCodes.InternalError) =>
        new(resCode, data, "error");

    public static ApiResponse<object> Error(string resCode = ResCodes.InternalError) =>
        new(resCode, new { }, "error");
}
