using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.Entity.Common
{
    public class Menu
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public bool Showcheck { get; set; }
        public bool Complete { get; set; }
        public bool Isexpand { get; set; }
        public int Checkstate { get; set; }
        public bool HasChildren { get; set; }
        public string Icon { get; set; }
    }
}
