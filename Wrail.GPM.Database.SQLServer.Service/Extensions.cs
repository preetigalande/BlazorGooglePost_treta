using System.Data;

namespace Wrail.GPM.Database.SQLServer.Service;

public static class Extensions
{
    /// <summary>
    /// creates a DataTable from a list of objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    public static DataTable ToDataTable<T>(this IEnumerable<T> data)
    {
        System.ComponentModel.PropertyDescriptorCollection properties =
            System.ComponentModel.TypeDescriptor.GetProperties(typeof(T));

        DataTable table = new DataTable();

        foreach (System.ComponentModel.PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

        foreach (T item in data)
        {
            DataRow row = table.NewRow();
            foreach (System.ComponentModel.PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }

        return table;
    }

    /// <summary>
    /// creates a DataTable named Status from a list of integers
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public static DataTable ToStatusDataTable(this List<int> status)
    {
        DataTable dataTable = new();

        dataTable.Columns.Add(new DataColumn("Status", typeof(int)));

        foreach (var item in status)
        {
            dataTable.Rows.Add(item);
        }

        return dataTable;
    }
}
