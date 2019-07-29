using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ProceduralGeneration.Scripts
{
    class RoadDirectionProbability
    {
        private static Random rand = new Random();
        readonly Dictionary<RoadDirection, float> probabilityForDirections;

        public RoadDirectionProbability(float up, float left, float right, float down)
        {
            probabilityForDirections.Add(RoadDirection.Up, up);
            probabilityForDirections.Add(RoadDirection.Left, left);
            probabilityForDirections.Add(RoadDirection.Right,right);
            probabilityForDirections.Add(RoadDirection.Down, down);
        }

        public float GetProbability(RoadDirection roadDirection)
        {
            return probabilityForDirections[roadDirection];
        }

        public void SetProbability(RoadDirection roadDirection, float prob)
        {
            if (prob >= 0.0f && prob <= 1.0f) probabilityForDirections[roadDirection] = prob;
            else throw new Exception("Invalid probability: " + prob);
        }

        public void DeltaProbability(RoadDirection roadDirection, float delta)
        {
            float prob = Math.Max(0.0f,Math.Min(1.0f, GetProbability(roadDirection) + delta));
            SetProbability(roadDirection, prob);
        }

        public RoadDirection GetNext()
        {
            float total = SumWeights();
            List<KeyValuePair<RoadDirection, float>> elements = probabilityForDirections.ToList();
            float diceRoll = (float)rand.NextDouble();
            float cumulative = 0.0f;
            for (int i = 0; i < elements.Count; i++)
            {
                cumulative += elements[i].Value;
                if (diceRoll < cumulative)
                {
                    return elements[i].Key;
                }
            }

            throw new Exception("no item selected, this should not happen");
        }

        public float SumWeights()
        {
            return probabilityForDirections.Sum(kv => kv.Value);
        }
    }
}
