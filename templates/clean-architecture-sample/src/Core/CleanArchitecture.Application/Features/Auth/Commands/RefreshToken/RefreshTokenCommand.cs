using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Models.Identity;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<AuthResponse>
    {
        public string RefreshToken { get; set; }
    }
}