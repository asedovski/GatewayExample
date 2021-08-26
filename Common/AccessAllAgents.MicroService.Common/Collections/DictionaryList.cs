using System.Collections.Generic;

namespace AccessAllAgents.MicroService.Common.Collections
{
    public class DictionaryList<TKey, TValue> : Dictionary<TKey, List<TValue>>
    {
        public void AddItem(TKey key, TValue value)
        {
            List<TValue> values;
            if (TryGetValue(key, out values))
            {
                values.Add(value);
                return;
            }

            Add(key, new List<TValue> { value });
        }
    }
}
