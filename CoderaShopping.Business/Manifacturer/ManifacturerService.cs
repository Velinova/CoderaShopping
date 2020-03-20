using CoderaShopping.Business.Mappers;
using CoderaShopping.Business.Models;
using CoderaShopping.Core;
using CoderaShopping.DataNHibernate;
using CoderaShopping.DataNHibernate.Repositories;
using CoderaShopping.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderaShopping.Business.Services
{
    public interface IManifacturerService
    {
        IList<ManifacturerViewModel> GetAll();
        Guid Create(CreateManifacturerViewModel model);
        void Delete(Guid id);
        void Update(ManifacturerViewModel model);
        ManifacturerViewModel GetById(Guid id);
    }

    public class ManifacturerService : IManifacturerService
    { 
        private readonly IManifacturerRepository _manifacturerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ManifacturerService(IManifacturerRepository manifacturerRepository, IUnitOfWork unitOfWork)
        {
            _manifacturerRepository = manifacturerRepository;
            _unitOfWork = unitOfWork;
        }
        public IList<ManifacturerViewModel> GetAll()
        {
            _unitOfWork.BeginTransaction();

            var manifacturers = _manifacturerRepository.GetAll();
            var manifacturersViewModel = manifacturers.Select(x => x.MapToViewModel()).ToList();

            _unitOfWork.Commit();

            return manifacturersViewModel;
        }
        public void Delete(Guid id)
        {
            _unitOfWork.BeginTransaction();

            var manifacturer = _manifacturerRepository.GetById(id);

            if (manifacturer == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.ManifacturerException.NOT_FOUND);
            }

            _manifacturerRepository.Delete(manifacturer);
            _unitOfWork.Commit();
        }
        public ManifacturerViewModel GetById(Guid id)
        {
            _unitOfWork.BeginTransaction();

            var manifacturer = _manifacturerRepository.GetById(id);

            if (manifacturer == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.ManifacturerException.NOT_FOUND);
            }

            var manifacturerViewModel = manifacturer.MapToViewModel();
            _unitOfWork.Commit();

            return manifacturerViewModel;
        }
        public void Update(ManifacturerViewModel model)
        {
            _unitOfWork.BeginTransaction();

            var manifacturer = _manifacturerRepository.GetById(model.Id);

            if (manifacturer == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.ManifacturerException.NOT_FOUND);
            }

            manifacturer.Address = model.Address;
            manifacturer.Country = model.Country;
            manifacturer.City = model.City;
            manifacturer.Name = model.Name;
            

            
            _manifacturerRepository.Update(manifacturer);
            _unitOfWork.Commit();
        }
        public Guid Create(CreateManifacturerViewModel model)
        {
            _unitOfWork.BeginTransaction();
            
            var manifacturer = new Manifacturer(model.Name, model.City, model.Country, model.Address);
            var isUnique = _manifacturerRepository.CheckUniqueness(manifacturer);
            if (!isUnique)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.ManifacturerException.ALREADY_EXISTS);
            }
            _manifacturerRepository.Add(manifacturer);
            _unitOfWork.Commit();
            return manifacturer.Id;
        }

    }
}
