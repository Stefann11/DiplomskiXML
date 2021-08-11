using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace UserMicroservice.Core.Model
{
    public class DocumentImagePath : ValueObject
    {
        private readonly string path;

        private DocumentImagePath(string path)
        {
            this.path = path;
        }

        public static Result<DocumentImagePath> Create(string path)
        {
            if (String.IsNullOrWhiteSpace(path)) return Result.Failure<DocumentImagePath>("Path cannot be empty");
            //if (!File.Exists(path)) return Result.Failure<DocumentImagePath>("Image does not exist on given path");
            return Result.Success(new DocumentImagePath(path));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return path;
        }

        public override string ToString()
        {
            return this.path.ToString();
        }

        public static implicit operator string(DocumentImagePath DocumentImagePath) => DocumentImagePath.path;
    }
}