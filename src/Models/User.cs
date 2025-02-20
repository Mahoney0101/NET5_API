﻿namespace Api.Models;

public class User
{
    [BsonId]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string? UserName { get; set; }

    public string? Password { get; set; }

}
