using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time_Blocking_App.Models
{
    /// <summary>
    /// A model to represent a block of time from the time blocking productivity method.
    /// </summary>
    public class TimeBlock
    {
        /// <summary>
        /// The unique ID for the time block.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// The name of the time block.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The start time of the time block.
        /// </summary>
        public TimeSpan Start { get; set; }

        /// <summary>
        /// The end time of the time block.
        /// </summary>
        public TimeSpan End { get; set; }

        /// <summary>
        /// Whether the time block is scheduled on Sunday.
        /// </summary>
        public bool IsOnSunday { get; set; }

        /// <summary>
        /// Whether the time block is scheduled on Monday.
        /// </summary>
        public bool IsOnMonday { get; set; }

        /// <summary>
        /// Whether the time block is scheduled on Tuesday.
        /// </summary>
        public bool IsOnTuesday { get; set; }

        /// <summary>
        /// Whether the time block is scheduled on Wednesday.
        /// </summary>
        public bool IsOnWednesday { get; set; }

        /// <summary>
        /// Whether the time block is scheduled on Thursday.
        /// </summary>
        public bool IsOnThursday { get; set; }

        /// <summary>
        /// Whether the time block is scheduled on Friday.
        /// </summary>
        public bool IsOnFriday { get; set; }

        /// <summary>
        /// Whether the time block is scheduled on Saturday.
        /// </summary>
        public bool IsOnSaturday { get; set; }
    }
}
