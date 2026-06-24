using Lab8.Models;

namespace Lab8
{
    public static class TestExtensions
    {
        public static int RemoveByKey(this List<Test> src, int key) =>
            src.RemoveAll(x => x.ID == key);

        public static IEnumerable<Test> WithSubject(this IList<Test> src, string subject) =>
            src.Where(x => x.Subject.Equals(subject, StringComparison.OrdinalIgnoreCase));

        public static IEnumerable<Test> WhereTimed(this IList<Test> src) =>
            src.Where(x => x.IsTimed);

        public static double AverageQuestions(this IList<Test> src) =>
            src.Any() ? src.Average(x => x.QuestionCount) : 0d;

        public static int MaxScore(this IList<Test> src) =>
            src.Any() ? src.Max(x => x.MaxScore) : 0;
    }
}
