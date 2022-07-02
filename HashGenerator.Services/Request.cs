﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashGenerator.Services
{

    public class Request<T>
    {
        public T? Data { get; init; }
        public string? Algorithm { get; init; }
    }
}
