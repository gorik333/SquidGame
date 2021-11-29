#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("p9zj29LnjvVYtsqgi6uLNs+fNbq2voKg4CyT4oLsVCN9ksJlBug/Dwhac25HI3LK2G8k8fJ1JMjIjJt0zX/8383w+/TXe7V7CvD8/Pz4/f6cquBzDwO949JAI3Rw40ebMUm5yxuMvZP27U+s5DSpYfuZhAUGpN4jtyuQyFYFAn7Fgir7SbsuhQCiCReuoPTKiLNLz2dwn/dKCaFhY1+w/MgI03w+1E5l74tOuUk5Bl59mOhCdFh3enpzfO2gaZw0Ns0f+1F7yVZ//PL9zX/89/9//Pz9Q7hPsBOTnefQsx6AXXdg/zQHXA+GBkl2k1QVVW2bSYakWjM5GJrbaIMXe15YgvM1k15fNXqnD3084VO+hyHgyVgPAQ0PbA06z3QaJv/+/P38");
        private static int[] order = new int[] { 2,6,5,5,4,11,10,13,8,13,10,11,12,13,14 };
        private static int key = 253;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
