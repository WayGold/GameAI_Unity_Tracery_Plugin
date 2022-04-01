using System;
using System.Collections.Generic;
using System.Text;

namespace TraceryPlugin
{
    class GrammarGenerator
    {
        public GrammarGenerator()
        {
            generation_list_ = new Dictionary<int, HashSet<string>>();
            random_ = new Random();
            min_weight_ = 0;
            max_weight_ = 0;
        }

        public void AddItem(int weight, string item)
        {
            if(!generation_list_.ContainsKey(weight))
            {
                generation_list_.Add(weight, new HashSet<string>());
            }
            generation_list_[weight].Add(item);

            if(weight < min_weight_)
            {
                min_weight_ = weight;
            }
            if(weight > max_weight_)
            {
                max_weight_ = weight;
            }
        }

        public void RemoveItem(int weight, string item)
        {
            if(generation_list_.ContainsKey(weight))
            {
                generation_list_[weight].Remove(item);
                if(generation_list_[weight].Count == 0)
                {
                    generation_list_.Remove(weight);
                    if(weight == min_weight_)
                    {
                        min_weight_ = 0;
                        foreach (var keyWeight in generation_list_.Keys)
                        {
                            if(keyWeight < min_weight_)
                            {
                                min_weight_ = keyWeight;
                            }
                        }
                    }
                    if(weight == max_weight_)
                    {
                        max_weight_ = 0;
                        foreach(var keyWeight in generation_list_.Keys)
                        {
                            if(keyWeight > max_weight_)
                            {
                                max_weight_ = keyWeight;
                            }
                        }
                    }
                }
            }
        }

        public List<string> GenerateGrammar(uint numValues, int meanWeight, int maxVariance)
        {
            List<string> values = new List<string>();
            // As long as there are elements to select
            if(generation_list_.Count > 0)
            { 
                for (int i = 0; i < numValues; i++)
                {
                    // This ensures the correct number of indices are selected
                    while(true)
                    {
                        // using a binomial will most likely keep the variance near the mean weight
                        double binomial = RandomBinomial();
                        int randomWeight = (int)Math.Round(binomial * (double)maxVariance);
                        randomWeight += meanWeight;

                        // lock the result to our min and max weight options
                        if (randomWeight > max_weight_)
                            randomWeight = max_weight_;
                        else if (randomWeight < min_weight_)
                            randomWeight = min_weight_;

                        // If the weight exists randomly select an element from that weighting.
                        if(generation_list_.ContainsKey(randomWeight))
                        {
                            int itemIndex = random_.Next(0, generation_list_[randomWeight].Count);
                            int itemIter = 0;
                            foreach (var element in generation_list_[randomWeight])
                            {
                                if(itemIter == itemIndex)
                                {
                                    values.Add(element);
                                    break;
                                }
                                itemIter++;
                            }
                            break;
                        }
                    }
                }
            }
            return values;
        }

        private double RandomBinomial()
        {
            double result = ((double)random_.Next(0, 101))/100;
            result -= ((double)random_.Next(0, 101))/100;
            return result;
        }


        private Dictionary<int, HashSet<string>> generation_list_;
        private Random random_;
        private int min_weight_;
        private int max_weight_;
    }
}
