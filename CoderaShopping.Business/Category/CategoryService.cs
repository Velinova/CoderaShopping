using CoderaShopping.Business.Mappers;
using CoderaShopping.Business.Models;
using CoderaShopping.DataNHibernate;
using CoderaShopping.DataNHibernate.Repositories;
using CoderaShopping.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderaShopping.Business.Services
{
    public interface ICategoryService
    {
        #region CRUD

        Guid Create(CreateCategoryViewModel model);
        void Update(UpdateCategoryViewModel model);
        CategoryViewModel GetById(Guid id);
        void Delete(Guid id);

        #endregion

        IList<CategoryGridModel> GetAll();

        IList<LookupViewModel> GetActive();
        CategoryViewModel GetDefault();
        IList<ProductViewModel> GetProductsById(Guid id);


    }

    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWorkScope;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public CategoryService(IUnitOfWork unitOfWorkScope, ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _unitOfWorkScope = unitOfWorkScope;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        #region CRUD

        public Guid Create(CreateCategoryViewModel model)
        {
            _unitOfWorkScope.BeginTransaction();

            var category = new Category(model.Name, model.Status, model.IsDefault);
            var isUnique = _categoryRepository.CheckUniqueness(category);
            if (!isUnique)
            {
                _unitOfWorkScope.Commit();
                throw new Exception(ExceptionMessages.CategoryException.ALREADY_EXISTS);
            }
            if (!model.Status && model.IsDefault)
            {
                _unitOfWorkScope.Commit();
                throw new Exception(ExceptionMessages.CategoryException.CHANGE_STATUS_ISDEFAULT);
            }
            // check if there is some other category that is default
            if (model.IsDefault)
            {
                var defaultCategory = _categoryRepository.GetDefaultCategory(null);
                if (defaultCategory != null)
                {
                    defaultCategory.IsDefault = false;
                    _categoryRepository.Update(defaultCategory);
                }
            }

            _categoryRepository.Add(category);

            _unitOfWorkScope.Commit();

            return category.Id;
        }

       
        public void Delete(Guid id)
        {
            _unitOfWorkScope.BeginTransaction();

            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                _unitOfWorkScope.Commit();
                throw new Exception(ExceptionMessages.CategoryException.NOT_FOUND);
            }
            if (category.Products.Any())
            {
                _unitOfWorkScope.Commit();
                throw new Exception(ExceptionMessages.CategoryException.REFERENCED_ITEMS);
            }
            if (category.IsDefault)
            {


                var first = _categoryRepository.GetAll().Where(x => x.Status && !x.IsDefault).FirstOrDefault();
                if (first == null)
                {
                    _unitOfWorkScope.Commit();
                    throw new Exception("Cant delete this category");

                }
                first.IsDefault = true;
                _categoryRepository.Update(first);
                _categoryRepository.Delete(category);
            }
            else
            {
                _categoryRepository.Delete(category);
            }

            _unitOfWorkScope.Commit();
        }

        public CategoryViewModel GetById(Guid id)
        {
            _unitOfWorkScope.BeginTransaction();

            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                _unitOfWorkScope.Commit();
                throw new Exception(ExceptionMessages.CategoryException.NOT_FOUND);
            }
            var categoryViewModel = category.MapToViewModel();

            _unitOfWorkScope.Commit();

            return categoryViewModel;
        }

        public void Update(UpdateCategoryViewModel model)
        {
            _unitOfWorkScope.BeginTransaction();

            var category = _categoryRepository.GetById(model.Id);
            if (category == null)
            {
                _unitOfWorkScope.Commit();
                throw new Exception(ExceptionMessages.CategoryException.NOT_FOUND);
            }
            if (category.IsDefault && model.IsDefault==false) {
                _unitOfWorkScope.Commit();
                throw new Exception(ExceptionMessages.CategoryException.DISELECT_DEFAULT);

            }
            category.Name = model.Name;
            category.Status = model.Status;
            category.IsDefault = model.IsDefault;

            var isUnique = _categoryRepository.CheckUniqueness(category);

            if (!isUnique)
            {
                _unitOfWorkScope.Commit();
                throw new Exception(ExceptionMessages.CategoryException.ALREADY_EXISTS);
            }

            if (!model.Status && model.IsDefault)
            {
                _unitOfWorkScope.Commit();
                throw new Exception(ExceptionMessages.CategoryException.CHANGE_STATUS_ISDEFAULT);
            }

            if (!model.IsDefault)
            {
                var active = _categoryRepository.GetActive();

                if (active == null)
                {
                    _unitOfWorkScope.Commit();
                    throw new Exception("Cant change default value");
                }


            }
            // check if there is some other category that is default
            if (model.IsDefault)
            {
                var defaultCategory = _categoryRepository.GetDefaultCategory(category.Id);
                if (defaultCategory != null)
                {
                    defaultCategory.IsDefault = false;
                    _categoryRepository.Update(defaultCategory);
                }
            }
            
            
            _categoryRepository.Update(category);

            _unitOfWorkScope.Commit();
        }

        #endregion

        public IList<CategoryGridModel> GetAll()
        {
            _unitOfWorkScope.BeginTransaction();

            var categories = _categoryRepository.GetAll();
            var categoryViewModels = categories.Select(x => x.MapToGridModel()).ToList();

            _unitOfWorkScope.Commit();

            return categoryViewModels;
        }

        public IList<LookupViewModel> GetActive()
        {
            _unitOfWorkScope.BeginTransaction();

            var category = _categoryRepository.GetActive();
            var categoryViewModels = category.Select(x => x.MapToLookupViewModel()).ToList();

            _unitOfWorkScope.Commit();

            return categoryViewModels;
        }

        public IList<ProductViewModel> GetProductsById(Guid id)
        {
            _unitOfWorkScope.BeginTransaction();

            var category = _categoryRepository.GetById(id);

            if (category == null)
            {
                _unitOfWorkScope.Commit();
                throw new Exception(ExceptionMessages.CategoryException.NOT_FOUND);
            }

            var products = category.Products;
            var productsViewModel = products.Select(x => x.MapToViewModel()).ToList();

            _unitOfWorkScope.Commit();
            return productsViewModel;
        }

        public CategoryViewModel GetDefault()
        {
            _unitOfWorkScope.BeginTransaction();

            var category = _categoryRepository.GetDefaultCategory(Guid.Empty);
            var categoryViewModel = category.MapToViewModel();

            _unitOfWorkScope.Commit();
            return categoryViewModel;
        }
    }
}
