namespace Lab8.Models
{
    public class Test
    {
        private int _id;
        private string _title;
        private string _subject;
        private int _questionCount;
        private int _maxScore;
        private bool _isTimed;

        public int ID
        {
            get 
            { 
                return _id; 
            }
            set 
            { 
                _id = value; 
            }
        }

        public string Title
        {
            get 
            { 
                return _title; 
            }
            set 
            { 
                _title = value; 
            }
        }

        public string Subject
        {
            get 
            { 
                return _subject; 
            }
            set 
            { 
                _subject = value; 
            }
        }

        public int QuestionCount
        {
            get 
            { 
                return _questionCount; 
            }
            set 
            { 
                _questionCount = value; 
            }
        }

        public int MaxScore
        {
            get 
            { 
                return _maxScore; 
            }
            set 
            { 
                _maxScore = value; 
            }
        }

        public bool IsTimed
        {
            get 
            { 
                return _isTimed; 
            }
            set 
            { 
                _isTimed = value; 
            }
        }

        public Test()
        {
        }

        public Test(
            int id,
            string title,
            string subject,
            int questionCount,
            int maxScore,
            bool isTimed)
        {
            ID = id;
            Title = title;
            Subject = subject;
            QuestionCount = questionCount;
            MaxScore = maxScore;
            IsTimed = isTimed;
        }

        public static Test Deserialize(BinaryReader reader)
        {
            return new(
                reader.ReadInt32(),
                reader.ReadString(),
                reader.ReadString(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadBoolean());
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(ID);
            writer.Write(Title);
            writer.Write(Subject);
            writer.Write(QuestionCount);
            writer.Write(MaxScore);
            writer.Write(IsTimed);
        }

        public override string ToString()
        {
            return $"[{ID}] {Title} | {Subject} | Вопросов: {QuestionCount}, Балл: {MaxScore}, Таймер: {IsTimed}";
        }
    }
}
