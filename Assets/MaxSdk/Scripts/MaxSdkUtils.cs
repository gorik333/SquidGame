using System;
using System.Collections.Generic;
using System.Text;

class MaxSdkUtils
{
    private static readonly char _DictKeyValueSeparator = (char)28;
    private static readonly char _DictKeyValuePairSeparator = (char)29;
    
    /// <summary>
    /// The native iOS and Android plugins forward dictionaries as a string such as:
    ///
    /// "key_1=value1
    ///  key_2=value2,
    ///  key=3-value3"
    ///  
    /// </summary>
    public static IDictionary<string, string> PropsStringToDict(string str)
    {
        var result = new Dictionary<string, string>();

        if (string.IsNullOrEmpty(str)) return result;

        var components = str.Split('\n');
        foreach (var component in components)
        {
            var ix = component.IndexOf('=');
            if (ix > 0 && ix < component.Length)
            {
                var key = component.Substring(0, ix);
                var value = component.Substring(ix + 1, component.Length - ix - 1);
                if (!result.ContainsKey(key))
                {
                    result[key] = value;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// The native iOS and Android plugins forward dictionaries as a string such as:
    ///
    /// "key_1=value1,key_2=value2,key=3-value3"
    ///  
    /// </summary>
    public static String DictToPropsString(IDictionary<string, string> dict)
    {
        StringBuilder serialized = new StringBuilder();
        
        if (dict != null)
        {
            foreach (KeyValuePair<string, string> entry in dict)
            {
                if (entry.Key != null && entry.Value != null)
                {
                    serialized.Append(entry.Key);
                    serialized.Append(_DictKeyValueSeparator);
                    serialized.Append(entry.Value);
                    serialized.Append(_DictKeyValuePairSeparator);
                }
            }
        }

        return serialized.ToString();
    }
}
