using System;

namespace GameRent.Application.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Synopsis { get; set; }
        public string Platform { get; set; }
        public DateTime LaunchDate { get; set; }
        public bool IsAvailable { get; set; }
    }
}