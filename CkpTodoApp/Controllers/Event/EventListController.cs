﻿using System.Text.Json;
using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Models.Event;
using CkpTodoApp.Services.ApiTokenService;
using CkpTodoApp.Services.DatabaseService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CkpTodoApp.Controllers.Event;

[Route("api/events/list")]
[ApiController]
public class EventListController : ControllerBase
{
  [HttpGet]
  public List<EventModel>? Get()
  {
    Request.Headers.TryGetValue("token", out StringValues headerValues);
    var jsonWebToken = headerValues.FirstOrDefault();

    if (string.IsNullOrEmpty(jsonWebToken))
    {
      Response.StatusCode = 401;
      return new List<EventModel>();
    }

    var apiToken = new ApiTokenModel(0, 0, jsonWebToken);
    var apiTokenService = new ApiTokenService();
    apiTokenService.Verify(apiToken);
    
    if (apiToken.UserId == 0)
    {
      Response.StatusCode = 401;
      return new List<EventModel>();
    }

    var databaseManagerController = new DatabaseService();
    var resultSql = databaseManagerController.ExecuteSQLQuery(
      @"SELECT json_group_array( 
          json_object(
            'Id', Id,
            'Title', Title,
            'Description', Description,
            'StartDate', StartDate,
            'EndDate', EndDate
          )
        )
        FROM events
        ORDER BY id ASC;"
    );

    return JsonSerializer.Deserialize<List<EventModel>>(resultSql);
  }
}