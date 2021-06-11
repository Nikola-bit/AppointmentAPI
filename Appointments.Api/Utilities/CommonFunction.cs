using Appointments.Api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Appointments.Api.Utilities
{
    public class CommonFunction
    {

        public static IList<T> DatatableToList<T>(DataTable table) where T : class, new()
        {
            Type classType = typeof(T);
            IList<PropertyInfo> propertyList = classType.GetProperties();

            //Parameter class has no public properties
            if (propertyList.Count == 0)
                return new List<T>();

            List<string> columnName = table.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();

            List<T> result = new List<T>();

            try
            {
                foreach (DataRow row in table.Rows)
                {
                    T classObject = new T();
                    foreach (PropertyInfo property in propertyList)
                    {
                        if (property != null && property.CanWrite) //Make sure property isn't read only
                        {
                            if (columnName.Contains(property.Name)) //If property is a column name
                            {
                                if (row[property.Name] != System.DBNull.Value)
                                {
                                    object propertyValue = System.Convert.ChangeType(
                                             row[property.Name],
                                             Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType
                                        );
                                    property.SetValue(classObject, propertyValue, null);
                                }
                            }
                        }
                    }
                    result.Add(classObject);
                }
                return result;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }
        public static DataSet ExecSp(string procedureName, SqlParameter[] parameters)
        {
            var db = new AppointmentContext();
            DataSet ds = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter(procedureName, db.Database.GetDbConnection().ConnectionString);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            foreach (SqlParameter par in parameters)
                da.SelectCommand.Parameters.AddWithValue(par.ParameterName, par.Value);
            da.Fill(ds);
            return ds;
        }
    }
}
