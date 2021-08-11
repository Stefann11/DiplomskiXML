using System;
using System.Collections.Generic;

namespace PostMicroservice.Core.Model
{
    public abstract class Post
    {
        private readonly List<Comment> comments;
        private readonly List<HashTag> hashTags;
        private readonly List<RegisteredUser> likes;
        private readonly List<RegisteredUser> dislikes;
        private readonly List<RegisteredUser> taggedUsers;

        public Guid Id { get; }
        public DateTime TimeStamp { get; }
        public Description Description { get; }
        public RegisteredUser RegisteredUser { get; }
        public IEnumerable<RegisteredUser> Likes => this.likes;
        public IEnumerable<RegisteredUser> Dislikes => this.dislikes;
        public IEnumerable<Comment> Comments => this.comments;
        public Location Location { get; }
        public IEnumerable<RegisteredUser> TaggedUsers => this.taggedUsers;
        public IEnumerable<HashTag> HashTags => this.hashTags;
        public bool IsBanned { get; }

        protected Post(Guid id, DateTime timeStamp, Description description,
            RegisteredUser registeredUser, IEnumerable<RegisteredUser> likes,
            IEnumerable<RegisteredUser> dislikes, IEnumerable<Comment> comments, Location location,
            IEnumerable<RegisteredUser> taggedUsers, IEnumerable<HashTag> hashTags, bool isBanned)
        {
            Id = id;
            TimeStamp = timeStamp;
            Description = description;
            RegisteredUser = registeredUser;
            this.likes = new List<RegisteredUser>(likes);
            this.dislikes = new List<RegisteredUser>(dislikes);
            this.comments = new List<Comment>(comments);
            Location = location;
            this.taggedUsers = new List<RegisteredUser>(taggedUsers);
            this.hashTags = new List<HashTag>(hashTags);
            IsBanned = isBanned;
        }

        public void AddComment(Comment comment)
        {
            this.comments.Add(comment);
        }

        public void AddHashTag(HashTag hashTag)
        {
            this.hashTags.Add(hashTag);
        }

        public void AddLike(RegisteredUser like)
        {
            this.likes.Add(like);
        }

        public void AddDislike(RegisteredUser dislike)
        {
            this.dislikes.Add(dislike);
        }

        public void AddTaggedUser(RegisteredUser user)
        {
            this.taggedUsers.Add(user);
        }
    }
}