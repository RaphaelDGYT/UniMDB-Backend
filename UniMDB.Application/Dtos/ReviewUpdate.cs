using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniMDB.Application.Dtos;

public class ReviewUpdate
{
    public byte Score { get; set; }
    public string Comment { get; set; }
}
