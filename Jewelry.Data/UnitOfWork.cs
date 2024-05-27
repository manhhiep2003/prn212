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
    }
}
