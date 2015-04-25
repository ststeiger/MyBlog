
/*
-- DROP TABLE T_BlogPost;

CREATE TABLE T_BlogPost
(
  BP_UID uuid NOT NULL,
  BP_Title national character varying(200),
  BP_Content text,
  BP_CreoleText text,
  BP_BbCode text,
  BP_HtmlContent text,
  BP_EntryDate timestamp without time zone,
  AutoAudit_CreatedDate timestamp without time zone,
  AutoAudit_CreatedBy national character varying(128),
  AutoAudit_ModifiedDate timestamp without time zone,
  AutoAudit_ModifiedBy national character varying(128),
  AutoAudit_RowVersion integer
);



CREATE TABLE T_BlogPost
(
  BP_UID uniqueidentifier NOT NULL,
  BP_Title national character varying(200),
  BP_Content text,
  BP_CreoleText text,
  BP_BbCode text,
  BP_HtmlContent text,
  BP_EntryDate datetime, -- varchar(50)
  AutoAudit_CreatedDate datetime,
  AutoAudit_CreatedBy national character varying(128),
  AutoAudit_ModifiedDate datetime,
  AutoAudit_ModifiedBy national character varying(128),
  AutoAudit_RowVersion integer
);
*/

-- UPDATE T_BlogPost SET BP_EntryDate = SUBSTRING(BP_EntryDate, 1, 23);
DELETE FROM T_BlogPost;

INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('0c15042a-b696-4356-b860-21069baad697', 'ASP.NET Forms authentication is broken !', 'How the cookie is encrypted and made tamper-proof is configured in the machineKey element in the application web.config. The standard ASP.NET behaviour is to AutoGenerate the keys. This setting causes new keys to be generated every time the application pool is started. If a user shows up on the site with a cookie created with old keys, the cookie is not recognized and the user will have to log in again. This is not desirable, especially since IIS may decide to stop an inactive application to conserve resources and then restart it when a visitor shows up.


Reference:
http://blog.appharbor.com/2012/02/22/asp-net-forms-authentication-considered-broken



http://www.codeproject.com/Articles/5353/Custom-Authentication-provider-by-implementing-IHt



http://support.microsoft.com/kb/910443/en-us


http://villagecoder.wordpress.com/2008/08/29/custom-forms-authentication-in-aspnet/

', NULL, NULL, 'How the cookie is encrypted and made tamper-proof is configured in the machineKey element in the application web.config. The standard ASP.NET behaviour is to AutoGenerate the keys. This setting causes new keys to be generated every time the application pool is started. If a user shows up on the site with a cookie created with old keys, the cookie is not recognized and the user will have to log in again. This is not desirable, especially since IIS may decide to stop an inactive application to conserve resources and then restart it when a visitor shows up.<br /><br /><br />Reference:<br /><a target="_blank" href="http://blog.appharbor.com/2012/02/22/asp-net-forms-authentication-considered-broken">http://blog.appharbor.com/2012/02/22/asp-net-forms-authentication-considered-broken</a><br /><br /><br /><br /><a target="_blank" href="http://www.codeproject.com/Articles/5353/Custom-Authentication-provider-by-implementing-IHt">http://www.codeproject.com/Articles/5353/Custom-Authentication-provider-by-implementing-IHt</a><br /><br /><br /><br /><a target="_blank" href="http://support.microsoft.com/kb/910443/en-us">http://support.microsoft.com/kb/910443/en-us</a><br /><br /><br /><a target="_blank" href="http://villagecoder.wordpress.com/2008/08/29/custom-forms-authentication-in-aspnet/">http://villagecoder.wordpress.com/2008/08/29/custom-forms-authentication-in-aspnet/</a><br /><br />', '2013-05-03 08:54:56.68', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) 
VALUES ('e3cfce85-589b-4327-8665-25db76f4098c', 
'A pinvoke library for Windows dlls', 'pinvoke  

Platform Invocation Services, commonly referred to as P/Invoke, is a feature of Common Language Infrastructure implementations, like Microsoft''s Common Language Runtime, that enables managed code to call native code.

www.pinvoke.net'
, NULL
, NULL
, 'pinvoke  <br /><br />Platform Invocation Services, commonly referred to as P/Invoke, is a feature of Common Language Infrastructure implementations, like Microsoft''s Common Language Runtime, that enables managed code to call native code.<br /><br /><a target="_blank" href="www.pinvoke.net">www.pinvoke.net</a>'
, '2013-05-03 11:09:22.993333'
, NULL, NULL, NULL, NULL, 1);





INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('1bba960e-ae49-4d91-a795-287936a4b4ea', 'Windows 8 cannot connect to WLAN', 'Systemsteuerung\Netzwerk und Internet\Netzwerkverbindungen -->
Control Panel\Network and Internet\Network Connections
or

ncpa.cpl




Right-click on wifi
--> Properties 
click on "Configure" (Konfigurieren)
Click on "Advanced" (Erweitert) tab

Select wireless mode

you''ll have
1) 802.11b
2) 802.11g
3) 802.11b/g

it will be on 3.

You Need to Switch it to 1)

Then you will be able to connect to your WLAN

If only you knew how Long it took me to find that out', NULL, NULL, 'Systemsteuerung\Netzwerk und Internet\Netzwerkverbindungen --><br />Control Panel\Network and Internet\Network Connections<br />or<br /><br />ncpa.cpl<br /><br /><br /><br /><br />Right-click on wifi<br />--> Properties <br />click on "Configure" (Konfigurieren)<br />Click on "Advanced" (Erweitert) tab<br /><br />Select wireless mode<br /><br />you''ll have<br />1) 802.11b<br />2) 802.11g<br />3) 802.11b/g<br /><br />it will be on 3.<br /><br />You Need to Switch it to 1)<br /><br />Then you will be able to connect to your WLAN<br /><br />If only you knew how Long it took me to find that out', '2013-05-08 20:27:49.766667', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('d49d6d80-d689-450c-9576-34aa90746ff4', 'Scaling web applications', 'http://www.quora.com/Quora-Infrastructure/Why-does-Quora-use-MySQL-as-the-data-store-instead-of-NoSQLs-such-as-Cassandra-MongoDB-or-CouchDB

http://de.slideshare.net/buunguyen/building-scalable-net-web-applications

http://www.magentocommerce.com/boards/viewthread/278526/

http://stackoverflow.com/questions/10558465/memcache-vs-redis

http://blog.stevensanderson.com/2008/04/05/improve-scalability-in-aspnet-mvc-using-asynchronous-requests/

http://highscalability.com/blog/2009/8/5/stack-overflow-architecture.html

', NULL, NULL, '<a target="_blank" href="http://www.quora.com/Quora-Infrastructure/Why-does-Quora-use-MySQL-as-the-data-store-instead-of-NoSQLs-such-as-Cassandra-MongoDB-or-CouchDB">http://www.quora.com/Quora-Infrastructure/Why-does-Quora-use-MySQL-as-the-data-store-instead-of-NoSQLs-such-as-Cassandra-MongoDB-or-CouchDB</a><br /><br /><a target="_blank" href="http://de.slideshare.net/buunguyen/building-scalable-net-web-applications">http://de.slideshare.net/buunguyen/building-scalable-net-web-applications</a><br /><br /><a target="_blank" href="http://www.magentocommerce.com/boards/viewthread/278526/">http://www.magentocommerce.com/boards/viewthread/278526/</a><br /><br /><a target="_blank" href="http://stackoverflow.com/questions/10558465/memcache-vs-redis">http://stackoverflow.com/questions/10558465/memcache-vs-redis</a><br /><br /><a target="_blank" href="http://blog.stevensanderson.com/2008/04/05/improve-scalability-in-aspnet-mvc-using-asynchronous-requests/">http://blog.stevensanderson.com/2008/04/05/improve-scalability-in-aspnet-mvc-using-asynchronous-requests/</a><br /><br /><a target="_blank" href="http://highscalability.com/blog/2009/8/5/stack-overflow-architecture.html">http://highscalability.com/blog/2009/8/5/stack-overflow-architecture.html</a><br /><br />', '2013-05-03 10:53:09.026667', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('2f430af8-d7c2-4d8b-8f2f-3abfe037cb32', '.NET datatype interop with native types', 'http://msdn.microsoft.com/en-us/library/ya5y69ds.aspx', NULL, NULL, '<a target="_blank" href="http://msdn.microsoft.com/en-us/library/ya5y69ds.aspx">http://msdn.microsoft.com/en-us/library/ya5y69ds.aspx</a>', '2013-05-03 10:54:04.153333', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('a2a0cbc8-7ca0-42d9-b719-40d71c6cf106', 'SSRS 2012 - No access to admin page', 'Immediately after installation, 
I have no access to the reports admin page (even when running as admin) 



thanks! issue solved.
I had to add localhost to local intranet, then run as admin, and all was fine.



Thank you for filing this issue. Two things you can try:
1) use the machine name in the URL rather than using localhost.
2) determine if the Admin account on your computer has a password specified. 
If not, specify a password and see if this resolves the access denied issue.


http://serverfault.com/questions/331852/allow-anonymous-access-to-ssrs



', NULL, NULL, 'Immediately after installation, <br />I have no access to the reports admin page (even when running as admin) <br /><br /><br /><br />thanks! issue solved.<br />I had to add localhost to local intranet, then run as admin, and all was fine.<br /><br /><br /><br />Thank you for filing this issue. Two things you can try:<br />1) use the machine name in the URL rather than using localhost.<br />2) determine if the Admin account on your computer has a password specified. <br />If not, specify a password and see if this resolves the access denied issue.<br /><br /><br /><a target="_blank" href="http://serverfault.com/questions/331852/allow-anonymous-access-to-ssrs">http://serverfault.com/questions/331852/allow-anonymous-access-to-ssrs</a><br /><br /><br /><br />', '2013-05-07 12:09:39.816667', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('45ee1de8-8002-4ec6-91d5-482200a16ebc', 'SQL Server: CountDistinct is possible !', 'SELECT COUNT(DISTINCT program_name) AS Count,
  program_type AS [Type] 
FROM cm_production 
WHERE push_number=@push_number 
GROUP BY program_type

Reference:
http://stackoverflow.com/questions/1521605/sql-server-query-selecting-count-with-distinct', NULL, NULL, 'SELECT COUNT(DISTINCT program_name) AS Count,<br />  program_type AS [Type] <br />FROM cm_production <br />WHERE push_number=@push_number <br />GROUP BY program_type<br /><br />Reference:<br /><a target="_blank" href="http://stackoverflow.com/questions/1521605/sql-server-query-selecting-count-with-distinct">http://stackoverflow.com/questions/1521605/sql-server-query-selecting-count-with-distinct</a>', '2013-05-03 08:51:09.503333', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('a51f8f42-38ae-407b-8baa-6b661cd9b39d', 'Comparison of blog software', 'http://blog-software-review.toptenreviews.com/




http://www.weblogmatrix.org/compare/dasBlog+Movable-Type+Wordpress




http://www.gutgames.com/post/Using-Akismets-API-In-C.aspx



http://www.java2v.com/Open-Source/CSharp/Bloggers/BlogEngine.NET/Joel/Net/AkismetFilter.cs.htm



', NULL, NULL, '<a target="_blank" href="http://blog-software-review.toptenreviews.com/">http://blog-software-review.toptenreviews.com/</a><br /><br /><br /><br /><br /><a target="_blank" href="http://www.weblogmatrix.org/compare/dasBlog+Movable-Type+Wordpress">http://www.weblogmatrix.org/compare/dasBlog+Movable-Type+Wordpress</a><br /><br /><br /><br /><br /><a target="_blank" href="http://www.gutgames.com/post/Using-Akismets-API-In-C.aspx">http://www.gutgames.com/post/Using-Akismets-API-In-C.aspx</a><br /><br /><br /><br /><a target="_blank" href="http://www.java2v.com/Open-Source/CSharp/Bloggers/BlogEngine.NET/Joel/Net/AkismetFilter.cs.htm">http://www.java2v.com/Open-Source/CSharp/Bloggers/BlogEngine.NET/Joel/Net/AkismetFilter.cs.htm</a><br /><br /><br /><br />', '2013-05-03 11:01:27.91', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('1b0f956a-31fa-4f29-9898-767299cff9c7', 'Building a blog with RavenDB', 'http://www.dotnetcurry.com/ShowArticle.aspx?ID=787', NULL, NULL, '<a target="_blank" href="http://www.dotnetcurry.com/ShowArticle.aspx?ID=787">http://www.dotnetcurry.com/ShowArticle.aspx?ID=787</a>', '2013-05-03 11:04:19.653333', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('c5d8f62e-bb4f-46d8-8813-8d0441aecc2d', 'Getting started with MongoDB', 'http://www.codeproject.com/Articles/87757/MongoDB-and-C', NULL, NULL, '<a target="_blank" href="http://www.codeproject.com/Articles/87757/MongoDB-and-C">http://www.codeproject.com/Articles/87757/MongoDB-and-C</a>', '2013-05-03 11:01:40.336667', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('82309682-5f60-45f2-8771-9138c10cb12e', 'A comparison between DB engines', 'http://db-engines.com/en/ranking', NULL, NULL, '<a target="_blank" href="http://db-engines.com/en/ranking">http://db-engines.com/en/ranking</a>', '2013-05-03 10:57:15.55', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('62064738-144a-4be7-8c6e-9f0e2cabda8d', 'HyperTable is here !', '
Build / Install:

http://code.google.com/p/hypertable/wiki/HowToBuild

http://code.google.com/p/hypertable/wiki/HowToBuild#Ubuntu_9.04_Jaunty_Jackalope_32-bit

http://code.google.com/p/hypertable/wiki/HowToInstall

http://hypertable.com/documentation/installation/quick_start_standalone/


Info:

http://de.slideshare.net/adorepump/hypertable-nosql

http://hypertable.com/why_hypertable/

http://code.google.com/p/hypertable/wiki/HyperRecord

http://code.google.com/p/hypertable/wiki/WhyWeChoseCppOverJava

http://hypertable.com/why_hypertable/hypertable_vs_hbase_2/



https://groups.google.com/forum/?fromgroups#!topic/hypertable-dev/K3tPn6LGmRQ

https://groups.google.com/forum/?fromgroups#!topic/hypertable-user/ygLe_JapGQM


', NULL, NULL, '<br />Build / Install:<br /><br /><a target="_blank" href="http://code.google.com/p/hypertable/wiki/HowToBuild">http://code.google.com/p/hypertable/wiki/HowToBuild</a><br /><br /><a target="_blank" href="http://code.google.com/p/hypertable/wiki/HowToBuild#Ubuntu_9.04_Jaunty_Jackalope_32-bit">http://code.google.com/p/hypertable/wiki/HowToBuild#Ubuntu_9.04_Jaunty_Jackalope_32-bit</a><br /><br /><a target="_blank" href="http://code.google.com/p/hypertable/wiki/HowToInstall">http://code.google.com/p/hypertable/wiki/HowToInstall</a><br /><br /><a target="_blank" href="http://hypertable.com/documentation/installation/quick_start_standalone/">http://hypertable.com/documentation/installation/quick_start_standalone/</a><br /><br /><br />Info:<br /><br /><a target="_blank" href="http://de.slideshare.net/adorepump/hypertable-nosql">http://de.slideshare.net/adorepump/hypertable-nosql</a><br /><br /><a target="_blank" href="http://hypertable.com/why_hypertable/">http://hypertable.com/why_hypertable/</a><br /><br /><a target="_blank" href="http://code.google.com/p/hypertable/wiki/HyperRecord">http://code.google.com/p/hypertable/wiki/HyperRecord</a><br /><br /><a target="_blank" href="http://code.google.com/p/hypertable/wiki/WhyWeChoseCppOverJava">http://code.google.com/p/hypertable/wiki/WhyWeChoseCppOverJava</a><br /><br /><a target="_blank" href="http://hypertable.com/why_hypertable/hypertable_vs_hbase_2/">http://hypertable.com/why_hypertable/hypertable_vs_hbase_2/</a><br /><br /><br /><br /><a target="_blank" href="https://groups.google.com/forum/?fromgroups#!topic/hypertable-dev/K3tPn6LGmRQ">https://groups.google.com/forum/?fromgroups#!topic/hypertable-dev/K3tPn6LGmRQ</a><br /><br /><a target="_blank" href="https://groups.google.com/forum/?fromgroups#!topic/hypertable-user/ygLe_JapGQM">https://groups.google.com/forum/?fromgroups#!topic/hypertable-user/ygLe_JapGQM</a><br /><br /><br />', '2013-05-03 10:56:37.983333', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('2d98bf47-f014-41d2-98b8-b3bce2dc5ebb', 'jQuery drag and drop columns', 'http://gridster.net/

', NULL, NULL, '<a target="_blank" href="http://gridster.net/">http://gridster.net/</a><br /><br />', '2013-05-03 10:52:20.506667', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('8c65b4e8-ac48-467f-9c56-b5ed32da0806', 'Family tree with jQuery', 'It''s possible with the Google Visualization API, it''s well documented: http://code.google.com/apis/visualization/documentation/gallery/orgchart.html#Example

Or using jQuery Orgchart:
https://github.com/caprica/jquery-orgchart

http://th3silverlining.com/2011/12/01/jquery-org-chart-a-plugin-for-visualising-data-in-a-tree-like-structure/

http://dl.dropboxusercontent.com/u/4151695/html/jOrgChart/example/example.html

D:\Stefan.Steiger\Desktop\jquery-orgchart-master\jquery-orgchart-master\demo\dogfood.html



https://developers.google.com/chart/interactive/docs/gallery/orgchart



Reference:
http://stackoverflow.com/questions/3454316/family-tree-design-suggestions', NULL, NULL, 'It''s possible with the Google Visualization API, it''s well documented: <a target="_blank" href="http://code.google.com/apis/visualization/documentation/gallery/orgchart.html#Example">http://code.google.com/apis/visualization/documentation/gallery/orgchart.html#Example</a><br /><br />Or using jQuery Orgchart:<br /><a target="_blank" href="https://github.com/caprica/jquery-orgchart">https://github.com/caprica/jquery-orgchart</a><br /><br /><a target="_blank" href="http://th3silverlining.com/2011/12/01/jquery-org-chart-a-plugin-for-visualising-data-in-a-tree-like-structure/">http://th3silverlining.com/2011/12/01/jquery-org-chart-a-plugin-for-visualising-data-in-a-tree-like-structure/</a><br /><br /><a target="_blank" href="http://dl.dropboxusercontent.com/u/4151695/html/jOrgChart/example/example.html">http://dl.dropboxusercontent.com/u/4151695/html/jOrgChart/example/example.html</a><br /><br /><a target="_blank" href="file:///D:/Stefan.Steiger/Desktop/jquery-orgchart-master/jquery-orgchart-master/demo/dogfood.html">D:\Stefan.Steiger\Desktop\jquery-orgchart-master\jquery-orgchart-master\demo\dogfood.html</a><br /><br /><br /><br /><a target="_blank" href="https://developers.google.com/chart/interactive/docs/gallery/orgchart">https://developers.google.com/chart/interactive/docs/gallery/orgchart</a><br /><br /><br /><br />Reference:<br /><a target="_blank" href="http://stackoverflow.com/questions/3454316/family-tree-design-suggestions">http://stackoverflow.com/questions/3454316/family-tree-design-suggestions</a>', '2013-05-03 08:53:03.99', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('0a52ac79-0cf4-46e4-a08d-c9a62c553143', 'jQuery Gantt charts', '

http://taitems.github.io/jQuery.Gantt/

https://github.com/taitems/jQuery.Gantt



http://mbielanczuk.com/jquery-gantt/



', NULL, NULL, '<br /><br /><a target="_blank" href="http://taitems.github.io/jQuery.Gantt/">http://taitems.github.io/jQuery.Gantt/</a><br /><br /><a target="_blank" href="https://github.com/taitems/jQuery.Gantt">https://github.com/taitems/jQuery.Gantt</a><br /><br /><br /><br /><a target="_blank" href="http://mbielanczuk.com/jquery-gantt/">http://mbielanczuk.com/jquery-gantt/</a><br /><br /><br /><br />', '2013-05-03 10:46:40.493333', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('b9869bad-bb9e-47e6-87cb-cd3f4890f255', 'Redis as memcached replacement', '
http://www.servicestack.net/mythz_blog/?p=474



http://www.servicestack.net/RedisStackOverflow/#!questions/Latest_Questions


https://github.com/ServiceStack/ServiceStack.Examples/blob/master/src/RedisStackOverflow/RedisStackOverflow.ServiceInterface/IRepository.cs



https://github.com/ServiceStack/ServiceStack.Redis


http://stackoverflow.com/questions/5006326/start-using-redis-with-asp-net








', NULL, NULL, '<br /><a target="_blank" href="http://www.servicestack.net/mythz_blog/?p=474">http://www.servicestack.net/mythz_blog/?p=474</a><br /><br /><br /><br /><a target="_blank" href="http://www.servicestack.net/RedisStackOverflow/#!questions/Latest_Questions">http://www.servicestack.net/RedisStackOverflow/#!questions/Latest_Questions</a><br /><br /><br /><a target="_blank" href="https://github.com/ServiceStack/ServiceStack.Examples/blob/master/src/RedisStackOverflow/RedisStackOverflow.ServiceInterface/IRepository.cs">https://github.com/ServiceStack/ServiceStack.Examples/blob/master/src/RedisStackOverflow/RedisStackOverflow.ServiceInterface/IRepository.cs</a><br /><br /><br /><br /><a target="_blank" href="https://github.com/ServiceStack/ServiceStack.Redis">https://github.com/ServiceStack/ServiceStack.Redis</a><br /><br /><br /><a target="_blank" href="http://stackoverflow.com/questions/5006326/start-using-redis-with-asp-net">http://stackoverflow.com/questions/5006326/start-using-redis-with-asp-net</a><br /><br /><br /><br /><br /><br /><br /><br /><br />', '2013-05-03 11:00:35.346667', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('e0f8def8-4e45-45bb-aa8e-d002b77098a8', 'Multi-File upload is now possible with IE 10', 'http://blueimp.github.io/jQuery-File-Upload/', NULL, NULL, '<a target="_blank" href="http://blueimp.github.io/jQuery-File-Upload/">http://blueimp.github.io/jQuery-File-Upload/</a>', '2013-05-03 10:49:55.77', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('ae0cdf7f-c42e-4346-93dd-d4444d93f087', 'A comparison of NoSQL systems', 'http://kkovacs.eu/cassandra-vs-mongodb-vs-couchdb-vs-redis


http://stackoverflow.com/questions/1777103/what-nosql-solutions-are-out-there-for-net


', NULL, NULL, '<a target="_blank" href="http://kkovacs.eu/cassandra-vs-mongodb-vs-couchdb-vs-redis">http://kkovacs.eu/cassandra-vs-mongodb-vs-couchdb-vs-redis</a><br /><br /><br /><a target="_blank" href="http://stackoverflow.com/questions/1777103/what-nosql-solutions-are-out-there-for-net">http://stackoverflow.com/questions/1777103/what-nosql-solutions-are-out-there-for-net</a><br /><br /><br />', '2013-05-03 10:55:31.356667', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('e49fd978-fd4f-499a-aa01-d5fc68afd8c9', 'It''s possible to perform cross-database queries with postgres !', 'databaseA=# select * from databaseB.public.someTableName;
ERROR:  cross-database references are not implemented:
 "databaseB.public.someTableName"


This functionality isn''t part of the default PostgreSQL install, but you can add it in. It''s called dblink.


Yes, you can by using DBlink (postgresql only) and DBI-Link (allows foreign cross database queriers) and TDS_LInk which allows queries to be run against MS SQL server.

I have used DB-Link and TDS-link before with great success.

Need to install postgresql-contrib 


Reference:
http://stackoverflow.com/questions/46324/possible-to-perform-cross-database-queries-with-postgres', NULL, NULL, 'databaseA=# select * from databaseB.public.someTableName;<br />ERROR:  cross-database references are not implemented:<br /> "databaseB.public.someTableName"<br /><br /><br />This functionality isn''t part of the default PostgreSQL install, but you can add it in. It''s called dblink.<br /><br /><br />Yes, you can by using DBlink (postgresql only) and DBI-Link (allows foreign cross database queriers) and TDS_LInk which allows queries to be run against MS SQL server.<br /><br />I have used DB-Link and TDS-link before with great success.<br /><br />Need to install postgresql-contrib <br /><br /><br />Reference:<br /><a target="_blank" href="http://stackoverflow.com/questions/46324/possible-to-perform-cross-database-queries-with-postgres">http://stackoverflow.com/questions/46324/possible-to-perform-cross-database-queries-with-postgres</a>', '2013-05-03 08:49:40.463333', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('a65bfd06-cc23-4495-8c51-d9d2787218a5', '40 Awsome jQuery plugins', 'http://designshack.net/articles/javascript/40-awesome-jquery-plugins-you-need-to-check-out/', NULL, NULL, '<a target="_blank" href="http://designshack.net/articles/javascript/40-awesome-jquery-plugins-you-need-to-check-out/">http://designshack.net/articles/javascript/40-awesome-jquery-plugins-you-need-to-check-out/</a>', '2013-05-03 10:50:24.936667', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('91b8fa4d-89ea-4791-aca3-ef4812c26368', 'Thrift vs. SOAP', 'http://www.linkedin.com/groups/Comparison-between-thrift-SOAP-50472.S.88711555

http://myblog.rsynnott.com/2007/12/facebook-thrift-less-horrific-than-soap.html', NULL, NULL, '<a target="_blank" href="http://www.linkedin.com/groups/Comparison-between-thrift-SOAP-50472.S.88711555">http://www.linkedin.com/groups/Comparison-between-thrift-SOAP-50472.S.88711555</a><br /><br /><a target="_blank" href="http://myblog.rsynnott.com/2007/12/facebook-thrift-less-horrific-than-soap.html">http://myblog.rsynnott.com/2007/12/facebook-thrift-less-horrific-than-soap.html</a>', '2013-05-03 10:53:38.42', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('b6a91e9a-a829-46b5-8156-f87366a0a4a5', 'An entire regex library', 'http://regexlib.com/Search.aspx?k=url&AspxAutoDetectCookieSupport=1', NULL, NULL, '<a target="_blank" href="http://regexlib.com/Search.aspx?k=url&AspxAutoDetectCookieSupport=1">http://regexlib.com/Search.aspx?k=url&AspxAutoDetectCookieSupport=1</a>', '2013-05-03 11:07:47.083333', NULL, NULL, NULL, NULL, 1);
INSERT INTO T_BlogPost (BP_UID, BP_Title, BP_Content, BP_CreoleText, BP_BbCode, BP_HtmlContent, BP_EntryDate, AutoAudit_CreatedDate, AutoAudit_CreatedBy, AutoAudit_ModifiedDate, AutoAudit_ModifiedBy, AutoAudit_RowVersion) VALUES ('d64db6db-a7a0-4953-8a92-fdb2a0f9a315', 'RavenDB', '
http://nosql.mypopescu.com/post/346471814/usecase-nosql-based-blogs



http://byterot.blogspot.ch/2012/11/nosql-benchmark-redis-mongodb-ravendb-cassandra-sqlserver.html




http://nosql.mypopescu.com/post/19392225838/ravendb-tutorial-building-an-asp-net-mvc-app-using



http://ravendb.net/docs/intro/quickstart


http://www.dotnetcurry.com/ShowArticle.aspx?ID=787

', NULL, NULL, '<br /><a target="_blank" href="http://nosql.mypopescu.com/post/346471814/usecase-nosql-based-blogs">http://nosql.mypopescu.com/post/346471814/usecase-nosql-based-blogs</a><br /><br /><br /><br /><a target="_blank" href="http://byterot.blogspot.ch/2012/11/nosql-benchmark-redis-mongodb-ravendb-cassandra-sqlserver.html">http://byterot.blogspot.ch/2012/11/nosql-benchmark-redis-mongodb-ravendb-cassandra-sqlserver.html</a><br /><br /><br /><br /><br /><a target="_blank" href="http://nosql.mypopescu.com/post/19392225838/ravendb-tutorial-building-an-asp-net-mvc-app-using">http://nosql.mypopescu.com/post/19392225838/ravendb-tutorial-building-an-asp-net-mvc-app-using</a><br /><br /><br /><br /><a target="_blank" href="http://ravendb.net/docs/intro/quickstart">http://ravendb.net/docs/intro/quickstart</a><br /><br /><br /><a target="_blank" href="http://www.dotnetcurry.com/ShowArticle.aspx?ID=787">http://www.dotnetcurry.com/ShowArticle.aspx?ID=787</a><br /><br />', '2013-05-03 11:03:52.903333', NULL, NULL, NULL, NULL, 1);


-- Completed on 2015-04-25 18:59:54 CEST
