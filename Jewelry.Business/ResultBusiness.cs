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
    public interface IResultBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        Task<IBusinessResult> Save(Result result);
        Task<IBusinessResult> Update(Result result);
        Task<IBusinessResult> DeleteById(string code);
    }
    public class ResultBusiness : IResultBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public ResultBusiness() 
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion
                var result = await _unitOfWork.ResultRepository.GetAllAsync();
                if (result == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
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
                var result = await _unitOfWork.ResultRepository.GetByIdAsync(code);
                if (result == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Result result)
        {
            try
            {
                int jewelryResult = await _unitOfWork.ResultRepository.CreateAsync(result);
                if (jewelryResult > 0)
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

        public async Task<IBusinessResult> Update(Result result)
        {
            try
            {
                int jewelryResult = await _unitOfWork.ResultRepository.UpdateAsync(result);

                if (jewelryResult > 0)
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
                var result = await _unitOfWork.ResultRepository.GetByIdAsync(code);
                if (result != null)
                {
                    var jewelryResult = await _unitOfWork.ResultRepository.RemoveAsync(result);
                    if (jewelryResult)
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
