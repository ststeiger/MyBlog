
namespace MyBlog
{


    public class Settings
    {


		protected static DB.Abstraction.UniversalConnectionStringBuilder GetMsCSB()
		{
			DB.Abstraction.UniversalConnectionStringBuilder csb = 
				DB.Abstraction.UniversalConnectionStringBuilder.CreateInstance (
					DB.Abstraction.cDAL.DataBaseEngine_t.MS_SQL);

			csb.Server = System.Environment.MachineName;

            if (System.StringComparer.InvariantCultureIgnoreCase.Equals(System.Environment.MachineName, "nvidiasucks"))
                csb.Server= @"NVIDIASUCKS\MS_SQL_2014";
            
            csb.DataBase = "Blogz";
			csb.IntegratedSecurity = true;
			csb.Pooling = false;
			csb.PersistSecurityInfo = false;

			return csb;
		} // End Function GetMsCSB


		protected static DB.Abstraction.UniversalConnectionStringBuilder GetPgCSB()
		{
			DB.Abstraction.UniversalConnectionStringBuilder csb = 
				DB.Abstraction.UniversalConnectionStringBuilder.CreateInstance (
					DB.Abstraction.cDAL.DataBaseEngine_t.PostGreSQL);

			csb.Server = "127.0.0.1";
			csb.DataBase = "blogz";
			csb.Port = 5432;
			csb.UserName = "pgwebservices";
			csb.Password = "foobar2000";

			return csb;
		} // End Function GetPgCSB


        protected static DB.Abstraction.UniversalConnectionStringBuilder GetCSB()
        {
            DB.Abstraction.UniversalConnectionStringBuilder retval; 
            bool bUsePG = false;



            if (System.StringComparer.InvariantCultureIgnoreCase.Equals(System.Environment.MachineName, "COR-W81-101")
                    || System.StringComparer.OrdinalIgnoreCase.Equals("nvidiasucks", System.Environment.MachineName)    
                )
            {
                if (bUsePG)
                    retval = GetPgCSB();
                else
                    retval = GetMsCSB();
            }
            else if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
                retval = GetPgCSB();
            else 
                retval = GetPgCSB();

            return retval;
        } // End Function GetCSB


        protected static DB.Abstraction.cDAL SetupDAL()
        {
			DB.Abstraction.UniversalConnectionStringBuilder csb = GetCSB ();
			return DB.Abstraction.cDAL.CreateInstance(csb.EngineName, csb.ConnectionString);
        } // End Function SetupDAL


        public static DB.Abstraction.cDAL DAL = SetupDAL();


    } // End Class Settings 


} // End Namespace MyBlog 
