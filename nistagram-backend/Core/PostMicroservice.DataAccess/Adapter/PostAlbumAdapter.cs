using PostMicroservice.DataAccess.Adaptee;
using PostMicroservice.DataAccess.Target;
using System.Data;

namespace PostMicroservice.DataAccess.Adapter
{
    public class PostAlbumAdapter : ITarget
    {
        private readonly PostAlbumAdaptee postAlbumAdaptee;

        public PostAlbumAdapter(PostAlbumAdaptee postAlbumAdaptee)
        {
            this.postAlbumAdaptee = postAlbumAdaptee;
        }

        public object ConvertSql(DataRow dataRow)
        {
            return postAlbumAdaptee.ConvertSqlDataReaderToPostAlbum(dataRow);
        }
    }
}