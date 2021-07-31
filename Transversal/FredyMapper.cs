using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Transversal
{
    public static class FredyMapper
    {
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                var list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            if (propertyInfo != null)
                            {
                                Type t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ??
                                         propertyInfo.PropertyType;
                                object safeValue = (row[prop.Name] == null) ? null : Convert.ChangeType(row[prop.Name], t);
                                propertyInfo.SetValue(obj, safeValue, null);
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                    list.Add(obj);
                }

                return list;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }
        }

        public static T ToObject<T>(this DataRow row) where T : class, new()
        {
            T obj = new T();
            foreach (var prop in obj.GetType().GetProperties())
            {
                try
                {
                    PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                    if (propertyInfo != null)
                    {
                        Type t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ??
                                 propertyInfo.PropertyType;
                        object safeValue = (row[prop.Name] == null) ? null : Convert.ChangeType(row[prop.Name], t);
                        propertyInfo.SetValue(obj, safeValue, null);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return obj;
        }
    }
}
