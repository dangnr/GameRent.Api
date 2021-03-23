using System;
using System.Collections.Generic;

namespace GameRent.Application.ViewModels
{
    public class RentViewModel : BaseViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public ClientViewModel Client { get; set; }
        public List<GameViewModel> Games { get; set; }
    }
}