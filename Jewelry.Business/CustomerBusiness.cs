using Jewelry.Business.Base;
using Jewelry.Common;
using Jewelry.Data;
using Jewelry.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewelry.Business
{
    public interface ICustomerBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        Task<IBusinessResult> Save(Customer customer);
        Task<IBusinessResult> Update(Customer request);
        Task<IBusinessResult> DeleteById(string code);
    }
    public class CustomerBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public CustomerBusiness() 
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion
                var customer = await _unitOfWork.CustomerRepository.GetAllAsync();
                if (customer == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, customer);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<IBusinessResult> GetById(string code)
        {
            try
            {
                #region Business rule
                #endregion
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(code);
                if (customer == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, customer);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<IBusinessResult> Save(Customer customer)
        {
            try
            {
                //int result = await _currencyRepository.CreateAsync(currency);
                int result = await _unitOfWork.CustomerRepository.CreateAsync(customer);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Update(Customer customer)
        {
            try
            {
                int result = await _unitOfWork.CustomerRepository.UpdateAsync(customer);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);

                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        public async Task<IBusinessResult> DeleteById(string code)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(code);
                if (customer != null)
                {
                    var result = await _unitOfWork.CustomerRepository.RemoveAsync(customer);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }
    }
}
