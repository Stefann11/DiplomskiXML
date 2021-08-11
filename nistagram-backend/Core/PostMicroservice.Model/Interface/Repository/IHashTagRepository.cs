using PostMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostMicroservice.Core.Interface.Repository
{
    public interface IHashTagRepository
    {
        public IEnumerable<HashTag> GetByText(string text);
    }
}