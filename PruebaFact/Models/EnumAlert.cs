using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaFact.Models
{
    public class EnumAlert
    {
        public enum NotificationType
        {
            error,
            success,
            warning,
            info
        }
    }
}