using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public static class ApiClient
{
    private const string BaseUrl = "http://localhost:5207";

    public static IEnumerator Get<T>(string endpoint, Action<ApiResult<T>> callback)
    {
        using var request = UnityWebRequest.Get($"{BaseUrl}/{endpoint}");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"GET {endpoint} succeeded.");

            var response = JsonConvert.DeserializeObject<T>(
                request.downloadHandler.text);

            callback?.Invoke(new ApiResult<T>
            {
                Success = true,
                Data = response
            });

            yield break;
        }

        Debug.LogError(
            $"GET {endpoint} failed. " +
            $"Status: {request.responseCode}, " +
            $"Error: {request.error}");

        var problemDetails = JsonConvert.DeserializeObject<ProblemDetailsResponse>(request.downloadHandler.text);

        callback?.Invoke(new ApiResult<T>
        {
            Success = false,
            ProblemDetails = problemDetails
        });
    }

    public static IEnumerator Post<TRequest, TResponse>(string endpoint, TRequest requestBody, Action<ApiResult<TResponse>> callback)
    {
        string requestJson = JsonConvert.SerializeObject(requestBody);

        using var request = UnityWebRequest.Post($"{BaseUrl}/{endpoint}", requestJson, "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"POST {endpoint} succeeded.");

            var response = JsonConvert.DeserializeObject<TResponse>(
                request.downloadHandler.text);

            callback?.Invoke(new ApiResult<TResponse>
            {
                Success = true,
                Data = response
            });

            yield break;
        }

        Debug.LogError(
            $"POST {endpoint} failed. " +
            $"Status: {request.responseCode}, " +
            $"Error: {request.error}");

        var problemDetails = JsonConvert.DeserializeObject<ProblemDetailsResponse>(request.downloadHandler.text);

        callback?.Invoke(new ApiResult<TResponse>
        {
            Success = false,
            ProblemDetails = problemDetails
        });
    }
}
