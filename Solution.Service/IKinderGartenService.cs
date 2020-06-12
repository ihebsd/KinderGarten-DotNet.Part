﻿using Service.Pattern;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Service
{
    public interface IKinderGartenService : IService<KinderGarten>
    {
        IEnumerable<KinderGarten> SearchKindergartenByName(string searchString);
        void IncNbVue(int? id);

    }
}
