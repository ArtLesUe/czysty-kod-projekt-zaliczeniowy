﻿using System.Net.Mail;
using System.Text.Json;
using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Services.DatabaseService;

namespace CkpTodoApp.Requests.User
{
  public class UserRegisterRequest
  {
    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? AboutMe { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? University { get; set; }

    public static bool IsValidEmail(string email)
    {
      try
      {
        MailAddress mail = new MailAddress(email);
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    public static bool IsEmailNotDuplicated(string email)
    {
      using (var context = new DatabaseFrameworkService())
      {
        List<ApiUserModel> userList = context.ApiUserModels.Where(f => f.Email == email).ToList();
        if (userList.Count == 0) return true;
        return false;
      }
    }

    public bool Validate()
    {
      return !string.IsNullOrEmpty(Name) &&
        !string.IsNullOrEmpty(Surname) &&
        !string.IsNullOrEmpty(Email) &&
        !string.IsNullOrEmpty(Password) &&
        IsValidEmail(Email) &&
        IsEmailNotDuplicated(Email);
    }
  }
}
