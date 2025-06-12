// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("Cbs4Gwk0PzATv3G/zjQ4ODg8OTrMkidTmSDGI+h2KI2ALximXJjkwBTn3t3ZZPLXq5vjmsSiFAR1KUoB8QFj7zzDSfwaGX0G3kRmsH571quGwmmVEt2kLWRrjOT74MTRkSawdMnvoFR4bYUfD5xaK1I4k/BZafDtzBIg+WBmfODSVO+eePdIGF3zVWjxOTS//mQaeo1bAj7sHk4PrRXV53cB1+bBq+KdNKsVV2OD9RoxTSn1qC115e7Uv8cp9zyH+JdytQ/0LiwgSoELGW3Vo3SJEkJ+r9nAftzQz8WLAkDnmenYWk/WsNEquZcZai9Euzg2OQm7ODM7uzg4OevDaWqBPMjLYRGOI9jBqBI3fJs8efwWRuHf/CZZXbhInRao+Ds6ODk4");
        private static int[] order = new int[] { 0,4,5,12,8,5,12,10,11,9,12,12,12,13,14 };
        private static int key = 57;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
