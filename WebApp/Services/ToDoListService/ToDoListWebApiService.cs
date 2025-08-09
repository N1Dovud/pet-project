using System.Net;
using System.Text.Json;
using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using WebApp.Business.ToDoLists;
using WebApp.Common;
using WebApp.Mappers;
using WebApp.Models.ToDoLists;
using WebApp.Services.DatabaseService;

namespace WebApp.Services.ToDoListService;

public class ToDoListWebApiService : IToDoListWebApiService
{
    private readonly HttpClient httpClient;
    private readonly string baseUrl;
    private readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public ToDoListWebApiService(IHttpClientFactory factory, IConfiguration configuration)
    {
        this.httpClient = factory.CreateClient("ApiWithJwt");
        this.baseUrl = configuration["WebApiAddress"];
    }

    public async Task<Result> AddToDoListAsync(ToDoList? list)
    {
        if (list == null)
        {
            return Result.Error("List cannot be null");
        }

        var route = "list";
        var uri = new Uri(this.baseUrl + route);
        var response = await this.httpClient.PostAsJsonAsync(uri, list.ToWebApiModel(), this.options);
        if (response.StatusCode == HttpStatusCode.Created)
        {
            return Result.Success("List created successfully");
        }

        return response.StatusCode switch
        {
            HttpStatusCode.BadRequest => Result.Error("Bad request"),
            HttpStatusCode.Unauthorized => Result.Error("Unauthorized"),
            _ => Result.Error($"Failed to create list. Status code: {response.StatusCode}"),
        };
    }

    public async Task<Result> UpdateToDoListAsync(ToDoList? list)
    {
        if (list == null)
        {
            return Result.Error("List cannot be null");
        }

        var route = "list";
        var uri = new Uri(this.baseUrl + route);
        var response = await this.httpClient.PutAsJsonAsync(uri, list.ToWebApiModel(), this.options);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return Result.Success("List updated successfully");
        }

        return response.StatusCode switch
        {
            HttpStatusCode.BadRequest => Result.Error("Bad request"),
            HttpStatusCode.Unauthorized => Result.Unauthorized("Unauthorized"),
            _ => Result.Error($"Failed to update a list. Status code: {response.StatusCode}"),
        };
    }

    public async Task<Result> DeleteToDoListAsync(long listId)
    {
        var route = "list";
        var uri = new Uri($"{this.baseUrl}{route}?listId={listId}");
        var response = await this.httpClient.DeleteAsync(uri);

        return response.StatusCode switch
        {
            HttpStatusCode.OK or HttpStatusCode.NoContent =>
                Result.Success("Deleted"),

            HttpStatusCode.NotFound =>
                Result.Error("Item does not exist"),

            HttpStatusCode.Forbidden =>
                Result.Error("You are not the owner of the list"),

            HttpStatusCode.Unauthorized =>
                Result.Unauthorized("Please sign in"),

            _ =>
                Result.Error("Probably server error")
        };
    }

    public async Task<List<ToDoList?>?> GetToDoListsAsync()
    {
        var route = "lists";
        var uri = new Uri(this.baseUrl + route);
        var response = await this.httpClient.GetAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }

            throw new Exception($"Failed to fetch lists from {uri}. Status code: {response.StatusCode}");
        }

        var json = await response.Content.ReadAsStringAsync();
        var lists = JsonSerializer.Deserialize<List<ToDoListWebApiModel>>(json, this.options);
        if (lists == null)
        {
            return null;
        }

        return [.. lists.Select(l => l.ToDomain())];
    }

    public async Task<ToDoList?> GetToDoListAsync(long listId)
    {
        var route = "list";
        var uri = new Uri($"{this.baseUrl}{route}?listId={listId}");
        var response = await this.httpClient.GetAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var json = await response.Content.ReadAsStringAsync();
        var list = JsonSerializer.Deserialize<ToDoListWebApiModel>(json, this.options);
        if (list == null)
        {
            return null;
        }

        return list.ToDomain();
    }
}
