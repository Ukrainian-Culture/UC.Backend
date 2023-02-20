using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configurations;

public class MailSettings
{
    public string? DisplayName { get; set; }
    public string? From { get; set; }
    public string? To { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Host { get; set; }
    public int Port { get; set; }
    public bool UseSSL { get; set; }
    public bool UseStartTls { get; set; }
    public string ReplyTo { get; set; }
    public string ReplyToName { get; set; }
}

