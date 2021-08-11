using PostMicroservice.Core.Model;
using PostMicroservice.DataAccess.Adaptee;
using System.Collections.Generic;
using System.Data;

namespace PostMicroservice.DataAccess.Adapter
{
    public class PostAdapter
    {
        private readonly PostAdaptee postAdaptee;

        public PostAdapter(PostAdaptee postAdaptee)
        {
            this.postAdaptee = postAdaptee;
        }

        public object ConvertSqlWithAttributes(DataRow dataRow, IEnumerable<RegisteredUser> likes,
            IEnumerable<RegisteredUser> dislikes, IEnumerable<HashTag> hashTags,
            IEnumerable<Comment> comments, IEnumerable<RegisteredUser> taggedUsers,
            IEnumerable<ContentPath> contentPaths)
        {
            return postAdaptee.ConvertSqlDataReaderToPostWithAttributes(dataRow, likes, dislikes,
                hashTags, comments, taggedUsers, contentPaths);
        }
    }
}