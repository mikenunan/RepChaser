using System;

namespace RepChaser.Models
{
    public static class GuidFactory { 

        public static string NewGuidString()
        {
            return Guid.NewGuid().ToString();
        }
    }
}