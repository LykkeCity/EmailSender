using System.Collections.Generic;

namespace EmailSender.Common
{
    public static class Utils
    {
        public static IEnumerable<IEnumerable<T>> ToPieces<T>(this IEnumerable<T> src, int countInPicese)
        {
            var result = new List<T>();

            foreach (var itm in src)
            {
                result.Add(itm);
                if (result.Count >= countInPicese)
                {
                    yield return result;
                    result = new List<T>();
                }
            }

            if (result.Count > 0)
                yield return result;
        }
    }
}
