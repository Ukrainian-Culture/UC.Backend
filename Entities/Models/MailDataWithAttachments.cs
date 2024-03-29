﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Entities.Models;
public class MailDataWithAttachments
{
    public List<string>? Bcc { get; set; } = null!;

    public string Subject { get; set; } = null!;
    public string? Body { get; set; }
    public IFormFileCollection? Attachments { get; set; }

}
