﻿using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Text.Json;

namespace CkpTodoApp.Controllers
{
  [Route("api/tasks/list")]
  [ApiController]
  public class TasksListController : ControllerBase
  {
    [DisableCors]
    [HttpGet]
    public List<TaskModel>? Get()
    {
      Request.Headers.TryGetValue("token", out StringValues headerValues);
      var jsonWebToken = headerValues.FirstOrDefault();

      if (string.IsNullOrEmpty(jsonWebToken))
      {
        Response.StatusCode = 401;
        return new List<TaskModel>();
      }

      var apiToken = new ApiTokenModel(0, 0, jsonWebToken);
      apiToken.Verify();

      if (apiToken.UserId == 0)
      {
        Response.StatusCode = 401;
        return new List<TaskModel>();
      }

      var databaseManagerController = new DatabaseManagerController();
      var resultSql = databaseManagerController.ExecuteSQLQuery(
        @"SELECT json_group_array( 
          json_object(
            'Id', Id,
            'Title', Title,
            'Description', Description
          )
        )
        FROM tasks
        ORDER BY id ASC;"
      );

      return JsonSerializer.Deserialize<List<TaskModel>>(resultSql);
    }
  }
}