using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayments.Core.Models
{
    public class ServiceResult
    {
        public bool IsSuccess { get; private set; }
        public string? ErrorMessage { get; private set; }

        protected ServiceResult(bool isSuccess, string? errorMessage = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static ServiceResult Success() => new ServiceResult(true);
        public static ServiceResult Failure(string errorMessage) => new ServiceResult(false, errorMessage);
    }
}
