
using Dapper;
using System.Collections.Generic;


using MyBlogCore.Controllers;


namespace MyBlogCore
{
    

    public class NewDal
    {

        public static async System.Threading.Tasks.Task Test()
        {
            SqlFactory fac = new SqlClientFactory();
            // DbConnection.ProviderFactory => DbProviderFactory; 

            using (System.Data.Common.DbConnection con = fac.Connection)
            {
                string sql = "SELECT * FROM T_BlogPost";
                string sql_paged = sql += fac.PagingTemplate(3, 2);
                string sql_limited = sql += fac.PagingTemplate(1);

                IEnumerable<T_BlogPost> a = con.Query<T_BlogPost>(sql);
                IEnumerable<T_BlogPost> aa = await con.QueryAsync<T_BlogPost>(sql_paged);

                T_BlogPost b = con.QuerySingle<T_BlogPost>(sql_limited);
                T_BlogPost ba = await con.QuerySingleAsync<T_BlogPost>(sql_limited);
            } // End Using con 

        } // End Sub Test 


    } // End Class NewDal 
    
    
} // End Namespace MyBlog.Controllers 
