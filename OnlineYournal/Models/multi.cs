
using System.Collections.Generic;



// namespace OnlineYournal.Models
namespace MyBlogCore.Controllers
{


    public class T_BlogPost
    {
        public System.Guid BP_UID = System.Guid.NewGuid();
        public System.Guid BP_Author_USR_UID = System.Guid.Empty;


        public string BP_Title;
        public string BP_Content;
        public string BP_HtmlContent;

        public string BP_Excerpt;
        public string BP_ExcerptHTML;

        public string BP_CommentCount;

        public System.Guid BP_PostType; // Post, Comment FollowUp


        public System.DateTime BP_EntryDate = System.DateTime.UtcNow;
        public System.DateTime BP_LastModifiedDate = System.DateTime.UtcNow;
    } // End Class T_BlogPost


    public class BlogIndex
    {
        public System.Collections.Generic.IList<T_BlogPost> lsBlogEntries;
    } // End Class BlogIndex 


    public class cSearchResult
    {
        public cSearchResult() { }
        public cSearchResult(string q)
        {
            this.searched_for = q;
        } // End Constructor 


        public string searched_for;
        public System.Collections.Generic.List<T_BlogPost> searchResults;
    } // End Class cSearchResult 

}
