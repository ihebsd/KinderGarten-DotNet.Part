﻿using Service.Pattern;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Service
{
    public interface IKidService : IService<Kid>
    {
        IEnumerable<Kid> GetKidByName(string FirstName);
       
    }
}
