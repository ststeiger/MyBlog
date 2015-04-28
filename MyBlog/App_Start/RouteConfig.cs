
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace MyBlog
{


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

				, new { year = @"\d{0,4}"
						,month = "(1|2|3|4|5|6|7|8|9|10|11|12)"
						,day = "((1|2|3|4|5|6|7|8|9|10|11|12|13|14|15|16|17|18|19|20|21|22|23|24|25|26|27|28|29|30|31))"
				}
            );



#if old 

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
