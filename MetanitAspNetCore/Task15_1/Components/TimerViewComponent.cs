using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task15_1.Components
{
    //[ViewComponent]
    public class TimerViewComponent
    {
        public IViewComponentResult Invoke(bool includeSeconds, bool formatAMPM)
        {
            string time = string.Empty;
            DateTime now = DateTime.Now;
            if (formatAMPM)
            {
                time = now.ToString("hh:mm");
            }
            else
            {
                time = now.ToString("HH:mm");
            }

            if (includeSeconds)
            {
                time += now.Second;
            }
            return new ContentViewComponentResult(time);
        }
    }
}
