using PostMicroservice.Api.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Api.Factories
{
    public class CollectionFactory
    {
        private readonly RegisteredUserFactory registeredUserFactory;
        private readonly PostSingleFactory postSingleFactory;
        private readonly PostAlbumFactory postAlbumFactory;

        public CollectionFactory(RegisteredUserFactory registeredUserFactory, PostSingleFactory postSingleFactory,
            PostAlbumFactory postAlbumFactory)
        {
            this.registeredUserFactory = registeredUserFactory;
            this.postSingleFactory = postSingleFactory;
            this.postAlbumFactory = postAlbumFactory;
        }

        public Collection Create(Core.Model.Collection collection)
        {
            return new Collection
            {
                Id = collection.Id,
                CollectionName = collection.CollectionName.ToString(),
                RegisteredUser = registeredUserFactory.Create(collection.RegisteredUser),
                Posts = new List<Post>()
            };
        }

        public IEnumerable<Collection> CreateCollections(IEnumerable<Core.Model.Collection> collections)
        {
            return collections.Select(collection => Create(collection)).ToList();
        }

        private IEnumerable<Post> Convert(IEnumerable<Core.Model.Post> posts)
        {
            List<Post> toReturn = new List<Post>();
            foreach (Core.Model.Post post in posts)
            {
                if (post.GetType().Name.Equals("PostSingle"))
                {
                }
            }
            return toReturn;
        }
    }
}