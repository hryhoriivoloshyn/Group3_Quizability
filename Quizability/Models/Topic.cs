using System;
using System.Collections.Generic;

#nullable disable

namespace Quizability.Models
{
    public partial class Topic
    {
        public Topic()
        {
            Quizes = new HashSet<Quize>();
        }

        public int TopicId { get; set; }
        public string TopicName { get; set; }

        public virtual ICollection<Quize> Quizes { get; set; }
    }
}
