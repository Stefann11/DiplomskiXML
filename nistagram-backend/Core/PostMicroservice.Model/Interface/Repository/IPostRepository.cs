using PostMicroservice.Core.Model;
using System;
using System.Collections.Generic;

namespace PostMicroservice.Core.Interface.Repository
{
    public interface IPostRepository : IRepository<Post>
    {
        public IEnumerable<Post> GetBy(Guid id, string hashTag, string country, string city, string street, string access);

        public Post SaveSinglePost(PostSingle post);

        public Post SaveAlbumPost(PostAlbum post);

        public bool Like(Guid id, Guid userId);

        public bool Dislike(Guid id, Guid userId);

        public void CommentPost(Guid postId, Comment comment);

        public IEnumerable<Post> GetByUserId(Guid id);

        public IEnumerable<Post> GetByCollectionAndUser(Guid collectionId, Guid userId);

        public IEnumerable<Post> GetForFollowing(IEnumerable<RegisteredUser> registeredUsers);

        public IEnumerable<Post> GetLikedByUser(Guid id);

        public IEnumerable<Post> GetDislikedByUser(Guid id);

        public void BanPost(Guid id);
    }
}