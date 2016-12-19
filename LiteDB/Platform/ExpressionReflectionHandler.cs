
using System;
using System.Linq.Expressions;
using System.Reflection;
using LiteDB;

namespace LiteDB.Platform
{
    public class ExpressionReflectionHandler : IReflectionHandler
    {
        public CreateObject CreateClass(Type type)
        {
            var res = Expression.Lambda<CreateObject>(Expression.New(type)).Compile();

            return res;
        }

        public CreateObject CreateStruct(Type type)
        {
            var newType = Expression.New(type);
            var convert = Expression.Convert(newType, typeof(object));
            var res = Expression.Lambda<CreateObject>(convert).Compile();

            return res;
        }

        public GenericGetter CreateGenericGetter(Type type, PropertyInfo propertyInfo, bool nonPublic)
        {
            if (propertyInfo == null) throw new ArgumentNullException("propertyInfo");

            var obj = Expression.Parameter(typeof(object), "o");
            var accessor = Expression.MakeMemberAccess(Expression.Convert(obj, propertyInfo.DeclaringType), propertyInfo);

            return Expression.Lambda<GenericGetter>(Expression.Convert(accessor, typeof(object)), obj).Compile();
        }

        public void Test()
        {
            var obj = Expression.Parameter(typeof(object), "obj");
            var value = Expression.Parameter(typeof(object), "val");
        }

        public GenericSetter CreateGenericSetter(Type type, PropertyInfo propertyInfo, bool nonPublic)
        {
            if (propertyInfo == null) throw new ArgumentNullException(propertyInfo.ToString());

            if (!propertyInfo.CanWrite)
                return null;
                
            var obj = Expression.Parameter(typeof(object), "obj");
            var value = Expression.Parameter(typeof(object), "val");
            var accessor = Expression.Property(Expression.Convert(obj, propertyInfo.DeclaringType), propertyInfo);
            var assign = ExpressionEx.Assign(accessor, Expression.Convert(value, propertyInfo.PropertyType));
            var conv = Expression.Convert(assign, typeof(object));

            return Expression.Lambda<GenericSetter>(conv, obj, value).Compile();
        }
    }
}

public static class ExpressionEx {
    public static BinaryExpression Assign(Expression left, Expression right) {
        var assign = typeof(Assigner<>).MakeGenericType(left.Type).GetMethod("Assign");

        var assignExpr = Expression.Add(left, right, assign);

        return assignExpr;
    }

    private static class Assigner<T> {
        public static T Assign(ref T left, T right) {
            return (left = right);
        }
    }
}