
// namespace MyBlogCore.Models
namespace MyBlogCore.Controllers
{


    public class T_BlogPost
    {
        public System.Guid BP_UID { get; set; }= System.Guid.NewGuid();
        public System.Guid BP_Author_USR_UID { get; set; }= System.Guid.Empty;


        public string BP_Title { get; set; }
        public string BP_Content{ get; set; }
        public string BP_HtmlContent{ get; set; }

        public string BP_Excerpt{ get; set; }
        public string BP_ExcerptHTML{ get; set; }

        public string BP_CommentCount{ get; set; }

        public System.Guid BP_PostType { get; set; } // Post, Comment FollowUp


        public System.DateTime BP_EntryDate{ get; set; } = System.DateTime.UtcNow;
        public System.DateTime BP_LastModifiedDate{ get; set; } = System.DateTime.UtcNow;
    } // End Class T_BlogPost
    

    public class BlogIndex
    {
        public System.Collections.Generic.IEnumerable<T_BlogPost> lsBlogEntries;
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
