using System;
using System.Collections.Generic;
using System.Text;

namespace InfluMe.Helpers {
    public static class ColorHelper {
        public static string RandomColor() {
            Random random = new Random();

            // to create lighter colors:
            // take a random integer between 0 & 128 (rather than between 0 and 255)
            // and then add 127 to make the color lighter

            int red = random.Next(128) + 127;
            int green = random.Next(128) + 127;
            int blue = random.Next(128) + 127;
            var hex = $"#{red:X2}{green:X2}{blue:X2}";
            // make color opaque
            return hex;
        }
    }
    
}
