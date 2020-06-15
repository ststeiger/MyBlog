
INSERT INTO T_BlogPost
(
	 BP_UID
	,BP_Title
	,BP_Content
	,BP_CreoleText
	,BP_BBCode
	,BP_HtmlContent
	,BP_EntryDate
	,AutoAudit_CreatedDate
	,AutoAudit_CreatedBy
	,AutoAudit_ModifiedDate
	,AutoAudit_ModifiedBy
	,AutoAudit_RowVersion
) 
SELECT 
     (xpath('//bp_uid/text()', myTempTable.myXmlColumn))[1]::text::uuid AS bp_uid
    ,(xpath('//bp_title/text()', myTempTable.myXmlColumn))[1]::text::character varying AS bp_title
    ,(xpath('//bp_content/text()', myTempTable.myXmlColumn))[1]::text::text AS bp_content
    ,(xpath('//bp_creoletext/text()', myTempTable.myXmlColumn))[1]::text::text AS bp_creoletext
    ,(xpath('//bp_bbcode/text()', myTempTable.myXmlColumn))[1]::text::text AS bp_bbcode
    ,(xpath('//bp_htmlcontent/text()', myTempTable.myXmlColumn))[1]::text::text AS bp_htmlcontent
    ,(xpath('//bp_entrydate/text()', myTempTable.myXmlColumn))[1]::text::timestamp without time zone AS bp_entrydate
    ,(xpath('//autoaudit_createddate/text()', myTempTable.myXmlColumn))[1]::text::timestamp without time zone AS autoaudit_createddate
    ,(xpath('//autoaudit_createdby/text()', myTempTable.myXmlColumn))[1]::text::character varying AS autoaudit_createdby
    ,(xpath('//autoaudit_modifieddate/text()', myTempTable.myXmlColumn))[1]::text::timestamp without time zone AS autoaudit_modifieddate
    ,(xpath('//autoaudit_modifiedby/text()', myTempTable.myXmlColumn))[1]::text::character varying AS autoaudit_modifiedby
    ,(xpath('//autoaudit_rowversion/text()', myTempTable.myXmlColumn))[1]::text::integer AS autoaudit_rowversion

    -- ,myTempTable.myXmlColumn as myXmlElement 
    -- Source: https://en.wikipedia.org/wiki/List_of_DNS_record_types
FROM unnest(xpath('//row', 
 CAST('<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<t_blogpost xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

<row>
  <bp_uid>0c15042a-b696-4356-b860-21069baad697</bp_uid>
  <bp_title>ASP.NET Forms authentication is broken !</bp_title>
  <bp_content>How the cookie is encrypted and made tamper-proof is configured in the machineKey element in the application web.config. The standard ASP.NET behaviour is to AutoGenerate the keys. This setting causes new keys to be generated every time the application pool is started. If a user shows up on the site with a cookie created with old keys, the cookie is not recognized and the user will have to log in again. This is not desirable, especially since IIS may decide to stop an inactive application to conserve resources and then restart it when a visitor shows up.


Reference:
http://blog.appharbor.com/2012/02/22/asp-net-forms-authentication-considered-broken



http://www.codeproject.com/Articles/5353/Custom-Authentication-provider-by-implementing-IHt



http://support.microsoft.com/kb/910443/en-us


http://villagecoder.wordpress.com/2008/08/29/custom-forms-authentication-in-aspnet/

</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>How the cookie is encrypted and made tamper-proof is configured in the machineKey element in the application web.config. The standard ASP.NET behaviour is to AutoGenerate the keys. This setting causes new keys to be generated every time the application pool is started. If a user shows up on the site with a cookie created with old keys, the cookie is not recognized and the user will have to log in again. This is not desirable, especially since IIS may decide to stop an inactive application to conserve resources and then restart it when a visitor shows up.&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;Reference:&lt;br /&gt;&lt;a target="_blank" href="http://blog.appharbor.com/2012/02/22/asp-net-forms-authentication-considered-broken"&gt;http://blog.appharbor.com/2012/02/22/asp-net-forms-authentication-considered-broken&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://www.codeproject.com/Articles/5353/Custom-Authentication-provider-by-implementing-IHt"&gt;http://www.codeproject.com/Articles/5353/Custom-Authentication-provider-by-implementing-IHt&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://support.microsoft.com/kb/910443/en-us"&gt;http://support.microsoft.com/kb/910443/en-us&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://villagecoder.wordpress.com/2008/08/29/custom-forms-authentication-in-aspnet/"&gt;http://villagecoder.wordpress.com/2008/08/29/custom-forms-authentication-in-aspnet/&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T08:54:56.68</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>e3cfce85-589b-4327-8665-25db76f4098c</bp_uid>
  <bp_title>A pinvoke library for Windows dlls</bp_title>
  <bp_content>pinvoke  

Platform Invocation Services, commonly referred to as P/Invoke, is a feature of Common Language Infrastructure implementations, like Microsoft''s Common Language Runtime, that enables managed code to call native code.

www.pinvoke.net</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>pinvoke  &lt;br /&gt;&lt;br /&gt;Platform Invocation Services, commonly referred to as P/Invoke, is a feature of Common Language Infrastructure implementations, like Microsoft''s Common Language Runtime, that enables managed code to call native code.&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="www.pinvoke.net"&gt;www.pinvoke.net&lt;/a&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T11:09:22.993333</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>1bba960e-ae49-4d91-a795-287936a4b4ea</bp_uid>
  <bp_title>Windows 8 cannot connect to WLAN</bp_title>
  <bp_content>Systemsteuerung\Netzwerk und Internet\Netzwerkverbindungen --&gt;
Control Panel\Network and Internet\Network Connections
or

ncpa.cpl




Right-click on wifi
--&gt; Properties 
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

If only you knew how Long it took me to find that out</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>Systemsteuerung\Netzwerk und Internet\Netzwerkverbindungen --&gt;&lt;br /&gt;Control Panel\Network and Internet\Network Connections&lt;br /&gt;or&lt;br /&gt;&lt;br /&gt;ncpa.cpl&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;Right-click on wifi&lt;br /&gt;--&gt; Properties &lt;br /&gt;click on "Configure" (Konfigurieren)&lt;br /&gt;Click on "Advanced" (Erweitert) tab&lt;br /&gt;&lt;br /&gt;Select wireless mode&lt;br /&gt;&lt;br /&gt;you''ll have&lt;br /&gt;1) 802.11b&lt;br /&gt;2) 802.11g&lt;br /&gt;3) 802.11b/g&lt;br /&gt;&lt;br /&gt;it will be on 3.&lt;br /&gt;&lt;br /&gt;You Need to Switch it to 1)&lt;br /&gt;&lt;br /&gt;Then you will be able to connect to your WLAN&lt;br /&gt;&lt;br /&gt;If only you knew how Long it took me to find that out</bp_htmlcontent>
  <bp_entrydate>2013-05-08T20:27:49.766667</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>d49d6d80-d689-450c-9576-34aa90746ff4</bp_uid>
  <bp_title>Scaling web applications</bp_title>
  <bp_content>http://www.quora.com/Quora-Infrastructure/Why-does-Quora-use-MySQL-as-the-data-store-instead-of-NoSQLs-such-as-Cassandra-MongoDB-or-CouchDB

http://de.slideshare.net/buunguyen/building-scalable-net-web-applications

http://www.magentocommerce.com/boards/viewthread/278526/

http://stackoverflow.com/questions/10558465/memcache-vs-redis

http://blog.stevensanderson.com/2008/04/05/improve-scalability-in-aspnet-mvc-using-asynchronous-requests/

http://highscalability.com/blog/2009/8/5/stack-overflow-architecture.html

</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>&lt;a target="_blank" href="http://www.quora.com/Quora-Infrastructure/Why-does-Quora-use-MySQL-as-the-data-store-instead-of-NoSQLs-such-as-Cassandra-MongoDB-or-CouchDB"&gt;http://www.quora.com/Quora-Infrastructure/Why-does-Quora-use-MySQL-as-the-data-store-instead-of-NoSQLs-such-as-Cassandra-MongoDB-or-CouchDB&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://de.slideshare.net/buunguyen/building-scalable-net-web-applications"&gt;http://de.slideshare.net/buunguyen/building-scalable-net-web-applications&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://www.magentocommerce.com/boards/viewthread/278526/"&gt;http://www.magentocommerce.com/boards/viewthread/278526/&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://stackoverflow.com/questions/10558465/memcache-vs-redis"&gt;http://stackoverflow.com/questions/10558465/memcache-vs-redis&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://blog.stevensanderson.com/2008/04/05/improve-scalability-in-aspnet-mvc-using-asynchronous-requests/"&gt;http://blog.stevensanderson.com/2008/04/05/improve-scalability-in-aspnet-mvc-using-asynchronous-requests/&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://highscalability.com/blog/2009/8/5/stack-overflow-architecture.html"&gt;http://highscalability.com/blog/2009/8/5/stack-overflow-architecture.html&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T10:53:09.026667</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>2f430af8-d7c2-4d8b-8f2f-3abfe037cb32</bp_uid>
  <bp_title>.NET datatype interop with native types</bp_title>
  <bp_content>http://msdn.microsoft.com/en-us/library/ya5y69ds.aspx</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>&lt;a target="_blank" href="http://msdn.microsoft.com/en-us/library/ya5y69ds.aspx"&gt;http://msdn.microsoft.com/en-us/library/ya5y69ds.aspx&lt;/a&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T10:54:04.153333</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>a2a0cbc8-7ca0-42d9-b719-40d71c6cf106</bp_uid>
  <bp_title>SSRS 2012 - No access to admin page</bp_title>
  <bp_content>Immediately after installation, 
I have no access to the reports admin page (even when running as admin) 



thanks! issue solved.
I had to add localhost to local intranet, then run as admin, and all was fine.



Thank you for filing this issue. Two things you can try:
1) use the machine name in the URL rather than using localhost.
2) determine if the Admin account on your computer has a password specified. 
If not, specify a password and see if this resolves the access denied issue.


http://serverfault.com/questions/331852/allow-anonymous-access-to-ssrs



</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>Immediately after installation, &lt;br /&gt;I have no access to the reports admin page (even when running as admin) &lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;thanks! issue solved.&lt;br /&gt;I had to add localhost to local intranet, then run as admin, and all was fine.&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;Thank you for filing this issue. Two things you can try:&lt;br /&gt;1) use the machine name in the URL rather than using localhost.&lt;br /&gt;2) determine if the Admin account on your computer has a password specified. &lt;br /&gt;If not, specify a password and see if this resolves the access denied issue.&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://serverfault.com/questions/331852/allow-anonymous-access-to-ssrs"&gt;http://serverfault.com/questions/331852/allow-anonymous-access-to-ssrs&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-07T12:09:39.816667</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>45ee1de8-8002-4ec6-91d5-482200a16ebc</bp_uid>
  <bp_title>SQL Server: CountDistinct is possible !</bp_title>
  <bp_content>SELECT COUNT(DISTINCT program_name) AS Count,
  program_type AS [Type] 
FROM cm_production 
WHERE push_number=@push_number 
GROUP BY program_type

Reference:
http://stackoverflow.com/questions/1521605/sql-server-query-selecting-count-with-distinct</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>SELECT COUNT(DISTINCT program_name) AS Count,&lt;br /&gt;  program_type AS [Type] &lt;br /&gt;FROM cm_production &lt;br /&gt;WHERE push_number=@push_number &lt;br /&gt;GROUP BY program_type&lt;br /&gt;&lt;br /&gt;Reference:&lt;br /&gt;&lt;a target="_blank" href="http://stackoverflow.com/questions/1521605/sql-server-query-selecting-count-with-distinct"&gt;http://stackoverflow.com/questions/1521605/sql-server-query-selecting-count-with-distinct&lt;/a&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T08:51:09.503333</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>a51f8f42-38ae-407b-8baa-6b661cd9b39d</bp_uid>
  <bp_title>Comparison of blog software</bp_title>
  <bp_content>http://blog-software-review.toptenreviews.com/




http://www.weblogmatrix.org/compare/dasBlog+Movable-Type+Wordpress




http://www.gutgames.com/post/Using-Akismets-API-In-C.aspx



http://www.java2v.com/Open-Source/CSharp/Bloggers/BlogEngine.NET/Joel/Net/AkismetFilter.cs.htm



</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>&lt;a target="_blank" href="http://blog-software-review.toptenreviews.com/"&gt;http://blog-software-review.toptenreviews.com/&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://www.weblogmatrix.org/compare/dasBlog+Movable-Type+Wordpress"&gt;http://www.weblogmatrix.org/compare/dasBlog+Movable-Type+Wordpress&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://www.gutgames.com/post/Using-Akismets-API-In-C.aspx"&gt;http://www.gutgames.com/post/Using-Akismets-API-In-C.aspx&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://www.java2v.com/Open-Source/CSharp/Bloggers/BlogEngine.NET/Joel/Net/AkismetFilter.cs.htm"&gt;http://www.java2v.com/Open-Source/CSharp/Bloggers/BlogEngine.NET/Joel/Net/AkismetFilter.cs.htm&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T11:01:27.91</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>1b0f956a-31fa-4f29-9898-767299cff9c7</bp_uid>
  <bp_title>Building a blog with RavenDB</bp_title>
  <bp_content>http://www.dotnetcurry.com/ShowArticle.aspx?ID=787</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>&lt;a target="_blank" href="http://www.dotnetcurry.com/ShowArticle.aspx?ID=787"&gt;http://www.dotnetcurry.com/ShowArticle.aspx?ID=787&lt;/a&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T11:04:19.653333</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>c5d8f62e-bb4f-46d8-8813-8d0441aecc2d</bp_uid>
  <bp_title>Getting started with MongoDB</bp_title>
  <bp_content>http://www.codeproject.com/Articles/87757/MongoDB-and-C</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>&lt;a target="_blank" href="http://www.codeproject.com/Articles/87757/MongoDB-and-C"&gt;http://www.codeproject.com/Articles/87757/MongoDB-and-C&lt;/a&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T11:01:40.336667</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>82309682-5f60-45f2-8771-9138c10cb12e</bp_uid>
  <bp_title>A comparison between DB engines</bp_title>
  <bp_content>http://db-engines.com/en/ranking</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>&lt;a target="_blank" href="http://db-engines.com/en/ranking"&gt;http://db-engines.com/en/ranking&lt;/a&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T10:57:15.55</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>62064738-144a-4be7-8c6e-9f0e2cabda8d</bp_uid>
  <bp_title>HyperTable is here !</bp_title>
  <bp_content>
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


</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>&lt;br /&gt;Build / Install:&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://code.google.com/p/hypertable/wiki/HowToBuild"&gt;http://code.google.com/p/hypertable/wiki/HowToBuild&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://code.google.com/p/hypertable/wiki/HowToBuild#Ubuntu_9.04_Jaunty_Jackalope_32-bit"&gt;http://code.google.com/p/hypertable/wiki/HowToBuild#Ubuntu_9.04_Jaunty_Jackalope_32-bit&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://code.google.com/p/hypertable/wiki/HowToInstall"&gt;http://code.google.com/p/hypertable/wiki/HowToInstall&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://hypertable.com/documentation/installation/quick_start_standalone/"&gt;http://hypertable.com/documentation/installation/quick_start_standalone/&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;Info:&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://de.slideshare.net/adorepump/hypertable-nosql"&gt;http://de.slideshare.net/adorepump/hypertable-nosql&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://hypertable.com/why_hypertable/"&gt;http://hypertable.com/why_hypertable/&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://code.google.com/p/hypertable/wiki/HyperRecord"&gt;http://code.google.com/p/hypertable/wiki/HyperRecord&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://code.google.com/p/hypertable/wiki/WhyWeChoseCppOverJava"&gt;http://code.google.com/p/hypertable/wiki/WhyWeChoseCppOverJava&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://hypertable.com/why_hypertable/hypertable_vs_hbase_2/"&gt;http://hypertable.com/why_hypertable/hypertable_vs_hbase_2/&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="https://groups.google.com/forum/?fromgroups#!topic/hypertable-dev/K3tPn6LGmRQ"&gt;https://groups.google.com/forum/?fromgroups#!topic/hypertable-dev/K3tPn6LGmRQ&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="https://groups.google.com/forum/?fromgroups#!topic/hypertable-user/ygLe_JapGQM"&gt;https://groups.google.com/forum/?fromgroups#!topic/hypertable-user/ygLe_JapGQM&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T10:56:37.983333</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>2d98bf47-f014-41d2-98b8-b3bce2dc5ebb</bp_uid>
  <bp_title>jQuery drag and drop columns</bp_title>
  <bp_content>http://gridster.net/

</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>&lt;a target="_blank" href="http://gridster.net/"&gt;http://gridster.net/&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T10:52:20.506667</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>8c65b4e8-ac48-467f-9c56-b5ed32da0806</bp_uid>
  <bp_title>Family tree with jQuery</bp_title>
  <bp_content>It''s possible with the Google Visualization API, it''s well documented: http://code.google.com/apis/visualization/documentation/gallery/orgchart.html#Example

Or using jQuery Orgchart:
https://github.com/caprica/jquery-orgchart

http://th3silverlining.com/2011/12/01/jquery-org-chart-a-plugin-for-visualising-data-in-a-tree-like-structure/

http://dl.dropboxusercontent.com/u/4151695/html/jOrgChart/example/example.html

D:\Stefan.Steiger\Desktop\jquery-orgchart-master\jquery-orgchart-master\demo\dogfood.html



https://developers.google.com/chart/interactive/docs/gallery/orgchart



Reference:
http://stackoverflow.com/questions/3454316/family-tree-design-suggestions</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>It''s possible with the Google Visualization API, it''s well documented: &lt;a target="_blank" href="http://code.google.com/apis/visualization/documentation/gallery/orgchart.html#Example"&gt;http://code.google.com/apis/visualization/documentation/gallery/orgchart.html#Example&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;Or using jQuery Orgchart:&lt;br /&gt;&lt;a target="_blank" href="https://github.com/caprica/jquery-orgchart"&gt;https://github.com/caprica/jquery-orgchart&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://th3silverlining.com/2011/12/01/jquery-org-chart-a-plugin-for-visualising-data-in-a-tree-like-structure/"&gt;http://th3silverlining.com/2011/12/01/jquery-org-chart-a-plugin-for-visualising-data-in-a-tree-like-structure/&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://dl.dropboxusercontent.com/u/4151695/html/jOrgChart/example/example.html"&gt;http://dl.dropboxusercontent.com/u/4151695/html/jOrgChart/example/example.html&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="file:///D:/Stefan.Steiger/Desktop/jquery-orgchart-master/jquery-orgchart-master/demo/dogfood.html"&gt;D:\Stefan.Steiger\Desktop\jquery-orgchart-master\jquery-orgchart-master\demo\dogfood.html&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="https://developers.google.com/chart/interactive/docs/gallery/orgchart"&gt;https://developers.google.com/chart/interactive/docs/gallery/orgchart&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;Reference:&lt;br /&gt;&lt;a target="_blank" href="http://stackoverflow.com/questions/3454316/family-tree-design-suggestions"&gt;http://stackoverflow.com/questions/3454316/family-tree-design-suggestions&lt;/a&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T08:53:03.99</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>0a52ac79-0cf4-46e4-a08d-c9a62c553143</bp_uid>
  <bp_title>jQuery Gantt charts</bp_title>
  <bp_content>

http://taitems.github.io/jQuery.Gantt/

https://github.com/taitems/jQuery.Gantt



http://mbielanczuk.com/jquery-gantt/



</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://taitems.github.io/jQuery.Gantt/"&gt;http://taitems.github.io/jQuery.Gantt/&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="https://github.com/taitems/jQuery.Gantt"&gt;https://github.com/taitems/jQuery.Gantt&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://mbielanczuk.com/jquery-gantt/"&gt;http://mbielanczuk.com/jquery-gantt/&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T10:46:40.493333</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>b9869bad-bb9e-47e6-87cb-cd3f4890f255</bp_uid>
  <bp_title>Redis as memcached replacement</bp_title>
  <bp_content>
http://www.servicestack.net/mythz_blog/?p=474



http://www.servicestack.net/RedisStackOverflow/#!questions/Latest_Questions


https://github.com/ServiceStack/ServiceStack.Examples/blob/master/src/RedisStackOverflow/RedisStackOverflow.ServiceInterface/IRepository.cs



https://github.com/ServiceStack/ServiceStack.Redis


http://stackoverflow.com/questions/5006326/start-using-redis-with-asp-net








</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>&lt;br /&gt;&lt;a target="_blank" href="http://www.servicestack.net/mythz_blog/?p=474"&gt;http://www.servicestack.net/mythz_blog/?p=474&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://www.servicestack.net/RedisStackOverflow/#!questions/Latest_Questions"&gt;http://www.servicestack.net/RedisStackOverflow/#!questions/Latest_Questions&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="https://github.com/ServiceStack/ServiceStack.Examples/blob/master/src/RedisStackOverflow/RedisStackOverflow.ServiceInterface/IRepository.cs"&gt;https://github.com/ServiceStack/ServiceStack.Examples/blob/master/src/RedisStackOverflow/RedisStackOverflow.ServiceInterface/IRepository.cs&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="https://github.com/ServiceStack/ServiceStack.Redis"&gt;https://github.com/ServiceStack/ServiceStack.Redis&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://stackoverflow.com/questions/5006326/start-using-redis-with-asp-net"&gt;http://stackoverflow.com/questions/5006326/start-using-redis-with-asp-net&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T11:00:35.346667</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>e0f8def8-4e45-45bb-aa8e-d002b77098a8</bp_uid>
  <bp_title>Multi-File upload is now possible with IE 10</bp_title>
  <bp_content>http://blueimp.github.io/jQuery-File-Upload/</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>&lt;a target="_blank" href="http://blueimp.github.io/jQuery-File-Upload/"&gt;http://blueimp.github.io/jQuery-File-Upload/&lt;/a&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T10:49:55.77</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>ae0cdf7f-c42e-4346-93dd-d4444d93f087</bp_uid>
  <bp_title>A comparison of NoSQL systems</bp_title>
  <bp_content>http://kkovacs.eu/cassandra-vs-mongodb-vs-couchdb-vs-redis


http://stackoverflow.com/questions/1777103/what-nosql-solutions-are-out-there-for-net


</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>&lt;a target="_blank" href="http://kkovacs.eu/cassandra-vs-mongodb-vs-couchdb-vs-redis"&gt;http://kkovacs.eu/cassandra-vs-mongodb-vs-couchdb-vs-redis&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://stackoverflow.com/questions/1777103/what-nosql-solutions-are-out-there-for-net"&gt;http://stackoverflow.com/questions/1777103/what-nosql-solutions-are-out-there-for-net&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T10:55:31.356667</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>e49fd978-fd4f-499a-aa01-d5fc68afd8c9</bp_uid>
  <bp_title>It''s possible to perform cross-database queries with postgres !</bp_title>
  <bp_content>databaseA=# select * from databaseB.public.someTableName;
ERROR:  cross-database references are not implemented:
 "databaseB.public.someTableName"


This functionality isn''t part of the default PostgreSQL install, but you can add it in. It''s called dblink.


Yes, you can by using DBlink (postgresql only) and DBI-Link (allows foreign cross database queriers) and TDS_LInk which allows queries to be run against MS SQL server.

I have used DB-Link and TDS-link before with great success.

Need to install postgresql-contrib 


Reference:
http://stackoverflow.com/questions/46324/possible-to-perform-cross-database-queries-with-postgres</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>databaseA=# select * from databaseB.public.someTableName;&lt;br /&gt;ERROR:  cross-database references are not implemented:&lt;br /&gt; "databaseB.public.someTableName"&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;This functionality isn''t part of the default PostgreSQL install, but you can add it in. It''s called dblink.&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;Yes, you can by using DBlink (postgresql only) and DBI-Link (allows foreign cross database queriers) and TDS_LInk which allows queries to be run against MS SQL server.&lt;br /&gt;&lt;br /&gt;I have used DB-Link and TDS-link before with great success.&lt;br /&gt;&lt;br /&gt;Need to install postgresql-contrib &lt;br /&gt;&lt;br /&gt;&lt;br /&gt;Reference:&lt;br /&gt;&lt;a target="_blank" href="http://stackoverflow.com/questions/46324/possible-to-perform-cross-database-queries-with-postgres"&gt;http://stackoverflow.com/questions/46324/possible-to-perform-cross-database-queries-with-postgres&lt;/a&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T08:49:40.463333</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>a65bfd06-cc23-4495-8c51-d9d2787218a5</bp_uid>
  <bp_title>40 Awsome jQuery plugins</bp_title>
  <bp_content>http://designshack.net/articles/javascript/40-awesome-jquery-plugins-you-need-to-check-out/</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>&lt;a target="_blank" href="http://designshack.net/articles/javascript/40-awesome-jquery-plugins-you-need-to-check-out/"&gt;http://designshack.net/articles/javascript/40-awesome-jquery-plugins-you-need-to-check-out/&lt;/a&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T10:50:24.936667</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>91b8fa4d-89ea-4791-aca3-ef4812c26368</bp_uid>
  <bp_title>Thrift vs. SOAP</bp_title>
  <bp_content>http://www.linkedin.com/groups/Comparison-between-thrift-SOAP-50472.S.88711555

http://myblog.rsynnott.com/2007/12/facebook-thrift-less-horrific-than-soap.html</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>&lt;a target="_blank" href="http://www.linkedin.com/groups/Comparison-between-thrift-SOAP-50472.S.88711555"&gt;http://www.linkedin.com/groups/Comparison-between-thrift-SOAP-50472.S.88711555&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://myblog.rsynnott.com/2007/12/facebook-thrift-less-horrific-than-soap.html"&gt;http://myblog.rsynnott.com/2007/12/facebook-thrift-less-horrific-than-soap.html&lt;/a&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T10:53:38.42</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>b6a91e9a-a829-46b5-8156-f87366a0a4a5</bp_uid>
  <bp_title>An entire regex library</bp_title>
  <bp_content>http://regexlib.com/Search.aspx?k=url&amp;AspxAutoDetectCookieSupport=1</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>&lt;a target="_blank" href="http://regexlib.com/Search.aspx?k=url&amp;AspxAutoDetectCookieSupport=1"&gt;http://regexlib.com/Search.aspx?k=url&amp;AspxAutoDetectCookieSupport=1&lt;/a&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T11:07:47.083333</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

<row>
  <bp_uid>d64db6db-a7a0-4953-8a92-fdb2a0f9a315</bp_uid>
  <bp_title>RavenDB</bp_title>
  <bp_content>
http://nosql.mypopescu.com/post/346471814/usecase-nosql-based-blogs



http://byterot.blogspot.ch/2012/11/nosql-benchmark-redis-mongodb-ravendb-cassandra-sqlserver.html




http://nosql.mypopescu.com/post/19392225838/ravendb-tutorial-building-an-asp-net-mvc-app-using



http://ravendb.net/docs/intro/quickstart


http://www.dotnetcurry.com/ShowArticle.aspx?ID=787

</bp_content>
  <bp_creoletext xsi:nil="true"/>
  <bp_bbcode xsi:nil="true"/>
  <bp_htmlcontent>&lt;br /&gt;&lt;a target="_blank" href="http://nosql.mypopescu.com/post/346471814/usecase-nosql-based-blogs"&gt;http://nosql.mypopescu.com/post/346471814/usecase-nosql-based-blogs&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://byterot.blogspot.ch/2012/11/nosql-benchmark-redis-mongodb-ravendb-cassandra-sqlserver.html"&gt;http://byterot.blogspot.ch/2012/11/nosql-benchmark-redis-mongodb-ravendb-cassandra-sqlserver.html&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://nosql.mypopescu.com/post/19392225838/ravendb-tutorial-building-an-asp-net-mvc-app-using"&gt;http://nosql.mypopescu.com/post/19392225838/ravendb-tutorial-building-an-asp-net-mvc-app-using&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://ravendb.net/docs/intro/quickstart"&gt;http://ravendb.net/docs/intro/quickstart&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;&lt;br /&gt;&lt;a target="_blank" href="http://www.dotnetcurry.com/ShowArticle.aspx?ID=787"&gt;http://www.dotnetcurry.com/ShowArticle.aspx?ID=787&lt;/a&gt;&lt;br /&gt;&lt;br /&gt;</bp_htmlcontent>
  <bp_entrydate>2013-05-03T11:03:52.903333</bp_entrydate>
  <autoaudit_createddate xsi:nil="true"/>
  <autoaudit_createdby xsi:nil="true"/>
  <autoaudit_modifieddate xsi:nil="true"/>
  <autoaudit_modifiedby xsi:nil="true"/>
  <autoaudit_rowversion>1</autoaudit_rowversion>
</row>

</t_blogpost>
' AS xml)   
)) AS myTempTable(myXmlColumn)
;