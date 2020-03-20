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
    public interface IProductService
    {
        #region CRUD
        ProductViewModel GetById(Guid id);
        Guid Create(CreateProductViewModel model);
        void Update(UpdateProductViewModel model);
        void Delete(Guid id);
        #endregion

        IList<ProductViewModel> GetAll();
        IList<ProductViewModel> GetProductsInStock();
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IManifacturerRepository _manifacturerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IManifacturerRepository manifacturerRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _manifacturerRepository = manifacturerRepository;
            _unitOfWork = unitOfWork;
        }

        public Guid Create(CreateProductViewModel model)
        {
            _unitOfWork.BeginTransaction();
            var category = _categoryRepository.GetById(model.Category.Id);
            if (category == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.CategoryException.NOT_FOUND);
            }

            if (!category.Status)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.CategoryException.DISABLED);
            }
            var manifacturer = _manifacturerRepository.GetById(model.Manifacturer.Id);
            var product = new Product(model.Name, model.Description, model.Quantity, model.Price, category, manifacturer);
            var isUnique = _productRepository.CheckUniqueness(product);
            if (!isUnique) {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.ProductException.ALREADY_EXISTS);
            }
            _productRepository.Add(product);
            _unitOfWork.Commit();
            return product.Id;
        }
        public void Update(UpdateProductViewModel model)
        {
            _unitOfWork.BeginTransaction();
            var product = _productRepository.GetById(model.Id); 

            if (product == null) {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.ProductException.NOT_FOUND);
            }

            var category = _categoryRepository.GetById(model.Category.Id);
            if(category == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.CategoryException.NOT_FOUND);
            }

            if (!category.Status)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.CategoryException.DISABLED);
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.Quantity = model.Quantity;
            product.Price = model.Price;
            product.Category = category;

            var isUnique = _productRepository.CheckUniqueness(product);

            if (!isUnique)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.ProductException.ALREADY_EXISTS);
            }

            _productRepository.Update(product);
            _unitOfWork.Commit();
            
        }
        public void Delete(Guid id)
        {
            _unitOfWork.BeginTransaction();
            var product = _productRepository.GetById(id);
            if(product == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.ProductException.NOT_FOUND);
            }
            _productRepository.Delete(product);
            _unitOfWork.Commit();
        }
        public ProductViewModel GetById(Guid id)
        {
            _unitOfWork.BeginTransaction();
            var product = _productRepository.GetById(id);

            if (product == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.ProductException.NOT_FOUND);
            }

            var productViewModel = product.MapToViewModel();
            _unitOfWork.Commit();

            return productViewModel;

        }

        public IList<ProductViewModel> GetAll()
        {
            _unitOfWork.BeginTransaction();

            var products = _productRepository.GetAll().OrderBy(x => x.Name);
            var productsViewModel = products.Select(x => x.MapToViewModel()).ToList();

            _unitOfWork.Commit();

            return productsViewModel;
        }

        public IList<ProductViewModel> GetProductsInStock()
        {
            _unitOfWork.BeginTransaction();

            var productsInStock = _productRepository.GetProductsInStock();
            var productsInStockViewModel = productsInStock.Select(x => x.MapToViewModel()).ToList();

            _unitOfWork.Commit();

            return productsInStockViewModel;
        }
    }
}
