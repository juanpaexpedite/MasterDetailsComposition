using MasterDetailsComposition.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetailsComposition.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Game> Games { get; } = new ObservableCollection<Game>()
            {
                new Game() { Code = "doom1", Name="Doom" },
                new Game() { Code = "doom2", Name="Doom 2" },
                new Game() { Code = "quake1", Name="Quake" },
                new Game() { Code = "quake2", Name="Quake II" },
                new Game() { Code = "quake3", Name="Quake III" },
                new Game() { Code = "doom3", Name ="Doom 3" },
                new Game() { Code = "rage",Name = "Rage"  },
                new Game() { Code = "doom4", Name="Doom" },
            };

        public ObservableCollection<String> DetailScreenshots { get; } = new ObservableCollection<string>()
        {
            "details0","details1","details2","details3","details4","details5",
            "details0","details1","details2","details3","details4","details5",
            "details0","details1","details2","details3","details4","details5"
        };
    }
}
