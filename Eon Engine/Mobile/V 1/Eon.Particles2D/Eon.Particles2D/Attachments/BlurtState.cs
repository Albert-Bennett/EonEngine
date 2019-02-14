/* Created 03/10/2013
 * 
 * Author: Albert Bennett.
 * Copyright @ Epsilonic Studios.
 */

namespace Eon.Particles2D.Attachments
{
    /// <summary>
    /// Defines the state of a Blurt effect. 
    /// </summary>
    public struct BlurtState
    {
        public bool DeValue;

        public float MinValue;
        public float MaxValue;
        public float Value;
        public float Decay;

        public void Update()
        {
            if (DeValue)
            {
                Value -= Decay;

                if (Value < MinValue)
                    DeValue = false;
            }
            else
            {
                Value += Decay;

                if (Value > MaxValue)
                    DeValue = true;
            }
        }

        public void ScreeResChanged()
        {
            MinValue = Common.ReCalibrateScale(MinValue);
            MaxValue = Common.ReCalibrateScale(MaxValue);

            Value = Common.ReCalibrateScale(Value);
            Decay = Common.ReCalibrateScale(Decay);
        }
    }
}
