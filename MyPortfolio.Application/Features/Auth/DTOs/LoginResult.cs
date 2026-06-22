using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Features.Auth.DTOs
{
    public record LoginResult
    (
        string AccessToken,
        string RefreshToken,
        DateTime AccessTokenExpiry
      );

}

