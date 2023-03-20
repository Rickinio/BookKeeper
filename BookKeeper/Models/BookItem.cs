﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookKeeper.Models
{
    public class BookItem
    {
        public int ID { get; set; }

        public string ISBN { get; set; }

        public int Box { get; set; }

        public BookItem() { }

        public BookItem(string isbn, int box) 
        { 
            ISBN = isbn; 
            Box = box; 
        }
    }
}
