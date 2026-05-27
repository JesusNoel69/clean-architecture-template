using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Interfaces.Identity
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
    }
}