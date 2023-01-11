﻿using Entities.Models;

namespace Contracts;

public interface ICultureRepository
{
    Task<Culture> GetCultureWithContentAsync(Guid cultureId, ChangesType asNoTracking);
}