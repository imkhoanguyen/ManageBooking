using ManageBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageBooking.Application.Common.Interfaces
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
    }
}
