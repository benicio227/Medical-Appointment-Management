﻿namespace MedicalManagement.Communication.Requests;
public class RequestUserJson
{
    public string Name {  get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role {  get; set; } = string.Empty;
}