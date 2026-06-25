using Lab8.Models;

namespace Lab8
{
    public class TestStorage : IDisposable
    {
        const string FILE = "database.dat";

        private readonly Stream _stream;
        private readonly List<Test> _tests = [];

        public TestStorage()
        {
            _stream = File.Open(FILE, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Initialize();
        }

        public List<Test> Tests
        {
            get
            {
                return _tests;
            }
        }

        public void Dispose()
        {
            _stream.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            EnsureValidPosition();
            var writer = new BinaryWriter(_stream);
            var count = _tests.Count;

            writer.Write(count);

            for (int i = 0; i < _tests.Count; i++)
            {
                _tests[i].Serialize(writer);
            }
        }

        private void Initialize()
        {
            if (_stream.Length > sizeof(int))
            {
                EnsureValidPosition();
                var reader = new BinaryReader(_stream);
                var count = reader.ReadInt32();

                for (int i = 0; i < count; i++)
                {
                    _tests.Add(Test.Deserialize(reader));
                }
            }
        }

        private void EnsureValidPosition()
        {
            if (_stream.Position != 0)
            {
                _stream.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}
