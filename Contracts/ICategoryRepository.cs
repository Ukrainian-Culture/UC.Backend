﻿using Entities.Models;
using System.Linq.Expressions;

namespace Contracts;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllByConditionAsync(Expression<Func<Category, bool>> expression,
        ChangesType trackChanges);

    Task<Category?> GetFirstByConditionAsync(Expression<Func<Category, bool>> expression,
        ChangesType trackChanges);
}