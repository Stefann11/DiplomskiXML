using PostMicroservice.DataAccess.Adaptee;
using PostMicroservice.DataAccess.Target;
using System.Data;

namespace PostMicroservice.DataAccess.Adapter
{
    public class CommentAdapter : ITarget
    {
        private readonly CommentAdaptee commentAdaptee;

        public CommentAdapter(CommentAdaptee commentAdaptee)
        {
            this.commentAdaptee = commentAdaptee;
        }

        public object ConvertSql(DataRow dataRow)
        {
            return commentAdaptee.ConvertSqlDataReaderToComment(dataRow);
        }
    }
}