using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkoleProjekt_GodtNavn
{
    public class Stat
    {
        /// <summary>
        /// How most we start with
        /// </summary>
        public int baseValue;
        /// <summary>
        /// How most we get per level
        /// </summary>
        public int levelValue;

        /// <summary>
        /// Max value.
        /// </summary>
        public int maxValue;
        /// <summary>
        /// Current value.
        /// </summary>
        public float currentValue;
        /// <summary>
        /// that you are allowed this to go into a minus value.
        /// </summary>
        public bool canMinus;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void LowerValue(float input)
        {
            currentValue -= input;
            if (!canMinus)
            {
                currentValue = 0;
            }
        }

        /// <summary>
        /// This lower current value with input value and return a true if current value is not 0 or lower.
        /// Normal use to Check if player die's or got enough Mana to cast a spell. 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool LowerValueBool(float input)
        {
            currentValue -= input;
            if (currentValue <= 0)
            {
                if (!canMinus)
                {
                    currentValue = 0;
                }
                return false;
            }

            return true;
        }

        public void AddValue(float input)
        {
            currentValue += input;
            if(currentValue > maxValue)
            {
                currentValue = maxValue;
            }
        }

    }
}
