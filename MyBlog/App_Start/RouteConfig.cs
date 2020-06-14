
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace MyBlog
{



    public class LocalhostConstraint : IRouteConstraint
    {

        public bool Match(HttpContextBase httpContext
            , Route route
            , string parameterName, RouteValueDictionary values
            , RouteDirection routeDirection)
        {
            return httpContext.Request.IsLocal;
        }
    }

	public class RouteConstraintFactory
	{
		public static IRouteConstraint CreateDateTimeConstraint()
		{
			bool c1 = false;

			if (c1)
				return new LocalhostConstraint ();

			if (c1)
				return new ValidMsDateConstraint ();

			return new ValidPgDateConstraint ();
		}

	}


	// 01.12.4714 BC
	// SELECT '4714-12-01T00:00:00 BC'::timestamp
	// SELECT '294277-01-09T04:00:54.775'::timestamp
	public class ValidPgDateConstraint : IRouteConstraint
	{

		public bool Match(HttpContextBase httpContext
			, Route route, string parameterName
			, RouteValueDictionary values
			, RouteDirection routeDirection)
		{
			int year = 0;
			int month = 0;
			int day = 0;

			bool validYear = Int32.TryParse(System.Convert.ToString(values["year"]), out year);
			bool validMonth = Int32.TryParse(System.Convert.ToString(values["month"]), out month);
			bool validDay = Int32.TryParse(System.Convert.ToString(values["day"]), out day);

			if (!validYear || !validMonth || !validDay)
				return false;

			if (year < -4713 || year > 294276)
				return false;

			if (month < 1 || month > 12)
				return false;

			if (day < 1 || day > 31)
				return false;


			try
			{
				if(year > 9999) 
					return true;

				if(year < 1)
					return true;

				System.DateTime dat = new System.DateTime(year, month, day);
				return true;
			}
			catch(Exception)
			{}

			return false;
		}
	}


    public class ValidMsDateConstraint : IRouteConstraint
    {

        public bool Match(HttpContextBase httpContext
            , Route route, string parameterName
            , RouteValueDictionary values
            , RouteDirection routeDirection)
        {
            int year = 0;
            int month = 0;
            int day = 0;

            bool validYear = Int32.TryParse(System.Convert.ToString(values["year"]), out year);
            bool validMonth = Int32.TryParse(System.Convert.ToString(values["month"]), out month);
            bool validDay = Int32.TryParse(System.Convert.ToString(values["day"]), out day);

            if (!validYear || !validMonth || !validDay)
                return false;

			if (year < 1753 || year > 9999)
				return false;

            if (month < 1 || month > 12)
                return false;

            if (day < 1 || day > 31)
                return false;

            
            try
            {
                System.DateTime dat = new System.DateTime(year, month, day);
                return true;
            }
            catch(Exception)
            {}

            return false;
        }
    }



    public class RouteConfig
    {


        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.IgnoreRoute("{resource*}.html");


			/*
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            */


            // http://localhost:2681/2013/12/20/1000
            
            routes.MapRoute(
                 "PostByDate"
                , "{year}/{month}/{day}/{postid}"
                , new { controller = "PostArchive", action = "Index"
                ,
                        year = UrlParameter.Optional,
                        month = UrlParameter.Optional,
                        day = UrlParameter.Optional,
                        postid = UrlParameter.Optional
                } // Parameterstandardwerte

                /*
				, new { year = @"\d{0,4}"
						,month = "(1|2|3|4|5|6|7|8|9|10|11|12)"
						,day = "((1|2|3|4|5|6|7|8|9|10|11|12|13|14|15|16|17|18|19|20|21|22|23|24|25|26|27|28|29|30|31))"
				}
                */
				, new { isValidDate = RouteConstraintFactory.CreateDateTimeConstraint () }

            );



            routes.MapRoute(
                "Default" // Routenname
                , "{controller}/{action}/{id}" // URL mit Parametern
                 //,new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameterstandardwerte
                // ,new { controller = "Blog", action = "NewEntry", id = UrlParameter.Optional } // Parameterstandardwerte
                //, new { controller = "Blog", action = "ShowEntry", id = UrlParameter.Optional } // Parameterstandardwerte
                //, new { controller = "Charts", action = "Test", id = UrlParameter.Optional } // Parameterstandardwerte
				//,new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
                ,new { controller = "Blog", action = "ShowEntry", id = UrlParameter.Optional }
                
                //,new { controller = @"^(?!archive$)" }
                , new { controller = @"^(?!archive$).*" }
            );



            routes.MapRoute(
                 "Archive"
                , "{controller}/{year}/{month}/{day}"
                , new { controller = "Archive", action = "Index", year = UrlParameter.Optional, month = UrlParameter.Optional, day = UrlParameter.Optional } // Parameterstandardwerte
                , new { controller = @"^Archive$" }
            );

#if old 
#endif

#if false
            var xxx = new MyBlog.Trololololol.MySimpleMembershipProvider();
            Console.WriteLine(xxx.GetType().ToString());


            // Type t = typeof(MyBlog.Trololololol.MySimpleMembershipProvider);
            // string aqn = t.AssemblyQualifiedName;
            // Console.WriteLine(aqn);


            System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();
            csb.InitialCatalog = "MapsDb";
            csb.IntegratedSecurity = true;
            csb.DataSource = Environment.MachineName;
            csb.Pooling = false;

            if (!WebMatrix.WebData.WebSecurity.Initialized)
            {
                // WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection("DBRuWho", "customer", "customerId", "email", true);
                WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection
                //MyInitializeDatabaseConnection
                ("crap", "SafeUserTableName", "SafeUserIdColumn", "SafeUserNameColumn", true);
            }

            WebMatrix.WebData.WebSecurity.CreateUserAndAccount("username", "test");

            WebMatrix.WebData.WebSecurity.ChangePassword("username", "old", "new");
#endif
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(System.Web.Routing.RouteTable.Routes);
        }

#if false


        // WebMatrix.WebData.WebSecurity
        //public static void MyInitializeDatabaseConnection(string connectionString, string providerName, string userTableName, string userIdColumn, string userNameColumn, bool autoCreateTables)
        public static void MyInitializeDatabaseConnection(string connectionString, string userTableName, string userIdColumn, string userNameColumn, bool autoCreateTables)
        {
            
            MyInitializeProviders(new DatabaseConnectionInfo
            {
                ConnectionString = connectionString,
                //ProviderName = providerName
            }, userTableName, userIdColumn, userNameColumn, autoCreateTables);
        }


        private static void MyInitializeProviders(DatabaseConnectionInfo connect, string userTableName, string userIdColumn, string userNameColumn, bool autoCreateTables)
        {
            WebMatrix.WebData.SimpleMembershipProvider simpleMembershipProvider = System.Web.Security.Membership.Provider as WebMatrix.WebData.SimpleMembershipProvider;
            if (simpleMembershipProvider != null)
            {
                //WebMatrix.WebData.WebSecurity.InitializeMembershipProvider
                MyInitializeMembershipProvider(simpleMembershipProvider, connect, userTableName, userIdColumn, userNameColumn, autoCreateTables);
            }

            /*
            WebMatrix.WebData.SimpleRoleProvider simpleRoleProvider = System.Web.Security.Roles.Provider as WebMatrix.WebData.SimpleRoleProvider;
            if (simpleRoleProvider != null)
            {
                WebMatrix.WebData.WebSecurity.InitializeRoleProvider(simpleRoleProvider, connect, userTableName, userIdColumn, userNameColumn, autoCreateTables);
            }
            */

            //WebMatrix.WebData.WebSecurity.Initialized = true;
        }


        // WebMatrix.WebData.WebSecurity
        internal static void MyInitializeMembershipProvider(WebMatrix.WebData.SimpleMembershipProvider simpleMembership, DatabaseConnectionInfo connect, string userTableName, string userIdColumn, string userNameColumn, bool createTables)
        {
            
            /*
            if (simpleMembership.InitializeCalled)
            {
                throw new InvalidOperationException("WebDataResources.Security_InitializeAlreadyCalled");
            }
             */
            //simpleMembership.ConnectionInfo = connect;
            simpleMembership.UserIdColumn = userIdColumn;
            simpleMembership.UserNameColumn = userNameColumn;
            simpleMembership.UserTableName = userTableName;
            if (createTables)
            {
                //simpleMembership.CreateTablesIfNeeded();
            }
            else
            {
                //simpleMembership.ValidateUserTable();
            }
            //simpleMembership.InitializeCalled = true;
        }

        internal class DatabaseConnectionInfo
        {

            private enum ConnectionType
            {
                ConnectionStringName,
                ConnectionString
            }

            private string _connectionStringName;
            private string _connectionString;

            public string ConnectionString
            {
                get
                {
                    return this._connectionString;
                }
                set
                {
                    this._connectionString = value;
                    this.Type = DatabaseConnectionInfo.ConnectionType.ConnectionString;
                }
            }

            public string ConnectionStringName
            {
                get
                {
                    return this._connectionStringName;
                }
                set
                {
                    this._connectionStringName = value;
                    this.Type = DatabaseConnectionInfo.ConnectionType.ConnectionStringName;
                }
            }

            public string ProviderName
            {
                get;
                set;
            }

            private DatabaseConnectionInfo.ConnectionType Type
            {
                get;
                set;
            }

            public WebMatrix.Data.Database Connect()
            {
                switch (this.Type)
                {
                    case DatabaseConnectionInfo.ConnectionType.ConnectionStringName:
                        return WebMatrix.Data.Database.Open(this.ConnectionStringName);
                    case DatabaseConnectionInfo.ConnectionType.ConnectionString:
                        return WebMatrix.Data.Database.OpenConnectionString(this.ConnectionString, this.ProviderName);
                    default:
                        return null;
                }
            }
        }

		
		#endif
        

    }


}
