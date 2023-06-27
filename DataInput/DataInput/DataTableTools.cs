using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataInput
{
    public static class DataTableTools
    {
		public static DataTable ListToDataTable<T>(this List<T> listValue)
		{
			//建立DataTable
			var dt = new DataTable();

			PropertyInfo[] propInfoList = null;

			foreach (var item in listValue)
			{
				//判斷DataTable又沒有定義欄位名稱與型態
				if (dt.Columns.Count == 0)
				{
					//取得本次輸入物件的所有的屬性
					propInfoList = item.GetType().GetProperties();

					//在DataTable中加入欄位的名稱與型別
					foreach (var propItem in propInfoList)
					{
						try
						{
							dt.Columns.Add(propItem.Name, Nullable.GetUnderlyingType(propItem.PropertyType) ?? propItem.PropertyType);
						}
						catch (Exception ex)
						{
							throw;
						}

					}
				}

				//建立新的列
				DataRow dr = dt.NewRow();

				//將資料逐筆加到DataTable
				foreach (var propItem in propInfoList)
				{
					try
					{
						var propValue = propItem.GetValue(item, null);
						propValue = propValue ?? DBNull.Value;
						dr[propItem.Name] = propValue;
					}
					catch (Exception ex)
					{
						throw;
					}

				}

				dt.Rows.Add(dr);
			}

			dt.AcceptChanges();

			return dt;
		}
	}
}
