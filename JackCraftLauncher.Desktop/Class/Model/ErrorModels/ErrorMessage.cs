using System;
using System.Text.Json.Serialization;

namespace JackCraftLauncher.Desktop.Class.Model.ErrorModels;

public class ErrorMessage
{
    [JsonPropertyName("error")] public string Error { get; set; }
    [JsonPropertyName("errorMessage")] public string ErrorMsg { get; set; }
    [JsonPropertyName("fix")] public string Fix { get; set; }
    [JsonIgnore] public Exception Exception { get; set; }
}