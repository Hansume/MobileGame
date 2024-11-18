using System;

[Serializable]
public class ApiResponse<T>
{
    public bool success;
    public string message;
    public T data;
}