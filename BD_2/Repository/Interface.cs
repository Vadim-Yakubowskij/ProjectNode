﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_2
{
    interface Interface
    {
        void delete(int id){}
        void create(string name, string more) { }
        void update(int id, string name) { }
        void read() { }
    }
}
