using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translator.exs
{
    public class DisTransException : Exception
    {
        public DisTransException(string msg) :base(msg){ 
        
        }
    }
}
