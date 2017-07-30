using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ld39.Entities.Behaviour
{
    public interface IElectrified
    {
        bool IsElectrified { get; set; }
        int RangeElectrified { get; }
    }
}
