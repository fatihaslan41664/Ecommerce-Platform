using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.DTOs.Configuration
{
    public class MenuDTO
    {
        public string MenuName { get; set; }
        public List<ActionDTO> Actions { get; set; } = new();
    }
}
