using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfusionGames.CityScramble.Models;

namespace InfusionGames.CityScramble.Events
{
    public class ClueSubmittedMessage
    {
        public ClueResponse ClueResponse { get; set; }
    }
}
