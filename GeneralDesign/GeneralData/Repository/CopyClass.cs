using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GeneralData.Repository
{
    /// <summary>A static class to create a new instance of a class from another class instance by matching properties and fields</summary>
    public static class CopyClass
    {
        /// <summary>Create a new instance of a class from another class instance by matching properties and fields</summary>
        /// <typeparam name="T1">The type of the class to be created</typeparam>
        /// <typeparam name="T2">The type of the class containing the properties and fields</typeparam>
        /// <param name="origClass">The object to copy</param>
        /// <returns>Returns an instance of type T1</returns>
        public static T1 CopyToNew<T1, T2>(T2 origClass) where T1 : class where T2 : class
        {
            T1 copy = (T1)Activator.CreateInstance(typeof(T1));
            Copy(copy, origClass);
            return copy;
        }
        /// <summary>Update an instance of a class from another class instance by matching properties and fields</summary>
        /// <typeparam name="T1">The type of the class to be updated</typeparam>
        /// <typeparam name="T2">The type of the class containing the properties and fields</typeparam>
        /// <param name="origClass">The object to copy</param>
        /// <param name="copyClass">The object to copy to</param>
        public static void Copy<T1, T2>(T1 copyClass, T2 origClass)
            where T1 : class
            where T2 : class
        {
            IList<PropertyInfo> propsT1 = new List<PropertyInfo>(typeof(T1).GetProperties());
            IList<PropertyInfo> propsT2 = new List<PropertyInfo>(typeof(T2).GetProperties());
            IList<FieldInfo> fieldsT1 = new List<FieldInfo>(typeof(T1).GetFields());
            IList<FieldInfo> fieldsT2 = new List<FieldInfo>(typeof(T2).GetFields());

            #region Loop through properties and fields to copy to new class instance
            (from p1 in propsT1
             from p2 in propsT2
             where string.Equals(p1.Name, p2.Name, StringComparison.CurrentCultureIgnoreCase) &&
             p1.PropertyType.FullName.ToString() != "System.Runtime.Serialization.ExtensionDataObject"
             select new { p1, p2 }).ToList()
             .ForEach(p => p.p1.SetValue(copyClass, p.p2.GetValue(origClass, null), null));

            (from f1 in fieldsT1
             from f2 in fieldsT2
             where string.Equals(f1.Name, f2.Name, StringComparison.CurrentCultureIgnoreCase) &&
             f1.IsPublic
             select new { f1, f2 }).ToList()
             .ForEach(f => f.f1.SetValue(copyClass, f.f2.GetValue(origClass)));
            #endregion
        }
    }
}
