using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Core.Pagination
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="field">Es la propiedad de la clase (entidad) por el cual se ordenarà</param>
        /// <param name="direction">Puede ser "ASC" o "DESC" cualquier otro valor sera considerado NULL</param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string field, string direction)
        {
            string orderByMethod = (direction == "ASC") ? "OrderBy" : (direction == "DESC" ? "OrderByDescending" : null);
            if (orderByMethod == null) throw new ArgumentException();

            var propertyInfo = typeof(T).GetProperty(field);
            var entityParam = Expression.Parameter(typeof(T), "e");

            Expression columnExpr = Expression.Property(entityParam, propertyInfo);
            LambdaExpression columnLambda = Expression.Lambda(columnExpr, entityParam);

            MethodInfo orderByGeneric = typeof(Queryable).GetMethods().Single(m => m.Name == orderByMethod
                                                   && m.GetParameters().Count() == 2
                                                   && m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IQueryable<>)
                                                   && m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == typeof(Expression<>));

            MethodInfo orderBy = orderByGeneric.MakeGenericMethod(new[] { typeof(T), propertyInfo.PropertyType });

            return (IQueryable<T>)orderBy.Invoke(null, new object[] { source, columnLambda });
        }

        public static PaginatedItemsResponse<TResponse> ToPagedResponse<TResponse, TRequest>(this IQueryable<TResponse> source, PaginatedItemsRequest<TRequest> pageRequest)
            where TRequest : class
            where TResponse : class
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (pageRequest == null)
                throw new ArgumentNullException("pageRequest");

            return new PaginatedItemsResponse<TResponse>(
                pageIndex: (pageRequest.Skip / pageRequest.PageSize) + 1,
                pageSize: pageRequest.PageSize,
                count: source.Count(),
                data: source.Skip(pageRequest.Skip).Take(pageRequest.PageSize).AsEnumerable());
        }
    }
}
