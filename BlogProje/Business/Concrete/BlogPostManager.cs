using Business.Abstract;
using Data;
using Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BlogPostManager : IBlogPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BlogPostManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public string ErrorMessage { get ; set ; }

        public bool Create(BlogPost entity)
        {
            if (Validation(entity))
            {
                _unitOfWork.BlogPosts.Create(entity);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public void Delete(BlogPost entity)
        {
            _unitOfWork.BlogPosts.Delete(entity);
        }

        public Task<List<BlogPost>> GetAll()
        {
            return _unitOfWork.BlogPosts.GetAll();
        }

        public BlogPost GetById(int id)
        {
            return _unitOfWork.BlogPosts.GetById(id);
        }

    

        public void Update(BlogPost entity)
        {
            _unitOfWork.BlogPosts.Update(entity);
        }

        public bool Validation(BlogPost entity)
        {
            var isValid = true;
            if (string.IsNullOrEmpty(entity.Content))
            {
                ErrorMessage += "Boş post olamaz.\n";
                isValid = false;
            }

      
            return isValid;

        }

    }
}
