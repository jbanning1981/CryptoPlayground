using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPlayground.Domain.Models
{
    public class KeyRequest
    {
        Guid ClientId { get; set; }
        Guid? KeyId { get; set; }

    }
}
