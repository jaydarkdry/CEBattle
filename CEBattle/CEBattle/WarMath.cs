using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEBattle
{
    /// <summary>
    /// A static class that handle all the math problem
    /// </summary>
    class WarMath
    {
        /// <summary>
        /// Return the balance of the battle or the odd of success.
        /// </summary>
        /// <param name="side1">The number of strenght of side1</param>
        /// <param name="side2">The number of strenght of side2</param>
        /// <returns>Return a following % of loss.  if value is less than 0, side 1 win, otherwise, side 2 win.</returns>
        public static float ResultBalance(int side1, int side2)
        {
            // Step 1, use
            /*
            Random r = new Random();
            float value = r.NextDouble() ....
            */

            // Step 2, calculate the loss according to the calculation
            // Example: side1: 300   side2: 100
            // Random caught: 350
            // Means than side2 win.
            // 400 (300+100)-350 = 50
            // 50/100 = 0.5
            // Side 2 so 0.5 and not -0.5
            // It means that the first skirmish was not that tight and the loss on side1 is pretty significative, at least 50 units on side one will be lost (Apply bonus later)

            return 0.0f; // Just to compile
        }
    }
}
