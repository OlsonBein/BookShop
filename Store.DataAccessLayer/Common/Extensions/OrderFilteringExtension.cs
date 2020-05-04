using Store.DataAccessLayer.Repositories.EFRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static Store.DataAccessLayer.Common.Enums.Filter.Enums;

namespace Store.DataAccessLayer.Common.Extensions
{
    public static class OrderFilteringExtension
    {
        public static IEnumerable<TSource> SortByProperty<TSource>(this IEnumerable<TSource> source, string property, AscendingDescendingSort sortType) where TSource : class
        {           
            if (sortType == AscendingDescendingSort.Ascending)
            {
                return source.OrderBy(x => x.GetType().GetProperty(property).GetValue(x, null));                
            }
            return source.OrderByDescending(x => x.GetType().GetProperty(property).GetValue(x, null));           
        }

        public static IEnumerable<TSource> SortBySubProperty<TSource>(
            this IEnumerable<TSource> source,
            string property,
            string subProperty,
            AscendingDescendingSort sortType)
        {
            if (sortType == AscendingDescendingSort.Ascending)
            {
                return source.OrderBy(x => x.GetType().GetProperty(property)
                    .PropertyType.GetProperty(subProperty).GetValue(x.GetType().GetProperty(property).GetValue(x), null));
            }
            return source.OrderByDescending(x => x.GetType().GetProperty(property)
                .PropertyType.GetProperty(subProperty).GetValue(x.GetType().GetProperty(property).GetValue(x), null));
        }
    }
}
