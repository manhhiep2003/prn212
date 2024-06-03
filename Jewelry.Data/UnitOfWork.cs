using Jewelry.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewelry.Data
{
    public class UnitOfWork
    {
        private RequestRepository _request;
        private RequestDetailRepository _requestDetail;
        private ResultRepository _result;
        private CustomerRepository _customer;

        public UnitOfWork()
        {
        }

        public RequestRepository RequestRepository
        {
            get
            {
                return _request ??= new Repository.RequestRepository();
            }
        }

        public RequestDetailRepository RequestDetailRepository
        {
            get
            {
                return _requestDetail ??= new Repository.RequestDetailRepository();
            }
        }

        public ResultRepository ResultRepository
        {
            get
            {
                return _result ??= new Repository.ResultRepository();
            }
        }

        public CustomerRepository CustomerRepository
        {
            get
            {
                return _customer ??= new Repository.CustomerRepository();
            }
        }
    }
}
