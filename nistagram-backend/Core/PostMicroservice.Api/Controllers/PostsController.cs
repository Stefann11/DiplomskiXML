using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostMicroservice.Api.Factories;
using PostMicroservice.Core.Interface.Repository;
using PostMicroservice.Core.Model;
using PostMicroservice.Core.Model.File;
using PostMicroservice.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PostMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostService _postService;
        private readonly IPostRepository _postRepository;
        private readonly UserService userService;
        private readonly LocationService locationService;
        private readonly PostSingleFactory postSingleFactory;
        private readonly PostAlbumFactory postAlbumFactory;
        private readonly IWebHostEnvironment _env;

        public PostsController(PostService postService, IPostRepository postRepository,
            PostSingleFactory postSingleFactory, IWebHostEnvironment env, UserService userService,
            LocationService locationService, PostAlbumFactory postAlbumFactory)
        {
            _postService = postService;
            _postRepository = postRepository;
            this.postSingleFactory = postSingleFactory;
            _env = env;
            this.userService = userService;
            this.locationService = locationService;
            this.postAlbumFactory = postAlbumFactory;
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            Post post = _postRepository.GetById(id).Value;
            if (post is null)
            {
                return BadRequest();
            }
            if (post.GetType().Name.Equals("PostSingle"))
            {
                return Ok(postSingleFactory.Create((PostSingle)post));
            }
            return Ok(postAlbumFactory.Create((PostAlbum)post));
        }

        [HttpGet]
        public IActionResult Search([FromQuery] Guid userId, [FromQuery] string hashTag,
            [FromQuery] string country, [FromQuery] string city, [FromQuery] string street,
            [FromQuery] string access)
        {
            if (Request.Query.Count == 0) return BadRequest();
            if (userId == Guid.Empty && String.IsNullOrWhiteSpace(hashTag)
                && String.IsNullOrEmpty(access)) return BadRequest();
            List<DTOs.Post> posts = new List<DTOs.Post>();
            List<Post> allPosts = _postRepository.GetBy(userId, hashTag, country, city,
                street, access).ToList();
            foreach (Post post in allPosts)
            {
                if (post.GetType().Name.Equals("PostSingle"))
                {
                    posts.Add(postSingleFactory.Create((PostSingle)post));
                }
                else
                {
                    posts.Add(postAlbumFactory.Create((PostAlbum)post));
                }
            }
            return Ok(posts);
        }

        [HttpGet("contents/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var content = _postService.GetImage(_env.WebRootPath, fileName);
            if (content.Type.Equals(".mp4")) content.Type = "video/mp4";
            else content.Type = "image/jpeg";
            FileContentResult fileContentResult = File(content.Bytes, content.Type);
            return Ok(fileContentResult);
        }

        [HttpPost("images")]
        public IActionResult GetImages(List<string> contentPaths)
        {
            List<FileContentResult> fileContentResults = new List<FileContentResult>();
            foreach (string contentPath in contentPaths)
            {
                var content = _postService.GetImage(_env.WebRootPath, contentPath);
                if (content.Type.Equals(".mp4")) content.Type = "video/mp4";
                else content.Type = "image/jpeg";
                fileContentResults.Add(File(content.Bytes, content.Type));
            }
            return Ok(fileContentResults);
        }

        [HttpPost("contents")]
        [Authorize(Roles = "RegisteredUser, Agent, VerifiedUser")]
        public IActionResult SaveImg([FromForm] FileModel file)
        {
            string fileName = _postService.ImageToSave(_env.WebRootPath, file);

            return Ok(fileName);
        }

        [HttpPost]
        [Authorize(Roles = "RegisteredUser, Agent, VerifiedUser")]
        public IActionResult Save(DTOs.PostAlbum post)
        {
            Result<DateTime> timeStamp = DateTime.Now;
            Result<Description> description = Description.Create(post.Description);
            RegisteredUser registeredUser = userService.GetById(post.RegisteredUser.Id);
            Location location = locationService.GetById(post.Location.Id);
            List<HashTag> hashTags = (from DTOs.HashTag hashTag in post.HashTags
                                      select HashTag.Create(HashTagText.Create(hashTag.HashTagText).Value).Value).ToList();
            List<RegisteredUser> taggedUsers = (from DTOs.RegisteredUser registered in post.TaggedUsers
                                                select RegisteredUser.Create(registered.Id, Username.Create(registered.Username).Value,
                                                FirstName.Create(registered.FirstName).Value, LastName.Create(registered.LastName).Value,
                                                ProfileImagePath.Create(registered.ProfileImagePath).Value, false, true,
                                                new List<RegisteredUser>(), new List<RegisteredUser>(), new List<RegisteredUser>(),
                                                new List<RegisteredUser>()).Value).ToList();
            Guid id = Guid.NewGuid();
            if (post.ContentPaths.Count() == 1)
            {
                Result<ContentPath> contentPath = ContentPath.Create(post.ContentPaths.FirstOrDefault());
                Result result = Result.Combine(timeStamp, description, contentPath);
                if (result.IsFailure) return BadRequest();
                if (_postService.SaveSinglePost(PostSingle.Create(id, timeStamp.Value, description.Value,
                    registeredUser, new List<RegisteredUser>(), new List<RegisteredUser>(),
                    new List<Comment>(), location, taggedUsers, hashTags, false,
                    contentPath.Value).Value) == null) return BadRequest();
            }
            else
            {
                List<ContentPath> contentPaths = (from string path in post.ContentPaths
                                                  select ContentPath.Create(path).Value).ToList();
                Result result = Result.Combine(timeStamp, description);
                if (_postService.SaveAlbumPost(PostAlbum.Create(id, timeStamp.Value, description.Value,
                registeredUser, new List<RegisteredUser>(), new List<RegisteredUser>(),
                new List<Comment>(), location, taggedUsers, hashTags, false,
                contentPaths).Value) == null) return BadRequest();
            }
            post.Id = id;
            return Ok(post);
        }

        [HttpPut("{id}/users/{userId}/likes")]
        [Authorize(Roles = "RegisteredUser, Agent, VerifiedUser")]
        public IActionResult Like([FromRoute] Guid id, [FromRoute] Guid userId)
        {
            bool like = _postService.Like(id, userId);
            if (like == false) return BadRequest();
            return Ok();
        }

        [HttpPut("{id}/users/{userId}/dislikes")]
        [Authorize(Roles = "RegisteredUser, Agent, VerifiedUser")]
        public IActionResult Dislike([FromRoute] Guid id, [FromRoute] Guid userId)
        {
            bool dislike = _postService.Dislike(id, userId);
            if (dislike == false) return BadRequest();
            return Ok();
        }

        [HttpPost("{id}/comments")]
        [Authorize(Roles = "RegisteredUser, Agent, VerifiedUser")]
        public IActionResult CommentPost([FromRoute] Guid id, [FromBody] DTOs.Comment comment)
        {
            Result<DateTime> timeStamp = DateTime.Now;
            Result<CommentText> commentText = CommentText.Create(comment.CommentText);
            Result result = Result.Combine(timeStamp, commentText);
            if (result.IsFailure) return BadRequest();
            RegisteredUser registeredUser = userService.GetById(comment.RegisteredUser.Id);
            Guid commentId = Guid.NewGuid();
            _postService.CommentPost(id, Comment.Create(commentId, timeStamp.Value, commentText.Value, registeredUser,
                new List<RegisteredUser>()).Value);
            return Ok(commentId);
        }

        [HttpGet("users/{id}")]
        public IActionResult GetByUserId(Guid id)
        {
            List<Post> posts = _postRepository.GetByUserId(id).ToList();
            List<DTOs.Post> toReturn = new List<DTOs.Post>();
            foreach (Post post in posts)
            {
                if (post.GetType().Name.Equals("PostSingle"))
                {
                    toReturn.Add(postSingleFactory.Create((PostSingle)post));
                }
                else
                {
                    toReturn.Add(postAlbumFactory.Create((PostAlbum)post));
                }
            }
            return Ok(toReturn);
        }

        [HttpGet("collections/{collectionId}/users/{userId}")]
        public IActionResult GetByCollectionAndUser(Guid collectionId, Guid userId)
        {
            List<Post> posts = _postRepository.GetByCollectionAndUser(collectionId, userId).ToList();
            List<DTOs.Post> toReturn = new List<DTOs.Post>();
            foreach (Post post in posts)
            {
                if (post.GetType().Name.Equals("PostSingle"))
                {
                    toReturn.Add(postSingleFactory.Create((PostSingle)post));
                }
                else
                {
                    toReturn.Add(postAlbumFactory.Create((PostAlbum)post));
                }
            }
            return Ok(toReturn);
        }

        [HttpPost("following")]
        public IActionResult GetForFollowing(List<DTOs.RegisteredUser> registeredUsers)
        {
            List<RegisteredUser> users = (registeredUsers.Select(registeredUser =>
                userService.GetById(registeredUser.Id))).ToList();
            List<Post> posts = _postRepository.GetForFollowing(users).ToList();
            List<DTOs.Post> toReturn = new List<DTOs.Post>();
            foreach (Post post in posts)
            {
                if (post.GetType().Name.Equals("PostSingle"))
                {
                    toReturn.Add(postSingleFactory.Create((PostSingle)post));
                }
                else
                {
                    toReturn.Add(postAlbumFactory.Create((PostAlbum)post));
                }
            }
            return Ok(toReturn);
        }

        [HttpGet("liked/{userId}")]
        [Authorize(Roles = "RegisteredUser, Agent, VerifiedUser")]
        public IActionResult GetLikedByUser(Guid userId)
        {
            List<Post> posts = _postRepository.GetLikedByUser(userId).ToList();
            List<DTOs.Post> toReturn = new List<DTOs.Post>();
            foreach (Post post in posts)
            {
                if (post.GetType().Name.Equals("PostSingle"))
                {
                    toReturn.Add(postSingleFactory.Create((PostSingle)post));
                }
                else
                {
                    toReturn.Add(postAlbumFactory.Create((PostAlbum)post));
                }
            }
            return Ok(toReturn);
        }

        [HttpGet("disliked/{userId}")]
        [Authorize(Roles = "RegisteredUser, Agent, VerifiedUser")]
        public IActionResult GetDislikedByUser(Guid userId)
        {
            List<Post> posts = _postRepository.GetDislikedByUser(userId).ToList();
            List<DTOs.Post> toReturn = new List<DTOs.Post>();
            foreach (Post post in posts)
            {
                if (post.GetType().Name.Equals("PostSingle"))
                {
                    toReturn.Add(postSingleFactory.Create((PostSingle)post));
                }
                else
                {
                    toReturn.Add(postAlbumFactory.Create((PostAlbum)post));
                }
            }
            return Ok(toReturn);
        }

        [HttpPut("{id}/ban")]
        [Authorize(Roles = "Admin")]
        public IActionResult BanPost([FromRoute] Guid id)
        {
            _postRepository.BanPost(id);
            return Ok();
        }
    }
}