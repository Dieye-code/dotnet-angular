﻿using System.Net;

namespace API.Application.Exceptions;

public class ConflictException : CustomException
{
    public ConflictException(string message) : base(message, null, HttpStatusCode.Conflict) 
    {
    }
}
