﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGroup.entities
{
    public class LibrarianStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Librarian> Librarians { get; set; }
    }
}
