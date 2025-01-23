using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs.BaseDtos
{
    public abstract record BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
