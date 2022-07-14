using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPlayground.Domain.Models
{

    public class HashRequest<T>
    {
        public T? Data { get; init; }
        public string? Algorithm { get; init; }
    }
}
