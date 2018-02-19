using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Volvo.FleetControl.Test.Extensions
{
    static class EnumerableExtension
    {
        public static T SelectRandomItem<T>(this IQueryable<T> queryable)
        {
            Random r = new Random();
            var count = queryable.Count();
            r.Next();
            return queryable.Select(s => new
            {
                Order = r.Next(count),
                Item = s
            }).OrderBy(s => s.Order)
            .Select(s => s.Item)
            .FirstOrDefault();
        }
    }
}
