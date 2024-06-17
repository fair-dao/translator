using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translator.entities
{
    public class LangItem
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string EnglishName { get; set; }

        public string SrcName { get; set; }
        public override string ToString()
        {
            return $"{SrcName} - {DisplayName} ( {Name} )";
        }
    }
}
