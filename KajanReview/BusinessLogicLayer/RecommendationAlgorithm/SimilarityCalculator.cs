using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.RecommendationAlgorithm
{
    public class SimilarityCalculator
    {
        public double CalculateCosineSimilarity(FeatureVector vector1, FeatureVector vector2)
        {
            double dotProduct = 0.0;
            double magnitude1 = 0.0;
            double magnitude2 = 0.0;

            foreach (var feature in vector1.Features)
            {
                if (vector2.Features.TryGetValue(feature.Key, out double value))
                {
                    dotProduct += feature.Value * value;
                }
                magnitude1 += Math.Pow(feature.Value, 2);
            }

            foreach (var feature in vector2.Features.Values)
            {
                magnitude2 += Math.Pow(feature, 2);
            }

            magnitude1 = Math.Sqrt(magnitude1);
            magnitude2 = Math.Sqrt(magnitude2);

            if (magnitude1 == 0 || magnitude2 == 0)
            {
                return 0.0;
            }

            return dotProduct / (magnitude1 * magnitude2);
        }
    }
}
