using BillPayments.Application.DTOs;
using BillPayments.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPayments.Application.Interfaces
{
    public interface IJwtService
    {
        AuthenticationResponseDTO CreateJwtToken(ApplicationUser user);
    }
}
