using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Models
{
    public class StudySessionDTO
    {
        public int StackID { get; set; }
        public DateTime Date { get; set; }
        public int Score { get; set; }
    }
}
