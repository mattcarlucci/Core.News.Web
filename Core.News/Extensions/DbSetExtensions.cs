// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 03-20-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-20-2018
// ***********************************************************************
// <copyright file="DbSetExtensions.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.News
{
    /// <summary>
    /// Class DbSetExtensions.
    /// </summary>
    public static class DbSetExtensions
    {
        /// <summary>
        /// Adds the or update.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db">The database.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>T.</returns>
        public static T AddOrUpdate<T>(this DbContext db,  T entity, 
            Expression<Func<T, bool>> predicate) where T : class
        {
            var _entity = db.Set<T>().SingleOrDefault(predicate);
            if (_entity != null) return _entity;

            //TODO: if we need to update, we can use automapper. 

            db.Set<T>().Add(entity);           
            db.SaveChanges();
            return entity;

        }
    }
}
