using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorTools
{
    public class ColorModifiers
    {
        public static Color ColorShift(Color color, float hShift, float sShift, float vShift)
        {
            float h;
            float s;
            float v;

            Color.RGBToHSV(color, out h, out s, out v);

            h = ShiftedFloat(h, hShift);
            s = ShiftedFloat(s, sShift);
            v = ShiftedFloat(v, vShift);

            return Color.HSVToRGB(h, s, v);
        }

        private static float ShiftedFloat(float input, float shift)
        {
            float shifted = input;
            shift = Mathf.Clamp(shift, -1, 1);
            shifted += shift;
            if (input < 0f)
            {
                input += 1f;
            }
            if (input > 1f)
            {
                input -= 1f;
            }
            return shifted;
        }
        public static Color ColorAlpha(Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }
    }
}
