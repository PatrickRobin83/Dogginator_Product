using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogginatorLibrary
{
    public static class EventAggregationProvider
    {
        public static EventAggregator DogginatorAggregator { get; set; } = new EventAggregator();
    }
}
