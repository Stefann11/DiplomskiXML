using PostMicroservice.Core.DTOs;
using PostMicroservice.Core.Interface.Repository;
using PostMicroservice.Core.Model;
using PostMicroservice.Core.Model.File;
using System;
using System.Collections.Generic;
using System.IO;

namespace PostMicroservice.Core.Services
{
    public class PostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll();
        }

        public Post GetById(Guid id)
        {
            return _postRepository.GetById(id).Value;
        }

        public Post Save(Post post)
        {
            return _postRepository.Save(post);
        }

        public Post Edit(Post post)
        {
            throw new NotImplementedException();
        }

        public Post Delete(Post post)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetByUserId(Guid id)
        {
            return _postRepository.GetBy(id, "", "", "", "", "");
        }

        public Content GetImage(string path, string fileName)
        {
            var type = Path.GetExtension(fileName);
            path = path + "\\images\\" + fileName;
            return new Content() { Bytes = System.IO.File.ReadAllBytes(path), Type = type };
        }

        public string ImageToSave(string path, FileModel file)
        {
            try
            {
                using (Stream stream = new FileStream(path + "\\images\\" + file.FileName, FileMode.Create))
                {
                    file.FormFile.CopyTo(stream);
                }
                return file.FileName;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Post SaveSinglePost(PostSingle post)
        {
            return _postRepository.SaveSinglePost(post);
        }

        public Post SaveAlbumPost(PostAlbum post)
        {
            return _postRepository.SaveAlbumPost(post);
        }

        public bool Like(Guid id, Guid userId)
        {
            return _postRepository.Like(id, userId);
        }

        public bool Dislike(Guid id, Guid userId)
        {
            return _postRepository.Dislike(id, userId);
        }

        public void CommentPost(Guid postId, Comment comment)
        {
            _postRepository.CommentPost(postId, comment);
        }
    }
}