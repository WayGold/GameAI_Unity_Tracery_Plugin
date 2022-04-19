using System;
using System.Collections.Generic;
using System.Text;

namespace TraceryPlugin
{
    public class GrammarManager
    {

        private Dictionary<string, GrammarRuleGenerator> grammarRules;

        public GrammarManager()
        {
            grammarRules = new Dictionary<string, GrammarRuleGenerator>();
        }

        void AddRule(string ruleName)
        {
            if(!grammarRules.ContainsKey(ruleName))
            { 
                grammarRules.Add(ruleName, new GrammarRuleGenerator());
            }
        }

        void RemoveRule(string ruleName)
        {
            grammarRules.Remove(ruleName);
        }

        bool AddRuleItem(string ruleName, string item, int weight)
        {
            if(grammarRules.ContainsKey(ruleName))
            {
                grammarRules[ruleName].AddItem(weight, item);
                return true;
            }
            return false;
        }

        void RemoveRuleItem(string ruleName, string item, int weight)
        {
            if(grammarRules.ContainsKey(ruleName))
            {
                grammarRules[ruleName].RemoveItem(weight, item);
            }
        }

        /// <summary>
        /// Generate a grammar that falls within a specified range
        /// </summary>
        /// <param name="numValues">The number of items to pick per category</param>
        /// <param name="relationshipAvg">The mean weight of language to generate</param>
        /// <param name="maxVariance">The maximum weight difference possible</param>
        /// <returns>A JSON string representing the grammar generated</returns>
        public string GetRelationshipGrammar(uint numValues, int relationshipAvg, int maxVariance)
        {
            string grammar = "{";
            int numRules = grammarRules.Count;
            int rulesProcessed = 0;
            foreach (var generator in grammarRules)
            {
                rulesProcessed++;
                grammar += "\"" + generator.Key + "\":[";
                var items = generator.Value.GenerateGrammar(numValues, relationshipAvg, maxVariance);
                int itemsProcessed = 0;
                foreach(var item in items)
                {
                    itemsProcessed++;
                    grammar += "\"" + item + "\"";
                    if(itemsProcessed < items.Count)
                    {
                        grammar += ",";
                    }
                }
                grammar += "]";
                if(rulesProcessed < numRules)
                {
                    grammar += ",";
                }
            }

            grammar += "}";
            return grammar;
        }
        
    }
}
