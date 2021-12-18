using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperEx
{
    public static class DapperExExtension
    {
        public static IEnumerable<dynamic> Query(this IDbConnection cnn,string sql)
        {
            using(var command=cnn.CreateCommand())
            {
                command.CommandText = sql;
                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return reader.CastToDynamic();
                    }
                }
            }
        }

        private static dynamic CastToDynamic(this IDataReader reader)
        {
            dynamic e = new ExpandoObject();
            var d = e as IDictionary<string, object>;
            for(int i = 0; i < reader.FieldCount; i++)
            {
                d.Add(reader.GetName(i), reader[i]);
            }
            return e;
        }
    }
}
