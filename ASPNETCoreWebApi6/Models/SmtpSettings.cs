﻿namespace ASPNETCoreWebApi6.Models;

#nullable disable

public class SmtpSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public string Sender { get; set; }
}
