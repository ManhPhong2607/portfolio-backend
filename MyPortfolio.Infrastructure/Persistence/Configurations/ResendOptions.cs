using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Infrastructure.Persistence.Configurations
{
    public class ResendOptions
    {
        public string ApiKey { get; set; } = default!;
        public string FromEmail { get; set; } = "onboarding@resend.dev";
        public string ToEmail { get; set; } = default!;
    }
}
