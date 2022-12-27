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
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        //private ICategoryRepository _categoryRepository;
        //public CategoryManager(ICategoryRepository categoryRepository)
        //{
        //    _categoryRepository = categoryRepository;
        //}

        public string ErrorMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Create(Category entity)
        {
            _unitOfWork.Categories.Create(entity);
            _unitOfWork.Save();

        }
        public void Delete(Category entity)
        {
                _unitOfWork.Categories.Delete(entity);
                _unitOfWork.Save();
            }

        public void DeleteFromCategory(int productId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Category>> GetAll()
        {
            return await _unitOfWork.Categories.GetAll();
        }

        public Category GetById(int id)
        {
            throw new NotImplementedException();
        }



        public void Update(Category entity)
        {
             _unitOfWork.Categories.Update(entity);
            _unitOfWork.Save();
        }

        public bool Validation(Category entity)
        {
           throw new System.NotImplementedException();
        }

        List<Category> ICategoryService.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
