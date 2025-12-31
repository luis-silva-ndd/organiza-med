using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organiza_Med.ModuloAtividade
{
    public class TempoDescanso
    {
        public static readonly Dictionary<TipoAtividadeEnum, TimeSpan> Valores = new()
        {
            { TipoAtividadeEnum.Consulta, TimeSpan.FromMinutes(10) },
            { TipoAtividadeEnum.Cirurgia, TimeSpan.FromHours(4) }
        };

    }
}
