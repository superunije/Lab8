using Lab8.Models;

namespace Lab8
{
    public class TestStorage : IDisposable
    {
        const string FILE = "database.dat";

        private readonly Stream _stream;
        private readonly List<Test> _data = [];

        public TestStorage()
        {
            _stream = File.Open(FILE, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Initialize();
        }

        public List<Test> Tests => _data;

        public void Dispose()
        {
            _stream.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            EnsureValidPosition();
            var writer = new BinaryWriter(_stream);
            var count = _data.Count;

            writer.Write(count);

            for (int i = 0; i < _data.Count; i++)
            {
                _data[i].Serialize(writer);
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
                    _data.Add(Test.Deserialize(reader));
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
