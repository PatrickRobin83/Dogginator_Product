/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   EventAggregationProvider.cs
 *   Date			:   2019-07-10 08:03
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.rietrob.dogginator_product.DogginatorLibrary
{
    public static class EventAggregationProvider
    {
        public static EventAggregator DogginatorAggregator { get; set; } = new EventAggregator();
    }
}
