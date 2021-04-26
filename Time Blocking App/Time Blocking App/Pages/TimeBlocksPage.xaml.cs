using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Time_Blocking_App.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Time_Blocking_App.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TimeBlocksPage : Page
    {
        public ObservableCollection<TimeBlock> TimeBlocks { get; set; }

        public TimeBlocksPage()
        {
            this.InitializeComponent();

            // Temporary to give some data to test UI with...
            List<TimeBlock> timeBlocks = new List<TimeBlock>()
            {
                new TimeBlock()
                {
                    ID = Guid.NewGuid(),
                    Name = "Game Development",
                    Start = new TimeSpan(13,0,0),
                    End = new TimeSpan(16,0,0),
                    IsOnSunday = true,
                    IsOnMonday = false,
                    IsOnTuesday = false,
                    IsOnWednesday = false,
                    IsOnThursday = false,
                    IsOnFriday = false,
                    IsOnSaturday = false
                },
                new TimeBlock()
                {
                    ID = Guid.NewGuid(),
                    Name = "Work",
                    Start = new TimeSpan(9,0,0),
                    End = new TimeSpan(17,0,0),
                    IsOnSunday = false,
                    IsOnMonday = true,
                    IsOnTuesday = true,
                    IsOnWednesday = true,
                    IsOnThursday = true,
                    IsOnFriday = true,
                    IsOnSaturday = false
                },
                new TimeBlock()
                {
                    ID = Guid.NewGuid(),
                    Name = "Class",
                    Start = new TimeSpan(19,30,0),
                    End = new TimeSpan(21,0,0),
                    IsOnSunday = false,
                    IsOnMonday = true,
                    IsOnTuesday = false,
                    IsOnWednesday = true,
                    IsOnThursday = false,
                    IsOnFriday = false,
                    IsOnSaturday = false
                },
                new TimeBlock()
                {
                    ID = Guid.NewGuid(),
                    Name = "Dinner",
                    Start = new TimeSpan(18,0,0),
                    End = new TimeSpan(19,0,0),
                    IsOnSunday = true,
                    IsOnMonday = true,
                    IsOnTuesday = true,
                    IsOnWednesday = true,
                    IsOnThursday = true,
                    IsOnFriday = true,
                    IsOnSaturday = true
                }
            };

            // Add the time blocks to the observable collection.
            this.TimeBlocks = new ObservableCollection<TimeBlock>();
            timeBlocks.ForEach(b => this.TimeBlocks.Add(b));
        }
    }
}
