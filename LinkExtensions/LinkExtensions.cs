using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SuperCore.NetCore
{
    public static class LinqExtensions
    {
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }


        private static PropertyInfo GetPropertyInfo(Type objType, string name)
        {
            var properties = objType.GetProperties();
            var matchedProperty = properties.FirstOrDefault(p => p.Name.ToUpper() == name.ToUpper());
            if (matchedProperty == null)
                throw new ArgumentException("name");

            return matchedProperty;
        }
        private static LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
        {
            var paramExpr = Expression.Parameter(objType);
            var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);
            var expr = Expression.Lambda(propAccess, paramExpr);
            return expr;
        }

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> query, string name)
        {
            var propInfo = GetPropertyInfo(typeof(T), name);
            var expr = GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IEnumerable<T>)genericMethod.Invoke(null, new object[] { query, expr.Compile() });
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string name)
        {
            var propInfo = GetPropertyInfo(typeof(T), name);
            var expr = GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
        }
        public static IQueryable<T> DoPagination<T>(this IQueryable<T> listObject, Microsoft.AspNetCore.Http.HttpContext httpContext, string defaultOrderByField)
        {
            int itemsPerPage = 1000;
            int page = 1;
            if (httpContext.Request.Headers.Where(x => x.Key == "itemsPerPage").Any())
            {
                itemsPerPage = Convert.ToInt32(httpContext.Request.Headers["itemsPerPage"][0]);
            }
            if (httpContext.Request.Headers.Where(x => x.Key == "page").Any())
            {
                page = Convert.ToInt32(httpContext.Request.Headers["page"][0]);
            }
            if (httpContext.Request.Headers.Where(x => x.Key == "orderBy").Any())
            {
                defaultOrderByField = httpContext.Request.Headers["orderBy"][0];
            }
            int total = listObject.Count();
            httpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");
            httpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
            httpContext.Response.Headers.Add("Access-Control-Expose-Headers", "*");
            httpContext.Response.Headers.Add("totalItems", listObject.Count().ToString());
            httpContext.Response.Headers.Add("itemsPerPage", itemsPerPage.ToString());
            httpContext.Response.Headers.Add("page", page.ToString());
            //listObject = listObject.ToList().Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            listObject = listObject.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            listObject = listObject.OrderBy(defaultOrderByField);
            return listObject;
        }

    }
}
