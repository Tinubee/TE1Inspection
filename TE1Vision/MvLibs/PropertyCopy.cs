using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace MvLibs
{
    public static class PropertyCopy
    {
        //public static void CopyProperties<T>(T source, T target, Boolean deepCopy = true) where T : class
        public static void CopyProperties(Object source, Object target, Boolean deepCopy = true)
        {
            if (source == null || target == null)
                throw new ArgumentNullException("Source or destination object is null");
            if (source.GetType() != target.GetType())
                throw new ArgumentException("Source and destination types are different.");

            PropertyInfo[] properties = GetProperties(target.GetType());
            foreach (PropertyInfo p in properties)
            {
                if (!CanWrite(p)) continue;
                Object value = p.GetValue(source);
                if (value == null) continue;
                if (IsScalarType(p.PropertyType)) p.SetValue(target, value);
                else if (deepCopy) CopyNonScalarProperty(p, source, target);
            }
        }

        public static PropertyInfo[] GetProperties(Type type) => type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        public static Boolean CanWrite(PropertyInfo p) => p.CanRead && p.CanWrite;

        public static Boolean IsScalarType(Type type) =>
            type.IsPrimitive || type.IsValueType || type == typeof(String) || type == typeof(Decimal);

        public static void CopyNonScalarProperty(PropertyInfo p, Object source, Object target)
        {
            if (p.PropertyType.IsClass && !p.PropertyType.IsArray)
            {
                Object value = p.GetValue(source);
                if (value == null) return;
                Object instance = Activator.CreateInstance(p.PropertyType);
                CopyProperties(value, instance, true);
                p.SetValue(target, instance);
            }
            else if (typeof(IEnumerable).IsAssignableFrom(p.PropertyType))
                CopyEnumerableProperty(p, source, target);
        }

        public static void CopyEnumerableProperty(PropertyInfo p, Object source, Object target)
        {
            IEnumerable values = (IEnumerable)p.GetValue(source);
            if (values == null) return;

            Type elementType = p.PropertyType.GetElementType() ?? p.PropertyType.GenericTypeArguments[0];
            Type listType = typeof(List<>).MakeGenericType(elementType);
            IList newList = (IList)Activator.CreateInstance(listType);

            foreach (Object item in values)
            {
                if (IsScalarType(item.GetType())) newList.Add(item);
                else
                {
                    Object newItem = Activator.CreateInstance(item.GetType());
                    CopyProperties(item, newItem, true);
                    newList.Add(newItem);
                }
            }
            p.SetValue(target, newList);
        }
    }
}
